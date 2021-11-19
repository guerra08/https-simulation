using System.Security.Cryptography;
using https_simulation.util;

namespace https_simulation.crypto
{
    /// <summary>
    /// Helper class to use AES algorithm
    /// </summary>
    internal class AES
    {

        /// <summary>
        /// Decrypts a message using AES
        /// </summary>
        /// <param name="hexMessage"></param>
        /// <param name="hexKey"></param>
        /// <returns><strong>String plain text messsage</strong></returns>
        public static byte[] DecryptHexStringWithAES(string hexMessage, string hexKey)
        {
            byte[] decryptedBytes = null;
            byte[] messageBytes = Conversor.HexStringToByteArray(hexMessage);
            byte[] keyBytes = Conversor.HexStringToByteArray(hexKey);
            byte[] dataBytes = messageBytes[16..messageBytes.Length];
            byte[] IV = messageBytes[0..16];
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = keyBytes;
                aes.IV = IV;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using MemoryStream msDecrypt = new();
                using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Write))
                {
                    csDecrypt.Write(dataBytes, 0, dataBytes.Length);
                }
                decryptedBytes = msDecrypt.ToArray();
            }
            return decryptedBytes;
        }

        /// <summary>
        /// Encrypts a text using AES
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="hexKey"></param>
        /// <returns><strong>byte array</strong></returns>
        public static byte[] EncryptPlainTextWithAES(string plainText, string hexKey)
        {
            byte[] encryptedBytes;
            byte[] keyBytes = Conversor.HexStringToByteArray(hexKey);
            byte[] IV = Generator.GenerateByteArray();
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = keyBytes;
                aes.IV = IV;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream msEncrypt = new();
                using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                encryptedBytes = msEncrypt.ToArray();
            }
            return IV.Concat(encryptedBytes).ToArray();
        }

    }
}
