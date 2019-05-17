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
        const string RELATIVE_FILEPATH = @"\TestFiles\TestCase2LinesCross.txt";

        [TestMethod]
        public void ParseTrainNetworkTest()
        {
            string totalFilePath = Path.GetFullPath(Directory.GetCurrentDirectory() + RELATIVE_FILEPATH);

            TrainNetworkParser parser = new TrainNetworkParser();
            ITrainNetwork network = parser.ParseTrainNetwork(totalFilePath);

            Assert.Fail();
        }
    }
}