using System.Text;
using System.Text.Json;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TodoApi;
using TodoApi.Controllers;

namespace TodoService.test;

public class TestApiService:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private TestApiService(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    public TestApiService() : this(new WebApplicationFactory<Program>())
    {
    }

    [Fact]
    public async Task should_create_new_todo_item_when_post()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsync("/api/todo/item", new StringContent(
            JsonSerializer.Serialize(new TodoDTO { Title = "Test1",StartDate=new DateTime(2024,1,1),EndDate = new DateTime(2024,1,2)}),
            Encoding.UTF8,
            "application/json"));
        TodoDTO todoItem=JsonSerializer.Deserialize<TodoDTO>(await response.Content.ReadAsStringAsync(),new JsonSerializerOptions{PropertyNameCaseInsensitive=true});
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("Test1",todoItem.Title);
        Assert.Equal(new DateTime(2024,1,1),todoItem.StartDate);
        Assert.Equal(new DateTime(2024,1,2),todoItem.EndDate);
        Assert.NotEqual(0,todoItem.Id);

    }
    [Fact]
    public async Task should_create_new_todo_item_into_existing_list()
    {
        // Arrange
        var client = _factory.CreateClient();
        await client.PostAsync("/api/todo/item", new StringContent(
            JsonSerializer.Serialize(new TodoDTO { Title = "Test1",StartDate=DateTime.Now }),
            Encoding.UTF8,
            "application/json"));
        // Act
        var response = await client.PostAsync("/api/todo/item", new StringContent(
            JsonSerializer.Serialize(new TodoDTO { Title = "Test2",StartDate=DateTime.Now }),
            Encoding.UTF8,
            "application/json"));
        
        TodoDTO todoItem=JsonSerializer.Deserialize<TodoDTO>(await response.Content.ReadAsStringAsync(),new JsonSerializerOptions{PropertyNameCaseInsensitive=true});
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("Test2",todoItem.Title);
        Assert.NotEqual(0,todoItem.Id);

    }
    [Fact]
    public async Task should_retrieve_correct_todo_items_when_GetAllItem()
    {
        // Arrange
        var client = _factory.CreateClient();

        await client.PostAsync("/api/todo/item", new StringContent(
            JsonSerializer.Serialize(new TodoDTO { Title = "Test1",StartDate=DateTime.Now }),
            Encoding.UTF8,"application/json"));
        await client.PostAsync("/api/todo/item", new StringContent(
            JsonSerializer.Serialize(new TodoDTO { Title = "Test2",StartDate=DateTime.Now }),
            Encoding.UTF8,"application/json"));
        // Act
        var validatedTodoItem=await client.GetAsync("/api/todo/items");
        // Assert
        validatedTodoItem.EnsureSuccessStatusCode();
        string content=await validatedTodoItem.Content.ReadAsStringAsync();
        List<TodoDTO> todoItems=JsonSerializer.Deserialize<List<TodoDTO>>(content,new JsonSerializerOptions{PropertyNameCaseInsensitive=true});

        Assert.Equal(2,todoItems.Count);
        Assert.Equal(1,todoItems[0].Id);
        Assert.Equal(2,todoItems[1].Id);
    }

}