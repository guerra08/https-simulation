using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace https_simulation.diffie_hellman
{
    internal class DiffieHellman
    {

        private const string _pStr = "B10B8F96 A080E01D DE92DE5E AE5D54EC 52C99FBC FB06A3C6 9A6A9DCA 52D23B61 6073E286 75A23D18 9838EF1E 2EE652C0 13ECB4AE A9061123 24975C3C D49B83BF ACCBDD7D 90C4BD70 98488E9C 219A7372 4EFFD6FA E5644738 FAA31A4F F55BCCC0 A151AF5F 0DC8B4BD 45BF37DF 365C1A65 E68CFDA7 6D4DA708 DF1FB2BC 2E4A4371";
        private const string _gStr = "A4D1CBD5 C3FD3412 6765A442 EFB99905 F8104DD2 58AC507F D6406CFF 14266D31 266FEA1E 5C41564B 777E690F 5504F213 160217B4 B01B886A 5E91547F 9E2749F4 D7FBD7D3 B9A92EE1 909D0D22 63F80A76 A6A24C08 7A091F53 1DBF0A01 69B6A28A D662A4D1 8E73AFA3 2D779D59 18D08BC8 858F4DCE F97C2A24 855E6EEB 22B3B2E5";
        private const int _aSize = 45;

        /// <summary>
        /// Generates "A" number using Diffie Hellman
        /// </summary>
        /// <returns><strong>BigInteger "A" value</strong></returns>
        public static BigInteger Generate_A()
        {
            BigInteger numericRepP = HexDigitsToNumberStr(_pStr);
            BigInteger numericRepG = HexDigitsToNumberStr(_gStr);
            BigInteger a = GenerateBigIntegerSmallerThan(_aSize);
            return BigInteger.ModPow(numericRepG, a, numericRepP);
        }

        /// <summary>
        /// Converts a space separated string of hex numbers into a BigInteger representation
        /// </summary>
        /// <param name="hexDigits">String of hex numbers separated by spaces</param>
        /// <returns><strong>BigInteger representation</strong></returns>
        private static BigInteger HexDigitsToNumberStr(string hexDigits)
        {
            return BigInteger.Parse(String.Join("", hexDigits.Split(" ").Select(item => long.Parse(item, System.Globalization.NumberStyles.HexNumber))));
        }

        /// <summary>
        /// Generates a BigInteger with a length smaller than a given int number
        /// </summary>
        /// <param name="max">Int max length</param>
        /// <returns><strong>BigInteger generated number</strong></returns>
        private static BigInteger GenerateBigIntegerSmallerThan(int max)
        {
            Random random = new Random();
            byte[] data = new byte[max];
            random.NextBytes(data);
            return new BigInteger(data, true);
        }
    }
}
