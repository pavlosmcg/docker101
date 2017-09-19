using System;
using Assembler.Instructions;
using Assembler.Parsing;
using NUnit.Framework;

namespace Assembler.Tests.Parsing
{
    [TestFixture]
    public class ComputeJumpParserTests
    {
        [Test]
        public void ParseInstruction_Returns_None_When_Jump_Is_Empty()
        {
            // arrange
            string input = string.Empty;
            var parser = new ComputeJumpParser();

            // act
            ComputeJumpType result = parser.ParseComputeJump(input);

            // assert 
            Assert.AreEqual(ComputeJumpType.None, result);
        }

        [TestCase("JMP", ComputeJumpType.JMP)]
        [TestCase("JGT", ComputeJumpType.JGT)]
        [TestCase("JEQ", ComputeJumpType.JEQ)]
        [TestCase("JGE", ComputeJumpType.JGE)]
        [TestCase("JLT", ComputeJumpType.JLT)]
        [TestCase("JNE", ComputeJumpType.JNE)]
        [TestCase("JLE", ComputeJumpType.JLE)]
        public void ParseInstruction_Returns_Correct_Jump_When_Jump_Is_Specified(string input, ComputeJumpType expected)
        {
            // arrange
            var parser = new ComputeJumpParser();

            // act
            ComputeJumpType result = parser.ParseComputeJump(input);

            // assert 
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ParseInstruction_Throws_ArgumentException_When_Jump_Cannot_Be_Parsed()
        {
            // arrange
            const string input = "LOL";
            var parser = new ComputeJumpParser();

            // act
            TestDelegate testAction = () => parser.ParseComputeJump(input);

            // assert 
            Assert.Throws<ArgumentException>(testAction);
        } 
    }
}