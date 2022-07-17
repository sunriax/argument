# Parse Boolean arguments

``` bash
Program.exe -b
# or
Program.exe -boolean
```

## With schema in `ArgumentsLib.json`

*`ArgumentsLib.json`*

``` json
{
  "ArgumentConfig": {
      "Schema": [
        {
          "Argument": [
            "b",
            "boolean",
          ],
          "Marshaler": "",
          "Required": true
        },
        {
          "Argument": [
            "b2",
            "boolean2",
          ],
          "Marshaler": "",
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
    
    bool a = parameter.GetValue<bool>("b");         // True
    bool b = parameter.GetValue<bool>("boolean");   // True

    bool c = parameter.GetValue<bool>("b2");        // False
    bool d = parameter.GetValue<bool>("boolean2");  // False
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
                "b",
                "boolean"
            },
            Marshaler = "",
            Required = true
        },
        new ArgumentSchema()
        {
            Argument = new List<string>()
            {
                "b2",
                "boolean2"
            },
            Marshaler = "",
            Required = false
        }
    };

    Argument argument = new Argument("ArgumentLib.json", args, schema);
    
    bool a = parameter.GetValue<bool>("b");         // True
    bool b = parameter.GetValue<bool>("boolean");   // True

    bool c = parameter.GetValue<bool>("b2");        // False
    bool d = parameter.GetValue<bool>("boolean2");  // False
}
```

---

Arguments that are `Required` must be passed into the Argument, otherwise the Library will throw an Exception. Arguments that are **not** `Required` are optional. If they are not passed into Argument they will become a default value. There can be more argument names for the same type. It is possible to create a long and/or a short argument name or whatever the user needs.

[Back](README.md)