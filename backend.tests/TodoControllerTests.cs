using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoApi.Models;

namespace TodoApi.Tests;

public class TodoControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TodoControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/todo");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Post_ThenGet_ReturnsCreatedItem()
    {
        var response = await _client.PostAsJsonAsync("/api/todo", new { title = "Integration test item" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var getResponse = await _client.GetFromJsonAsync<List<TodoItem>>("/api/todo");
        Assert.Contains(getResponse!, t => t.Title == "Integration test item");
    }

    [Fact]
    public async Task Post_WithEmptyTitle_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync("/api/todo", new { title = "" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Delete_NonExistentItem_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync("/api/todo/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}