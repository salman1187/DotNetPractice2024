using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeLibrary
{
    public class Calculator
    {
        public int FindMax(int fno, int sno)
        {
            int max = fno;
            if (fno > sno)
            {
                max = fno;
            }
            else
            {
                max = sno;
            }

            return max;
        }

    }
}

