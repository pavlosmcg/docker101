using Assembler.Instructions;
using Assembler.Parsing;
using NSubstitute;
using NUnit.Framework;

namespace Assembler.Tests.Parsing
{
    [TestFixture]
    public class VariableInstructionParserTests
    {
        [Test]
        public void ParseInstruction_Returns_Delegates_To_Next_Parser_When_Line_Does_Not_Start_With_At_Symbol()
        {
            // arrange
            const string line = "somevar";
            var nextParser = Substitute.For<IInstructionParser>();
            var labelParser = Substitute.For<ISymbolParser>();
            var parser = new VariableInstructionParser(nextParser, labelParser);

            // act
            parser.ParseInstruction(line);

            // assert 
            nextParser.Received().ParseInstruction(Arg.Is(line));
        }

        [Test]
        public void ParseInstruction_Returns_VariableInstruction_When_Label_Is_Valid()
        {
            // arrange
            const string line = "@somevar";
            var nextParser = Substitute.For<IInstructionParser>();
            var labelParser = Substitute.For<ISymbolParser>();
            var parser = new VariableInstructionParser(nextParser, labelParser);

            // act
            IInstruction result = parser.ParseInstruction(line);

            // assert 
            nextParser.DidNotReceive().ParseInstruction(Arg.Any<string>());
            labelParser.Received().ParseLabel(Arg.Is("somevar"));
            Assert.AreEqual(typeof(VariableInstruction), result.GetType());
        }
    }
}