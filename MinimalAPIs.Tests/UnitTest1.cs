namespace MinimalAPIs.Tests;
using MinimalAPIs;

public class UnitTest1
{
    [Fact]
    public void BuscarTareaPorId_CuandoExiste_DebeRetornarLaTareaCorrecta()
    {
        // 1. ARRANGE (Preparar)
        var listaDePrueba = new List<TodoItem>
        {
            new TodoItem(1, "Crear función de sumar los mayores de 20", "InProgress"),
            new TodoItem(2, "Hacer prueba de integracion", "Complete")
        };
        int idBuscado = 2;

        // 2. ACT (Ejecutar)
        var resultado = listaDePrueba.FirstOrDefault(t => t.Id == idBuscado);

        // 3. ASSERT (Verificar)
        Assert.NotNull(resultado);
        Assert.Equal(idBuscado, resultado.Id);
        Assert.Equal("Hacer prueba de integracion", resultado.Description);
    }

    [Fact]
    public void BuscarTareaPorId_CuandoNoExiste_DebeRetornarNulo()
    {
        // 1. ARRANGE (Preparar)
        var listaDePrueba = new List<TodoItem>
        {
            new TodoItem(1, "Crear función de sumar los mayores de 20", "InProgress"),
            new TodoItem(2, "Hacer prueba de integracion", "Complete")
        };
        int idBuscado = 99;

        // 2. ACT (Ejecutar)
        var resultado = listaDePrueba.FirstOrDefault(t=>t.Id == idBuscado);

        // 3. ASSERT (Verificar)
        Assert.Null(resultado);
    }
}