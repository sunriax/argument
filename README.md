[![Version: 1.0 Release](https://img.shields.io/badge/Version-1.0%20Release-green.svg)](https://github.com/sunriax) [![NuGet](https://img.shields.io/nuget/dt/ragae.argument.svg)](https://www.nuget.org/packages/ragae.argument) [![Build Status](https://www.travis-ci.com/sunriax/argument.svg?branch=main)](https://www.travis-ci.com/sunriax/argument) [![codecov](https://codecov.io/gh/sunriax/argument/branch/main/graph/badge.svg)](https://codecov.io/gh/sunriax/argument) [![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

# Argument Reader

## Description:

With Argument Reader command line arguments can be passed into a .net standard/core application. The standard project can handle 4 types of arguments:

* Boolean
* Strings *(\*)*
* Integers *(#)*
* Doubles *(##)*

Own argument types can be build with own classes. They need to inherit from the **MarshalerLib**. Libraries are getting loaded dynamically on startup. It is not necessary to recompile the complete solution if a new marshaler is added.

---

## Installation

To install ArgumentReader it is possible to download necessary libraries [[zip](https://github.com/sunriax/argument/releases/latest/download/Argument.zip) | [tar.gz](https://github.com/sunriax/argument/releases/latest/download/Argument.tar.gz)] or install the library via nuget.

```
PM> Install-Package RaGae.Argument
```

After adding/installing the ArgumentsLib in a project it is necessary to add the required marshalers to a directory in your project or install via nuget. For installing the marshalers manually, it is possible to use the **copy** scripts in this repository to download the marshalers. Each file can also be downloaded directly (see [Available Marshalers](#Directly-download-Marshalers-(Standard))).

### Installation with download script

**Windows**

``` bash
C:\Users\...\Solution\Project> mkdir Marshaler

# It is necessary to move the copy.bat script in that directory

C:\Users\...\Solution\Project> mkdir Marshaler
C:\Users\...\Solution\Project> cd Marshaler
C:\Users\...\Solution\Project> copy.bat
# ...
#BooleanMarshalerLib
Download [Y/N]? y
#IntegerMarshalerLib
Download [Y/N]? y
#StringMarshalerLib
Download [Y/N]? y
#DoubleMarshalerLib
Download [Y/N]? y
#End of downloading
C:\Users\...\Solution\Project>
```

**Linux**

``` bash
~/Solution/Project/: mkdir Marshaler

# It is necessary to move the copy.sh script in that directory

~/Solution/Project $: cd Marshaler
~/Solution/Project $: chmod 0700 ./copy.sh
~/Solution/Project $: ./copy.sh
# ...
#BooleanMarshalerLib
Download [Y/N]? y
#IntegerMarshalerLib
Download [Y/N]? y
#StringMarshalerLib
Download [Y/N]? y
#DoubleMarshalerLib
Download [Y/N]? y
#End of downloading
~/Solution/Project $:
```

[![Installed Marshalers](https://raw.githubusercontent.com/sunriax/argument/master/marshaler.png)](https://github.com/sunriax/argument/tree/master/ReadArgument)

To copy the Marshalers to output folder setup the *.csproj file.

*`*.csproj`*
``` xml
<Project Sdk="Microsoft.NET.Sdk">
  // ...

  <ItemGroup>
    <LibraryFiles Include="$(ProjectDir)Marshaler\*" />
  </ItemGroup>

  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Copy SourceFiles="@(LibraryFiles)" DestinationFolder="$(TargetDir)Marshaler" SkipUnchangedFiles="true" />
  </Target>
  
  // ...
</Project>
```

### Directly download Marshalers (Standard)

* [RaGae.ArgumentLib.BooleanMarshalerLib.dll](https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.BooleanMarshalerLib.dll)
* [RaGae.ArgumentLib.StringMarshalerLib.dll](https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.StringMarshalerLib.dll)
* [RaGae.ArgumentLib.IntegerMarshalerLib.dll](https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.IntegerMarshalerLib.dll)
* [RaGae.ArgumentLib.DoubleMarshalerLib.dll](https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.DoubleMarshalerLib.dll)

**Configuration setup after download or manual installation**

``` yaml
{
  "ReflectionConfig": [
    {
      "ReflectionPath": "Marshaler",
      "FileSpecifier": "*MarshalerLib.dll"
    }
  ]
}
```

### NuGet installation

|Marshaler|Downloads|
|---------|---------|
| Boolean Marshaler | [![NuGet](https://img.shields.io/nuget/dt/ragae.argument.booleanmarshaler.svg)](https://www.nuget.org/packages/ragae.argument.booleanmarshaler) |
| String Marshaler | [![NuGet](https://img.shields.io/nuget/dt/ragae.argument.stringmarshaler.svg)](https://www.nuget.org/packages/ragae.argument.stringmarshaler) |
| Integer Marshaler | [![NuGet](https://img.shields.io/nuget/dt/ragae.argument.integermarshaler.svg)](https://www.nuget.org/packages/ragae.argument.integermarshaler) |
| Double Marshaler | [![NuGet](https://img.shields.io/nuget/dt/ragae.argument.doublemarshaler.svg)](https://www.nuget.org/packages/ragae.argument.doublemarshaler) |

```
PM> Install-Package RaGae.Argument.BooleanMarshaler
PM> Install-Package RaGae.Argument.StringMarshaler
PM> Install-Package RaGae.Argument.IntegerMarshaler
PM> Install-Package RaGae.Argument.DoubleMarshaler
```

**Configuration setup after nuget installation**

``` yaml
{
  "ReflectionConfig": [
    {
      "Files": [
        "RaGae.ArgumentLib.BooleanMarshalerLib.dll",
        "RaGae.ArgumentLib.StringMarshalerLib.dll",
        "RaGae.ArgumentLib.IntegerMarshalerLib.dll",
        "RaGae.ArgumentLib.DoubleMarshalerLib.dll"
      ]
    }
  ]
}
```

An example project howto use the ArgumentReader can be found within this repository in the [ReadArgument](https://github.com/sunriax/argument/tree/master/ReadArgument) project

---

## Structure

### Initialize with schema in constructor

``` csharp
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
    }
    // ...
};

Argument argument = new Argument("ArgumentLib.json", "Arguments from command line", schema);
```

*ArgumentLib.json*

``` yaml
{
  "ReflectionConfig": [
    {
        "ReflectionPath": "Marshaler",
        "FileSpecifier": "*MarshalerLib.dll"
    }
  ],
  "ArgumentConfig": {
    "Delimiter": "-:/"
  }
}
```

### Initialize with schema in *.json file

``` csharp
Argument argument = new Argument("ArgumentLib.json", "Arguments from command line");
```

*ArgumentsLib.json*

``` yaml
{
  "ReflectionConfig": [
    {
        "ReflectionPath": "Marshaler",
        "FileSpecifier": "*MarshalerLib.dll"
    }
  ],
  "ArgumentConfig": {
    "Schema": [
        {
            "Argument": [
                "string",
                "text",
                "data"
            ],
            "Marshaler": "*",
            "Required": true
    }
    ],
    "Delimiter": "-:/"
  }
}
```

### Arguments parameter

#### ConfigFile

Path to `*.json` file where the necessary parameters can be changed.

``` csharp
Argument argument = new Argument("ArgumentLib.json", "...", "...");
```

#### Args[] from CLI

Arguments that are passed from the command line as array

``` bash
Program.exe -StRiNg "Test string" -string2 "Test string2" -InT 1234 -number2 5678 -DoUbLe 123,456 -decimal2 456,123 -BoOl
```

``` csharp
string[] args = {
    "-StRiNg",
    "Test string"
    "-string2",
    "Test string2",
    "-InT",
    "1234",
    "-number2",
    "5678",
    "-DoUbLe",
    "123,456",
    "-decimal2",
    "456,123",
    "-BoOl"
};
```

``` csharp
Argument argument = new Argument("...", args, "...");
// or
Argument argument = new Argument("...", args);
```

#### Schema

This parameter can be null.

``` csharp
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
    }
    // ...
};

Argument argument = new Argument("...", "...", schema);
```

If null configuration is necessary in *ArgumentsLib.json* file

``` csharp
Argument argument = new Argument("...", "...");
```

`*ArgumentsLib.json*`

``` yaml
{
  "ArgumentConfig": {
      "Schema": [
        {
          "Argument": [
            "string",
            "text",
            "data"
          ],
          "Marshaler": "*",
          "Required": true
        }
      ]
  }
}
```

---

## Parse Arguments

* [Parse Boolean arguments](boolean.md)
* [Parse String arguments](string.md)
* [Parse Integer arguments](integer.md)
* [Parse Double arguments](double.md)

---

## Build your own Marshaler

1. Create a new VisualStudio .NET Standard Classlibrary (**??MarshalerLib**)
1. Link a new project reference to [RaGae.ArgumentLib.MarshalerLib.dll](https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.MarshalerLib.dll) (in this repository) or install as nuget (see below)
1. Write Marshaler (See example code below)
1. Copy the TestMarshalerLib.dll to Marshaler directory in your executeable project
1. Implement the *?* in your schema

```
PM> Install-Package RaGae.Argument.Marshaler
```

``` csharp
using System;
using RaGae.ArgumentLib.MarshalerLib;

namespace RaGae.ArgumentLib.TestMarshalerLib
{
    public class TestMarshalerLib : Marshaler
    {
        // Only schemas allowed that are not used (string.Empty, *, #, ## are already used from standard marshalers)
        public override string Schema => "?";

        public override void Set(Iterator<string> currentArgument)
        {
            try
            {
                // If implementation should use an argument behind the command (e.g. -a "??"),
                // it is necessary to move the Iterator to the next position.
                Value = currentArgument.Next();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new TestMarshalerException(ErrorCode.MISSING);
            }

            // If no argument behind the command is used just add your value
            Value = "This is my personal marshaler";
        }

        public class TestMarshalerException : BaseArgumentException
        {
            public TestMarshalerException(ErrorCode errorCode) : base(errorCode) { }

            public TestMarshalerException(ErrorCode errorCode, string message) : base(errorCode, message) { }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find test parameter for -{base.ErrorArgumentId}";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
```

---

## References

The original Argument Marshaler was written in Java and published by Robert C. Martin in his book Clean Code. This project adapt his implementations and extends it dynamically.

---

R. Gächter
