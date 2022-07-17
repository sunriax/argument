using RaGae.ArgumentLib;
using RaGae.ArgumentLib.MarshalerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Sdk;
using ArgumentException = RaGae.ArgumentLib.ArgumentException;

namespace ArgumentLibTest
{
    public class ArgumentConfigTest
    {
        [Fact]
        public void CreateArgumentConfig_Passing()
        {
            ArgumentConfig a = new ArgumentConfig()
            {
                Delimiter = "-:/"
            };

            Assert.NotNull(a);
            Assert.Null(a.Schema);
            Assert.Equal("-:/", a.Delimiter);
        }

        [Fact]
        public void CreateArgumentSchema_Passing()
        {
            IEnumerable<ArgumentSchema> s = new List<ArgumentSchema>()
            {
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "c",
                        "com",
                        "command"
                    },
                    Marshaler = "*",
                    Required = true
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "c2",
                        "com2",
                        "command2"
                    },
                    Marshaler = "*",
                    Required = false
                }
            };


            Assert.True(s.ElementAt(0).Required);
            Assert.False(s.ElementAt(1).Required);

            Assert.Equal(ErrorCode.OK, s.ElementAt(0).Error);
            Assert.Equal(ErrorCode.OK, s.ElementAt(1).Error);

            Assert.Equal("*", s.ElementAt(0).Marshaler);
            Assert.Equal("*", s.ElementAt(1).Marshaler);

            Assert.Equal(new List<string>() { "c", "com", "command" }, s.ElementAt(0).Argument);
            Assert.Equal(new List<string>() { "c2", "com2", "command2" }, s.ElementAt(1).Argument);
        }

        [Fact]
        public void CreateArgumentSchema_Failing()
        {
            IEnumerable<ArgumentSchema> s = new List<ArgumentSchema>()
            {
                new ArgumentSchema()
                {
                    Argument = null
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "string",
                        "f4!se"
                    }
                },
                new ArgumentSchema()
                {
                    Argument = new List<string>()
                    {
                        "good",
                        "b d"
                    }
                }
            };

            Assert.Equal(ErrorCode.EMPTY, s.ElementAt(0).Error);
            Assert.Equal(ErrorCode.EMPTY, s.ElementAt(1).Error);
            Assert.Equal(ErrorCode.INVALID, s.ElementAt(2).Error);
            Assert.Equal(ErrorCode.INVALID, s.ElementAt(3).Error);
        }

        [Fact]
        public void CreateArgumentConfigWithSchema_Passing()
        {
            ArgumentConfig a = new ArgumentConfig()
            {
                Schema = new List<ArgumentSchema>()
                {
                    new ArgumentSchema()
                    {
                        Argument = new List<string>()
                        {
                            "c",
                            "com",
                            "command"
                        },
                        Marshaler = "*",
                        Required = true
                    },
                    new ArgumentSchema()
                    {
                        Argument = new List<string>()
                        {
                            "c2",
                            "com2",
                            "command2"
                        },
                        Marshaler = "*",
                        Required = false
                    }
                },
                Delimiter = "-:/"
            };

            Assert.NotNull(a);
            Assert.NotNull(a.Schema);
            Assert.Equal("-:/", a.Delimiter);

            Assert.True(a.Schema.ElementAt(0).Required);
            Assert.False(a.Schema.ElementAt(1).Required);

            Assert.Equal(ErrorCode.OK, a.Schema.ElementAt(0).Error);
            Assert.Equal(ErrorCode.OK, a.Schema.ElementAt(1).Error);

            Assert.Equal("*", a.Schema.ElementAt(0).Marshaler);
            Assert.Equal("*", a.Schema.ElementAt(1).Marshaler);

            Assert.Equal(new List<string>() { "c", "com", "command" }, a.Schema.ElementAt(0).Argument);
            Assert.Equal(new List<string>() { "c2", "com2", "command2" }, a.Schema.ElementAt(1).Argument);
        }

        [Fact]
        public void CreateArgumentConfigWithEmptySchema_Failing()
        {
            ArgumentConfig a = null;
                
            BaseArgumentException ex = Assert.Throws<ArgumentException>(() => a = new ArgumentConfig()
            {
                Schema = new List<ArgumentSchema>()
                {
                    new ArgumentSchema()
                    {
                        Argument = null
                    },
                    new ArgumentSchema()
                    {
                        Argument = new List<string>()
                    }
                },
                Delimiter = "-:/"
            });

            Assert.Null(a);

            Assert.Equal(ErrorCode.EMPTY, ex.ErrorCode);
            Assert.Null(ex.ErrorArgumentId);
            Assert.Equal("Argument", ex.ErrorParameter);
            Assert.Equal("Config File: Argument contains no data!", ex.ErrorMessage());
        }

        [Fact]
        public void CreateArgumentConfigWithWrongSchema_Failing()
        {
            ArgumentConfig a = null;

            BaseArgumentException ex = Assert.Throws<ArgumentException>(() => a = new ArgumentConfig()
            {
                Schema = new List<ArgumentSchema>()
                {
                    new ArgumentSchema()
                    {
                        Argument = new List<string>()
                        {
                            "string",
                            "f4!se"
                        }
                    },
                    new ArgumentSchema()
                    {
                        Argument = new List<string>()
                        {
                            "good",
                            "b d"
                        }
                    }
                },
                Delimiter = "-:/"
            });

            Assert.Null(a);

            Assert.Equal(ErrorCode.INVALID, ex.ErrorCode);
            Assert.Null(ex.ErrorArgumentId);
            Assert.Equal("Argument", ex.ErrorParameter);
            Assert.Equal("Config File: Argument contains invalid data!", ex.ErrorMessage());
        }
    }
}
