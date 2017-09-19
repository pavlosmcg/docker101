using System.Collections.Generic;
using Assembler.Instructions;

namespace Assembler.SymbolResolution
{
    public interface ILabelResolver
    {
        /// <summary>
        /// Runs through label instructions, removing them when they've been added to the symbol table.
        /// </summary>
        IEnumerable<IInstruction> ResolveLabels(IDictionary<string, int> symbolTable, IEnumerable<IInstruction> instructions);
    }
}