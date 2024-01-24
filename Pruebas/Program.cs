using System.Text.RegularExpressions;
using TrucoJuego;

internal class Program
{
    private static void Main(string[] args)
    {
        //Prueba programa = new Prueba();
        //programa.B();

        //Console.ReadLine(); 
        //////////////////////////////////////////////////////////////////////////////////

        //Carta c = new Carta();
        //c.DefinirCarta();
        //Console.WriteLine(c.CartaActual.ToString());
        string a = "10 DE BASTO";
        string b = Regex.Match(a, "ORO").ToString();
        Console.WriteLine(Regex.Match(a, "[0-9]"));
        //Console.WriteLine(b);
        //Regex.Match(a, "10");
    }
}

public class Prueba
{
    public async Task B()
    {
        bool b = await A(); // Esto hace que espere a que se complete la tarea
                            // para recibir el retorno
        await Task.Delay(2000);
        Console.WriteLine($"{b}");
    }
    public async Task<bool> A()
    {
        await Task.Delay(2000); // frena el flujo por completo
        Console.WriteLine("hola");
        return true;
    }
}