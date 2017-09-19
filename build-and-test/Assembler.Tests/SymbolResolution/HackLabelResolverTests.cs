using System.Collections.Generic;
using Assembler.Instructions;
using Assembler.SymbolResolution;
using Assembler.SymbolResolution.Hack;
using NUnit.Framework;
using System.Linq;

namespace Assembler.Tests.SymbolResolution
{
    [TestFixture]
    public class HackLabelResolverTests
    {
        [Test]
        public void ResolveLabels_Adds_Label_To_Symbol_Table()
        {
            // arrange
            IDictionary<string, int> symbolTable = new Dictionary<string, int>();
            IInstruction[] instructions =
                {
                    new VariableInstruction("i"), 
                    new ComputeInstruction(ComputeDestinationType.Memory, "1"), 
                    new VariableInstruction("R2"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "0"),
                    new LabelInstruction("LOOP"),
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new VariableInstruction("R0"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "D-M"),
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "D", ComputeJumpType.JGT),
                    new VariableInstruction("R1"), 
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new VariableInstruction("R2"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "M+D"),
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "M+1"),
                    new VariableInstruction("LOOP"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                    new LabelInstruction("END"),
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                }; // mult program - first thing I wrote in assembly language
            var labelResolver = new HackLabelResolver(new IsLabelVisitor());

            // act
            labelResolver.ResolveLabels(symbolTable, instructions).ToArray();

            // assert
            Assert.AreEqual(2, symbolTable.Count);
            Assert.AreEqual(4, symbolTable["LOOP"]);
            Assert.AreEqual(18, symbolTable["END"]);
        }

        [Test]
        public void ResolveLabels_Removes_Label_Instructions()
        {
            // arrange
            IDictionary<string, int> symbolTable = new Dictionary<string, int>();
            IInstruction[] instructions =
                {
                    new VariableInstruction("i"), 
                    new ComputeInstruction(ComputeDestinationType.Memory, "1"), 
                    new VariableInstruction("R2"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "0"),
                    new LabelInstruction("LOOP"),
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new VariableInstruction("R0"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "D-M"),
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "D", ComputeJumpType.JMP),
                    new VariableInstruction("R1"), 
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new VariableInstruction("R2"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "M+D"),
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "M+1"),
                    new VariableInstruction("LOOP"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                    new LabelInstruction("END"),
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP), 
                }; // mult program - first thing I wrote in assembly language
            var labelResolver = new HackLabelResolver(new IsLabelVisitor());

            // act
            IInstruction[] resolvedInstructions = labelResolver.ResolveLabels(symbolTable, instructions).ToArray();

            // assert
            Assert.AreEqual(0, resolvedInstructions.Count(i => i is LabelInstruction));
            Assert.AreEqual(20, resolvedInstructions.Count());
        }

        [Test]
        public void ResolveLabels_Creates_Unknown_Instruction_If_Label_Is_Defined_Twice()
        {
            // arrange
            IDictionary<string, int> symbolTable = new Dictionary<string, int>();
            IInstruction[] instructions =
                {
                    new VariableInstruction("i"), 
                    new ComputeInstruction(ComputeDestinationType.Memory, "1"), 
                    new VariableInstruction("R2"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "0"),
                    new LabelInstruction("LOOP"),
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new VariableInstruction("R0"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "D-M"),
                    new LabelInstruction("LOOP"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                     new VariableInstruction("LOOP"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                    
                };
            var labelResolver = new HackLabelResolver(new IsLabelVisitor());

            // act
            IEnumerable<IInstruction> resolvedInstructions = labelResolver.ResolveLabels(symbolTable, instructions);

            // assert
            Assert.AreEqual(1, resolvedInstructions.Count(i => i is UnknownInstruction));
        }
    }
}