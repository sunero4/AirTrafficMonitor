using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Logging;
using TransponderReceiver;

namespace AirTrafficMonitor.Controllers
{
    public class TransponderDataReceivedController
    {
        private readonly ITransponderReceiver _transponderReceiver;
        private readonly ITransponderDataConversion _transponderDataConversion;
        private readonly ITrackLogging _trackLogging;
        private IAirspace _airspace;

        public event EventHandler<TrackEventArgs> OnMovementInAirspace;

        public TransponderDataReceivedController(ITransponderReceiver transponderReceiver, ITransponderDataConversion transponderDataConversion, ITrackLogging trackLogging, IAirspace airspace)
        {
            _transponderReceiver = transponderReceiver;
            _transponderDataConversion = transponderDataConversion;
            _trackLogging = trackLogging;
            _airspace = airspace;
            OnMovementInAirspace += _airspace.OnMovementInAirspaceDetected;
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
                OnMovementInAirspace?.Invoke(this, new TrackEventArgs() {Track = track});
                _trackLogging.LogTrack(track);
            }
        }
    }
}
