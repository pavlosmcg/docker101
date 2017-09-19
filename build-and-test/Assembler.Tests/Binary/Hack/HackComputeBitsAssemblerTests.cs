using System;
using Assembler.Binary.Hack;
using NUnit.Framework;

namespace Assembler.Tests.Binary.Hack
{
    [TestFixture]
    public class HackComputeBitsAssemblerTests
    {
        [Test]
        public void AssembleComputeBits_Returns_7_Compute_Bits()
        {
            // arrange
            const string input = "D&A";
            var computeBitAssembler = new HackComputeBitsAssembler();

            // act
            string result = computeBitAssembler.AssembleComputeBits(input);

            // assert
            Assert.AreEqual(7, result.Length);
        }

        [TestCase("blorg")]
        [TestCase("2")]
        [TestCase("-2")]
        [TestCase("00")]
        [TestCase("MM")]
        [TestCase("DD")]
        [TestCase("AA")]
        [TestCase("A+")]
        [TestCase("A&")]
        [TestCase("A-")]
        [TestCase("0-1")]
        [TestCase("D++A")]
        [TestCase("M--D")]
        [TestCase("H-L")]
        [TestCase("C+Y")]
        [TestCase("!P")]
        [TestCase("-P")]
        [TestCase("I||F")]
        [TestCase("O|J")]
        [TestCase("T&&Q")]
        [TestCase("R&V")]
        [TestCase("F")]
        public void AssembleComputeBits_Returns_Throws_ArguementException_When_Input_Is_Invalid(string input)
        {
            // arrange
            var computeBitAssembler = new HackComputeBitsAssembler();

            // act
            TestDelegate testDelegate = () => computeBitAssembler.AssembleComputeBits(input);

            // assert
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [TestCase("0", "0101010")]
        [TestCase("1", "0111111")]
        [TestCase("-1", "0111010")]
        [TestCase("D", "0001100")]
        [TestCase("A", "0110000")]
        [TestCase("!D", "0001101")]
        [TestCase("!A", "0110001")]
        [TestCase("-D", "0001111")]
        [TestCase("-A", "0110011")]
        [TestCase("D+1", "0011111")]
        [TestCase("A+1", "0110111")]
        [TestCase("D-1", "0001110")]
        [TestCase("A-1", "0110010")]
        [TestCase("D+A", "0000010")]
        [TestCase("D-A", "0010011")]
        [TestCase("A-D", "0000111")]
        [TestCase("D&A", "0000000")]
        [TestCase("D|A", "0010101")] 
        [TestCase("M","1110000")]
        [TestCase("!M","1110001")]
        [TestCase("-M","1110011")]
        [TestCase("M+1","1110111")]
        [TestCase("M-1","1110010")]
        [TestCase("D+M","1000010")]
        [TestCase("D-M","1010011")]
        [TestCase("M-D","1000111")]
        [TestCase("D&M","1000000")]
        [TestCase("D|M","1010101")]
        public void AssembleComputeBits_Returns_Correct_Comp_Bits(string input, string expected)
        {
            // arrange
            var computeBitAssembler = new HackComputeBitsAssembler();

            // act
            string result = computeBitAssembler.AssembleComputeBits(input);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}