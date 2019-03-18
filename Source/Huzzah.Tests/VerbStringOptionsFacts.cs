using FluentAssertions;
using Xunit;

namespace Huzzah.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class VerbStringOptions : StringOptions
    {
        [OptionParameter]
        public string AnOption { get; set; }
    }

    public class StringOptions
    {
        [OptionParameter]
        public string AnotherOption { get; set; }
    }

    public class VerbStringOptionsFacts
    {
        [Fact]
        public void VerbSubclassReturned()
        {
            var args = new[]
            {
                "verb",
                "--anoption",
                "optionvalue"
            };

            var parse = CommandLineArgumentParser.Parse<StringOptions>(args);
            var options = parse.ParsedOptions;

            options.Should().BeOfType<VerbStringOptions>();
            var verbOptions = options as VerbStringOptions;
            verbOptions.AnOption.Should().Be("optionvalue");
            parse.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void VerbSubclassReturned_CanSetBaseclassProperty()
        {
            var args = new[]
            {
                "verb",
                "--anoption",
                "optionvalue",
                "--anotheroption",
                "basic"
            };

            var parse = CommandLineArgumentParser.Parse<StringOptions>(args);
            var options = parse.ParsedOptions;

            options.Should().BeOfType<VerbStringOptions>();
            var verbOptions = options as VerbStringOptions;
            verbOptions.AnotherOption.Should().Be("basic");
            parse.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void NoVerbSubclassFound_DefaultIsReturnedWithSet()
        {
            var args = new[]
            {
                "unknownverb",
                "--anotheroption",
                "basic"
            };

            var parse = CommandLineArgumentParser.Parse<StringOptions>(args);
            var options = parse.ParsedOptions;

            options.Should().BeOfType<StringOptions>();
            options.AnotherOption.Should().Be("basic");
            parse.Result.Should().Be(OptionsResult.Success);
        }
    }
}
