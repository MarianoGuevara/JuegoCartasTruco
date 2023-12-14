using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        int a = 0;

        switch (a)
        {
            case 0:
            case 3:
                Console.WriteLine("0 o 3");
                break;
            case 1:
                Console.WriteLine("1");
                break;
            case 2:
                Console.WriteLine("2");
                break;
        }
    }
}