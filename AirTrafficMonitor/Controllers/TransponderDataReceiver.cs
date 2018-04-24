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
    public class TransponderDataReceiver
    {
        private readonly ITransponderReceiver _transponderReceiver;
        private readonly ITransponderDataConversion _transponderDataConversion;
        private readonly ISeparation _separation;
        private readonly Airspace _airspace;


        public TransponderDataReceiver(ITransponderReceiver transponderReceiver,
            ITransponderDataConversion transponderDataConversion, ISeparation separation, Airspace airspace)
        {
            _transponderReceiver = transponderReceiver;
            _transponderDataConversion = transponderDataConversion;
            _separation = separation;
            _airspace = airspace;
        }

        public void StartReceivingData()
        {
            _transponderReceiver.TransponderDataReady += OnTransponderDataReady;
        }

        public void OnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            foreach (var transponderData in rawTransponderDataEventArgs.TransponderData)
            {
                _transponderDataConversion.ConvertData(transponderData);
            }
            _separation.MonitorSeparation(_airspace.PlanesInAirspace);
        }
    }
}
