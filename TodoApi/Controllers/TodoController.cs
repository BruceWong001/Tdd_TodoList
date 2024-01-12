using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TodoApi;

namespace TodoApi.Controllers;

public class TodoDTO
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Title { get; set; }
}
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly ILogger<TodoController> _logger;
    private  IRepository _repository;
    private ItemCollection _itemCollection;

    public TodoController(IRepository repository, IMapper mapper, ILogger<TodoController> logger)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _itemCollection = new ItemCollection(_repository);
    }

    [HttpGet("items")]
    public List<TodoDTO> Get()
    {
        var items = _itemCollection.GetAllItems();
        var ret=items.Select(item=>_mapper.Map<TodoDTO>(item)).ToList();
        return ret;
    }
    [HttpPost("item")]
    public TodoDTO Post(TodoDTO item)
    {
        var newItem = _itemCollection.NewItem();
        newItem.Parse(_mapper.Map<TodoItem>(item)); 
        newItem.Save();
        return _mapper.Map<TodoDTO>(newItem);
    }

}
