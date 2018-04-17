﻿using System.Collections.Generic;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.VelocityCalc;

namespace AirTrafficMonitor.AirspaceManagement
{
    public class Airspace : IAirspace
    {
        private readonly IVelocityCalculator _velocityCalculator;

        public Airspace(IVelocityCalculator velocityCalculator, Coordinates southWestCorner, Coordinates northEastCorner, int lowerAltitudeBoundary, int upperAltitudeBoundary)
        {
            _velocityCalculator = velocityCalculator;
            PlanesInAirspace = new Dictionary<string, List<Track>>();
            SoutWestCorner = southWestCorner;
            NorthEastCorner = northEastCorner;
            LowerAltitudeBoundary = lowerAltitudeBoundary;
            UpperAltitudeBoundary = upperAltitudeBoundary;
        }

        public Coordinates SoutWestCorner { get; set; }
        public Coordinates NorthEastCorner { get; set; }
        public int LowerAltitudeBoundary { get; set; }
        public int UpperAltitudeBoundary { get; set; }
        public Dictionary<string, List<Track>> PlanesInAirspace { get; set; }
    }
}