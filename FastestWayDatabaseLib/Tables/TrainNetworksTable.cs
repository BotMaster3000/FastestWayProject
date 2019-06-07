using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayDatabaseLib.Interfaces;

namespace FastestWayDatabaseLib.Tables
{
    internal class TrainNetworksTable : ITable
    {
        public string TableName { get; } = "TrainNetworks";

        public string GetTableDefinition()
        {
            return $@"
CREATE TABLE {TableName}(
    TN_ID int NOT NULL,
    Name varchar(255) NOT NULL,
    PRIMARY KEY (TN_ID)
)
";
        }
    }
}
