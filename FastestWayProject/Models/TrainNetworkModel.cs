using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayProject.Interfaces;

namespace FastestWayProject.Models
{
    public class TrainNetworkModel : ITrainNetwork
    {
        public ILineInterface[] Lines { get; }

        public TrainNetworkModel(ILineInterface[] lines)
        {
            Lines = lines;
        }
    }
}
