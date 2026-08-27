using TodoApi.Services;

namespace TodoApi.Tests;

public class TodoServiceTests
{
    [Fact]
    public void GetAll_WhenEmpty_ReturnsEmptyCollection()
    {
        var service = new TodoService();

        var result = service.GetAll();

        Assert.Empty(result);
    }

    [Fact]
    public void Add_ReturnsItemWithTitleAndAssignedId()
    {
        var service = new TodoService();

        var item = service.Add("Buy milk");

        Assert.Equal("Buy milk", item.Title);
        Assert.True(item.Id > 0);
    }

    [Fact]
    public void Add_MultipleItems_AssignsUniqueIds()
    {
        var service = new TodoService();

        var first = service.Add("First");
        var second = service.Add("Second");

        Assert.NotEqual(first.Id, second.Id);
    }

    [Fact]
    public void GetAll_AfterAdding_ReturnsAddedItems()
    {
        var service = new TodoService();
        service.Add("Buy milk");
        service.Add("Walk dog");

        var result = service.GetAll();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void Delete_ExistingItem_RemovesItAndReturnsTrue()
    {
        var service = new TodoService();
        var item = service.Add("Buy milk");

        var deleted = service.Delete(item.Id);

        Assert.True(deleted);
        Assert.Empty(service.GetAll());
    }

    [Fact]
    public void Delete_NonExistentItem_ReturnsFalse()
    {
        var service = new TodoService();

        var deleted = service.Delete(999);

        Assert.False(deleted);
    }
}