namespace HERENCIA_ETC;

public class Estudiante : Persona //Herencia
{
    public string Carrera{get; set;}

    public Estudiante(string nombre, string sexo, int edad, string dni, string carrera) : base(nombre, sexo, edad, dni)
    {
        Carrera = carrera;    
    }

    // Herencia
    public override void Presentarse()
    {
        Console.WriteLine($"Hola, me llamo {Nombre}, tengo {Edad} años y estudio  {Carrera}");
    }
}