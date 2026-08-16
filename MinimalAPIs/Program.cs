using System.ComponentModel;
using MinimalAPIs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Justo antes de  MappGet  y de Run  es  donde  se escribe lo que  queremos que  se ejecute
List<TodoItem> Tasks = new List<TodoItem> 
{
    new TodoItem(1, "Crear función de sumar los mayores de 20", "InProgress"),
    new TodoItem(2, "Hacer prueba de integracion",  "Complete")
};



app.MapGet("/", () => "Hello World!");
app.MapGet("/tasks", () => Tasks);
app.MapGet("/tasks/{id}", (int id) =>
{
    var tareaEncontrada = Tasks.FirstOrDefault( t => t.Id == id);
    if (tareaEncontrada is not null)
    {
        return Results.Ok(tareaEncontrada);
    } else
    {
        return Results.NotFound();
    }
    
});

app.Run();



