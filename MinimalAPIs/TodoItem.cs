namespace MinimalAPIs;
public class TodoItem
{
    public int Id {get; set;}
    public string? Description {get; set;}
    public string? State {get; set;}

    // Construtor vacio
    public TodoItem()
    {
        
    }

    // Constructor
    public TodoItem(int id, string description, string state)
    {
        Id = id;
        Description = description;
        State = state;
    }
}