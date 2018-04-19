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
        private readonly IAirspaceMovementMonitoring _airspaceMovementMonitoring;
        private readonly IAirspaceMonitoring _airspaceManagement;

        public event EventHandler<TrackEventArgs> OnMovementInAirspace;

        public TransponderDataReceivedController(ITransponderReceiver transponderReceiver, ITransponderDataConversion transponderDataConversion, ITrackLogging trackLogging, IAirspaceMovementMonitoring airspaceMovementMonitoring, IAirspaceMonitoring airspaceMonitoring)
        {
            _transponderReceiver = transponderReceiver;
            _transponderDataConversion = transponderDataConversion;
            _trackLogging = trackLogging;
            _airspaceMovementMonitoring = airspaceMovementMonitoring;
            OnMovementInAirspace += _airspaceMovementMonitoring.OnMovementInAirspaceDetected;
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

                if (_airspaceManagement.IsPlaneInAirspace(track.Position, track.Altitude))
                {
                    OnMovementInAirspace?.Invoke(this, new TrackEventArgs() { Track = track });
                    _trackLogging.LogTrack(track);
                }
            }
            _airspaceMovementMonitoring.CheckSeparation();
        }
    }
}
