using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Controllers;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Logging;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    class TransponderDataReceivedControllerUnitTests
    {
        private ITransponderReceiver _transponderReceiverFake;
        private ITransponderDataConversion _transponderDataConversionFake;
        private ITrackLogging _trackLogging;
        private TransponderDataReceivedController _uut;

        [SetUp]
        public void Setup()
        {
            _transponderReceiverFake = Substitute.For<ITransponderReceiver>();
            _transponderDataConversionFake = Substitute.For<ITransponderDataConversion>();
            _trackLogging = Substitute.For<ITrackLogging>();
            _uut = new TransponderDataReceivedController(_transponderReceiverFake, _transponderDataConversionFake, _trackLogging);
        }

        [Test]
        public void StartReceivingTransponderData_OnTransponderDataReadySubscribesToEvent_CorrectCallIsReceived()
        {
            _uut.StartReceivingTransponderData();
            _transponderReceiverFake.Received().TransponderDataReady += _uut.OnTransponderDataReady;
        }
    }
}
