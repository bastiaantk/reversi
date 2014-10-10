using System;

class Hallo2
{
    static void Main()
    {
        Console.WriteLine("Hallo wat is je naam?");
        String naam = Console.ReadLine();
        Console.WriteLine("Hallo, " + naam + "!");
        Console.WriteLine("Je naam heeft {0} letters.", naam.Length);
        Console.ReadLine();
    }
}
