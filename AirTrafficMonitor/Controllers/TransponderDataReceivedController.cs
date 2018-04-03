using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Converting;
using TransponderReceiver;

namespace AirTrafficMonitor.Controllers
{
    public class TransponderDataReceivedController
    {
        private readonly ITransponderReceiver _transponderReceiver;
        private readonly ITransponderDataConversion _transponderDataConversion;

        public TransponderDataReceivedController(ITransponderReceiver transponderReceiver, ITransponderDataConversion transponderDataConversion)
        {
            _transponderReceiver = transponderReceiver;
            _transponderDataConversion = transponderDataConversion;
        }
        public void StartReceivingTransponderData()
        {
            _transponderReceiver.TransponderDataReady += OnTransponderDataReady;
        }

        public void StopReceivingTransponderData()
        {
            _transponderReceiver.TransponderDataReady -= OnTransponderDataReady;
        }

        private void OnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                var convertedData = _transponderDataConversion.ConvertData(data);
                Console.WriteLine($"Tag: {convertedData.Tag}, X: {convertedData.Position.X}, Y: {convertedData.Position.Y}");
            }
        }
    }
}
