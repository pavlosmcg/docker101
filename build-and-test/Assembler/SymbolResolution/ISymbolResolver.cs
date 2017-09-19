using System.Collections.Generic;
using Assembler.Instructions;

namespace Assembler.SymbolResolution
{
    public interface ISymbolResolver
    {
        IEnumerable<IInstruction> ResolveSymbolicInstructions(IInstruction[] instructions);
    }
}