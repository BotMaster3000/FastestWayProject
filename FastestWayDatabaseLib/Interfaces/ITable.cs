﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestWayDatabaseLib.Interfaces
{
    public interface ITable
    {
        string TableName { get; }

        string GetTableDefinition();
    }
}
