using Microsoft.Extensions.Caching.Memory;
namespace TodoApi;

public class TodoItem
{
    private readonly IRepository _todolistRepository;
    public TodoItem()
    {
        //this constructor is for DI.
    }
    public TodoItem(IRepository todolistRepository)
    {
        this._todolistRepository = todolistRepository;
    }
    public int Id { get; set; }=0;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Title { get; set; }

    public TodoItem Save()
    {
        return _todolistRepository.Save(this);
    }
    
    /// <summary>
    /// This method is used to parse an existing TodoItem object into the current TodoItem instance.
    /// </summary>
    /// <param name="oldItem">The TodoItem object that needs to be parsed.</param>
    public void Parse(TodoItem oldItem)
    {
        // Assign the Date property of the old TodoItem to the current TodoItem instance.
        this.StartDate=oldItem.StartDate;
        this.EndDate=oldItem.EndDate;
        
        // Assign the Title property of the old TodoItem to the current TodoItem instance.
        this.Title=oldItem.Title;
    }
}

public interface IRepository
{
    TodoItem Save(TodoItem item);
    List<TodoItem> GetAllItems();
    void Remove(TodoItem item);
}

public class ItemCollection
{
    private readonly IRepository _todolistRepository;
    public ItemCollection(IRepository todolistRepository)
    {
        this._todolistRepository = todolistRepository;
    }
    public List<TodoItem> Items { get; private set; } = new List<TodoItem>();
    public List<TodoItem> GetAllItems()
    {
        var Items=_todolistRepository.GetAllItems();
        
        return Items;
    }

    public TodoItem NewItem()
    {
        return new TodoItem(_todolistRepository);
    }

    public List<TodoItem> Remove(TodoItem item )
    {
        _todolistRepository.Remove(item);
        
        return _todolistRepository.GetAllItems();
    }
}

public class MemoryRepository:IRepository
{
    private IMemoryCache cache=new MemoryCache(new MemoryCacheOptions());
    private List<TodoItem> _items=new List<TodoItem>();
    private string key="todoItems";
    public MemoryRepository()
    {
        //this constructor is for DI.
        cache.Set(key,_items);
    }
    /// <summary>
    /// only for unit test
    /// </summary>
    /// <param name="items"></param>
    public MemoryRepository(List<TodoItem> items)
    {
        _items=items;
        cache.Set(key,_items);
    }
    public TodoItem Save(TodoItem item)
    {
        _items = cache.Get<List<TodoItem>>(key) ?? new List<TodoItem>();
        var findedItem=_items.Find(match=>match.Id==item.Id);
        if (findedItem!=null)
        {
            findedItem.StartDate=item.StartDate;
            findedItem.Title=item.Title;
        }
        else
        {
            var itemsCount=_items.Count;
            if (itemsCount == 0)
            {
                item.Id = 1;
            }
            else
            {
                item.Id=_items[itemsCount-1].Id > itemsCount?_items[itemsCount-1].Id+1:itemsCount+1;
            }
            _items.Add(item);
        }
        cache.Set(key,_items);
        return item;
    }

    public List<TodoItem> GetAllItems()
    {
        _items = cache.Get<List<TodoItem>>(key) ?? new List<TodoItem>();
      
        return _items;
    }

    public void Remove(TodoItem item)
    {
        _items = cache.Get<List<TodoItem>>(key) ?? new List<TodoItem>();
        var findedItem=_items.Find(match=>match.Id==item.Id);
        if (findedItem!=null)
        {
            _items.Remove(findedItem);
        }
        cache.Set(key,_items);
    }
}

