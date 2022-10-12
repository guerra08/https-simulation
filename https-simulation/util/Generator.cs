using System.Numerics;

namespace https_simulation.util;

/// <summary>
/// Class to help with the generation of specific values and structures
/// </summary>
public static class Generator
{
    /// <summary>
    /// Generates a random byte array
    /// </summary>
    /// <param name="size"></param>
    /// <returns><strong>byte array</strong></returns>
    public static byte[] GenerateByteArray(int size = 16)
    {
        var rnd = new Random();
        var b = new byte[size];
        rnd.NextBytes(b);
        return b;
    }

    /// <summary>
    /// Generates a BigInteger with a length smaller than a given int number
    /// </summary>
    /// <param name="max">Int max length</param>
    /// <returns><strong>BigInteger generated number</strong></returns>
    public static BigInteger GenerateBigIntegerSmallerThan(int max)
    {
        var random = new Random();
        var data = new byte[max];
        random.NextBytes(data);
        return new BigInteger(data, true);
    }

}