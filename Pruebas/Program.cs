using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        B();
    }
    private static async Task B()
    {

        await A();
        Console.WriteLine("chau");
    }
    private static async Task<bool> A()
    {
        await Task.Delay(2000);
        Console.WriteLine("hola");
        return true;
    }
}