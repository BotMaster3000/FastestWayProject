using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestWayProject.Interfaces
{
    public interface IStationInterface
    {
        string Name { get; }
        IStationConnectionInterface[] StationConnections { get; }
    }
}
