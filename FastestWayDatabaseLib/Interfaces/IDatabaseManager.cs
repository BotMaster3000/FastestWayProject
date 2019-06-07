using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestWayDatabaseLib.Interfaces
{
    public interface IDatabaseManager
    {
        string DatabaseName { get; }

        void SetupDatabase();
    }
}
