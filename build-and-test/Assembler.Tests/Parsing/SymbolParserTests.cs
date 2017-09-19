using Assembler.Parsing;
using NUnit.Framework;

namespace Assembler.Tests.Parsing
{
    [TestFixture]
    public class SymbolParserTests
    {
        [TestCase("i", "i")]
        [TestCase("ii", "ii")]
        [TestCase("BLORG", "BLORG")]
        [TestCase("blorg", "blorg")]
        [TestCase("END_PLOTZ", "END_PLOTZ")]
        [TestCase("FRAMI:STAN", "FRAMI:STAN")]
        [TestCase("CA$H", "CA$H")]
        [TestCase("BL0RG9", "BL0RG9")]
        [TestCase("9BLORG", null)]
        [TestCase(".PLOTZ", ".PLOTZ")]
        [TestCase("_PLOTZ", "_PLOTZ")]
        [TestCase("$PLOTZ", "$PLOTZ")]
        [TestCase(":PLOTZ", ":PLOTZ")]
        [TestCase("L@BEL", null)]
        [TestCase("LABEL!", null)]
        [TestCase("C&A", null)]
        [TestCase("FIFTY%", null)]
        [TestCase("CA£H", null)]
        [TestCase("output.create", "output.create")]
	    [TestCase("RET_ADDRESS_CALL233", "RET_ADDRESS_CALL233")]
	    [TestCase("output.createshiftedmap","output.createshiftedmap")]
	    [TestCase("LOOP_output.createshiftedmap", "LOOP_output.createshiftedmap")]
	    [TestCase("output.createshiftedmap$while_exp0", "output.createshiftedmap$while_exp0")]
	    [TestCase("RET_ADDRESS_EQ21","RET_ADDRESS_EQ21")]
	    [TestCase("output.createshiftedmap$if_true0","output.createshiftedmap$if_true0")]
        public void ParseInstruction_Returns_LabelInstruction_When_Label_Is_Valid(string input, string expected)
        {
            // arrange
            var parser = new SymbolParser();

            // act
            string result = parser.ParseLabel(input);

            // assert 
            Assert.AreEqual(expected, result);
        }
    }
}