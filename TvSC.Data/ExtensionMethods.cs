﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TvSC.Data
{
    public static class ExtensionMethods
    {
        public static string ToHash(this string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
