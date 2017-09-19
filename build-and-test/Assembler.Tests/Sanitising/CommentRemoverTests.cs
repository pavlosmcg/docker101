using Assembler.Sanitising;
using NUnit.Framework;

namespace Assembler.Tests.Sanitising
{
    [TestFixture]
    public class CommentRemoverTests
    {
        [TestCase("// this is a comment", "")]
        [TestCase("a=b", "a=b")]
        [TestCase(" hello ", " hello ")]
        [TestCase("//", "")]
        [TestCase(" //framistan", " ")]
        public void RemoveComments_Clears_Comment_Lines(string input, string expected)
        {
            // arrange
            var commentRemover = new CommentRemover();

            // act
            string cleanedLine = commentRemover.RemoveComments(input);

            // assert
            Assert.AreEqual(expected, cleanedLine);
        }

        [TestCase("blorg = plotz // set blorg equal to plotz", "blorg = plotz ")]
        [TestCase("fester", "fester")]
        [TestCase("a=b", "a=b")]
        [TestCase("x // abc", "x ")]
        public void RemoveComments_Removes_End_Of_Line_Comments(string input, string expected)
        {
            // arrange
            var commentRemover = new CommentRemover();

            // act
            string cleanedLine = commentRemover.RemoveComments(input);

            // assert
            Assert.AreEqual(expected, cleanedLine);
        }
    }
}