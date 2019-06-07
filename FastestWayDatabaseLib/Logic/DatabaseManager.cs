using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayDatabaseLib.Interfaces;

namespace FastestWayDatabaseLib.Logic
{
    public class DatabaseManager : IDatabaseManager
    {
        public string DatabaseName { get; } = "TrainNetworkDatabase.sqlite";

        public void SetupDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
