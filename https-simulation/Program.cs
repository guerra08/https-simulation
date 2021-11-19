﻿using System.Numerics;
using System.Globalization;
using https_simulation.diffie_hellman;
using https_simulation.crypto;

class Program
{
    /// <summary>
    /// Bruno Guerra - HTTPS simulation
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
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
            case "3":
                EncryptWithAESWrapper();
                break;
            case "4":
                DecryptWithAESWrapper();
                break;
            default:
                Console.WriteLine("Invalid option.");
                PrintOptions();
                break;
        }
    }

    /// <summary>
    /// Wrapper to calculate the 'V' value
    /// </summary>
    private static void CalculateVWrapper()
    {
        try
        {
            Console.WriteLine("Calculating 'V' value");
            Console.WriteLine("Input the hex value of 'B': ");
            BigInteger B = BigInteger.Parse(Console.ReadLine(), NumberStyles.HexNumber);
            Console.WriteLine("Input the hex value of 'a': ");
            BigInteger a = BigInteger.Parse(Console.ReadLine(), NumberStyles.HexNumber);
            BigInteger V = DiffieHellman.Calculate_V(B, a);
            Console.WriteLine("'V' value: ");
            Console.WriteLine(V);
        } catch (Exception ex)
        {
            Console.WriteLine("Error when calculating 'V' value.");
            Console.WriteLine(ex.Message);
        }
        
    }

    /// <summary>
    /// Wrapper to generate 'A' value
    /// </summary>
    private static void GenerateAWrapper()
    {
        Console.WriteLine("Generating 'A' value");
        BigInteger a = DiffieHellman.Generate_a();
        Console.WriteLine("Hex representation of 'a'");
        Console.WriteLine(a.ToString("X2"));

        BigInteger A = DiffieHellman.Generate_A(a);
        Console.WriteLine("Hex representation of 'A'");
        Console.WriteLine(A.ToString("X2"));
    }

    /// <summary>
    /// Wrapper to generate 'S' value
    /// </summary>
    private static void CalculateSWrapper()
    {
        Console.WriteLine("Calculating 'S' value");
        Console.WriteLine("Input the BigInteger 'V' value: ");
        string VInput = Console.ReadLine();
        if (String.IsNullOrEmpty(VInput)) 
        {
            Console.WriteLine("The value for 'V' cannot be empty");
            Environment.Exit(1);
        }
        else
        {
            try
            {
                BigInteger V = BigInteger.Parse(VInput);
                byte[] result = SHA.CalculateS(V);
                Console.WriteLine("Raw S: ");
                Console.WriteLine(String.Join("", result));
                Console.WriteLine("Hex representation of S (16 bytes): ");
                Console.WriteLine(String.Join("", result.Select(item => item.ToString("X2"))));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }
        
    }

    /// <summary>
    /// Wrapper to decrypt a message with AES
    /// </summary>
    private static void DecryptWithAESWrapper()
    {
        Console.WriteLine("Decrypting message with AES");
        Console.WriteLine("Input the cypher text in hex: ");
        string cypherText = Console.ReadLine();
        if(String.IsNullOrEmpty(cypherText))
        {
            Console.WriteLine("The cypher text cannot be empty");
            Environment.Exit(1);
        }
        Console.WriteLine("Input the S hex key: ");
        string key = Console.ReadLine();
        if (String.IsNullOrEmpty(key))
        {
            Console.WriteLine("The key cannot be empty");
            Environment.Exit(1);
        }
        try
        {
            byte[] decrypted = AES.DecryptHexStringWithAES(cypherText, key);
            Console.WriteLine("Plain text: ");
            Console.WriteLine(String.Join("", decrypted.Select(item => Convert.ToChar(item, new CultureInfo("pt-BR")))));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        
    }

    /// <summary>
    /// Wrapper to decrypt a message with AES
    /// </summary>
    private static void EncryptWithAESWrapper()
    {
        Console.WriteLine("Encrypting message with AES");
        Console.WriteLine("Input the text to be encrypted: ");
        string plainText = Console.ReadLine();
        if (String.IsNullOrEmpty(plainText))
        {
            Console.WriteLine("The text cannot be empty");
            Environment.Exit(1);
        }
        Console.WriteLine("Input the S hex key: ");
        string key = Console.ReadLine();
        if (String.IsNullOrEmpty(key))
        {
            Console.WriteLine("The hex key cannot be empty");
            Environment.Exit(1);
        }
        try
        {
            byte[] encrypted = AES.EncryptPlainTextWithAES(plainText, key);
            Console.WriteLine("Encrypted text: ");
            Console.WriteLine(String.Join("", encrypted.Select(item => item.ToString("X2"))));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        
    }

    /// <summary>
    /// Prints the program options
    /// </summary>
    private static void PrintOptions()
    {
        Console.WriteLine("Use one of the following number as argument to execute an action: ");
        Console.WriteLine("0 - Generate 'A' value");
        Console.WriteLine("1 - Calculate 'V' value");
        Console.WriteLine("2 - Calculate 'S' value");
        Console.WriteLine("3 - Encrypt with AES");
        Console.WriteLine("4 - Decrypt with AES");
    }

}

