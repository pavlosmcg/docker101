using System;
using System.Collections.Generic;
using System.Linq;
using Assembler.Instructions;

namespace Assembler.SymbolResolution.Hack
{
    public class HackLabelResolver : ILabelResolver
    {
        private readonly IInstructionVisitor<bool> _isLabelVisitor;

        public HackLabelResolver(IInstructionVisitor<bool> isLabelVisitor)
        {
            _isLabelVisitor = isLabelVisitor;
        }

        public IEnumerable<IInstruction> ResolveLabels(IDictionary<string, int> symbolTable, IEnumerable<IInstruction> instructions)
        {
            // figure out whether it's a label
            // if it's a label
            // -- if it doesn't exist add it to the table and remove the instruction
            // -- if it does already exist, change it to an unknown instruction
            var lineNumber = 0;
            foreach (var instruction in instructions)
            {
                if (instruction.Accept(_isLabelVisitor))
                {
                    var labelInstruction = (LabelInstruction)instruction;
                    if (!symbolTable.ContainsKey(labelInstruction.Label))
                    {
                        symbolTable.Add(labelInstruction.Label, lineNumber);
                        continue;
                    }
                    yield return new UnknownInstruction(String.Format("Label '{0}' is not allowed or is defined more than once", labelInstruction.Label));
                }
                else
                {
                    yield return instruction;
                    lineNumber++;
                }
            }
        }
    }
}