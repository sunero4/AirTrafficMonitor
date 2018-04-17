using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.AirspaceManagement
{
    public class Separation : ISeparation
    {
        public event EventHandler<SeparationEventArgs> SeparationEvent;

        public void MonitorSeparation(Dictionary<string, List<Track>> Tracks)
        {
            for (int i = 0; i < Tracks.Count; i++)
            {
                for (int j = i+1; j < Tracks.Count-i; j++)
                {
                    var needSeparation = CheckSeparation(Tracks.ElementAt(i).Value[1].Position.X, Tracks.ElementAt(i).Value[1].Position.Y,
                        Tracks.ElementAt(i + j).Value[1].Position.X, Tracks.ElementAt(i).Value[1].Position.Y);
                    if (needSeparation)
                    {
                        SeparationEvent?.Invoke(this, new SeparationEventArgs() {Track1 = Tracks.ElementAt(i).Value[1].Tag,
                            TimeOfOccurence = Tracks.ElementAt(i).Value[1].TimeStamp, Track2 = Tracks.ElementAt(j).Value[1].Tag});
                    }
                }
            }
        }



        private bool CheckSeparation(double X1, double Y1, double X2, double Y2)
        {
            var xAfstand = Math.Abs(X1 - X2);
            var yAfstand = Math.Abs(Y1 - Y2);
            if (xAfstand < 5000 && yAfstand < 300)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
