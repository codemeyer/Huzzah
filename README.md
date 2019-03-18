# Huzzah

Huzzah is a single C# file that you drop into your .NET Console Application to implement a basic form
of command line argument parsing, including support for
long and short switches, verb commands, defaults and required values.


## Usage

In its most basic form, Huzzah maps all values that are passed as command line arguments to any properties it can find on the object that you pass to it.

For example, consider this command line call:

    myapp.exe --argument this --foo bar --thegreatest 99

And a C# class representing the possible command line arguments, defined thus:

    public class Options
    {
        public string Argument { get; set; }
        public string Foo { get; set; }
        public int TheGreatest { get; set; }
    }

Finally, having placed the CommandLineArgumentParser.cs file in your project, with a main method defined like this:

    static void Main(string[] args)
    {
        var options = Huzzah.CommandLineArgumentParser.Parse<Options>(args).ParsedOptions;

        Console.WriteLine(options.Argument);
        Console.WriteLine(options.Foo);
        Console.WriteLine(options.TheGreatest);
    }

When running, this will output the following to the console, since Huzzah will have mapped the properties it could find:

    this
    bar
    99


## Installation

Grab the CommandLineArgumentParser.cs and place it somewhere in your Console Application.
Change the namespace (and anything else) if you want to.


## Advanced Usages

By adding the `OptionParameter` attribute to any property, it's possible to
add more advanced functionality, such as:


### Changing the name of the parameter

You can override the name of the parameter that Huzzah will look for.

    [OptionParameter(LongName="something")]
    public string Argument { get; set; }

In this case, `--something` will be the argument name instead of `--argument`.


### Adding a short variant

It's also possible to set short variants, e.g.:

    [OptionParameter(ShortName='a')]
    public string Argument { get; set; }

Which means that you can use `myapp.exe -a this` as well as `myapp.exe --argument this`.


### Required values

By setting the `IsRequired` flag, Huzzah will check if a value has been provided. Given:

    [OptionParameter(required: true)]
    public string RequiredText { get; set; }

And

    var result = Huzzah.CommandLineArgumentParser.Parse<Options>(args);

Then `result.Result` will be `OptionsResult.MissingRequiredArgument`,
and `result.MissingRequiredOptions` will contain the name of the argument
that is missing.


### Default values

You can have fallback default values declared, that will be set if nothing else is.

    [OptionParameter(DefaultValue = "DefaultText")]
    public string Text { get; set; }

If `--text` is never set, the property will get the `DefaultText` value.


### Boolean values

Boolean values support "true", "True", "false" and "False", and if the
value is omitted, the value is set to true, e.g. given:

    public bool Boolean { get; set; }

    myapp.exe --boolean --argument text

This would set the `Boolean` property to true.


### Arrays

Arrays are supported in the following way. Given:

    public string[] StringArray { get; set; }

Then calling the app with:

    myapp.exe --stringarray FirstValue SecondValue

Will make the `StringArray` contain two values, `FirstValue` and `SecondValue`.


### Verbs

If you want a command line app with verb support, i.e.

    myapp.exe action --argument this
    myapp.exe perform --foo bar
    // etc

This can be done in the following way:

Have a base `Options` class.

    public class Options
    {
    }

This can contain any parameters that will be available for _all_ verbs.

Then add other classes that inherit your base class, and have the
wanted verb as a prefix to the name, e.g.

    public class ActionOptions
    {
        public string Argument { get; set;}
    }

With a command line such as this:

    myapp.exe action --argument this

You then call Huzzah with the `Options` base class:

    var options = Huzzah.CommandLineArgumentParser.Parse<Options>(args).ParsedOptions;

Huzzah will return an `ActionsOptions` class with `Argument` set to `this`.


## Limitations

Huzzah probably doesn't work well with generics. Or with anything more
advanced than strings, integers, booleans, arrays and other simple types.

If you're looking for something more advanced, I would suggest
[Command Line Parser Library for CLR and NetStandard](https://github.com/commandlineparser/commandline).
