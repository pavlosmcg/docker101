using System;

namespace Assembler.Instructions
{
    public struct UnknownInstruction : IInstruction, IEquatable<UnknownInstruction>
    {
        private readonly string _line;

        public UnknownInstruction(string line)
        {
            _line = line;
        }

        public string Line
        {
            get { return _line; }
        }

        public T Accept<T>(IInstructionVisitor<T> instructionVisitor)
        {
            return instructionVisitor.VisitInstruction(this);
        }

        public bool Equals(UnknownInstruction other)
        {
            return string.Equals(other.Line, Line);
        }
    }
}