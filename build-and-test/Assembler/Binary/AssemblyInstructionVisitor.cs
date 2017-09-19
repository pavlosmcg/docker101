using System;
using Assembler.Instructions;

namespace Assembler.Binary
{
    public class AssemblyInstructionVisitor : IInstructionVisitor<string[]>
    {
        private readonly IInstructionAssembler<ComputeInstruction> _computeAssembler;
        private readonly IInstructionAssembler<AddressInstruction> _addressAssembler;

        public AssemblyInstructionVisitor(IInstructionAssembler<ComputeInstruction> computeAssembler, IInstructionAssembler<AddressInstruction> addressAssembler)
        {
            _computeAssembler = computeAssembler;
            _addressAssembler = addressAssembler;
        }

        public string[] VisitInstruction(UnknownInstruction instruction)
        {
            return new[] { string.Format("Error: Unknown instruction '{0}'", instruction.Line) };
        }

        public string[] VisitInstruction(AddressInstruction instruction)
        {
            return _addressAssembler.AssembleInstruction(instruction);
        }

        public string[] VisitInstruction(ComputeInstruction instruction)
        {
            return _computeAssembler.AssembleInstruction(instruction);
        }

        public string[] VisitInstruction(LabelInstruction instruction)
        {
            throw new NotSupportedException("Symbols need to be resolved before they can be assembled");
        }

        public string[] VisitInstruction(VariableInstruction instruction)
        {
            throw new NotSupportedException("Symbols need to be resolved before they can be assembled");
        }
    }
}