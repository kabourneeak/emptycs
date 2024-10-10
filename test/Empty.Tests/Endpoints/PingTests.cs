using Empty.Api.Controllers;
using Empty.Sdk.Endpoints;
using Empty.Sdk.Models;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Empty.Tests.Endpoints;

public class PingTests
{
    [Test]
    public async Task Ping_Controller_ShouldReturnTime()
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
            .AssertType<TimeDto>();

        // assert
        Assert.That(result.Time, Is.EqualTo(env.Time.GetUtcNow()));
    }

    [Test]
    public async Task Ping_Endpoint_ShouldReturnTime()
    {
        // arrange
        await using var env = new TestEnvironmentBuilder()
            .WithApiServer()
            .Build();

        await env.StartAsync();

        var subject = env.ApiServer.Services.GetRequiredService<PingV1Endpoints>();

        // act
        var result = await subject.Time(CancellationToken.None);

        // assert
        Assert.That(result.Time, Is.EqualTo(env.Time.GetUtcNow()));
    }
}
