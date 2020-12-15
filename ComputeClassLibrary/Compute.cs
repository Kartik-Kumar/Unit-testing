using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeClassLibrary
{
    public class Compute
    {
        public int FindSum(int a, int b)
        {
            return a + b;
        }

        public bool IsPrime(int a)
        {
            
            for(int i = 2; i <= Math.Sqrt(a); i++)
            {
                if (a % i == 0) return false;
            }

            return true;
        }
    }
}
