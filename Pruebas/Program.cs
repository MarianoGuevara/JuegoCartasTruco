using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        string carta = "1 ORO";
        Console.WriteLine(Regex.IsMatch(carta, @"\b1 ESPADA"));
    }
}