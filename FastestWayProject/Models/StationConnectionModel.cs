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
        public IStationInterface Station { get; set; }
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }

        public StationConnectionModel(int hours = 0, int minutes = 0, int seconds = 0, IStationInterface station = null)
        {
            Station = station;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }
    }
}
