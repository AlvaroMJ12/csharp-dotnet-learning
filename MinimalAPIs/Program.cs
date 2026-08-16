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


// Endpoint GET
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

// Endpoit POST
app.MapPost("/tasks", (TodoItem nuevaTarea) =>
{
   Tasks.Add(nuevaTarea);

    return Results.Created($"/tasks/{nuevaTarea.Id}", nuevaTarea);

});

// Endpoint DELETE
app.MapDelete("/tasks/{id}", (int id)=>{
    var tareaEncontrada = Tasks.FirstOrDefault(t => t.Id == id);
    if(tareaEncontrada is null)
    {
        return Results.NotFound("No se ha encontrado la tarea");
    }
    else
    {
        Tasks.Remove(tareaEncontrada);
        return Results.NoContent();
    }
});

// Endpont UPDATE

app.MapPut("/tasks/{id}", (int id, TodoItem tareaActualizada)=>{
    var tareaEncontrada = Tasks.FirstOrDefault(t=> t.Id == id);
    if(tareaEncontrada is null)
    {
        return Results.NotFound("Tarea no encontrada");
    }
    else
    {
        tareaEncontrada.Description = tareaActualizada.Description;
        tareaEncontrada.State = tareaActualizada.State;

        return Results.NoContent();
    }
});
app.Run();



