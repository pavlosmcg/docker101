using System.Text.RegularExpressions;

namespace Assembler.Parsing
{
    public class SymbolParser : ISymbolParser
    {
        public string ParseLabel(string label)
        {
            const string pattern = @"^([A-Za-z_\.\$:][\w\.\$:]*)$";
            Match match = new Regex(pattern).Match(label);

            if (!match.Success)
                return null;

            return match.Groups[1].Value;
        }
    }
}