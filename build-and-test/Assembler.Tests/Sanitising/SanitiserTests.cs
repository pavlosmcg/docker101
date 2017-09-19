using Assembler.Sanitising;
using NSubstitute;
using NUnit.Framework;

namespace Assembler.Tests.Sanitising
{
    [TestFixture]
    public class SanitiserTests
    {
        [Test]
        public void Sanitise_Removes_Empty_Lines()
        {
            // arrange
            string[] inputLines = {"blorg", "fester", "", "framistan"};
            var whitespaceRemover = Substitute.For<IWhitespaceRemover>();
            whitespaceRemover.RemoveWhiteSpace(Arg.Any<string>()).Returns(param => param[0]);
            var commentRemover = Substitute.For<ICommentRemover>();
            commentRemover.RemoveComments(Arg.Any<string>()).Returns(param => param[0]);
            var sanitiser = new Sanitiser(whitespaceRemover, commentRemover);

            // act
            string[] cleanedLines = sanitiser.Sanitise(inputLines);

            // assert
            Assert.AreEqual(3, cleanedLines.Length);
        }

        [Test]
        public void Sanitise_Removes_Whitespace_Lines()
        {
            // arrange
            string[] inputLines = { "blorg", "fester", "    ", "framistan" };
            var whitespaceRemover = Substitute.For<IWhitespaceRemover>();
            whitespaceRemover.RemoveWhiteSpace(Arg.Any<string>()).Returns(param => string.IsNullOrWhiteSpace((string)param[0]) ? "" : param[0]);
            var commentRemover = Substitute.For<ICommentRemover>();
            commentRemover.RemoveComments(Arg.Any<string>()).Returns(param => param[0]);
            var sanitiser = new Sanitiser(whitespaceRemover, commentRemover);

            // act
            string[] cleanedLines = sanitiser.Sanitise(inputLines);

            // assert
            Assert.AreEqual(3, cleanedLines.Length);
        }
    }
}
