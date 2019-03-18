using FluentAssertions;
using Xunit;

namespace Huzzah.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DefaultValuesOptions
    {
        [OptionParameter(DefaultValue = "DefaultText")]
        public string Text { get; set; }

        [OptionParameter(DefaultValue = null)]
        public string TextNull { get; set; }

        [OptionParameter(DefaultValue = 12345)]
        public int Number { get; set; }

        [OptionParameter(DefaultValue = true)]
        public bool BooleanTrue { get; set; }

        [OptionParameter(DefaultValue = false)]
        public bool BooleanFalse { get; set; }

        [OptionParameter(DefaultValue = new[] { "Default1", "Default2" })]
        public string[] TextArray { get; set; }

        [OptionParameter(DefaultValue = new[] { 1, 25 })]
        public int[] NumberArray { get; set; }
    }

    public class DefaultValuesOptionsFacts
    {
        [Fact]
        public void String()
        {
            var args = new string[] {};

            var result = CommandLineArgumentParser.Parse<DefaultValuesOptions>(args);

            result.ParsedOptions.Text.Should().Be("DefaultText");
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void StringNull()
        {
            var args = new string[] {};

            var result = CommandLineArgumentParser.Parse<DefaultValuesOptions>(args);

            result.ParsedOptions.TextNull.Should().BeNull();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void Integer()
        {
            var args = new string[] { };

            var result = CommandLineArgumentParser.Parse<DefaultValuesOptions>(args);

            result.ParsedOptions.Number.Should().Be(12345);
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void BooleanTrue()
        {
            var args = new string[] { };

            var result = CommandLineArgumentParser.Parse<DefaultValuesOptions>(args);

            result.ParsedOptions.BooleanTrue.Should().BeTrue();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void BooleanFalse()
        {
            var args = new string[] { };

            var result = CommandLineArgumentParser.Parse<DefaultValuesOptions>(args);

            result.ParsedOptions.BooleanFalse.Should().BeFalse();
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void StringArray()
        {
            var args = new string[] { };

            var result = CommandLineArgumentParser.Parse<DefaultValuesOptions>(args);

            result.ParsedOptions.TextArray.Length.Should().Be(2);
            result.ParsedOptions.TextArray[0].Should().Be("Default1");
            result.ParsedOptions.TextArray[1].Should().Be("Default2");
            result.Result.Should().Be(OptionsResult.Success);
        }

        [Fact]
        public void IntegerArray()
        {
            var args = new string[] { };

            var result = CommandLineArgumentParser.Parse<DefaultValuesOptions>(args);

            result.ParsedOptions.NumberArray.Length.Should().Be(2);
            result.ParsedOptions.NumberArray[0].Should().Be(1);
            result.ParsedOptions.NumberArray[1].Should().Be(25);
            result.Result.Should().Be(OptionsResult.Success);
        }
    }
}
