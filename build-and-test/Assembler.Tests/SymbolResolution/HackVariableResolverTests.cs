using System.Collections.Generic;
using Assembler.Instructions;
using Assembler.SymbolResolution;
using Assembler.SymbolResolution.Hack;
using NUnit.Framework;
using System.Linq;

namespace Assembler.Tests.SymbolResolution
{
    [TestFixture]
    public class HackVariableResolverTests
    {
        [Test]
        public void ResolveVariables_Adds_Variables_To_Symbol_Table()
        {
            // arrange
            IDictionary<string, int> symbolTable = new Dictionary<string, int>
            {
                // resolved labels should already be in symbol table
                {"LOOP", 4},
                {"END", 18}
            };
            IInstruction[] instructions =
                {
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "1"),
                    new VariableInstruction("sum"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "0"),
                    // LOOP label was here
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new AddressInstruction(100),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "D-A"),
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "D", ComputeJumpType.JGT),
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new VariableInstruction("sum"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "M+1"),
                    new VariableInstruction("LOOP"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                    // END label was here
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                };
            var variableResolver = new HackVariableResolver(new IsVariableVisitor());

            // act
            variableResolver.ResolveVariables(symbolTable, instructions).ToArray();

            // assert
            Assert.AreEqual(4, symbolTable.Count);
            Assert.AreEqual(16, symbolTable["i"]);
            Assert.AreEqual(17, symbolTable["sum"]);
        }

        [Test]
        public void ResolveVariables_Converts_Variable_Instructions_To_Address_Instructions()
        {
            // arrange
            IDictionary<string, int> symbolTable = new Dictionary<string, int>
            {
                // resolved labels should already be in symbol table
                {"LOOP", 4},
                {"END", 18}
            };
            IInstruction[] instructions =
                {
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "1"),
                    new VariableInstruction("sum"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "0"),
                    // LOOP label was here
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new AddressInstruction(100),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "D-A"),
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "D", ComputeJumpType.JGT),
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new VariableInstruction("sum"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "M+1"),
                    new VariableInstruction("LOOP"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                    // END label was here
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                };
            var variableResolver = new HackVariableResolver(new IsVariableVisitor());
            var initialVariableInstructions = instructions.Count(i => i is VariableInstruction);
            var initialAddressInstructions = instructions.Count(i => i is AddressInstruction);

            // act
            var resolvedInstructions = variableResolver.ResolveVariables(symbolTable, instructions).ToArray();

            // assert
            Assert.AreEqual(0, resolvedInstructions.Count(i => i is VariableInstruction));
            int numAddressInstructions = initialAddressInstructions + initialVariableInstructions;
            Assert.AreEqual(numAddressInstructions,
                            resolvedInstructions.Count(i => i is AddressInstruction));
            Assert.AreEqual(16, ((AddressInstruction)resolvedInstructions[0]).Address);
            Assert.AreEqual(17, ((AddressInstruction)resolvedInstructions[2]).Address);
        }

        [Test]
        public void ResolveVariables_Uses_Correct_Addresses_For_Labels()
        {
            // arrange
            IDictionary<string, int> symbolTable = new Dictionary<string, int>
            {
                // resolved labels should already be in symbol table
                {"LOOP", 4},
                {"END", 18}
            };
            IInstruction[] instructions =
                {
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "1"),
                    new VariableInstruction("sum"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "0"),
                    // LOOP label was here
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new AddressInstruction(100),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "D-A"),
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "D", ComputeJumpType.JGT),
                    new VariableInstruction("i"),
                    new ComputeInstruction(ComputeDestinationType.DataRegister, "M"),
                    new VariableInstruction("sum"),
                    new ComputeInstruction(ComputeDestinationType.Memory, "M+1"),
                    new VariableInstruction("LOOP"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                    // END label was here
                    new VariableInstruction("END"),
                    new ComputeInstruction(ComputeDestinationType.None, "0", ComputeJumpType.JMP),
                };
            var variableResolver = new HackVariableResolver(new IsVariableVisitor());
            
            // act
            var resolvedInstructions = variableResolver.ResolveVariables(symbolTable, instructions).ToArray();

            // assert
            Assert.AreEqual(18, ((AddressInstruction)resolvedInstructions[8]).Address);
            Assert.AreEqual(4, ((AddressInstruction)resolvedInstructions[14]).Address);
            Assert.AreEqual(18, ((AddressInstruction)resolvedInstructions[16]).Address);
        }
    }
}