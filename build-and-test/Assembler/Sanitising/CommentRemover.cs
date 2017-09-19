using System;

namespace Assembler.Sanitising
{
    public class CommentRemover : ICommentRemover
    {
        public string RemoveComments(string line)
        {
            return line.Split(new[] { "//" }, StringSplitOptions.None)[0];
        }
    }
}