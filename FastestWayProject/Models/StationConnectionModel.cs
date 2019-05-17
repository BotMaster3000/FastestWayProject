using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayProject.Interfaces;

namespace FastestWayProject.Models
{
    public class StationConnectionModel : IStationConnectionInterface
    {
        public IStationInterface Station { get; }
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }

        public StationConnectionModel(IStationInterface station, int hours = 0, int minutes = 0, int seconds = 0)
        {
            Station = station;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }
    }
}
