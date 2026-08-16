namespace HERENCIA_ETC;

public class Persona
{
    //Encapsulacion
    public string Nombre {get; set;}
    public string Sexo {get; set;}
    public int Edad {get; set;}
    public string DNI{get; private set;}

    //Abstraccion
    public Persona(string nombre, string sexo, int edad, string dni)
    {
        Nombre = nombre;
        Sexo = sexo;
        Edad = edad;
        DNI = dni;
    }

    //Polimorfismo
    public virtual void Presentarse()
    {
        Console.WriteLine($"Hola, me llamo {Nombre} y tengo {Edad} años");
    }
    
}