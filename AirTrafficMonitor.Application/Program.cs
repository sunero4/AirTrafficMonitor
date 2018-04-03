using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Controllers;
using AirTrafficMonitor.Converting;
using TransponderReceiver;

namespace AirTrafficMonitor.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var transponderDataConversion = new TransponderDataConversion(new StringToDateTimeConversion());
            var c = new TransponderDataReceivedController(TransponderReceiverFactory.CreateTransponderDataReceiver(), transponderDataConversion);
            c.StartReceivingTransponderData();
            Console.ReadLine();
        }
    }
}
