using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Logging;
using Castle.Core.Smtp;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    public class XmlConvertingUnitTest
    {
        private SeparationXmlLogging _uut;
        private string tag1;
        private string tag2;
        private DateTime timeOfOccurence;
        private string formatted;
        private SeparationEventArgs earg;

        [SetUp]
        public void SetUp()
        {

            _uut = new SeparationXmlLogging();
            tag1 = "ABC123";
            tag2 = "ABC234";
            timeOfOccurence = new DateTime(2000, 1, 1);
            formatted = timeOfOccurence.ToString("yyyy-MM-ddTHH:mm:ssK");

            earg = new SeparationEventArgs();
            earg.TimeOfOccurence = timeOfOccurence;
            earg.Track1 = tag1;
            earg.Track2 = tag2;
        }

        [Test]
        public void ConvertInfoToXelement_DataIsConverted_XelementWithTag1Created()
        {
            var testElement = _uut.ConvertToXmlElement(tag1, tag2, timeOfOccurence);

            var result = testElement.Descendants().ToList();

            Assert.That(result[0].Value, Is.EqualTo(tag1));
        }

        [Test]
        public void ConvertInfoToXelement_DataIsConverted_XelementWithTag2Created()
        {
            var testElement = _uut.ConvertToXmlElement(tag1, tag2, timeOfOccurence);

            var result = testElement.Descendants().ToList();
            Assert.That(result[1].Value, Is.EqualTo(tag2));
        }

        [Test]
        public void ConvertInfoToXelement_DataIsConverted_XelementWithTimeOfOccurenceIsCreated()
        {
            var testElement = _uut.ConvertToXmlElement(tag1, tag2, timeOfOccurence);

            var result = testElement.Descendants().ToList();
            Assert.That(result[2].Value, Is.EqualTo(formatted));
        }
    }
}
