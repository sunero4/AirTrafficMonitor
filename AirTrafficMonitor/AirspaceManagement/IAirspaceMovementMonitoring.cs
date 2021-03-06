﻿using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.AirspaceManagement
{
    public interface IAirspaceMovementMonitoring
    {
        void OnMovementInAirspaceDetected(object sender, TrackEventArgs trackEventArgs);

        void OnPlaneNotInAirspace(object sender, TrackEventArgs trackEventArgs);
    }
}