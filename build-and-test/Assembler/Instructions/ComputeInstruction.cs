using System;

namespace Assembler.Instructions
{
    public struct ComputeInstruction : IInstruction, IEquatable<ComputeInstruction>
    {
        private readonly ComputeDestinationType _destinationType;
        private readonly string _computation;
        private readonly ComputeJumpType _jumpType;

        public ComputeInstruction(ComputeDestinationType destinationType, string computation, ComputeJumpType jumpType = ComputeJumpType.None)
        {
            _destinationType = destinationType;
            _computation = computation;
            _jumpType = jumpType;
        }

        public ComputeDestinationType DestinationType
        {
            get { return _destinationType; }
        }

        public string Computation
        {
            get { return _computation; }
        }

        public ComputeJumpType JumpType
        {
            get { return _jumpType; }
        }

        public T Accept<T>(IInstructionVisitor<T> instructionVisitor)
        {
            return instructionVisitor.VisitInstruction(this);
        }

        public bool Equals(ComputeInstruction other)
        {
            if (other.DestinationType != DestinationType)
                return false;

            if (other.Computation != Computation)
                return false;

            if (other.JumpType != JumpType)
                return false;

            return true;
        }
    }
}