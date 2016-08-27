using Effortless.Net.Encryption;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication.BaseClassWebApplication
{
    public class EnDecryption
    {

        string key = ConfigurationManager.AppSettings["EnDeCryptionKey"];
        string password = ConfigurationManager.AppSettings["EnDeCryptionPassword"];
 
        public string Encryption(string valueEncrypt)
        {
            string encrypted = Strings.Encrypt(valueEncrypt, password, key, string.Empty.PadLeft(32, '$'), Bytes.KeySize.Size256);
            return encrypted;
        }
        public string Decryption(string valueDecrypte)
        {
            string decrypted = Strings.Decrypt(valueDecrypte, password, key, string.Empty.PadLeft(32, '$'), Bytes.KeySize.Size256);
            return decrypted;
        }
    }
}