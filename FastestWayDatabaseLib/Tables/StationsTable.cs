using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayDatabaseLib.Interfaces;

namespace FastestWayDatabaseLib.Tables
{
    internal class StationsTable : ITable
    {
        public string TableName { get; } = "Stations";

        public string GetTableDefinition()
        {
            return $@"
CREATE TABLE {TableName}(
    S_ID int NOT NULL,
    Name varchar(255) NOT NULL,
    PRIMARY KEY (S_ID)
)
";
        }
    }
}
