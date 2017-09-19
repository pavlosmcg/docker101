using Assembler.Instructions;

namespace Assembler.Binary
{
    public interface IBinaryAssembler
    {
        string[] AssembleInstruction(IInstruction instruction);
    }
}