namespace Assembler.Instructions
{
    public interface IInstruction
    {
        T Accept<T>(IInstructionVisitor<T> instructionVisitor);
    }
}