using System;
using System.Text;
using Assembler.Instructions;

namespace Assembler.Binary.Hack
{
    public class HackComputeInstructionAssembler : IInstructionAssembler<ComputeInstruction>
    {
        private readonly IHackComputeBitsAssembler _computeBitsAssembler;

        public HackComputeInstructionAssembler(IHackComputeBitsAssembler computeBitsAssembler)
        {
            _computeBitsAssembler = computeBitsAssembler;
        }

        public string[] AssembleInstruction(ComputeInstruction instruction)
        {
            // Hack instructions are only ever 16-bits long
            var builder = new StringBuilder(16, 16);

            // set compute instruction marker bits
            builder.Append("111");
            
            // set computation bits for ALU
            try
            {
                builder.Append(_computeBitsAssembler.AssembleComputeBits(instruction.Computation));
            }
            catch (Exception)
            {
                return new[] { string.Format("Error: Invalid Hack computation '{0}'", instruction.Computation) };
            }
            
            // set dest bits
            builder.Append(Convert.ToString((byte)instruction.DestinationType, 2).PadLeft(3, '0'));

            // set jump bits
            builder.Append(Convert.ToString((byte)instruction.JumpType, 2).PadLeft(3, '0'));

            return new[] { builder.ToString() };
        }
    }
}