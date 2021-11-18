using System.Numerics;

using https_simulation.diffie_hellman;
using https_simulation.crypto;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("HTTPS Simulation - Bruno Guerra");
        if (args.Length == 0)
        {
            PrintOptions();
            return;
        }
        string mode = args[0];
        switch (mode)
        {
            case "0":
                GenerateAWrapper();
                break;
            case "1":
                CalculateVWrapper();
                break;
            case "2":
                CalculateSWrapper();
                break;
            default:
                Console.WriteLine("Invalid option.");
                PrintOptions();
                break;
        }
    }

    private static void CalculateVWrapper()
    {
        try
        {
            Console.WriteLine("Input the hex value of 'B': ");
            BigInteger B = BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Input the hex value of 'a': ");
            BigInteger a = BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            BigInteger V = DiffieHellman.Calculate_V(B, a);
            Console.WriteLine("'V' value: ");
            Console.WriteLine(V);
        } catch (Exception ex)
        {
            Console.WriteLine("Error when calculating 'V' value.");
            Console.WriteLine(ex.Message);
        }
        
    }

    private static void GenerateAWrapper()
    {
        BigInteger a = DiffieHellman.Generate_a();
        Console.WriteLine("Hex representation of 'a'");
        Console.WriteLine(a.ToString("X"));

        BigInteger A = DiffieHellman.Generate_A(a);
        Console.WriteLine("Hex representation of 'A'");
        Console.WriteLine(A.ToString("X"));
    }

    private static void CalculateSWrapper()
    {
        Console.WriteLine("Input the BigInteger 'V' value: ");
        BigInteger V = BigInteger.Parse(Console.ReadLine());
        byte[] result = SHA.CalculateS(V);
        Console.WriteLine("Hex representation of S (16 bytes): ");
        Console.WriteLine(String.Join("", result.Select(item => item.ToString("X")).ToArray()));
    }

    private static void PrintOptions()
    {
        Console.WriteLine("Use one of the following number as argument to execute an action: ");
        Console.WriteLine("0 - Generate 'A' value");
        Console.WriteLine("1 - Calculate 'V' value");
        Console.WriteLine("2 - Calculate 'S' value");
    }

}

