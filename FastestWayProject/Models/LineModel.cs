using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayProject.Interfaces;

namespace FastestWayProject.Models
{
    public class LineModel : ILineInterface
    {
        public string Name { get; }
        public IStationInterface[] Stations { get; set; }

        public LineModel(string name)
        {
            Name = name;
        }
    }
}
