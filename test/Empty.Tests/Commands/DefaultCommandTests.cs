namespace Empty.Tests.Commands;

public class DefaultCommandTests
{
    [Test]
    public async Task DefaultCommand_ShouldReturnHelp()
    {
        // arrange
        await using var env = new TestEnvironmentBuilder()
            .WithCli()
            .Build();

        await env.StartAsync();

        // act
        var result = await env.Cli.RunAsync([]);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.ExitCode, Is.EqualTo(0));
            Assert.That(result.Output, Does.StartWith("USAGE:"));
        });
    }
}