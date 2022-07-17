using RaGae.ArgumentLib;
using RaGae.ArgumentLib.MarshalerLib;
using RaGae.ExceptionLib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ArgumentException = RaGae.ArgumentLib.ArgumentException;

namespace ReadArgument
{
    class Program
    {
        static void Main(string[] args)
        {
            Argument argument = null;

            Console.WriteLine("WITH SCHEMA IN ARGUMENTLIB.JSON");

            try
            {
                argument = new Argument("ArgumentLib.json", args);
                Console.WriteLine("Arguments");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.ErrorMessage());
                return;
            }
            catch (BaseException<int> ex)
            {
                Console.WriteLine(ex.ErrorMessage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nBoolean Values:\n");
            Console.WriteLine(argument.GetValue<bool>("BoOl"));
            Console.WriteLine(argument.GetValue<bool>("BoOlEaN"));
            Console.WriteLine(argument.GetValue<bool>("bool2"));
            Console.WriteLine(argument.GetValue<bool>("boolean2"));

            Console.WriteLine("\nInteger Values:\n");
            Console.WriteLine(argument.GetValue<int>("int"));
            Console.WriteLine(argument.GetValue<int>("NuMbEr"));
            Console.WriteLine(argument.GetValue<int>("int2"));
            Console.WriteLine(argument.GetValue<int>("number2"));

            Console.WriteLine("\nDecimal Values:\n");
            Console.WriteLine(argument.GetValue<double>("DoUbLe"));
            Console.WriteLine(argument.GetValue<double>("DeCiMaL"));
            Console.WriteLine(argument.GetValue<double>("double2"));
            Console.WriteLine(argument.GetValue<double>("decimal2"));

            Console.WriteLine("\nString Values:\n");
            Console.WriteLine(argument.GetValue<string>("StRiNg"));
            Console.WriteLine(argument.GetValue<string>("TeXt"));
            Console.WriteLine(argument.GetValue<string>("DaTa"));
            Console.WriteLine(argument.GetValue<string>("string2"));
            Console.WriteLine(argument.GetValue<string>("text2"));
            Console.WriteLine(argument.GetValue<string>("data2"));

            Console.WriteLine("\nWITHOUT SCHEMA IN ARGUMENTLIB.NOSCHEMA.JSON");

            IEnumerable<ArgumentSchema> schema = new List<ArgumentSchema>()
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


            try
            {
                argument = new Argument("ArgumentLib.NoSchema.json", args, schema);
                Console.WriteLine("Arguments");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.ErrorMessage());
                return;
            }
            catch (BaseException<int> ex)
            {
                Console.WriteLine(ex.ErrorMessage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nBoolean Values:\n");
            Console.WriteLine(argument.GetValue<bool>("BoOl"));
            Console.WriteLine(argument.GetValue<bool>("BoOlEaN"));
            Console.WriteLine(argument.GetValue<bool>("bool2"));
            Console.WriteLine(argument.GetValue<bool>("boolean2"));

            Console.WriteLine("\nInteger Values:\n");
            Console.WriteLine(argument.GetValue<int>("int"));
            Console.WriteLine(argument.GetValue<int>("NuMbEr"));
            Console.WriteLine(argument.GetValue<int>("int2"));
            Console.WriteLine(argument.GetValue<int>("number2"));

            Console.WriteLine("\nDecimal Values:\n");
            Console.WriteLine(argument.GetValue<double>("DoUbLe"));
            Console.WriteLine(argument.GetValue<double>("DeCiMaL"));
            Console.WriteLine(argument.GetValue<double>("double2"));
            Console.WriteLine(argument.GetValue<double>("decimal2"));

            Console.WriteLine("\nString Values:\n");
            Console.WriteLine(argument.GetValue<string>("StRiNg"));
            Console.WriteLine(argument.GetValue<string>("TeXt"));
            Console.WriteLine(argument.GetValue<string>("DaTa"));
            Console.WriteLine(argument.GetValue<string>("string2"));
            Console.WriteLine(argument.GetValue<string>("text2"));
            Console.WriteLine(argument.GetValue<string>("data2"));

            Console.WriteLine("\nPRESS ANY KEY TO QUIT!");

            Console.ReadKey();
        }
    }
}
