using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastestWayProject.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayProject.Interfaces;
using System.IO;

namespace FastestWayProject.Parsers.Tests
{
    [TestClass]
    public class TrainNetworkParserTests
    {
        private const string RELATIVE_FILEPATH = @"\TestFiles\TestCase2LinesCross.txt";

        [TestMethod]
        public void ParseTrainNetworkTest()
        {
            string totalFilePath = Path.GetFullPath(Directory.GetCurrentDirectory() + RELATIVE_FILEPATH);

            TrainNetworkParser parser = new TrainNetworkParser();
            ITrainNetwork network = parser.ParseTrainNetwork(totalFilePath);

            const int LINES_IN_TESTCASE = 2;
            const int STATIONS_IN_TESTCASE = 6;
            string[] lineNames = new string[] { "S1", "S8" };
            string[] stationNamesS1 = new string[] { "Wollankstraße", "Bornholmer Straße", "Gesundbrunnen" };
            string[] stationNamesS8 = new string[] { "Pankow", "Bornholmer Straße", "Schönhauser Allee" };

            object[][] stationConnections = new object[][] // Station1;Station2;Hours;Minutes
            {
                new object[] { "Wollankstraße", "Bornholmer Straße", 0, 5 },
                new object[] { "Gesundbrunnen", "Bornholmer Straße", 0, 6 },
                new object[] { "Pankow", "Bornholmer Straße", 0, 3 },
                new object[] { "Schönhauser Allee", "Bornholmer Straße", 0, 9 },
            };

            AssertLinesAndStationsCount(network, LINES_IN_TESTCASE, STATIONS_IN_TESTCASE);

            AssertLinesExist(network, lineNames);

            AssertStationsExists(network, "S1", stationNamesS1);
            AssertStationsExists(network, "S8", stationNamesS8);

            AssertLineTimesAreSet(network, stationConnections);
        }

        private void AssertLinesAndStationsCount(ITrainNetwork network, int linesInTestCase, int stationsInTestCase)
        {
            Assert.AreEqual(linesInTestCase, network.Lines.Length);
            int totalStations = 0;
            foreach (ILineInterface line in network.Lines)
            {
                totalStations += line.Stations.Length;
            }
            Assert.AreEqual(stationsInTestCase, totalStations);
        }

        private void AssertLinesExist(ITrainNetwork network, string[] lineNames)
        {
            foreach (string lineName in lineNames)
            {
                bool isFound = false;
                foreach (ILineInterface line in network.Lines)
                {
                    if (line.Name == lineName)
                    {
                        isFound = true;
                        break;
                    }
                }
                Assert.IsTrue(isFound, $"Line {lineName} was not found");
            }
        }

        private void AssertStationsExists(ITrainNetwork network, string lineName, string[] stationNames)
        {
            foreach (string stationName in stationNames)
            {
                bool stationFound = false;
                foreach (ILineInterface line in network.Lines)
                {
                    if (line.Name == lineName)
                    {
                        foreach (IStationInterface station in line.Stations)
                        {
                            if (station.Name == stationName)
                            {
                                stationFound = true;
                                break;
                            }
                        }
                        break;
                    }
                }
                Assert.IsTrue(stationFound, $"Station {stationName} was not found");
            }
        }

        private void AssertLineTimesAreSet(ITrainNetwork network, object[][] stationConnections)
        {
            foreach (object[] stationConnection in stationConnections)
            {
                AssertConnectionIsCorrect(network, stationConnection, 0);
                AssertConnectionIsCorrect(network, stationConnection, 1);
            }
        }

        private void AssertConnectionIsCorrect(ITrainNetwork network, object[] stationConnection, int stationPosition)
        {
            int otherstationPosition = stationPosition == 0 ? 1 : 0;
            bool stationIsCorrect = false;
            foreach (ILineInterface line in network.Lines)
            {
                bool stationFound = false;
                foreach (IStationInterface station in line.Stations)
                {
                    if (station.Name == stationConnection[stationPosition].ToString())
                    {
                        foreach (IStationConnectionInterface connection in station.StationConnections)
                        {
                            if (connection.Station.Name == stationConnection[otherstationPosition].ToString())
                            {
                                if (connection.Hours == (int)stationConnection[2]
                                    && connection.Minutes == (int)stationConnection[3])
                                {
                                    stationIsCorrect = true;
                                }
                                break;
                            }
                        }
                        stationFound = true;
                        break;
                    }
                }
                if (stationFound) break;
            }
            Assert.IsTrue(stationIsCorrect, $"Station {stationConnection[stationPosition]}"
                + $" is falsely connected to {stationConnection[otherstationPosition]}");
        }
    }
}