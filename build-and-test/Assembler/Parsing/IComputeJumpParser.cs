using Assembler.Instructions;

namespace Assembler.Parsing
{
    public interface IComputeJumpParser
    {
        ComputeJumpType ParseComputeJump(string jump);
    }
}