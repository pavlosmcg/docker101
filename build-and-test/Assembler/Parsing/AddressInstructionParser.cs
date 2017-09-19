using Assembler.Instructions;

namespace Assembler.Parsing
{
    public class AddressInstructionParser : IInstructionParser
    {
        private readonly IInstructionParser _nextParser;

        public AddressInstructionParser(IInstructionParser nextParser)
        {
            _nextParser = nextParser;
        }

        public IInstruction ParseInstruction(string line)
        {
            if (!line.StartsWith("@"))
                return _nextParser.ParseInstruction(line);

            int address;
            if (!int.TryParse(line.Substring(1), out address))
                return _nextParser.ParseInstruction(line);

            return new AddressInstruction(address);
        }
    }
}