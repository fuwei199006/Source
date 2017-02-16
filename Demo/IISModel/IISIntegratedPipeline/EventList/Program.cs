using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventList
{
    class Program
    {
        static void Main(string[] args)
        {
            var _event = new EventClass();

            _event.OneStep += _event_OneStep;
            _event.TwoStep += _event_TwoStep;
            _event.RaiseEvent();
            Console.Read();

        }

        private static void _event_TwoStep(object sender, EventArgs e)
        {
            Console.WriteLine("TwoStep");
        }

        private static void _event_OneStep(object sender, EventArgs e)
        {
            Console.WriteLine("OnStep");
        }
    }

    public class EventClass
    {
        public EventHandlerList EventList = new EventHandlerList();
         
        public event EventHandler OneStep
        {
            add
            {
                EventList.AddHandler("oneStep",value);
            }
            remove
            {
                EventList.RemoveHandler("oneStep",value);
            }
        }

        public event EventHandler TwoStep
        {
            add
            {
                EventList.AddHandler("twoStep", value);
            }
            remove
            {
                EventList.RemoveHandler("twoStep", value);
            }
        }

        public void RaiseEvent()
        {
            var oneHandler = (EventHandler)EventList["oneStep"];
            var twoHandler = (EventHandler)EventList["twoStep"];
            oneHandler(new object(), null);
            twoHandler(new object(), null);
        }
    }
}
