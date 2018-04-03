using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Converting;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    public class StringToDateTimeConversionUnitTests
    {
        public class TestData
        {
            public string DateTimeAsString { get; set; }
            public DateTime DateTime { get; set; }
        }

        private static TestData[] _validTestData = new TestData[]
        {
            new TestData()
            {
                DateTime = new DateTime(2015, 10, 06, 21, 34, 56, 789),
                DateTimeAsString = "20151006213456789"
            },
            new TestData()
            {
                DateTime = new DateTime(2018, 11, 24, 12, 34, 22, 456),
                DateTimeAsString = "20181124123422456"
            }, 
            new TestData()
            {
                DateTime = new DateTime(1870, 07, 04, 23, 54, 43, 123),
                DateTimeAsString = "18700704235443123"
            } 
        };

        private StringToDateTimeConversion _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new StringToDateTimeConversion();
        }

        [Test]
        public void ConvertToDateTime_ValidInput_ConvertsToCorrectDateTime([ValueSource(nameof(_validTestData))]TestData testData)
        {
            var converted = _uut.ConvertToDateTime(testData.DateTimeAsString);
            Assert.That(converted, Is.EqualTo(testData.DateTime));
        }

        [TestCase("201b0704235443123")]
        [TestCase("2016070423545312,")]
        public void ConvertToDateTime_InvalidInput_ThrowsFormatException(string dateTimeAsString)
        {
            Assert.Throws<FormatException>(() => _uut.ConvertToDateTime(dateTimeAsString));
        }
    }
}
