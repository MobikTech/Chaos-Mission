using System;
using System.Collections.Generic;

namespace ChaosMission.Extensions.CSharpExtensions
{
    public static class ListExtensions
    {
        private static readonly Random s_rnd = new Random();  

        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = s_rnd.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }
    }
}