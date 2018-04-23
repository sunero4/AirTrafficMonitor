﻿using System;
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

        public void MonitorSeparation(Dictionary<string, List<Track>> tracks)
        {
            for (int i = 0; i < tracks.Count; i++)
            {
                for (int j = i+1; j < tracks.Count-i; j++)
                {
                    if (tracks.ElementAt(i).Value.Count > 1 && tracks.ElementAt(j).Value.Count > 1 && tracks.ElementAt(i + j).Value.Count > 1)
                    {
                        var needSeparation = CheckSeparation(tracks.ElementAt(i).Value[1].Position.X, tracks.ElementAt(i).Value[1].Position.Y,
                            tracks.ElementAt(i + j).Value[1].Position.X, tracks.ElementAt(i).Value[1].Position.Y);
                        if (needSeparation)
                        {
                            SeparationEvent?.Invoke(this, new SeparationEventArgs()
                            {
                                Track1 = tracks.ElementAt(i).Value[1].Tag,
                                TimeOfOccurence = tracks.ElementAt(i).Value[1].TimeStamp,
                                Track2 = tracks.ElementAt(j).Value[1].Tag
                            });
                        }
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
