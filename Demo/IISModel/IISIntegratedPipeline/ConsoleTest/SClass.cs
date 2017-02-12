using System;

namespace ConsoleTest
{
    public class SClass:PClass
    {
        public override void ProcessRequest(string context)
        {
            base.ProcessRequest(context);
        }

        public override void InitFrame(string text)
        {
            Console.WriteLine("bbbbbb");
            
        }
    }
}