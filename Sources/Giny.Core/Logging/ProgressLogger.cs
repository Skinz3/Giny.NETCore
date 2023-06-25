using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Logging
{
    public class ProgressLogger
    {
        const char Block = '■';
        const string Back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";

        private int PreviousValue
        {
            get;
            set;
        }
        public void Flush()
        {
            Console.Write(Back);
        }
        public void WriteProgressBar(int current, int max)
        {
            var ratio = current / (double)max;
            var percent = (int)Math.Ceiling(ratio * 100);

            if (percent <= PreviousValue)
            {
                return;
            }

            PreviousValue = percent;

            Console.Write(Back);

            Console.Write("[");
            var p = (int)((percent / 10f) + .5f);
            for (var i = 0; i < 10; ++i)
            {
                if (i >= p)
                    Console.Write(' ');
                else
                    Console.Write(Block);
            }
            Console.Write("] {0,3:##0}%", percent);
        }
    }

}
