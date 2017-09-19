using Assembler.Instructions;

namespace Assembler.Binary
{
    public interface IInstructionAssembler<T>
    {
        string[] AssembleInstruction(T instruction);
    }
}