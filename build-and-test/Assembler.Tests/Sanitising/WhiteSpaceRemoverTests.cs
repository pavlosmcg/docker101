using NUnit.Framework;
using Assembler.Sanitising;

namespace Assembler.Tests.Sanitising
{
    [TestFixture]
    public class WhiteSpaceRemoverTests
    {
        [TestCase("blorg = plotz", "blorg=plotz")]
        [TestCase("fes ter", "fester")]
        [TestCase(" framistan", "framistan")]
        [TestCase(" h e     l l o", "hello")]
        [TestCase("@world   ", "@world")]
        public void RemoveWhiteSpace_Removes_All_Whitespace_From_Line(string input, string expected)
        {
            // arrange
            var whitespaceRemover = new WhitespaceRemover();

            // act
            string cleanedLine = whitespaceRemover.RemoveWhiteSpace(input);

            // assert
            Assert.AreEqual(expected, cleanedLine);
        }
    }
}