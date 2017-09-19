using Assembler.Instructions;

namespace Assembler.SymbolResolution
{
    public class IsVariableVisitor : IInstructionVisitor<bool>
    {
        public bool VisitInstruction(UnknownInstruction instruction)
        {
            return false;
        }

        public bool VisitInstruction(AddressInstruction instruction)
        {
            return false;
        }

        public bool VisitInstruction(ComputeInstruction instruction)
        {
            return false;
        }

        public bool VisitInstruction(LabelInstruction instruction)
        {
            return false;
        }

        public bool VisitInstruction(VariableInstruction instruction)
        {
            return true;
        }
    }
}