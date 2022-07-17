using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using RaGae.ArgumentLib;
using RaGae.ArgumentLib.MarshalerLib;
using RaGae.ExceptionLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Sdk;
using ArgumentException = RaGae.ArgumentLib.ArgumentException;

namespace ArgumentLibTest
{
    public enum ArgumentConstructor
    {
        WithSchema,
        WithoutSchema
    }

    public class ArgumentTest
    {
        private static string configFile = "ArgumentLib.json";
        private static string configFiles = "ArgumentLib.Files.json";
        private static string configEmptyDelimiter = "ArgumentLib.EmptyDelimiter.json";
        private static string[] otherDelimiters = { ":", "/" };
        private static string wrongDelimiter = "!";
        private static string[][] args = new string[][]
        {
            new string[]
            {
                "-StRiNg",
                "Test string",
                "-string2",
                "Test string2",
                "-InT",
                "1234",
                "-int2",
                "5678",
                "-DoUbLe",
                Convert.ToDouble("1234,4321", CultureInfo.CreateSpecificCulture("de-AT")).ToString(),
                "-double2",
                Convert.ToDouble("4321,1234", CultureInfo.CreateSpecificCulture("de-AT")).ToString()
            },
            new string[]
            {
                "-TeXt",
                "Test string",
                "-text2",
                "Test string2",
                "-NuMbEr",
                "1234",
                "-number2",
                "5678",
                "-DeCiMaL",
                Convert.ToDouble("1234,4321", CultureInfo.CreateSpecificCulture("de-AT")).ToString(),
                "-decimal2",
                Convert.ToDouble("4321,1234", CultureInfo.CreateSpecificCulture("de-AT")).ToString()
            },
            new string[]
            {
                "-DaTa",
                "Test string",
                "-data2",
                "Test string2",
                "-int",
                "1234",
                "-InT2",
                "5678",
                "-double",
                Convert.ToDouble("1234,4321", CultureInfo.CreateSpecificCulture("de-AT")).ToString(),
                "-DoUblE2",
                Convert.ToDouble("4321,1234", CultureInfo.CreateSpecificCulture("de-AT")).ToString()
            },
            new string[]
            {
                "-data",
                "Test string",
                "-DaTa2",
                "Test string2",
                "-number",
                "1234",
                "-NuMbEr2",
                "5678",
                "-decimal",
                Convert.ToDouble("1234,4321", CultureInfo.CreateSpecificCulture("de-AT")).ToString(),
                "-DeCiMaL2",
                Convert.ToDouble("4321,1234", CultureInfo.CreateSpecificCulture("de-AT")).ToString()
            },
        };

        private static string[] booleanArgs =
        {
                "-BoOl,-BoOlEaN",
                "-bool2,-boolean2"
        };

        private static IEnumerable<ArgumentSchema> schema = new List<ArgumentSchema>()
            {
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "string",
                        "text",
                        "data"
                    },
                    Marshaler = "*",
                    Required = true
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "StRiNg2",
                        "TeXt2",
                        "DaTa2"
                    },
                    Marshaler = "*",
                    Required = false
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "int",
                        "number"
                    },
                    Marshaler = "#",
                    Required = true
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "InT2",
                        "number2"
                    },
                    Marshaler = "#",
                    Required = false
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "double",
                        "decimal"
                    },
                    Marshaler = "##",
                    Required = true
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "DoUbLe2",
                        "DeCiMaL2"
                    },
                    Marshaler = "##",
                    Required = false
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "bool",
                        "boolean"
                    },
                    Marshaler = "",
                    Required = true
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "BoOl2",
                        "BoOlEaN2"
                    },
                    Marshaler = "",
                    Required = false
                },
            };

        public static IEnumerable<ArgumentConstructor> GetConstructorTypes()
        {
            yield return ArgumentConstructor.WithSchema;
            yield return ArgumentConstructor.WithoutSchema;
        }

        public static IEnumerable<object[]> GetData_Passing(bool length)
        {
            foreach (string[] item in args)
            {
                List<string> itemOnlyRequired = new List<string>();
                List<string> itemWithOptional = item.ToList();

                for (int i = 0; i < item.Length; i += 4)
                {
                    itemOnlyRequired.Add(item[i]);
                    itemOnlyRequired.Add(item[(i + 1)]);
                }

                int itemOnlyRequiredLength = itemOnlyRequired.Count;
                int itemWithOptionalLength = itemWithOptional.Count;

                Random r = new Random();

                itemOnlyRequired.Add(booleanArgs[0].Split(",")[r.Next(0, 1)]);

                itemWithOptional.Add(booleanArgs[0].Split(",")[r.Next(0, 1)]);
                itemWithOptional.Add(booleanArgs[1].Split(",")[r.Next(0, 1)]);

                foreach (ArgumentConstructor constructor in GetConstructorTypes())
                {
                    if (length)
                    {
                        yield return new object[] { constructor, configFile, itemWithOptional.ToArray(), itemWithOptionalLength };
                        yield return new object[] { constructor, configFiles, itemWithOptional.ToArray(), itemWithOptionalLength };

                        foreach (string delimiter in otherDelimiters)
                        {
                            yield return new object[] { constructor, configFile, itemWithOptional.Select(x => x.Replace("-", delimiter)).ToArray(), itemWithOptionalLength };
                            yield return new object[] { constructor, configFiles, itemWithOptional.Select(x => x.Replace("-", delimiter)).ToArray(), itemWithOptionalLength };
                        }

                        yield return new object[] { constructor, configFile, itemOnlyRequired.ToArray(), itemOnlyRequiredLength };
                        yield return new object[] { constructor, configFiles, itemOnlyRequired.ToArray(), itemOnlyRequiredLength };

                        foreach (string delimiter in otherDelimiters)
                        {
                            yield return new object[] { constructor, configFile, itemOnlyRequired.Select(x => x.Replace("-", delimiter)).ToArray(), itemOnlyRequiredLength };
                            yield return new object[] { constructor, configFiles, itemOnlyRequired.Select(x => x.Replace("-", delimiter)).ToArray(), itemOnlyRequiredLength };
                        }
                    }
                    else
                    {
                        yield return new object[] { constructor, configFile, itemWithOptional.ToArray() };
                        yield return new object[] { constructor, configFiles, itemWithOptional.ToArray() };

                        foreach (string delimiter in otherDelimiters)
                        {
                            yield return new object[] { constructor, configFile, itemWithOptional.Select(x => x.Replace("-", delimiter)).ToArray() };
                            yield return new object[] { constructor, configFiles, itemWithOptional.Select(x => x.Replace("-", delimiter)).ToArray() };
                        }

                        yield return new object[] { constructor, configFile, itemOnlyRequired.ToArray() };
                        yield return new object[] { constructor, configFiles, itemOnlyRequired.ToArray() };

                        foreach (string delimiter in otherDelimiters)
                        {
                            yield return new object[] { constructor, configFile, itemOnlyRequired.Select(x => x.Replace("-", delimiter)).ToArray() };
                            yield return new object[] { constructor, configFiles, itemOnlyRequired.Select(x => x.Replace("-", delimiter)).ToArray() };
                        }
                    }
                }
            }
        }

        private Argument CreateConstructor_Passing(ArgumentConstructor type, string path, IEnumerable<string> argument, IEnumerable<ArgumentSchema> schema)
        {
            Argument a;

            switch (type)
            {
                case ArgumentConstructor.WithSchema:
                    a = new Argument(path, argument, schema);
                    break;
                case ArgumentConstructor.WithoutSchema:
                    a = new Argument(path, argument);
                    break;
                default:
                    throw new XunitException("TILT: should not be reached!");
            }
            return a;
        }

        public static IEnumerable<object[]> GetWrongConfig_Failing()
        {
            foreach (ArgumentConstructor constructor in GetConstructorTypes())
            {
                yield return new object[] { constructor, null, null, null, ErrorCode.GLOBAL, null, $"Config <> not found!", "There was an ERROR with 'Config <> not found!'" };
                yield return new object[] { constructor, string.Empty, null, null, ErrorCode.GLOBAL, null, $"Config <> not found!", "There was an ERROR with 'Config <> not found!'" };
                yield return new object[] { constructor, "   ", null, null, ErrorCode.GLOBAL, null, $"Config <   > not found!", "There was an ERROR with 'Config <   > not found!'" };
                yield return new object[] { constructor, "wrong", null, null, ErrorCode.GLOBAL, null, $"Config <wrong> not found!", "There was an ERROR with 'Config <wrong> not found!'" };
                yield return new object[] { constructor, "ArgumentLib.WrongFiles.json", null, null, ErrorCode.REFLECTION, null, "Assemblyfile <Marshaler/Wrong.dll> not found!", null };
                yield return new object[] { constructor, "ArgumentLib.WrongPath.json", null, null, ErrorCode.REFLECTION, null, "Directory <WrongPath> not found!", null };
                yield return new object[] { constructor, "ArgumentLib.WrongSpecifier.json", null, null, ErrorCode.REFLECTION, null, "Directory <Marshaler/*Wrong.dll> contains no assemblies!", null };
                yield return new object[] { constructor, "ArgumentLib.EmptySchema.json", null, null, ErrorCode.EMPTY, null, "Schema", "Config File: Schema contains no data!" };
                yield return new object[] { constructor, "ArgumentLib.InvalidSchema.json", null, new List<ArgumentSchema>()
                {
                    new ArgumentSchema()
                    {
                        Argument = new List<string>()
                        {
                            "str!ng",
                            "t??t"
                        },
                        Marshaler = "*",
                        Required = true
                    }
                }, ErrorCode.INVALID, null, "Argument", "Config File: Argument contains invalid data!" };
                yield return new object[] { constructor, "ArgumentLib.WrongMarshaler.json", null, null, ErrorCode.REFLECTION, null, "PropertyName <Schema:+> not found!", null };
                yield return new object[] { constructor, "ArgumentLib.json", args[0].Skip(1), null, ErrorCode.INVALID_PARAMETER, null, "Test string", "'Test string' is not a valid parameter" };
                yield return new object[] { constructor, "ArgumentLib.json", args[0].Select(x => x.Replace("-", wrongDelimiter)), schema, ErrorCode.INVALID_PARAMETER, null, "!StRiNg", "'!StRiNg' is not a valid parameter" };
                yield return new object[] { constructor, "ArgumentLib.json", new string[] { args[0][0].Remove(args[0][0].Length - 1) }, schema.Take(1), ErrorCode.UNEXPECTED_ARGUMENT, "strin", null, "Argument -strin unexpected" };
                yield return new object[] { constructor, "ArgumentLib.WrongAttachment.json", new string[] { args[0][4], "abcd" }, schema.Skip(2).Take(1), ErrorCode.INVALID, "int", "abcd", "Argument -int expects an integer but was 'abcd'" };
            }
        }

        private Exception CreateConstructor_Failing(ArgumentConstructor type, string path, IEnumerable<string> argument, IEnumerable<ArgumentSchema> schema)
        {
            Argument a;
            Exception ex;

            switch (type)
            {
                case ArgumentConstructor.WithSchema:
                    ex = Record.Exception(() => a = new Argument(path, argument, schema));
                    break;
                case ArgumentConstructor.WithoutSchema:
                    ex = Record.Exception(() => a = new Argument(path, argument));
                    break;
                default:
                    throw new XunitException("TILT: should not be reached!");
            }
            return ex;
        }

        [Theory]
        [MemberData(nameof(GetData_Passing), false)]
        public void CreateReference_Passing(ArgumentConstructor type, string config, string[] arguments)
        {
            Argument a = CreateConstructor_Passing(type, config, arguments, schema);
        }

        [Theory]
        [MemberData(nameof(GetData_Passing), true)]
        public void CreateReferenceAndGetValue_Passing(ArgumentConstructor type, string config, string[] arguments, int length)
        {
            Argument a = CreateConstructor_Passing(type, config, arguments, schema);

            string text = arguments[0].Substring(1).ToString();

            for (int i = 0; i < (length - 1); i += 2)
            {
                Assert.Equal(arguments[(i + 1)], a.GetValue<object>(arguments[i].Substring(1).ToString()).ToString());
            }

            Random r = new Random();

            if ((arguments.Length - length) % 2 == 0)
            {
                Assert.True(a.GetValue<bool>(booleanArgs[0].Split(",")[r.Next(0, 1)].Substring(1).ToString()));
                Assert.True(a.GetValue<bool>(booleanArgs[1].Split(",")[r.Next(0, 1)].Substring(1).ToString()));
            }
            else
            {
                Assert.True(a.GetValue<bool>(booleanArgs[0].Split(",")[r.Next(0, 1)].Substring(1).ToString()));
                Assert.False(a.GetValue<bool>(booleanArgs[1].Split(",")[r.Next(0, 1)].Substring(1).ToString()));
            }
        }

        [Theory]
        [MemberData(nameof(GetWrongConfig_Failing))]
        public void CreateReference_Failing(ArgumentConstructor type, string config, IEnumerable<string> argument, IEnumerable<ArgumentSchema> schema, ErrorCode error, string argumentId, string parameter, string message)
        {
            Exception a = CreateConstructor_Failing(type, config, argument, schema);

            if (a is BaseArgumentException)
            {
                BaseArgumentException ex = a as BaseArgumentException;

                Assert.Equal(error, ex.ErrorCode);

                if (!string.IsNullOrEmpty(argumentId))
                    Assert.Equal(argumentId, ex.ErrorArgumentId);
                else
                    Assert.Null(ex.ErrorArgumentId);

                Assert.Equal(parameter, ex.ErrorParameter);

                if (!string.IsNullOrEmpty(message))
                    Assert.Equal(message, ex.ErrorMessage());
                else
                    Assert.Equal(parameter, ex.ErrorMessage());
            }
            else
            {
                throw new XunitException();
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Passing), false)]
        public void CreateReferenceAndGetRequiredValue_Failing(ArgumentConstructor type, string config, string[] arguments)
        {
            Argument arg = CreateConstructor_Passing(type, config, arguments.Skip(2).Take(2), schema.Take(4));

            Exception a = Assert.Throws<ArgumentException>(() => arg.GetValue<string>(arguments[0].Substring(1).ToString()));

            if (a is BaseArgumentException)
            {
                BaseArgumentException ex = a as BaseArgumentException;

                Assert.Equal(ErrorCode.MISSING, ex.ErrorCode);
                Assert.Equal("string, text, data", ex.ErrorArgumentId);
                Assert.Null(ex.ErrorParameter);
                Assert.Equal("Missing parameter: ('string, text, data')", ex.ErrorMessage());
            }
            else
            {
                throw new XunitException();
            }
        }

        [Fact]
        public void CreateReferenceWithEmptyDelimiterAndGetValues_Passing()
        {
            foreach (ArgumentConstructor constructor in GetConstructorTypes())
            {
                Argument a = CreateConstructor_Passing(constructor, configEmptyDelimiter, args[0].Take(4), schema.Take(2));

                Assert.Equal(args[0][1], a.GetValue<object>(args[0][0].Substring(1).ToString()));
                Assert.Equal(args[0][3], a.GetValue<object>(args[0][2].Substring(1).ToString()));
            }
        }

        [Fact]
        public void CreateReferenceWithEmptyDelimiterAndGetValues_Failing()
        {
            foreach (ArgumentConstructor constructor in GetConstructorTypes())
            {
                Argument a;

                ArgumentException ex = Assert.Throws<ArgumentException>(() => a = CreateConstructor_Passing(constructor, configEmptyDelimiter, args[0].Take(4).Select(x => x.Replace("-", "/")).ToArray(), schema.Take(2)));

                Assert.Equal(ErrorCode.INVALID_PARAMETER, ex.ErrorCode);
                Assert.Equal(args[0][0].Replace("-", "/"), ex.ErrorParameter);
                Assert.Equal($"'{args[0][0].Replace("-", "/")}' is not a valid parameter", ex.ErrorMessage());
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Passing), false)]
        public void CreateReferenceWithWrongDelimiterAndGetValues_Failing(ArgumentConstructor type, string config, string[] arguments)
        {
            Argument a = CreateConstructor_Passing(type, config, arguments, schema);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => a.GetValue<object>(arguments[1].Substring(2).ToString()).ToString());

            Assert.Equal(ErrorCode.INVALID_PARAMETER, ex.ErrorCode);
            Assert.Null(ex.ErrorArgumentId);
            Assert.Equal(arguments[1].Substring(2).ToString(), ex.ErrorParameter);
            Assert.Equal($"'{arguments[1].Substring(2).ToString()}' is not a valid parameter", ex.ErrorMessage());
        }

        [Theory]
        [MemberData(nameof(GetData_Passing), true)]
        public void CreateReferenceAndGetArgumentsFound_Passing(ArgumentConstructor type, string config, string[] arguments, int length)
        {
            Argument a = CreateConstructor_Passing(type, config, arguments, schema);

            List<string> argumentList = new List<string>();

            for (int i = 0; i < length; i += 2)
            {
                argumentList.Add(arguments.ElementAt(i).Substring(1).ToLower());
            }

            for (int i = length; i < arguments.Length; i++)
            {
                argumentList.Add(arguments.ElementAt(i).Substring(1).ToLower());
            }

            Assert.True(argumentList.SequenceEqual(a.ArgumentsFound));
        }

        [Fact]
        public void CreateReferenceAndGetSchema_Passing()
        {
            List<string> arguments = args[0].ToList();
            arguments.Add(booleanArgs[0].Split(",")[0]);

            Argument a = CreateConstructor_Passing(ArgumentConstructor.WithSchema, configFile, args[0], schema);

            List<string> originalSchema = new List<string>();
            List<string> argumentSchema = new List<string>();

            foreach (ArgumentSchema s in schema)
            {
                originalSchema.Add(string.Join(",", s.Argument.Select(x => x.ToLower())));
            }

            foreach (IEnumerable<string> s in a.Schema)
            {
                argumentSchema.Add(string.Join(",", s));
            }

            Assert.True(originalSchema.SequenceEqual(argumentSchema));
        }
    }
}
