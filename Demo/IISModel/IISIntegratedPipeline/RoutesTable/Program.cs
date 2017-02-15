using RouteLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutesTable
{
    class Program
    {
        static void Main(string[] args)
        {
            RouteTable.Strs.Add("12312312");
            RouteTable.Strs.Add("1231");
            RouteTable.Strs.Add("12");
            TestConsole.Print();

            Console.Read();
        }
    }
}
