using Assembler.Instructions;

namespace Assembler.Parsing
{
    public class UnknownInstructionParser : IInstructionParser
    {
        public IInstruction ParseInstruction(string line)
        {
            return new UnknownInstruction(line);
        }
    }
}