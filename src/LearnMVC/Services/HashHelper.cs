using System;
using System.Security.Cryptography;
using System.Text;

namespace LearnMVC.Services
{
    public static class HashHelper
    {
        public static string GetMD5Hash(this string strToHash)
        {
            if (strToHash == null)
                throw new ArgumentNullException("strToHash");

            string hashStr = string.Empty;
            foreach (byte b in new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(strToHash)))
            {
                hashStr += b.ToString("X2");
            }

            return hashStr;
        }
    }
}