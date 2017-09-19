namespace Assembler.Instructions
{
    public interface IInstructionVisitor<T>
    {
        T VisitInstruction(UnknownInstruction instruction);
        T VisitInstruction(AddressInstruction instruction);
        T VisitInstruction(ComputeInstruction instruction);
        T VisitInstruction(LabelInstruction instruction);
        T VisitInstruction(VariableInstruction instruction);
    }
}