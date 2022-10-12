using System.Numerics;
using System.Security.Cryptography;

namespace https_simulation.crypto;

/// <summary>
/// SHA helper class
/// </summary>
public static class Sha
{
    /// <summary>
    /// Calculates 'S' using SHA256 and returns the first n bytes
    /// </summary>
    /// <param name="v">V value</param>
    /// <param name="takeSize">int size to be taken</param>
    /// <returns><strong>byte array of computed bytes</strong></returns>
    public static byte[] CalculateS(BigInteger v, int takeSize = 16)
    {
        var sha256 = SHA256.Create();
        var vByteArray = v.ToByteArray(false, true);
        var hashArray = sha256.ComputeHash(vByteArray).ToArray();
        return hashArray[0..takeSize];
    }

}