# Parse Integer arguments

``` bash
Program.exe -i 1234
# or
Program.exe -integer 1234
```

## With schema in `ArgumentsLib.json`

*`ArgumentsLib.json`*

``` json
{
  "ArgumentConfig": {
      "Schema": [
        {
          "Argument": [
            "i",
            "integer",
          ],
          "Marshaler": "#",
          "Required": true
        },
        {
          "Argument": [
            "i2",
            "integer2",
          ],
          "Marshaler": "#",
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
    
    int a = parameter.GetValue<int>("i");           // 1234
    int b = parameter.GetValue<int>("integer");     // 1234

    int c = parameter.GetValue<int>("i2");          // 0
    int d = parameter.GetValue<int>("integer2");    // 0
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
                "i",
                "integer"
            },
            Marshaler = "#",
            Required = true
        },
        new ArgumentSchema()
        {
            Argument = new List<string>()
            {
                "i2",
                "integer2"
            },
            Marshaler = "#",
            Required = false
        }
    };

    Argument argument = new Argument("ArgumentLib.json", args, schema);
    
    int a = parameter.GetValue<int>("i");           // 1234
    int b = parameter.GetValue<int>("integer");     // 1234

    int c = parameter.GetValue<int>("i2");          // 0
    int d = parameter.GetValue<int>("integer2");    // 0
}
```

---

Arguments that are `Required` must be passed into the Argument, otherwise the Library will throw an Exception. Arguments that are **not** `Required` are optional. If they are not passed into Argument they will become a default value. There can be more argument names for the same type. It is possible to create a long and/or a short argument name or whatever the user needs.

[Back](README.md)