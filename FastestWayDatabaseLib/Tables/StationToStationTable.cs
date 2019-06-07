using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayDatabaseLib.Interfaces;

namespace FastestWayDatabaseLib.Tables
{
    internal class StationToStationTable : ITable
    {
        public string TableName { get; } = "StationToStation";

        public string GetTableDefinition()
        {
            return $@"
CREATE TABLE {TableName}(
    STS_ID int NOT NULL,
    S1_ID int NOT NULL,
    S2_ID int NOT NULL,
    Hours int NOT NULL,
    Minutes int NOT NULL,
    Seconds int NOT NULL,
    PRIMARY KEY(STS_ID, S1_ID, S2_ID),
    FOREIGN KEY(S1_ID) REFERENCES Stations(S_ID),
    FOREIGN KEY(S2_ID) REFERENCES Stations(S_ID)
)
";
        }
    }
}
