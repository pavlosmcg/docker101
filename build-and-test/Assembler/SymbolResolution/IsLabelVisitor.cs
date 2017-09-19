using Assembler.Instructions;

namespace Assembler.SymbolResolution
{
    public class IsLabelVisitor : IInstructionVisitor<bool>
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
            return true;
        }

        public bool VisitInstruction(VariableInstruction instruction)
        {
            return false;
        }
    }
}