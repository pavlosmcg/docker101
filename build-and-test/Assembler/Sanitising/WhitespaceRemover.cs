using System.Text.RegularExpressions;

namespace Assembler.Sanitising
{
    public class WhitespaceRemover : IWhitespaceRemover
    {
        public string RemoveWhiteSpace(string line)
        {
            return Regex.Replace(line, @"\s+", "");
        }
    }
}