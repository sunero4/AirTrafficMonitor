using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
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
        private IAirspaceMonitoring _airspaceManagement;

        public event EventHandler<TrackEventArgs> OnMovementInAirspace;

        public TransponderDataReceivedController(ITransponderReceiver transponderReceiver, ITransponderDataConversion transponderDataConversion, ITrackLogging trackLogging, IAirspace airspace, IAirspaceMonitoring airspaceMonitoring)
        {
            _transponderReceiver = transponderReceiver;
            _transponderDataConversion = transponderDataConversion;
            _trackLogging = trackLogging;
            _airspace = airspace;
            OnMovementInAirspace += _airspace.OnMovementInAirspaceDetected;
            _airspaceManagement = airspaceMonitoring;
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

                if (_airspaceManagement.IsPlaneInAirspace(_airspace, track.Position, track.Altitude))
                {
                    OnMovementInAirspace?.Invoke(this, new TrackEventArgs() { Track = track });
                    _trackLogging.LogTrack(track);
                }
            }
        }
    }
}
