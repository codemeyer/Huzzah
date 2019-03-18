using FluentAssertions;
using Xunit;

namespace Huzzah.Tests
{
    public class WrongTypeArgumentFacts
    {
        [Fact]
        public void IntegerAsStringFails()
        {
            var args = new[]
            {
                "--number",
                "NaN"
            };

            var result = CommandLineArgumentParser.Parse<AttributeOnlyOptions>(args);

            result.ParsedOptions.Should().BeNull();
            result.Result.Should().Be(OptionsResult.ParsingException);
            result.ExceptionParameter.Should().Be("number");
        }

        [Fact]
        public void IntegerAsStringFailsWithOtherName()
        {
            var args = new[]
            {
                "--number",
                "NaN"
            };

            var result = CommandLineArgumentParser.Parse<OtherNameOptions>(args);

            result.ParsedOptions.Should().BeNull();
            result.Result.Should().Be(OptionsResult.ParsingException);
            result.ExceptionParameter.Should().Be("number");
        }

        [Fact]
        public void IntegerArrayWithStringFails()
        {
            var args = new[]
            {
                "--numberarray",
                "8",
                "eight",
                "88"
            };

            var result = CommandLineArgumentParser.Parse<AttributeOnlyOptions>(args);

            result.ParsedOptions.Should().BeNull();
            result.Result.Should().Be(OptionsResult.ParsingException);
            result.ExceptionParameter.Should().Be("numberarray");
        }
    }
}
