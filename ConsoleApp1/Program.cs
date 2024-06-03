// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

for (int i = 1; i<4; i++)
{
    double result = Math.Pow(5, i);
    Console.WriteLine($"{i} -> Ket qua: {result}");
}

Console.ReadKey();