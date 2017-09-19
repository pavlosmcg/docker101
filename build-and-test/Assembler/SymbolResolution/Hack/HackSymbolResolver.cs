using System.Collections.Generic;
using System.Linq;
using Assembler.Instructions;

namespace Assembler.SymbolResolution.Hack
{
    public class HackSymbolResolver : ISymbolResolver
    {
        private readonly ILabelResolver _hackLabelResolver;
        private readonly IVariableResolver _hackVariableResolver;

        private readonly IDictionary<string, int> _symbolTable = new Dictionary<string, int>()
            {
                {"SP", 0},
                {"LCL", 1},
                {"ARG", 2},
                {"THIS", 3},
                {"THAT", 4},
                {"R0", 0},
                {"R1", 1},
                {"R2", 2},
                {"R3", 3},
                {"R4", 4},
                {"R5", 5},
                {"R6", 6},
                {"R7", 7},
                {"R8", 8},
                {"R9", 9},
                {"R10", 10},
                {"R11", 11},
                {"R12", 12},
                {"R13", 13},
                {"R14", 14},
                {"R15", 15},
                {"SCREEN", 16384},
                {"KBD", 24576}
            };

        public HackSymbolResolver(ILabelResolver hackLabelResolver, IVariableResolver hackVariableResolver)
        {
            _hackLabelResolver = hackLabelResolver;
            _hackVariableResolver = hackVariableResolver;
        }

        public IEnumerable<IInstruction> ResolveSymbolicInstructions(IInstruction[] instructions)
        {
            // must go through labels first, removing them when they've been added to the symbol table
            var linesWithLabelsRemoved = _hackLabelResolver.ResolveLabels(_symbolTable, instructions).ToArray();

            // then go through variables, resolving them after adding to table if not already there
            return _hackVariableResolver.ResolveVariables(_symbolTable, linesWithLabelsRemoved).ToArray();
        }
    }
}