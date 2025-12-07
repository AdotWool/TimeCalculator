using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCalculator
{
    public struct timeStream
    {
        public int time10, time01, time20, time02;
        public string ampm1, ampm2;
        public byte operation;
        public bool mode;
        public timeStream(int time10, int time01, string ampm1, int time20, int time02, string ampm2, byte operation, bool mode)
        {
            this.time10 = time10;
            this.time01 = time01;
            this.time20 = time20;
            this.time02 = time02;
            this.ampm1 = ampm1;
            this.ampm2 = ampm2;
            this.operation = operation;
            this.mode = mode;
        }
    }
}
