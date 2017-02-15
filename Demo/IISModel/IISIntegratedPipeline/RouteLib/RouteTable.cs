using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteLib
{
   public class RouteTable
    {

        private static List<string> StrLists = new List<string>();

        public static List<string> Strs
        {
            get
            {
                return StrLists;
            }
        }
    }
}
