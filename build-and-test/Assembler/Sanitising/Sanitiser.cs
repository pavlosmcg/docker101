using System.Linq;

namespace Assembler.Sanitising
{
    public class Sanitiser : ISanitiser
    {
        private readonly IWhitespaceRemover _whitespaceRemover;
        private readonly ICommentRemover _commentRemover;

        public Sanitiser(IWhitespaceRemover whitespaceRemover, ICommentRemover commentRemover)
        {
            _whitespaceRemover = whitespaceRemover;
            _commentRemover = commentRemover;
        }

        public string[] Sanitise(string[] lines)
        {
            return lines
                .Select(l => _whitespaceRemover.RemoveWhiteSpace(l))
                .Select(l => _commentRemover.RemoveComments(l))
                .Where(l => !string.IsNullOrEmpty(l)).ToArray();
        }
    }
}