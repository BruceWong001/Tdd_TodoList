namespace TodoApi.test;

public class memoryRepository
{
    [Fact]
    public void should_success_add_one_item()
    {
        // Arrange
        var repository=new MemoryRepository();
        var item = new TodoItem();
        item.StartDate = new DateTime(2024, 1, 1);
        item.EndDate = new DateTime(2024, 1, 2);
        
        item.Title = "task1";
        //Act
        TodoItem savedItem= repository.Save(item);
        var items=repository.GetAllItems();
        // Assert
        Assert.Equal(1,items.Count());
        Assert.Equal(1,items[0].Id);
        Assert.Equal(item.Title,savedItem.Title);
        Assert.Equal(new DateTime(2024, 1, 1),savedItem.StartDate);
        Assert.Equal(new DateTime(2024, 1, 2),savedItem.EndDate);
        Assert.NotEqual(0,savedItem.Id);
    }
    [Fact]
    public void should_success_add_one_item_when_remove_middle_one()
    {
        // Arrange
        List<TodoItem> existingItems=new List<TodoItem>
        {
            new TodoItem(){StartDate = DateTime.Now,Title = "task1",Id=1},
            new TodoItem(){StartDate = DateTime.Now,Title = "task2",Id=2},
            new TodoItem(){ StartDate = DateTime.Now,Title = "task3",Id=3}
        };
        var repository=new MemoryRepository(existingItems);
        repository.Remove(existingItems[1]);
        // Act
        var item4 = new TodoItem(){ StartDate = DateTime.Now,Title = "New Task4"};
        repository.Save(item4);
        var items=repository.GetAllItems();
        // Assert
        
        Assert.Equal(3,items.Count);
        Assert.Equal(4,items[2].Id);
    }
    
    [Fact]
    public void should_arrange_real_id_when_add_one_item_with_non_existingId()
    {
        // Arrange
        var repository=new MemoryRepository();
        var item1 = new TodoItem();
        item1.StartDate=DateTime.Now;
        item1.Title = "task1";
        var item2 = new TodoItem();
        item2.StartDate=DateTime.Now;item2.Title = "task2";
        var item3 = new TodoItem();
        item3.StartDate=DateTime.Now;item3.Title = "task3";
        repository.Save(item1);
        repository.Save(item2);
        repository.Save(item3);
        // Act
        var item4 = new TodoItem(){ StartDate = DateTime.Now,Title = "New Task4",Id=10};
        repository.Save(item4);
        var items=repository.GetAllItems();
        // Assert
        Assert.Equal(4,items.Count);
        Assert.Equal(4,items[3].Id);
    }
    
    [Fact]
    public void should_zero_when_initial_repository()
    {
        var repository=new MemoryRepository();
        var items=repository.GetAllItems();
        Assert.Equal(0,items.Count);
    }
    [Fact]
    public void should_return_all_Items()
    {
        // Arrange
        var repository=new MemoryRepository();
        var item1 = new TodoItem();
        item1.StartDate=DateTime.Now;
        item1.Title = "task1";
        var item2 = new TodoItem();
        item2.StartDate=DateTime.Now;item2.Title = "task2";
        var item3 = new TodoItem();
        item3.StartDate=DateTime.Now;item3.Title = "task3";
        repository.Save(item1);
        repository.Save(item2);
        repository.Save(item3);
        // Act
        var items=repository.GetAllItems();
        // Assert
        Assert.Equal(3, items.Count);
    }

    [Fact]
    public void should_update_existing_item()
    {
        // Arrange
        List<TodoItem> existingItems=new List<TodoItem>
        {
            new TodoItem(){StartDate = DateTime.Now,Title = "task1",Id=1},
            new TodoItem(){StartDate = DateTime.Now,Title = "task2",Id=2},
            new TodoItem(){ StartDate = DateTime.Now,Title = "task3",Id=3}
        };
        var repository=new MemoryRepository(existingItems);
        // Act
        var item2 = new TodoItem(){ StartDate = DateTime.Now,Title = "New Task2",Id=2};
        repository.Save(item2);
        // Assert
        var items=repository.GetAllItems();
        Assert.Equal(3, items.Count);
        Assert.Equal("New Task2", items[1].Title);
        
    }
    [Fact]
    public void should_remove_one_item()
    {
        // Arrange
        var repository = new MemoryRepository();
        var item1 = new TodoItem() { StartDate = DateTime.Now, Title = "task1"};
        repository.Save(item1);
        // Act
        repository.Remove(item1);
        var items=repository.GetAllItems();
        // Assert
        Assert.Equal(0, items.Count);
    }
    [Fact]
    public void should_remove_item_in_middle()
    {
        // Arrange
        List<TodoItem> existingItems=new List<TodoItem>
        {
            new TodoItem(){StartDate = DateTime.Now,Title = "task1",Id=1},
            new TodoItem(){StartDate = DateTime.Now,Title = "task2",Id=2},
            new TodoItem(){ StartDate = DateTime.Now,Title = "task3",Id=3}
        };
        var repository=new MemoryRepository(existingItems);
        var item2 = new TodoItem(){ StartDate = DateTime.Now,Title = "New Task2",Id=2};
        // Act
        repository.Remove(item2);
        var items=repository.GetAllItems();
        // Assert
        Assert.Equal(2, items.Count);
        Assert.Equal("task3", items[1].Title);
        
    }
}

