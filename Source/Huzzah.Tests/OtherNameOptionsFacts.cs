using FluentAssertions;
using Xunit;

namespace Huzzah.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class OtherNameOptions
    {
        [OptionParameter(LongName = "text")]
        public string LongNameText { get; set; }

        [OptionParameter(LongName = "number")]
        public int LongNameNumber { get; set; }

        [OptionParameter(LongName = "boolean")]
        public bool LongNameBoolean { get; set; }

        [OptionParameter(LongName = "textarray")]
        public string[] LongNameTextArray { get; set; }

        [OptionParameter(LongName = "numberarray")]
        public int[] LongNameNumberArray { get; set; }
    }

    public class OtherNameOptionsFacts
    {
        [Fact]
        public void String()
        {
            var args = new[]
            {
                "--text",
                "string"
            };

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameText.Should().Be("string");
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameText.Should().Be("string");
            result.Result.Should().Be(OptionsResult.Success);
        }


        [Fact]
        public void Parse_StringParamWithNoArguments_NothingSet()
        {
            var args = new[]
            {
                "--text"
            };

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameText.Should().Be(default(string));
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameNumber.Should().Be(99);
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameNumber.Should().Be(-66);
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameBoolean.Should().BeTrue();
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameBoolean.Should().BeTrue();
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameBoolean.Should().BeFalse();
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameBoolean.Should().BeFalse();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void BooleanSwitchOnlyIsTrue()
        {
            var args = new[]
            {
                "--boolean"
            };

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameBoolean.Should().BeTrue();
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameTextArray.Length.Should().Be(2);
            result.ParsedOptions.LongNameTextArray[0].Should().Be("string");
            result.ParsedOptions.LongNameTextArray[1].Should().Be("text");
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameNumberArray.Length.Should().Be(2);
            result.ParsedOptions.LongNameNumberArray[0].Should().Be(5);
            result.ParsedOptions.LongNameNumberArray[1].Should().Be(66);
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

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.LongNameNumberArray.Length.Should().Be(3);
            result.ParsedOptions.LongNameNumberArray[0].Should().Be(8);
            result.ParsedOptions.LongNameNumberArray[1].Should().Be(-99);
            result.ParsedOptions.LongNameNumberArray[2].Should().Be(7);
            result.Result.Should().Be(OptionsResult.Success);
        }
    }
}
