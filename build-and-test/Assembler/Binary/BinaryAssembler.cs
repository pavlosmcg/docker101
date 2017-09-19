using Assembler.Instructions;

namespace Assembler.Binary
{
    public class BinaryAssembler : IBinaryAssembler
    {
        private readonly IInstructionVisitor<string[]> _instructionVisitor;

        public BinaryAssembler(IInstructionVisitor<string[]> instructionVisitor)
        {
            _instructionVisitor = instructionVisitor;
        }

        public string[] AssembleInstruction(IInstruction instruction)
        {
            return instruction.Accept(_instructionVisitor);
        }
    }
}