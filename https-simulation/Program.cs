using System.Numerics;

using https_simulation.diffie_hellman;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("HTTPS Simulation - Bruno Guerra");
        if (args.Length == 0) return;
        string mode = args[0];
        switch (mode)
        {
            case "0":
                WrapperGenerateA();
                break;
            case "1":
                WrapperCalculateV();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }

        
    }

    private static void WrapperCalculateV()
    {
        try
        {
            Console.WriteLine("Input the hex value of 'B': ");
            BigInteger B = BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Input the value of 'a': ");
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

    private static void WrapperGenerateA()
    {
        BigInteger a = DiffieHellman.Generate_a();
        Console.WriteLine("Hex representation of 'a'");
        Console.WriteLine(a.ToString("X"));

        BigInteger A = DiffieHellman.Generate_A(a);
        Console.WriteLine("Hex representation of 'A'");
        Console.WriteLine(A.ToString("X"));
    }

}

