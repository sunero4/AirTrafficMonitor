using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Receiving;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    class TransponderDataReceiverUnitTests
    {
        private TransponderDataReceiver _uut;
        private ITransponderReceiver _transponderReceiverFake;
        private ITransponderDataConversion _transponderDataConversionFake;
        private ISeparation _separationFake;
        private Airspace _airspace;

        [SetUp]
        public void Setup()
        {
            _transponderReceiverFake = Substitute.For<ITransponderReceiver>();
            _transponderDataConversionFake = Substitute.For<ITransponderDataConversion>();
            _separationFake = Substitute.For<ISeparation>();
            _airspace = new Airspace(new Coordinates() {X = 10000, Y = 10000}, new Coordinates() {X = 25000, Y = 25000}, 500, 10000);
            _uut = new TransponderDataReceiver(_transponderReceiverFake, _transponderDataConversionFake, _separationFake, _airspace);
        }

        [Test]
        public void StartReceivingData_SubscribesToTransponderDataReadyEvent_CorrectCallIsReceived()
        {
            _uut.StartReceivingData();
            _transponderReceiverFake.Received().TransponderDataReady += _uut.OnTransponderDataReady;
        }

        [Test]
        public void OnTransponderDataReady_CallsTransponderDataConversion_CorrectMethodCallIsReceived()
        {
            _uut.OnTransponderDataReady(this, new RawTransponderDataEventArgs(new List<string>() { "XYZ123;5000;6000;10000;20151006213456789"}));
            _transponderDataConversionFake.Received().ConvertData("XYZ123;5000;6000;10000;20151006213456789");
        }

        [Test]
        public void OnTransponderDataReady_CallsSeparation_CorrectMethodCallIsReceived()
        {
            _uut.OnTransponderDataReady(this, new RawTransponderDataEventArgs(new List<string>() { "XYZ123;5000;6000;10000;20151006213456789"}));
            _separationFake.ReceivedWithAnyArgs().MonitorSeparation(new Dictionary<string, List<Track>>());
        }
    }
}
