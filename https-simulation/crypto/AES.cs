using System.Security.Cryptography;
using System.Text;
using https_simulation.util;

namespace https_simulation.crypto
{
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
                using MemoryStream msDecrypt = new(dataBytes);
                using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Write))
                {
                    csDecrypt.Write(dataBytes, 0, dataBytes.Length);
                }
                decryptedBytes = msDecrypt.ToArray();
            }
            return decryptedBytes;
        }

        public static byte[] EncryptPlainTextWithAES(string plainText, string hexKey)
        {
            byte[] encryptedBytes = null;
            byte[] messageBytes = Encoding.ASCII.GetBytes(plainText);
            byte[] keyBytes = Conversor.HexStringToByteArray(hexKey);
            byte[] IV = Generator.GenerateByteArray();
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = keyBytes;
                aes.IV = IV;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream msEncrypt = new(messageBytes);
                using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(messageBytes, 0, messageBytes.Length);
                }
                encryptedBytes = msEncrypt.ToArray();
            }
            return encryptedBytes;
        }

    }
}
