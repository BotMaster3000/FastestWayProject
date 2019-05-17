using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayProject.Interfaces;

namespace FastestWayProject.Models
{
    public class StationModel : IStationInterface
    {
        public string Name { get; }
        public IStationConnectionInterface[] StationConnections { get; }

        public StationModel(string name)
        {
            Name = name;
        }
    }
}
