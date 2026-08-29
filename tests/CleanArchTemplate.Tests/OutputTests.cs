using CleanArchTemplate.Application.Common.Outputs;
using Xunit;

namespace CleanArchTemplate.Tests;

public sealed class OutputTests
{
    [Fact]
    public void AddErrorMessage_Should_Mark_Output_As_Invalid()
    {
        var output = new Output<string>();

        output.AddErrorMessage("Invalid input.");

        Assert.False(output.IsValid);
        Assert.True(output.HasErrors());
    }
}
