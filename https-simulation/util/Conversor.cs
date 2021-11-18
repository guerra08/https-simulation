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
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            using (var sr = new StringReader(input))
            {
                for (var i = 0; i < outputLength; i++)
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return output;
        }

    }
}
