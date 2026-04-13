using System.Net;
using dotnet_practice.Tests.Infrastructure;

namespace dotnet_practice.Tests.Integration;

public class GamesEndpointsTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient client;

    public GamesEndpointsTests(TestWebApplicationFactory factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task GetGameById_ReturnsNotFound_WhenGameDoesNotExist()
    {
        var response = await client.GetAsync("/games/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Fact]
    public async Task GetGames_ReturnsBadRequest_WhenPageIsInvalid()
    {
        var response = await client.GetAsync("/games?page=0&pageSize=10");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetGames_ReturnsBadRequest_WhenPageSizeIsInvalid()
    {
        var response = await client.GetAsync("/games?page=1&pageSize=0");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);


    }
}
