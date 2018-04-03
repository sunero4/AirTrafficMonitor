using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor
{
    public class TransponderDataConversionException : Exception
    {
        public override string Message => "The input was invalid. Check amount of separators.";
    }
}
