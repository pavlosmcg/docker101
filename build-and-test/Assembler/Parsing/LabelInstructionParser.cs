using System.Text.RegularExpressions;
using Assembler.Instructions;

namespace Assembler.Parsing
{
    public class LabelInstructionParser : IInstructionParser
    {
        private readonly IInstructionParser _nextParser;
        private readonly ISymbolParser _labelParser;

        public LabelInstructionParser(IInstructionParser nextParser, ISymbolParser labelParser)
        {
            _nextParser = nextParser;
            _labelParser = labelParser;
        }

        public IInstruction ParseInstruction(string line)
        {
            const string pattern = @"^\((.+)\)$";
            Match match = new Regex(pattern, RegexOptions.IgnoreCase).Match(line);

            if (!match.Success)
                return _nextParser.ParseInstruction(line);

            var parsedLabel = _labelParser.ParseLabel(match.Groups[1].Value);
            if (parsedLabel == null)
                return _nextParser.ParseInstruction(line);

            return new LabelInstruction(parsedLabel);
        }
    }
}