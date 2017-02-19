using RouteLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoutesTable
{
    class Program
    {
        delegate string MyDelegate(string text);
        static void Main(string[] args)
        {
            ////var method = typeof (MyClass).GetMethod("Print");
            var a = typeof (MyClass).Assembly;
            var type = a.GetType("RoutesTable.MyClass");
            MyDelegate initObj = Delegate.CreateDelegate(typeof(MyDelegate), type, "Print") as MyDelegate;
 
            var res=initObj("addd");
            Console.WriteLine(res);
            Console.Read();


        }
    }
}
