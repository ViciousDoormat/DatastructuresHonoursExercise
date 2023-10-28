using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(GenerateMunten(10)); //vul hier wat leuks in
    }
    static string GenerateMunten(int amountOfMunten, int smallestMunt = 0)
    {
        if (amountOfMunten < 0)
            throw new Exception("Amount of munten should be greater than or equal to 0");
        int[] pizza = new int[amountOfMunten];
        for (int i = 0; i < amountOfMunten; i++)
        {
            pizza[i] = smallestMunt + i;
        }
        Random random = new Random();
        return MakeMuntenString(pizza.OrderBy(x => random.Next()).ToArray(), pizza.Length - 1);
    }
    static string MakeMuntenString(int[] munten, int j, int i = 0)
    {
        if (i == j)
            return munten[i].ToString();
        int m = (i + j) / 2;
        return MakeMuntenString(munten, m, i) + " " + MakeMuntenString(munten, j, m + 1);
    }
}
