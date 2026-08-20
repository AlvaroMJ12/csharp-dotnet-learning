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
app.MapGet("/tasks", async(TodoDbContext context) => await context.TodoItems.ToListAsync());
// Obtenemos el de el id que queremos
app.MapGet("/tasks/{id}", async(int id, TodoDbContext context) =>
{
    var tareaEncontrada = await context.TodoItems.FirstOrDefaultAsync( t => t.Id == id);
    if (tareaEncontrada is not null)
    {
        return Results.Ok(tareaEncontrada);
    } else
    {
        return Results.NotFound();
    }
    
});

// Endpoit POST
app.MapPost("/tasks", async(TodoItem nuevaTarea, TodoDbContext context) =>
{
    await context.TodoItems.AddAsync(nuevaTarea);
    await context.SaveChangesAsync();

    return Results.Created($"/tasks/{nuevaTarea.Id}", nuevaTarea);

});

// Endpoint DELETE
app.MapDelete("/tasks/{id}", async(int id, TodoDbContext context)=>{
    var tareaEncontrada = await context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
    if(tareaEncontrada is null)
    {
        return Results.NotFound("No se ha encontrado la tarea");
    }
    else
    {
        context.TodoItems.Remove(tareaEncontrada);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }
});

// Endpont UPDATE

app.MapPut("/tasks/{id}", async(int id, TodoItem tareaActualizada, TodoDbContext context)=>{
    var tareaEncontrada = await context.TodoItems.FirstOrDefaultAsync(t=> t.Id == id);
    if(tareaEncontrada is null)
    {
        return Results.NotFound("Tarea no encontrada");
    }
    else
    {
        tareaEncontrada.Description = tareaActualizada.Description;
        tareaEncontrada.State = tareaActualizada.State;
        await context.SaveChangesAsync();

        return Results.NoContent();
    }
});
app.Run();



