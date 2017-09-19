using System;
using Assembler.Binary;
using Assembler.Instructions;
using NSubstitute;
using NUnit.Framework;

namespace Assembler.Tests.Binary
{
    [TestFixture]
    public class HackInstructionParserTests
    {
        [Test]
        public void VisitUnknownInstruction_Always_Returns_Error_Text()
        {
            // arrange
            var computeAssembler = Substitute.For<IInstructionAssembler<ComputeInstruction>>();
            var addressAssembler = Substitute.For<IInstructionAssembler<AddressInstruction>>();
            IInstructionVisitor<string[]> visitor = new AssemblyInstructionVisitor(computeAssembler, addressAssembler);
            const string inputLine = "MORE CLASH!";
            var instruction = new UnknownInstruction(inputLine);

            // act
            string[] result = visitor.VisitInstruction(instruction);

            // assert 
            Assert.AreEqual(1, result.Length);
            Assert.IsTrue(result[0].Contains("Error"));
            Assert.IsTrue(result[0].Contains(inputLine));
        }

        [Test] //TODO get rid of these useless tests
        public void VisitAddressInstruction_Delegates_To_AddressInstructionAssembler()
        {
            // arrange
            var computeAssembler = Substitute.For<IInstructionAssembler<ComputeInstruction>>();
            var addressAssembler = Substitute.For<IInstructionAssembler<AddressInstruction>>();
            IInstructionVisitor<string[]> visitor = new AssemblyInstructionVisitor(computeAssembler, addressAssembler);
            var instruction = new AddressInstruction(1234);

            // act
            visitor.VisitInstruction(instruction);

            // assert 
            addressAssembler.Received().AssembleInstruction(Arg.Is(instruction));
        }

        [Test]
        public void VisitComputeInstruction_Delegates_To_ComputeInstructionAssembler()
        {
            // arrange
            var computeAssembler = Substitute.For<IInstructionAssembler<ComputeInstruction>>();
            var addressAssembler = Substitute.For<IInstructionAssembler<AddressInstruction>>();
            IInstructionVisitor<string[]> visitor = new AssemblyInstructionVisitor(computeAssembler, addressAssembler);
            var instruction = new ComputeInstruction(ComputeDestinationType.Memory, "A-D", ComputeJumpType.JMP);

            // act
            visitor.VisitInstruction(instruction);

            // assert 
            computeAssembler.Received().AssembleInstruction(Arg.Is(instruction));
        }

        [Test]
        public void VisitLabelInstruction_Throws_NotSupportedException()
        {
            // arrange
            var computeAssembler = Substitute.For<IInstructionAssembler<ComputeInstruction>>();
            var addressAssembler = Substitute.For<IInstructionAssembler<AddressInstruction>>();
            IInstructionVisitor<string[]> visitor = new AssemblyInstructionVisitor(computeAssembler, addressAssembler);
            var instruction = new LabelInstruction("SOME_LABEL");

            // act
            TestDelegate testDelegate = () => visitor.VisitInstruction(instruction);
            
            // assert
            Assert.Throws<NotSupportedException>(testDelegate);
        }

        [Test]
        public void VisitVariableInstruction_Throws_NotSupportedException()
        {
            // arrange
            var computeAssembler = Substitute.For<IInstructionAssembler<ComputeInstruction>>();
            var addressAssembler = Substitute.For<IInstructionAssembler<AddressInstruction>>();
            IInstructionVisitor<string[]> visitor = new AssemblyInstructionVisitor(computeAssembler, addressAssembler);
            var instruction = new VariableInstruction("some.value");

            // act
            TestDelegate testDelegate = () => visitor.VisitInstruction(instruction);

            // assert
            Assert.Throws<NotSupportedException>(testDelegate);
        }
    }
}