# Parse String arguments

``` bash
Program.exe -s "Text"
# or
Program.exe -string "Text"
```

## With schema in `ArgumentsLib.json`

*`ArgumentsLib.json`*

``` json
{
  "ArgumentConfig": {
      "Schema": [
        {
          "Argument": [
            "t",
            "text",
          ],
          "Marshaler": "*",
          "Required": true
        },
        {
          "Argument": [
            "t2",
            "text2",
          ],
          "Marshaler": "*",
          "Required": false
        }
      ]
  }
}
```

``` csharp
static void Main(string[] args)
{
    Argument argument = new Argument("ArgumentLib.json", args);
    
    string a = parameter.GetValue<string>("t");         // Text
    string b = parameter.GetValue<string>("text");      // Text

    string c = parameter.GetValue<string>("t2");
    string d = parameter.GetValue<string>("text2");
}
```

## With schema in `Program`

``` csharp
static void Main(string[] args)
{
    IEnumerable<ArgumentSchema> schema = new List<ArgumentSchema>()
    {
        new ArgumentSchema()
        {
            Argument = new List<string>()
            {
                "t",
                "text"
            },
            Marshaler = "*",
            Required = true
        },
        new ArgumentSchema()
        {
            Argument = new List<string>()
            {
                "t2",
                "text2"
            },
            Marshaler = "*",
            Required = false
        }
    };

    Argument argument = new Argument("ArgumentLib.json", args, schema);
    
    string a = parameter.GetValue<string>("t");         // Text
    string b = parameter.GetValue<string>("text");      // Text

    string c = parameter.GetValue<string>("t2");
    string d = parameter.GetValue<string>("text2");
}
```

---

Arguments that are `Required` must be passed into the Argument, otherwise the Library will throw an Exception. Arguments that are **not** `Required` are optional. If they are not passed into Argument they will become a default value. There can be more argument names for the same type. It is possible to create a long and/or a short argument name or whatever the user needs.

[Back](README.md)