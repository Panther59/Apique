using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLibrary
{
    public static class Encryption
    {
        public static string Encrypt(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Convert.ToBase64String(Encoding.Unicode.GetBytes(input));
            }
            else
            {
                return null;
            }
        }

        public static string Decrypt(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Encoding.Unicode.GetString(Convert.FromBase64String(input));
            }
            else
            {
                return null;
            }
        }
    }
}
