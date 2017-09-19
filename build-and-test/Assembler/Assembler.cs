using System.Linq;
using Assembler.Binary;
using Assembler.Instructions;
using Assembler.Parsing;
using Assembler.Sanitising;
using Assembler.SymbolResolution;

namespace Assembler
{
    public class Assembler : IAssembler
    {
        private readonly ISanitiser _sanitiser;
        private readonly IInstructionParser _instructionParser;
        private readonly IBinaryAssembler _binaryAssembler;
        private readonly ISymbolResolver _symbolResolver;

        public Assembler(ISanitiser sanitiser, IInstructionParser instructionParser, IBinaryAssembler binaryAssembler, ISymbolResolver symbolResolver)
        {
            _sanitiser = sanitiser;
            _instructionParser = instructionParser;
            _binaryAssembler = binaryAssembler;
            _symbolResolver = symbolResolver;
        }

        public string[] Assemble(string[] lines)
        {
            // get the array of lines cleaned up before parsing
            string[] cleanLines = _sanitiser.Sanitise(lines);

            // parse each line into its instruction type
            IInstruction[] instructions = cleanLines.Select(l => _instructionParser.ParseInstruction(l)).ToArray();

            // resolve symbolic instructions
            IInstruction[] resolvedInstructions = _symbolResolver.ResolveSymbolicInstructions(instructions).ToArray();

            // assemble each instruction into binary line(s)
            string[] assembledLines = resolvedInstructions.SelectMany(i => _binaryAssembler.AssembleInstruction(i)).ToArray();

            return assembledLines.ToArray();
        }
    }
}