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
            byte[] IV = strBytes.Take(16).ToArray();
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = IV;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(strBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
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
