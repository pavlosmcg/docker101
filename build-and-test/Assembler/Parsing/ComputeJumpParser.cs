using System;
using Assembler.Instructions;

namespace Assembler.Parsing
{
    public class ComputeJumpParser : IComputeJumpParser
    {
        public ComputeJumpType ParseComputeJump(string jump)
        {
            if (string.IsNullOrEmpty(jump))
                return ComputeJumpType.None;

            ComputeJumpType jumpType;
            if (Enum.TryParse(jump, out jumpType))
            {
                return jumpType;
            }

            throw new ArgumentException("Cannot parse compute instruct jump command", jump);
        }
    }
}