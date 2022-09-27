﻿using System.Security.Cryptography;
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
            var messageBytes = Conversor.HexStringToByteArray(hexMessage);
            var keyBytes = Conversor.HexStringToByteArray(hexKey);
            var dataBytes = messageBytes[16..messageBytes.Length];
            var iv = messageBytes[..16];
            
            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = keyBytes;
            aes.IV = iv;
            
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream msDecrypt = new();
            using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Write))
            {
                csDecrypt.Write(dataBytes, 0, dataBytes.Length);
            }
            return msDecrypt.ToArray();
        }

        /// <summary>
        /// Encrypts a text using AES
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="hexKey"></param>
        /// <returns><strong>byte array</strong></returns>
        public static byte[] EncryptPlainTextWithAES(string plainText, string hexKey)
        {
            var keyBytes = Conversor.HexStringToByteArray(hexKey);
            var iv = Generator.GenerateByteArray();
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = keyBytes;
                aes.IV = iv;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream msEncrypt = new();
                using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                return iv.Concat(msEncrypt.ToArray()).ToArray();
            }
        }

    }
}
