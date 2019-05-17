using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestWayProject.Interfaces
{
    public interface IStationConnectionInterface
    {
        IStationInterface Station { get; }
        int Hours { get; }
        int Minutes { get; }
        int Seconds { get; }
    }
}
