using System.Collections.Generic;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.Extensions
{
    public static class ListSlidingWindowExtension
    {
        public static void AddToSlidingWindowList(this List<Track> list, Track track, int maxSize)
        {
            if (list.Count >= maxSize)
            {
                list.RemoveAt(0);
            }
            list.Add(track);
        }
    }
}