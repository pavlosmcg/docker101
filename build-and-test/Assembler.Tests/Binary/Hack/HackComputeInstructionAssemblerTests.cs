using Assembler.Binary;
using Assembler.Binary.Hack;
using Assembler.Instructions;
using NSubstitute;
using NUnit.Framework;

namespace Assembler.Tests.Binary.Hack
{
    [TestFixture]
    public class HackComputeInstructionAssemblerTests
    {
        [Test]
        public void AssembleInstruction_Returns_Single_Line()
        {
            // arrange
            var instruction = new ComputeInstruction(ComputeDestinationType.None, "D+1", ComputeJumpType.None);
            var computeBitsAssembler = Substitute.For<IHackComputeBitsAssembler>();
            computeBitsAssembler.AssembleComputeBits(Arg.Any<string>()).Returns("0000000");
            var assembler = new HackComputeInstructionAssembler(computeBitsAssembler);

            // act
            string[] result = assembler.AssembleInstruction(instruction);

            // assert 
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void AssembleInstruction_Returns_16_Bit_Line()
        {
            // arrange
            var instruction = new ComputeInstruction(ComputeDestinationType.None, "D+1", ComputeJumpType.None);
            var computeBitsAssembler = Substitute.For<IHackComputeBitsAssembler>();
            computeBitsAssembler.AssembleComputeBits(Arg.Any<string>()).Returns("0000000");
            var assembler = new HackComputeInstructionAssembler(computeBitsAssembler);

            // act
            string[] result = assembler.AssembleInstruction(instruction);
            string line = result[0];

            // assert 
            Assert.AreEqual(16, line.Length);
        }

        [Test]
        public void AssembleInstruction_Returns_Line_Starts_With_Three_Digits()
        {
            // arrange
            var instruction = new ComputeInstruction(ComputeDestinationType.None, "D+1", ComputeJumpType.None);
            var computeBitsAssembler = Substitute.For<IHackComputeBitsAssembler>();
            computeBitsAssembler.AssembleComputeBits(Arg.Any<string>()).Returns("0000000");
            var assembler = new HackComputeInstructionAssembler(computeBitsAssembler);

            // act
            string[] result = assembler.AssembleInstruction(instruction);
            string line = result[0];

            // assert 
            Assert.AreEqual("111", line.Substring(0, 3));
        }

        [TestCase(ComputeJumpType.None, "1110000000000000")]
        [TestCase(ComputeJumpType.JGT, "1110000000000001")]
        [TestCase(ComputeJumpType.JEQ, "1110000000000010")]
        [TestCase(ComputeJumpType.JGE, "1110000000000011")]
        [TestCase(ComputeJumpType.JLT, "1110000000000100")]
        [TestCase(ComputeJumpType.JNE, "1110000000000101")]
        [TestCase(ComputeJumpType.JLE, "1110000000000110")]
        [TestCase(ComputeJumpType.JMP, "1110000000000111")]
        public void AssembleInstruction_Returns_Correct_Jump_Bits(ComputeJumpType jumpType, string expected)
        {
            // arrange
            var instruction = new ComputeInstruction(ComputeDestinationType.None, "D&A", jumpType);
            var computeBitsAssembler = Substitute.For<IHackComputeBitsAssembler>();
            computeBitsAssembler.AssembleComputeBits(Arg.Any<string>()).Returns("0000000");
            var assembler = new HackComputeInstructionAssembler(computeBitsAssembler);

            // act
            string[] result = assembler.AssembleInstruction(instruction);
            string line = result[0];

            // assert
            Assert.AreEqual(expected, line);
        }

        [TestCase(ComputeDestinationType.None, "1110000000000000")]
        [TestCase(ComputeDestinationType.Memory, "1110000000001000")]
        [TestCase(ComputeDestinationType.DataRegister, "1110000000010000")]
        [TestCase(ComputeDestinationType.Memory | ComputeDestinationType.DataRegister, "1110000000011000")]
        [TestCase(ComputeDestinationType.AddressRegister, "1110000000100000")]
        [TestCase(ComputeDestinationType.AddressRegister | ComputeDestinationType.Memory, "1110000000101000")]
        [TestCase(ComputeDestinationType.AddressRegister | ComputeDestinationType.DataRegister, "1110000000110000")]
        [TestCase(ComputeDestinationType.AddressRegister | ComputeDestinationType.Memory | ComputeDestinationType.DataRegister, "1110000000111000")]
        public void AssembleInstruction_Returns_Correct_Dest_Bits(ComputeDestinationType destinationType, string expected)
        {
            // arrange
            var instruction = new ComputeInstruction(destinationType, "D&A", ComputeJumpType.None);
            var computeBitsAssembler = Substitute.For<IHackComputeBitsAssembler>();
            computeBitsAssembler.AssembleComputeBits(Arg.Any<string>()).Returns("0000000");
            var assembler = new HackComputeInstructionAssembler(computeBitsAssembler);

            // act
            string[] result = assembler.AssembleInstruction(instruction);
            string line = result[0];

            // assert
            Assert.AreEqual(expected, line);
        }
    }
}