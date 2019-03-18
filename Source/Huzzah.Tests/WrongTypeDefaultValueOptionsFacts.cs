using System;
using FluentAssertions;
using Xunit;

namespace Huzzah.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WrongTypeDefaultValueOptions
    {
        [OptionParameter(DefaultValue = 99)]
        public string WrongType { get; set; }
    }

    public class WrongTypeDefaultValueOptionsFacts
    {
        [Fact]
        public void WrongTypeThrowsException()
        {
            var args = new[]
            {
                "--specified",
                "whatever"
            };

            Action action = () => CommandLineArgumentParser.Parse<WrongTypeDefaultValueOptions>(args);

            action.Should().Throw<ArgumentException>();
        }
    }
}
