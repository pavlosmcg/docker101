using System;
using Assembler.Instructions;
using Assembler.Parsing;
using NUnit.Framework;

namespace Assembler.Tests.Parsing
{
    [TestFixture]
    public class ComputeDestinationParserTests
    {
        [Test]
        public void ParseInstruction_Throws_ArgumentException_When_Destination_Cannot_Be_Parsed()
        {
            const string dest = "ACDC";
            var parser = new ComputeDestinationParser();

            // act
            TestDelegate testAction = () => parser.ParseComputeDestination(dest);

            // assert 
            Assert.Throws<ArgumentException>(testAction);
        }

        [TestCase("", ComputeDestinationType.None)]
        [TestCase("M", ComputeDestinationType.Memory)]
        [TestCase("D", ComputeDestinationType.DataRegister)]
        [TestCase("MD", ComputeDestinationType.Memory | ComputeDestinationType.DataRegister)]
        [TestCase("A", ComputeDestinationType.AddressRegister)]
        [TestCase("AM", ComputeDestinationType.AddressRegister | ComputeDestinationType.Memory)]
        [TestCase("AD", ComputeDestinationType.AddressRegister | ComputeDestinationType.DataRegister)]
        [TestCase("AMD", ComputeDestinationType.AddressRegister | ComputeDestinationType.Memory | ComputeDestinationType.DataRegister)]
        public void ParseInstruction_Returns_Correct_Destinations(string dest, ComputeDestinationType expected)
        {
            var parser = new ComputeDestinationParser();

            // act
            ComputeDestinationType result = parser.ParseComputeDestination(dest);

            // assert 
            Assert.AreEqual(expected, result);
        }
    }
}