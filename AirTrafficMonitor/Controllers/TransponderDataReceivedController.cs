using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Logging;
using TransponderReceiver;

namespace AirTrafficMonitor.Controllers
{
    public class TransponderDataReceivedController
    {
        private readonly ITransponderReceiver _transponderReceiver;
        private readonly ITransponderDataConversion _transponderDataConversion;
        private readonly ITrackLogging _trackLogging;

        public TransponderDataReceivedController(ITransponderReceiver transponderReceiver, ITransponderDataConversion transponderDataConversion, ITrackLogging trackLogging)
        {
            _transponderReceiver = transponderReceiver;
            _transponderDataConversion = transponderDataConversion;
            _trackLogging = trackLogging;
        }
        public void StartReceivingTransponderData()
        {
            _transponderReceiver.TransponderDataReady += OnTransponderDataReady;
        }

        public void StopReceivingTransponderData()
        {
            _transponderReceiver.TransponderDataReady -= OnTransponderDataReady;
        }

        public void OnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                var track = _transponderDataConversion.ConvertData(data);
                _trackLogging.LogTrack(track);
            }
        }
    }
}
