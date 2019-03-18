using System.Linq;
using FluentAssertions;
using Xunit;

namespace Huzzah.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RequiredFieldsOptions
    {
        [OptionParameter(required: true)]
        public string RequiredText { get; set; }

        [OptionParameter(LongName = "namba", IsRequired = true)]
        public string RequiredNumber { get; set; }
    }

    public class RequiredFieldsOptionsFacts
    {
        [Fact]
        public void RequiredMandatoryMissing_ResultNo()
        {
            var args = new string[] { };

            var parse = CommandLineArgumentParser.Parse<RequiredFieldsOptions>(args);

            parse.Result.Should().Be(OptionsResult.MissingRequiredArgument);
            parse.MissingRequiredOptions.Count.Should().Be(2);
            parse.MissingRequiredOptions.First().Should().Be("requiredtext");
            parse.MissingRequiredOptions.Last().Should().Be("namba");
        }
    }
}
