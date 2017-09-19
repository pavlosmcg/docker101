using Assembler.Instructions;
using Assembler.Parsing;
using NSubstitute;
using NUnit.Framework;

namespace Assembler.Tests.Parsing
{
    [TestFixture]
    public class AddressInstructionParserTests
    {
        [Test]
        public void ParseInstruction_Returns_Delegates_To_Next_Parser_When_Line_Does_Not_Start_With_At_Symbol()
        {
            // arrange
            const string line = "12345";
            var nextParser = Substitute.For<IInstructionParser>();
            var parser = new AddressInstructionParser(nextParser);

            // act
            parser.ParseInstruction(line);

            // assert 
            nextParser.Received().ParseInstruction(Arg.Is(line));
        }

        [Test]
        public void ParseInstruction_Returns_Delegates_To_Next_Parser_When_Address_Is_Not_Integer()
        {
            // arrange
            const string line = "@blorg";
            var nextParser = Substitute.For<IInstructionParser>();
            var parser = new AddressInstructionParser(nextParser);

            // act
            parser.ParseInstruction(line);

            // assert 
            nextParser.Received().ParseInstruction(Arg.Is(line));
        }

        [Test]
        public void ParseInstruction_Returns_AddressInstruction_When_Address_Is_Valid()
        {
            // arrange
            const string line = "@1234";
            var nextParser = Substitute.For<IInstructionParser>();
            var parser = new AddressInstructionParser(nextParser);

            // act
            IInstruction result = parser.ParseInstruction(line);

            // assert 
            nextParser.DidNotReceive().ParseInstruction(Arg.Any<string>());
            Assert.AreEqual(1234, ((AddressInstruction)result).Address);
        }
    }
}