namespace HERENCIA_ETC;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Persona persona1 = new Persona("Alvaro", "Hombre", 25, "49790296B");
        Estudiante estudiante1 = new Estudiante("Manuel", "Hombre", 23, "49790376C", "DAW");

        Console.WriteLine("Comenzamos programa y llamamos a los objetos");

        persona1.Presentarse();
        estudiante1.Presentarse();

        Console.WriteLine("Llamamos a la lista y los presentamos");

        // Creamos una lista
        List<Persona> genteMayorDeEdad = new List<Persona>();
        genteMayorDeEdad.Add(persona1); 
        genteMayorDeEdad.Add(estudiante1);

        foreach(Persona p in genteMayorDeEdad)
        {
            p.Presentarse();
        }

        Console.WriteLine("Ahora vamos con los diccionarios");

        //Creamos un diccionario
        Dictionary<string, Persona> registroPersonas = new Dictionary<string, Persona>();
        registroPersonas.Add(persona1.DNI, persona1);
        registroPersonas.Add(estudiante1.DNI, estudiante1);

        Persona personaDniBuscado = registroPersonas["49790296B"];
        personaDniBuscado.Presentarse();
    }
    
}
