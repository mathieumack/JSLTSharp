# JSLTSharp
This library let you to apply some transformations on a json object. Like an xslt document, transform a json string to another

I have created this library in .NET which enables the transformation of JSON documents using very a simple transformation language like an xslt transformation for XML document, but for JSON.
I've also created this package as the excellent [https://github.com/WorkMaze/JUST.net](JUST.net) package does not responds exactly to my needs, and in case of big volumetry of data, performances was not good for my needs.

==========

# Onboarding Instructions 

## Installation

1. Add nuget package: 

> Install-Package JSLTSharp

2. In your application, you must instanciate a new JsonTransform object, and call the method 'Transform' to transform your json: 

```c#
var input = @"{
        'field1': 13246.51,
        'field2': true
    }";
    
var transformation = @"{
        'resultField1': '$.field1->ToInteger()',
        'resultField2': '$.field2'
    }";

var transformEngine = new JsonTransform();
var result = transformEngine.Transform(input, transformation);
```

result value :

```json
{
    "resultField1": 13246,
    "resultField2": true
}
```

## Selectors
In your transformation description, you can refers to fields by using a JsonPath expression

Ex :
```json
{
    "resultField1": "$.field1->ToInteger()",
    "resultField2": "$.field2"
}
```
$.field1 refers to to the property named field1 on the json input.

:information_source: If you have to test your selector, you can use the online tool https://jsonpath.com/.

# Create my own function

The package can be extended by using your own functions. Code your own C# function, and register it on the service collection of your application. More details on the Wiki.

# IC
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=mathieumack_JSLTSharp&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=mathieumack_JSLTSharp)
[![.NET](https://github.com/mathieumack/JSLTSharp/actions/workflows/ci.yml/badge.svg)](https://github.com/mathieumack/JSLTSharp/actions/workflows/ci.yml)
[![NuGet package](https://buildstats.info/nuget/JSLTSharp?includePreReleases=true)](https://nuget.org/packages/JSLTSharp)

# Documentation : I want more

Do not hesitate to check unit tests on the solution. It's a good way to check how transformations are tested.

Also, to get more samples, go to the [Wiki](https://github.com/mathieumack/JSLTSharp/wiki). 

Do not hesitate to contribute.


# Support / Contribute
If you have any questions, problems or suggestions, create an issue or fork the project and create a Pull Request.

You want more ? Feel free to create an issue or contribute by adding new functionnalities by forking the project and create a pull request.

And if you like this project, don't forget to star it !

You can also support me with a coffee :

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/mathieumack)