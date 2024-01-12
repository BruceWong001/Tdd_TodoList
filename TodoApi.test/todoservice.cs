namespace TodoApi.test;

public class todoservice
{
    [Fact]
    public void should_return_all_todo_Items()
    {
        // Arrange
        
        var mockRepository=new Mock<IRepository>();
        var todolist = new ItemCollection(mockRepository.Object);
        var item1=todolist.NewItem();
        item1.StartDate=DateTime.Now; item1.Title = "task1";
        var item2=todolist.NewItem();
        item2.StartDate=DateTime.Now; item2.Title = "task2";
        var item3=todolist.NewItem();
        item3.StartDate=DateTime.Now; item3.Title = "task3";
        mockRepository.Setup(x=>x.Save(It.IsAny<TodoItem>()));
        mockRepository.Setup(x=>x.GetAllItems()).Returns(new List<TodoItem>
        {
            item1,item2,item3
        });
        var items= new ItemCollection(mockRepository.Object);

        // Act
        var result = items.GetAllItems();

        // Assert
        Assert.Equal(3, result.Count());

    }
    [Fact]
    public void should_success_add_one_in_existing_todolist()
    {
        // Arrange
        var mockRepository=new Mock<IRepository>();
        var todolist = new ItemCollection(mockRepository.Object);
        var item1=todolist.NewItem();
        item1.StartDate=DateTime.Now; item1.Title = "task1";
        var item2=todolist.NewItem();
        item2.StartDate=DateTime.Now; item2.Title = "task2";
        var item3=todolist.NewItem();
        item3.StartDate=DateTime.Now; item3.Title = "task3";
        List<TodoItem> ret = new List<TodoItem>() { item1,item2,item3};
        
        mockRepository.Setup(x=>x.Save(It.IsAny<TodoItem>())).Callback<TodoItem>((task) => 
        {
            // 在这里添加你想在 Save 方法被调用时执行的代码
            ret.Add(task);
        });
        mockRepository.Setup(x=>x.GetAllItems()).Returns(ret);
        //Act
        var newItem = todolist.NewItem();
        newItem.StartDate=DateTime.Now; newItem.Title = "new task";
        newItem.Save();
        var result = todolist.GetAllItems();
        // Assert
        Assert.Equal(4, result.Count());
    }

    [Fact]
    public void should_create_a_new_todo_item()
    {
        // Arrange
        TodoItem savedItem=null;
        var mockRepository = new Mock<IRepository>();
        mockRepository.Setup(m => m.Save(It.IsAny<TodoItem>()))
            .Callback<TodoItem>(item =>
            {
                savedItem = item;
                savedItem.Id = 1;
            } )
            .Returns(() => savedItem);
        //Act
        TodoItem newItem = new ItemCollection(mockRepository.Object).NewItem();
        newItem.StartDate = new DateTime(2024, 1, 1);
        newItem.EndDate = new DateTime(2024, 1, 2);
        newItem.Title = "new Todo Item";
        TodoItem ret=newItem.Save();
        // Assert
        mockRepository.Verify(x=>x.Save(It.IsAny<TodoItem>()),Times.Once);
        Assert.NotEqual(0,ret.Id);
        Assert.Equal(newItem.Title,ret.Title);
        Assert.Equal(newItem.StartDate,ret.StartDate);
        Assert.Equal(newItem.EndDate,ret.EndDate);
        
    }

    [Fact]
    public void should_PassValue_to_new_item()
    {
        TodoItem newItem = new ItemCollection(null).NewItem();
        TodoItem oldItem = new TodoItem(){StartDate = DateTime.Now,Title = "old Todo Item"};
        newItem.Parse(oldItem);
        Assert.Equal(oldItem.StartDate,newItem.StartDate);
        Assert.Equal(oldItem.Title,newItem.Title);
    }
    [Fact]
    public void parse_shouldCopyProperties()
    {
        // Arrange
        var sourceItem = new TodoItem
        {
            StartDate = DateTime.Now.AddDays(-1),
            EndDate = DateTime.Now.AddDays(1),
            Title = "Source Title"
        };

        var targetItem = new TodoItem();

        // Act
        targetItem.Parse(sourceItem);

        // Assert
        Assert.Equal(sourceItem.StartDate, targetItem.StartDate);
        Assert.Equal(sourceItem.EndDate, targetItem.EndDate);
        Assert.Equal(sourceItem.Title, targetItem.Title);
    }
    
    //should_update_existing_item
  
    //should_remove_one_item
}