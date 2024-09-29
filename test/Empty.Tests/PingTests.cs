using Empty.Api.Controllers;
using Empty.Sdk.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Empty.Tests;

public class PingTests
{
    [Test]
    public async Task Ping_ShouldReturnTime()
    {
        // arrange
        await using var env = new TestEnvironmentBuilder()
            .WithApiServer()
            .Build();

        await env.StartAsync();

        var subject = env.ApiServer.Services.GetRequiredService<PingV1Controller>();

        // act
        var result = (await subject.Time())
            .AssertType<OkObjectResult>()
            .Value
            .AssertType<TimeDto>()
            .Time;

        // assert
        Assert.That(result, Is.EqualTo(env.Time.GetUtcNow()));
    }
}
