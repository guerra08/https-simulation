using System.Numerics;
using System.Security.Cryptography;

namespace https_simulation.crypto
{
    internal class SHA
    {
        public static byte[] CalculateS(BigInteger V)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] result = sha256.ComputeHash(V.ToByteArray());
            return result;
        }

    }
}
