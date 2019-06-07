using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayProject.Enums;

namespace FastestWayProject.Interfaces
{
    public interface ITag
    {
        DataEnums TagType { get; }
        string Value { get; }
    }
}
