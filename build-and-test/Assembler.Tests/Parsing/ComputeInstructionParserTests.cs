using System;
using Assembler.Instructions;
using Assembler.Parsing;
using NSubstitute;
using NUnit.Framework;

namespace Assembler.Tests.Parsing
{
    [TestFixture]
    public class ComputeInstructionParserTests
    {
        [Test]
        public void ParseInstruction_Returns_Delegates_To_Next_Parser_When_Line_Contains_More_Than_One_Equals()
        {
            // arrange
            const string line = "MD=1=1";
            var nextParser = Substitute.For<IInstructionParser>();
            var destinationParser = Substitute.For<IComputeDestinationParser>();
            var jumpParser = Substitute.For<IComputeJumpParser>();
            var parser = new ComputeInstructionParser(nextParser, destinationParser, jumpParser);

            // act
            parser.ParseInstruction(line);

            // assert 
            nextParser.Received().ParseInstruction(Arg.Is(line));
        }

        [Test]
        public void ParseInstruction_Returns_Delegates_To_Next_Parser_When_Line_Contains_More_Than_One_Semicolon()
        {
            // arrange
            const string line = "M+1;JMP;JGT";
            var nextParser = Substitute.For<IInstructionParser>();
            var destinationParser = Substitute.For<IComputeDestinationParser>();
            var jumpParser = Substitute.For<IComputeJumpParser>();
            var parser = new ComputeInstructionParser(nextParser, destinationParser, jumpParser);

            // act
            parser.ParseInstruction(line);

            // assert 
            nextParser.Received().ParseInstruction(Arg.Is(line));
        }

        [TestCase("M+1", 0)]
        [TestCase("M=1", 0)]
        [TestCase("D=M+1;JGT", 0)]
        [TestCase("0;JMP", 0)]
        [TestCase("", 1)]
        [TestCase("M=M+1=", 1)]
        [TestCase("=M", 1)]
        [TestCase("M=", 1)]
        [TestCase(";JMP", 1)]
        [TestCase(":JMP", 1)]
        [TestCase("MDA=M-1;", 1)]
        [TestCase("MD==1;JMP", 1)]
        [TestCase("MD==1", 1)]
        [TestCase("AMD=HH;;JGT", 1)]
        [TestCase("AMD=X+1;JNE", 1)]
        [TestCase("ZK=M+1;JEQ", 1)]
        public void ParseInstruction_Returns_Delegates_To_Next_Parser_When_Instruction_Is_Invalid(string line, int timesDelegated)
        {
            // arrange
            var nextParser = Substitute.For<IInstructionParser>();
            var destinationParser = Substitute.For<IComputeDestinationParser>();
            var jumpParser = Substitute.For<IComputeJumpParser>();
            var parser = new ComputeInstructionParser(nextParser, destinationParser, jumpParser);

            // act
            parser.ParseInstruction(line);

            // assert 
            nextParser.Received(timesDelegated).ParseInstruction(Arg.Any<string>());
        }

        [Test, Combinatorial]
        public void ParseInstruction_Returns_ComputeInstruction_When_Instruction_Is_Valid(
            [Values("", "M", "D", "MD", "A", "AM", "AD", "AMD")]string dest, 
            [Values("", "JGT", "JEQ", "JGE", "JLT", "JNE", "JLE", "JMP")]string jump,
            [Values("0", "1", "-1", "D", "A", "!D", "!A", "-D", "-A", "D+1", "A+1", "D-1", "A-1", "D+A", "D-A",
                "A-D", "D&A", "D|A", "M", "!M", "-M", "M+1", "M-1", "D+M", "D-M", "M-D", "D&M", "D|M")]string comp)
        {
            // arrange
            string equals = "=", semicolon = ";";
            if (string.IsNullOrEmpty(dest))
                equals = string.Empty;
            if (string.IsNullOrEmpty(jump))
                semicolon = string.Empty;
            string line = dest + equals + comp + semicolon + jump;

            var nextParser = Substitute.For<IInstructionParser>();
            var destinationParser = Substitute.For<IComputeDestinationParser>();
            var jumpParser = Substitute.For<IComputeJumpParser>();
            var parser = new ComputeInstructionParser(nextParser, destinationParser, jumpParser);

            // act
            IInstruction result = parser.ParseInstruction(line);

            // assert 
            nextParser.DidNotReceive().ParseInstruction(Arg.Any<string>());
            destinationParser.Received().ParseComputeDestination(Arg.Is(dest));
            jumpParser.Received().ParseComputeJump(Arg.Is(jump));
            Assert.AreEqual(typeof(ComputeInstruction), result.GetType());
        }
    }
}