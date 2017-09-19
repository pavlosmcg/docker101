using Assembler.Instructions;
using Assembler.Parsing;
using NSubstitute;
using NUnit.Framework;

namespace Assembler.Tests.Parsing
{
    [TestFixture]
    public class LabelInstructionParserTests
    {
        [Test]
        public void ParseInstruction_Returns_Delegates_To_Next_Parser_When_Line_Does_Not_Start_With_Opening_Bracket()
        {
            // arrange
            const string line = "BLORG)";
            var nextParser = Substitute.For<IInstructionParser>();
            var labelParser = Substitute.For<ISymbolParser>();
            var parser = new LabelInstructionParser(nextParser, labelParser);

            // act
            parser.ParseInstruction(line);

            // assert 
            nextParser.Received().ParseInstruction(Arg.Is(line));
        }

        [TestCase("(BLORG)", "BLORG")]
        [TestCase("(blorg)", "blorg")]
        [TestCase("(END_PLOTZ)", "END_PLOTZ")]
        [TestCase("(FRAMI:STAN)", "FRAMI:STAN")]
        [TestCase("(CA$H)", "CA$H")]
        public void ParseInstruction_Returns_LabelInstruction_When_Label_Is_Valid(string line, string expected)
        {
            // arrange
            var nextParser = Substitute.For<IInstructionParser>();
            var labelParser = Substitute.For<ISymbolParser>();
            var parser = new LabelInstructionParser(nextParser, labelParser);

            // act
            IInstruction result = parser.ParseInstruction(line);

            // assert 
            nextParser.DidNotReceive().ParseInstruction(Arg.Any<string>());
            labelParser.Received().ParseLabel(Arg.Is(expected));
            Assert.AreEqual(typeof(LabelInstruction), result.GetType());
        }
    }
}