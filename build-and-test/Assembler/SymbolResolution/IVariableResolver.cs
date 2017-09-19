using System.Collections.Generic;
using Assembler.Instructions;

namespace Assembler.SymbolResolution
{
    public interface IVariableResolver
    {
        /// <summary>
        /// Runs through variables, resolving to address instructions after adding them to the symbol table if not already there
        /// </summary>
        IEnumerable<IInstruction> ResolveVariables(IDictionary<string, int> symbolTable, IEnumerable<IInstruction> instructions);
    }
}