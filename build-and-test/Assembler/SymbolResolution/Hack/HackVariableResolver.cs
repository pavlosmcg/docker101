using System.Collections.Generic;
using System.Linq;
using Assembler.Instructions;

namespace Assembler.SymbolResolution.Hack
{
    public class HackVariableResolver : IVariableResolver
    {
        private readonly IInstructionVisitor<bool> _isVariableVisitor;

        public HackVariableResolver(IInstructionVisitor<bool> isVariableVisitor)
        {
            _isVariableVisitor = isVariableVisitor;
        }

        public IEnumerable<IInstruction> ResolveVariables(IDictionary<string, int> symbolTable, IEnumerable<IInstruction> instructions)
        {
            var variableCounter = 15;

            foreach (var instruction in instructions)
            {
                if (instruction.Accept(_isVariableVisitor))
                {
                    var variableInstruction = (VariableInstruction) instruction;
                    int address;

                    // if it's not in the symbol table, add the variable
                    if (!symbolTable.TryGetValue(variableInstruction.Label, out address))
                    {
                        address = ++variableCounter; // get the next variable address
                        symbolTable.Add(variableInstruction.Label, address);
                    }

                    // convert this variable instruction into a resolved address instruction
                    yield return new AddressInstruction(address);
                }
                else
                {
                    yield return instruction;    
                }
            }
        }
    }
}