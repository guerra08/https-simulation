using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace https_simulation.crypto
{
    internal class AES
    {

        /// <summary>
        /// Decrypts a 
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="hexKey"></param>
        /// <returns></returns>
        public static string DecryptHexStringWithAES(string hexString, string hexKey)
        {
            string plainText = null;
            byte[] strBytes = Enumerable.Range(0, hexString.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                     .ToArray();
            byte[] keyBytes = Enumerable.Range(0, hexKey.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hexKey.Substring(x, 2), 16))
                     .ToArray();
            byte[] cypherText = strBytes[16..^0];
            byte[] IV = strBytes[0..16];
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = keyBytes;
                aes.IV = IV;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream msDecrypt = new (cypherText))
                {
                    using (CryptoStream csDecrypt = new (msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new (csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plainText;
        }

    }
}
