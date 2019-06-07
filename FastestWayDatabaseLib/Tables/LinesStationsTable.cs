using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayDatabaseLib.Interfaces;

namespace FastestWayDatabaseLib.Tables
{
    internal class LinesStationsTable : ITable
    {
        public string TableName { get; } = "LinesStations";

        public string GetTableDefinition()
        {
            return $@"
CREATE TABLE {TableName}(
    LS_ID int NOT NULL,
    L_ID int NOT NULL,
    S_ID int NOT NULL,
    PRIMARY KEY(LS_ID, L_ID, S_ID),
    FOREIGN KEY(L_ID) REFERENCES Lines(L_ID),
    FOREIGN KEY(S_ID) REFERENCES Stations(S_ID)
)
";
        }
    }
}
