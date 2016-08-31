﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPbkdf2Sha512
{
    class GenerateComparableHash
    {
        public static string StandardiseBase64(string tofix)
        {
            return tofix.Replace('.', '+') + "==";
        }

        public static string DestandardiseBase64(string standard)
        {
            return standard.Replace('+', '.').Substring(0, standard.Length - 2);
        } 

        public static string Generate64BitHash(string password, string salt, int iterations)
        {
            using (var prf = new HMACSHA512PseudoRandomFunction(System.Text.Encoding.UTF8.GetBytes(password)))
            {
                var saltbytes = Convert.FromBase64String(salt);

                using (var nh = new PBKDF2DeriveBytes(prf, saltbytes, iterations))
                {
                    var bytes = nh.GetBytes(64);
                    var encoded = Convert.ToBase64String(bytes);
                    return encoded;
                }
            }
        }
    }
}