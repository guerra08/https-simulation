using System.Numerics;
using System.Globalization;
using https_simulation.diffie_hellman;
using https_simulation.crypto;

namespace https_simulation;

class Program
{
    /// <summary>
    /// Bruno Guerra - HTTPS simulation
    /// 
    /// Data used on program testing
    /// 
    /// g: 115740200527109164239523414760926155534485715860090261532154107313946218459149402375178179458041461723723231563839316251515439564315555249353831328479173170684416728715378198172203100328308536292821245983596065287318698169565702979765910089654821728828592422299160041156491980943427556153020487552135890973413
    /// p: 124325339146889384540494091085456630009856882741872806181731279018491820800119460022367403769795008250021191767583423221479185609066059226301250167164084041279837566626881119772675984258163062926954046545485368458404445166682380071370274810671501916789361956272226105723317679562001235501455748016154805420913
    /// a: 49554C4187504A24EE1E8ABDAD9849668DD0F409416B0BDB45DF537E5E1DEC2B008A6229EC7D3A747F0C4F5E6D
    /// A: 08C0382C6313CADC791E374A1C82A5E6B3C51058D82AD1351CF02530F21019811E6C79AFD962A9CE2A7F69DB57D83AD86EE93A3FBB6EAAD52F4CF2670E0446993367169F047AB3D71354DC7D26AC423C22F3DFEA6951591AE32C12C823A1DE0371A69EE6F7C223D63212B42D87DCE58EDC3EB3780D23F9B5FF5CB0A487C48DB33
    /// B: 1545552E00F93EC5FD047585FC71B6418D88E044211D95F88ACFF8A02AB62A7B9082A05C2435D5A7AAD1A37EBB7285410346442164A246DB13320C92DCBF9322D70028A9A0CE28E31C3D16F49E72124F0482C0BDC52B11F6DED8EF7E28FA4E8664D717372B71D813CBE834AB70CB4EE713E21171A4686873F06FE43BA3407B9D
    /// V: 18718693198531336633055881010286642666110350132910185537058137283477508378814370810835031027629215120461879604130365277090585049441594186510181758158557755168886916382964947795134299568817556980241546970231851964947235128236461444738102495749959246481757736726921442907060926028890823092113443923396729821147
    /// S: 3C0E19ACE399802769F74FE627745E25
    /// Cypher message: 7234FC91290A5BBCC6AEB1766890D8EA82A4F0F844D5E24B0CFBD23626328DEE71731B5CE38EAB9774EB44776FF49B3747AFE997964EA2456F552D25D4F856E6130F7A87AC644F598EB8041F3D4A7EC797B955DE0BED27B06D6A24FB4AF567080B83DF4EF87CC080B1691E0283C6A42E22D0C04577F321D4D494E9C092642BFD
    /// Plain text: Legal. Se deu tudo certo conseguirÃ¡s ler esta mensagem. Inverte ela, cifra, e me envia de volta.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Bruno Guerra - HTTPS Simulation");
        if (args.Length == 0)
        {
            PrintOptions();
            return;
        }
        var mode = args[0];
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
                EncryptWithAesWrapper();
                break;
            case "4":
                DecryptWithAesWrapper();
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
        Console.WriteLine("Calculating 'V' value");

        Console.WriteLine("Input the hex value of 'B': ");
        var bStr = Console.ReadLine();

        Console.WriteLine("Input the hex value of 'a': ");
        var aStr = Console.ReadLine();

        if (string.IsNullOrEmpty(aStr) || string.IsNullOrEmpty(bStr))
        {
            Console.WriteLine("Invalid inputs");
            Environment.Exit(1);
        }

        try
        {
            var b = BigInteger.Parse(bStr, NumberStyles.HexNumber);
            var a = BigInteger.Parse(aStr, NumberStyles.HexNumber);
            var v = DiffieHellman.Calculate_V(b, a);
            Console.WriteLine($"'V' value: {v}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        
    }

    /// <summary>
    /// Wrapper to generate 'A' value
    /// </summary>
    private static void GenerateAWrapper()
    {
        Console.WriteLine("Generating 'A' value");
        var a = DiffieHellman.Generate_a();
        
        Console.WriteLine("Hex representation of 'a'");
        Console.WriteLine(a.ToString("X2"));

        var aUpper = DiffieHellman.Generate_A(a);
        Console.WriteLine("Hex representation of 'A'");
        Console.WriteLine(aUpper.ToString("X2"));
    }

    /// <summary>
    /// Wrapper to generate 'S' value
    /// </summary>
    private static void CalculateSWrapper()
    {
        Console.WriteLine("Calculating 'S' value");

        Console.WriteLine("Input the BigInteger 'V' value: ");
        var vInput = Console.ReadLine();

        if (string.IsNullOrEmpty(vInput))
        {
            Console.WriteLine("Invalid input for V");
            Environment.Exit(1);
        }

        try
        {
            var v = BigInteger.Parse(vInput);
            var result = Sha.CalculateS(v);
            Console.WriteLine("Raw S: ");
            Console.WriteLine(string.Join("", result));
            Console.WriteLine("Hex representation of S (16 bytes): ");
            Console.WriteLine(string.Join("", result.Select(item => item.ToString("X2"))));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        
    }

    /// <summary>
    /// Wrapper to decrypt a message with AES
    /// </summary>
    private static void DecryptWithAesWrapper()
    {
        Console.WriteLine("Decrypting message with AES");

        Console.WriteLine("Input the cypher text in hex: ");
        var cypherText = Console.ReadLine();

        Console.WriteLine("Input the S hex key: ");
        var key = Console.ReadLine();

        if (string.IsNullOrEmpty(cypherText) || string.IsNullOrEmpty(key))
        {
            Console.WriteLine("Invalid input.");
            Environment.Exit(1);
        }

        try
        {
            var decrypted = Aes.DecryptHexStringWithAes(cypherText, key);
            Console.WriteLine("Plain text: ");
            Console.WriteLine(string.Join("", decrypted.Select(item => Convert.ToChar(item, new CultureInfo("pt-BR")))));
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
    private static void EncryptWithAesWrapper()
    {
        Console.WriteLine("Encrypting message with AES");

        Console.WriteLine("Input the text to be encrypted: ");
        var plainText = Console.ReadLine();

        Console.WriteLine("Input the S hex key: ");
        var key = Console.ReadLine();
        
        if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(key))
        {
            Console.WriteLine("Invalid input.");
            Environment.Exit(1);
        }

        try
        {
            var encrypted = Aes.EncryptPlainTextWithAes(plainText, key);
            Console.WriteLine("Encrypted text: ");
            Console.WriteLine(string.Join("", encrypted.Select(item => item.ToString("X2"))));
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

