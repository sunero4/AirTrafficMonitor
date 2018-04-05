using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.Rendering
{
    public interface ITrackToStringRepresentation
    {
        string GenerateStringRepresentation(Track track);
    }
}
