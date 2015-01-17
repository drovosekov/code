using System;
using System.Security.Cryptography;
using System.Text;

namespace SelfUpdateApp.settings
{
    public static class Secure
    {
        private static readonly byte[] Entropy = Encoding.Unicode.GetBytes("a3as4dhsf%gsf1bbc_6EKey");
        private const string PassPhrase = "EncryptionKey"; /////encryption Key text 

        public static string EncryptString(this string cipherText)
        {
            byte[] cipherTextBytes = Encoding.UTF8.GetBytes(cipherText);
            byte[] protectedBytes = ProtectedData.Protect(cipherTextBytes, Entropy, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(protectedBytes);

        }

        public static string DecryptString(this string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            byte[] unProtectedBytes = ProtectedData.Unprotect(cipherTextBytes, Entropy, DataProtectionScope.CurrentUser);
            return  Encoding.UTF8.GetString(unProtectedBytes, 0, unProtectedBytes.Length); 
        }

    }
}