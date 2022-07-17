# Parse Integer arguments

``` bash
Program.exe -i 1234,5678
# or
Program.exe -integer 1234,5678
```

## With schema in `ArgumentsLib.json`

*`ArgumentsLib.json`*

``` json
{
  "ArgumentConfig": {
      "Schema": [
        {
          "Argument": [
            "d",
            "double",
          ],
          "Marshaler": "##",
          "Required": true
        },
        {
          "Argument": [
            "d2",
            "double2",
          ],
          "Marshaler": "##",
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
    
    double a = parameter.GetValue<double>("d");         // 1234,5678
    double b = parameter.GetValue<double>("double");    // 1234,5678

    double c = parameter.GetValue<double>("d2");        // 0
    double d = parameter.GetValue<double>("double2");   // 0
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
                "d",
                "double"
            },
            Marshaler = "##",
            Required = true
        },
        new ArgumentSchema()
        {
            Argument = new List<string>()
            {
                "d2",
                "double2"
            },
            Marshaler = "##",
            Required = false
        }
    };

    Argument argument = new Argument("ArgumentLib.json", args, schema);
    
    double a = parameter.GetValue<double>("d");         // 1234,5678
    double b = parameter.GetValue<double>("double");    // 1234,5678

    double c = parameter.GetValue<double>("d2");        // 0
    double d = parameter.GetValue<double>("double2");   // 0
}
```

---

Arguments that are `Required` must be passed into the Argument, otherwise the Library will throw an Exception. Arguments that are **not** `Required` are optional. If they are not passed into Argument they will become a default value. There can be more argument names for the same type. It is possible to create a long and/or a short argument name or whatever the user needs.

[Back](README.md)