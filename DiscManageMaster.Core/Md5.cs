using System;
using System.Security.Cryptography;
using System.Text;

namespace DiscManageMaster.Core
{
    /// <summary>
    /// Summary description for MD5.
    /// </summary>
    public class MD5
    {
        public static string Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            int i;
			
            for (i=0;i<=15;i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}