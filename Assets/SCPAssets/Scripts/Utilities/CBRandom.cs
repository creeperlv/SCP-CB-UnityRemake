using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
namespace SCPCB.Utilities
{
    public class CBRandom
    {
        static int randomSeed=int.MinValue;
        static System.Random random=new System.Random();
        public static void SetSeed(string str)
        {
            SHA1 sha1 = SHA1.Create();
            var h=sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            foreach (var item in h)
            {
                randomSeed += item;   
            }
            random = new System.Random(randomSeed);
        }
        public static int NextInt(int LowBoundary, int HighBoundary) => random.Next(LowBoundary, HighBoundary);
        public static double NextDouble() => random.NextDouble();
        public static void NextBytes(byte[]buffer) => random.NextBytes(buffer);
    }

}