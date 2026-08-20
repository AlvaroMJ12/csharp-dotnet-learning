using System.ComponentModel;
using MinimalAPIs;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

// Contenedor de dependencias para que cualquier endpoint que declare TodoDbContext context como parámetro recibirá la base de datos automáticamente
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data Source=todo.db"));

var app = builder.Build();

/* Justo antes de  MappGet  y de Run  es  donde  se escribe lo que  queremos que  se ejecute
List<TodoItem> Tasks = new List<TodoItem> 
{
    new TodoItem(1, "Crear función de sumar los mayores de 20", "InProgress"),
    new TodoItem(2, "Hacer prueba de integracion",  "Complete")
};
*/


// Endpoint GET
app.MapGet("/", () => "Hello World!");
// Obtenemos toda la lista de la bd
app.MapGet("/tasks", (TodoDbContext context) => context.TodoItems.ToList());
// Obtenemos el de el id que queremos
app.MapGet("/tasks/{id}", (int id, TodoDbContext context) =>
{
    var tareaEncontrada = context.TodoItems.FirstOrDefault( t => t.Id == id);
    if (tareaEncontrada is not null)
    {
        return Results.Ok(tareaEncontrada);
    } else
    {
        return Results.NotFound();
    }
    
});

// Endpoit POST
app.MapPost("/tasks", (TodoItem nuevaTarea, TodoDbContext context) =>
{
    context.TodoItems.Add(nuevaTarea);
    context.SaveChanges();

    return Results.Created($"/tasks/{nuevaTarea.Id}", nuevaTarea);

});

// Endpoint DELETE
app.MapDelete("/tasks/{id}", (int id, TodoDbContext context)=>{
    var tareaEncontrada = context.TodoItems.FirstOrDefault(t => t.Id == id);
    if(tareaEncontrada is null)
    {
        return Results.NotFound("No se ha encontrado la tarea");
    }
    else
    {
        context.TodoItems.Remove(tareaEncontrada);
        context.SaveChanges();
        return Results.NoContent();
    }
});

// Endpont UPDATE

app.MapPut("/tasks/{id}", (int id, TodoItem tareaActualizada, TodoDbContext context)=>{
    var tareaEncontrada = context.TodoItems.FirstOrDefault(t=> t.Id == id);
    if(tareaEncontrada is null)
    {
        return Results.NotFound("Tarea no encontrada");
    }
    else
    {
        tareaEncontrada.Description = tareaActualizada.Description;
        tareaEncontrada.State = tareaActualizada.State;
        context.SaveChanges();

        return Results.NoContent();
    }
});
app.Run();



