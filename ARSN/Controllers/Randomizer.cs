using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Controllers
{
    public static class Randomizer
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count-1;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
