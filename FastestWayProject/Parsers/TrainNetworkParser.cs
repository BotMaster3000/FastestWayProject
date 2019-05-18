using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayProject.Interfaces;
using FastestWayProject.Enums;
using FastestWayProject.Models;

namespace FastestWayProject.Parsers
{
    public class TrainNetworkParser : ITrainNetworkParser
    {
        public ITrainNetwork ParseTrainNetwork(string fileName)
        {
            string[] fileContent = ReadFileContent(fileName);
            return Parse(fileContent);
        }

        private string[] ReadFileContent(string fileName)
        {
            return File.ReadAllLines(fileName);
        }

        private ITrainNetwork Parse(string[] content)
        {
            List<ILineInterface> lines = new List<ILineInterface>();
            List<IStationInterface> stations = new List<IStationInterface>();
            List<IStationInterface> currentLineStations = new List<IStationInterface>();
            ILineInterface currentLine = null;
            IStationInterface previousStation = null;
            IStationInterface currentStation = null;
            IStationConnectionInterface currentTimeConnection = null;
            foreach (string dataLine in content)
            {
                switch (DetermineContentType(dataLine))
                {
                    case DataEnums.LINE:
                        if (currentLineStations?.Count > 0)
                        {
                            currentLine.Stations = currentLineStations.ToArray();
                            currentLineStations.Clear();
                        }
                        currentLine = ParseLine(dataLine);
                        lines.Add(currentLine);
                        break;
                    case DataEnums.STATION:
                        previousStation = currentStation;
                        IStationInterface tempStation = ParseStation(dataLine);
                        if (StationAlreadyInList(tempStation, stations))
                        {
                            currentStation = GetStation(tempStation.Name, stations);
                        }
                        else
                        {
                            currentStation = ParseStation(dataLine);
                            stations.Add(currentStation);
                        }
                        currentLineStations.Add(currentStation);
                        if (previousStation != null && currentTimeConnection != null)
                        {
                            ConnectStations(previousStation, currentStation, currentTimeConnection);
                            currentTimeConnection = null;
                        }
                        break;
                    case DataEnums.TIME:
                        currentTimeConnection = ParseStationConnection(dataLine);
                        break;
                }
            }
            if (currentLineStations?.Count > 0)
            {
                currentLine.Stations = currentLineStations.ToArray();
            }
            return new TrainNetworkModel(lines.ToArray());
        }

        private DataEnums DetermineContentType(string line)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.Length > 0)
            {
                switch (trimmedLine[0])
                {
                    case '[':
                        return DataEnums.LINE;
                    case '-':
                        return DataEnums.TIME;
                    default:
                        return DataEnums.STATION;
                }
            }
            return DataEnums.UNDEFINED;
        }

        private ILineInterface ParseLine(string dataLine)
        {
            string lineName = dataLine.Trim(new char[] { '[', ']' });
            return new LineModel(lineName);
        }

        private IStationInterface ParseStation(string dataLine)
        {
            string stationName = dataLine.Trim();
            return new StationModel(stationName);
        }

        private bool StationAlreadyInList(IStationInterface station, List<IStationInterface> stationList)
        {
            foreach(IStationInterface stationListItem in stationList)
            {
                if (stationListItem.Name == station.Name)
                {
                    return true;
                }
            }
            return false;
        }

        private IStationInterface GetStation(string name, List<IStationInterface> stationList)
        {
            foreach(IStationInterface stationListItem in stationList)
            {
                if(stationListItem.Name == name)
                {
                    return stationListItem;
                }
            }
            return null;
        }

        private void ConnectStations(IStationInterface firstStation, IStationInterface secondStation, IStationConnectionInterface connection)
        {
            IStationConnectionInterface[] stationConnections = GetStationArray(firstStation);
            stationConnections[stationConnections.Length - 1] =
                new StationConnectionModel(hours: connection.Hours, minutes: connection.Minutes, station: secondStation);
            firstStation.StationConnections = stationConnections;

            stationConnections = GetStationArray(secondStation);
            stationConnections[stationConnections.Length - 1] =
                new StationConnectionModel(hours: connection.Hours, minutes: connection.Minutes, station: firstStation);
            secondStation.StationConnections = stationConnections;
        }

        private IStationConnectionInterface[] GetStationArray(IStationInterface station)
        {
            IStationConnectionInterface[] stationConnections = new IStationConnectionInterface[1];
            if (station.StationConnections?.Length > 0)
            {
                stationConnections = new IStationConnectionInterface[station.StationConnections.Length + 1];
                station.StationConnections.CopyTo(stationConnections, 0);
            }
            return stationConnections;
        }

        private IStationConnectionInterface ParseStationConnection(string dataLine)
        {
            string connection = dataLine.Trim('-').Replace(" ", "");
            string[] time = connection.Split(':');
            return new StationConnectionModel(hours: Convert.ToInt32(time[0]), minutes: Convert.ToInt32(time[1]));
        }
    }
}
