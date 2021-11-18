using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace https_simulation.util
{
    internal class Conversor
    {
        /// <summary>
        /// Converts a hex string into a byte array
        /// </summary>
        /// <param name="input"></param>
        /// <returns><strong>byte array</strong></returns>
        public static byte[] HexStringToByteArray(string input)
        {
            return Enumerable
                .Range(0, input.Length / 2)
                .Select(x => Convert.ToByte(input.Substring(x * 2, 2), 16))
                .ToArray();
        }

    }
}
