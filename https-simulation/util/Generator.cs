using System.Numerics;
namespace https_simulation.util
{
    internal class Generator
    {
        /// <summary>
        /// Generates a random byte array
        /// </summary>
        /// <param name="size"></param>
        /// <returns><strong>byte array</strong></returns>
        public static byte[] GenerateByteArray(int size = 16)
        {
            Random rnd = new Random();
            byte[] b = new byte[size];
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
            Random random = new Random();
            byte[] data = new byte[max];
            random.NextBytes(data);
            return new BigInteger(data, true);
        }

    }
}
