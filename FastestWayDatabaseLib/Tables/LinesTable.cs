using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayDatabaseLib.Interfaces;

namespace FastestWayDatabaseLib.Tables
{
    internal class LinesTable : ITable
    {
        public string TableName { get; } = "Lines";

        public string GetTableDefinition()
        {
            return $@"
CREATE TABLE {TableName}(
    L_ID int NOT NULL PRIMARY KEY,
    Name varchar(255) NOT NULL,
    TN_ID int,
    PRIMARY KEY (TN_ID),
    FOREIGN KEY (TN_ID) REFERENCES TrainNetworks(TN_ID)
)
";
        }
    }
}
