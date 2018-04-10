using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Exceptions;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    class ExceptionUnitTest
    {
        private TransponderDataConversionException _uut;


        [SetUp]
        public void SetUp()
        {
            _uut = new TransponderDataConversionException();
        }

        [Test]
        public void TestOutputException_ThrowException_CorrectStringIsContainedInException()
        {
           Assert.That(_uut.Message == "The input was invalid. Check amount of separators.");
        }
    }
}
