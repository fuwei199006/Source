using System;

namespace ConsoleTest
{
    public class PClass
    {
        public virtual void ProcessRequest(string context)
        {
            this.InitFrame("aaaaa");
        }


        public virtual void InitFrame(string text)
        {
            Console.WriteLine(text);
        }

    }
}