using System.Numerics;

using https_simulation.diffie_hellman;

BigInteger a = DiffieHellman.Generate_a();
Console.WriteLine("Hex representation of 'a'");
Console.WriteLine(a.ToString("X"));

BigInteger A = DiffieHellman.Generate_A(a);
Console.WriteLine("Hex representation of 'A'");
Console.WriteLine(A.ToString("X"));
