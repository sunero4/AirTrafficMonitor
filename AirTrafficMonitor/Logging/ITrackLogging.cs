using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.Logging
{
    public interface ITrackLogging
    {
        void LogTrack(Track track);
    }
}
