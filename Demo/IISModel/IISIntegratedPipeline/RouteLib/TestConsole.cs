using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteLib
{
    public class TestConsole
    {
        public static void Print()
        {
            foreach (var item in RouteTable.Strs)
            {
                Console.WriteLine(item);
            }
        }
    }
}
