namespace Empty.Tests.Commands;

public class PingCommandTests
{
    [Test]
    public async Task Ping_Cli_ShouldReturnTime()
    {
        // arrange
        await using var env = new TestEnvironmentBuilder()
            .WithApiServer()
            .WithCli()
            .Build();

        await env.StartAsync();

        // act
        var result = await env.Cli.RunAsync(["ping"]);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.ExitCode, Is.EqualTo(0));
            Assert.That(result.Output, Is.EqualTo("The server time is 2024-01-01 12:00:00 AM +00:00"));
        });
    }

    [Test]
    public async Task Ping_Cli_ShouldReturnErrorIfApiNotAvailable()
    {
        // arrange
        await using var env = new TestEnvironmentBuilder()
            .WithCli()
            .Build();

        await env.StartAsync();

        env.Http
            .ForCallsTo("*/api/ping/v1/time")
            .SimulateTimeout();

        // act
        var result = await env.Cli.RunAsync(["ping"]);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.ExitCode, Is.EqualTo(-1));
            Assert.That(result.Output, Is.EqualTo("Error: Call timed out: GET http://localhost:5197/api/ping/v1/time"));
        });
    }    
}