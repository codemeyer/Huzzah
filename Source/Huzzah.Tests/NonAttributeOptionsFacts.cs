using FluentAssertions;
using Xunit;

namespace Huzzah.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NonAttributeOptions
    {
        public string Text { get; set; }

        public int Number { get; set; }

        public bool Boolean { get; set; }

        public string[] TextArray { get; set; }

        public int[] NumberArray { get; set; }
    }

    public class NonAttributeOptionsFacts
    {
        [Fact]
        public void String()
        {
            var args = new[]
            {
                "--text",
                "string"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Text.Should().Be("string");
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void StringParamWithMultipleArguments_UsesFirstArg()
        {
            var args = new[]
            {
                "--text",
                "string",
                "ignored"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Text.Should().Be("string");
            result.Result.Should().Be(OptionsResult.Success);
        }


        [Fact]
        public void Parse_StringParamWithNoArguments_NothingSet()
        {
            var args = new[]
            {
                "--text"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Text.Should().Be(default(string));
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void PositiveInteger()
        {
            var args = new[]
            {
                "--number",
                "99"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Number.Should().Be(99);
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void NegativeInteger()
        {
            var args = new[]
            {
                "--number",
                "-66"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Number.Should().Be(-66);
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void BooleanValueTrue()
        {
            var args = new[]
            {
                "--boolean",
                "true"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Boolean.Should().BeTrue();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void BooleanValueCapitalTrue()
        {
            var args = new[]
            {
                "--boolean",
                "True"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Boolean.Should().BeTrue();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void BooleanValueFalse()
        {
            var args = new[]
            {
                "--boolean",
                "false"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Boolean.Should().BeFalse();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void BooleanValueCapitalFalse()
        {
            var args = new[]
            {
                "--boolean",
                "False"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Boolean.Should().BeFalse();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void BooleanSwitchOnlyIsTrue()
        {
            var args = new[]
            {
                "--boolean"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.Boolean.Should().BeTrue();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void StringArray()
        {
            var args = new[]
            {
                "--textarray",
                "string",
                "text"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.TextArray.Length.Should().Be(2);
            result.ParsedOptions.TextArray[0].Should().Be("string");
            result.ParsedOptions.TextArray[1].Should().Be("text");
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void IntegerArray()
        {
            var args = new[]
            {
                "--numberarray",
                "5",
                "66"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.NumberArray.Length.Should().Be(2);
            result.ParsedOptions.NumberArray[0].Should().Be(5);
            result.ParsedOptions.NumberArray[1].Should().Be(66);
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void IntegerArrayWithNegativeValue()
        {
            var args = new[]
            {
                "--numberarray",
                "8",
                "-99",
                "7"
            };

            var result = CommandLineArgumentParser.Parse<NonAttributeOptions>(args);

            result.ParsedOptions.NumberArray.Length.Should().Be(3);
            result.ParsedOptions.NumberArray[0].Should().Be(8);
            result.ParsedOptions.NumberArray[1].Should().Be(-99);
            result.ParsedOptions.NumberArray[2].Should().Be(7);
            result.Result.Should().Be(OptionsResult.Success);
        }
    }
}
