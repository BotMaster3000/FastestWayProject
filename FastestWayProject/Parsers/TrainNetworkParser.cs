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
        private List<IStationInterface> stationList = new List<IStationInterface>();

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
            ILineInterface currentLine = null;
            List<ITag> currentLineTags = new List<ITag>();
            foreach (string dataLine in content)
            {
                switch (DetermineContentType(dataLine))
                {
                    case DataEnums.LINE:
                        if (currentLine != null)
                        {
                            AddTagsToCurrentLine(currentLine, currentLineTags);
                            stationList.AddRange(currentLine.Stations);
                            currentLineTags.Clear();
                        }
                        currentLine = ParseLine(dataLine);
                        lines.Add(currentLine);
                        break;
                    case DataEnums.STATION:
                        currentLineTags.Add(GetStationTag(dataLine));
                        break;
                    case DataEnums.TIME:
                        currentLineTags.Add(GetTimeTag(dataLine));
                        break;
                }
            }
            if (currentLineTags.Count > 0 && currentLine != null)
            {
                AddTagsToCurrentLine(currentLine, currentLineTags);
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

        private ITag GetStationTag(string dataLine)
        {
            return new TagModel(DataEnums.STATION, dataLine.Trim());
        }

        private ITag GetTimeTag(string dataLine)
        {
            string value = dataLine.Trim('-').Trim();
            return new TagModel(DataEnums.TIME, value);
        }

        private void AddTagsToCurrentLine(ILineInterface currentLine, List<ITag> tags)
        {
            IStationInterface previousStation;
            IStationInterface currentStation = null;
            IStationConnectionInterface currentStationConnection = null;
            List<IStationInterface> stations = new List<IStationInterface>();
            foreach (ITag tag in tags)
            {
                switch (tag.TagType)
                {
                    case DataEnums.STATION:
                        previousStation = currentStation;
                        IStationInterface tempStation = ParseStation(tag.Value);
                        currentStation = StationAlreadyInList(tempStation)
                            ? GetStation(tempStation.Name)
                            : tempStation;

                        stations.Add(currentStation);

                        ConnectStations(currentStation, previousStation, currentStationConnection);
                        currentStationConnection = null;
                        break;
                    case DataEnums.TIME:
                        currentStationConnection = ParseStationConnection(tag.Value);
                        break;
                }
            }
            currentLine.Stations = stations.ToArray();
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

        private bool StationAlreadyInList(IStationInterface station)
        {
            foreach (IStationInterface stationListItem in stationList)
            {
                if (stationListItem.Name == station.Name)
                {
                    return true;
                }
            }
            return false;
        }

        private IStationInterface GetStation(string name)
        {
            foreach (IStationInterface stationListItem in stationList)
            {
                if (stationListItem.Name == name)
                {
                    return stationListItem;
                }
            }
            return null;
        }

        private void ConnectStations(IStationInterface firstStation, IStationInterface secondStation, IStationConnectionInterface connection)
        {
            if (firstStation != null && secondStation != null && connection != null)
            {
                IStationConnectionInterface[] stationConnections = GetStationArray(firstStation);
                stationConnections[stationConnections.Length - 1] =
                    new StationConnectionModel(
                        hours: connection.Hours,
                        minutes: connection.Minutes,
                        station: secondStation);
                firstStation.StationConnections = stationConnections;

                stationConnections = GetStationArray(secondStation);
                stationConnections[stationConnections.Length - 1] =
                    new StationConnectionModel(
                        hours: connection.Hours,
                        minutes: connection.Minutes,
                        station: firstStation);
                secondStation.StationConnections = stationConnections;
            }
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
