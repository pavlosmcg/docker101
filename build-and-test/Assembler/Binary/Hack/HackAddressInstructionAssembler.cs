using System;
using System.Text;
using Assembler.Instructions;

namespace Assembler.Binary.Hack
{
    public class HackAddressInstructionAssembler : IInstructionAssembler<AddressInstruction>
    {
        public string[] AssembleInstruction(AddressInstruction instruction)
        {
            // Hack instructions are only ever 16-bits long
            var builder = new StringBuilder(16, 16);

            // set address instruction marker bit
            builder.Append("0");

            // set 15 address location bits
            builder.Append(Convert.ToString(instruction.Address, 2).PadLeft(15, '0'));

            return new[] { builder.ToString() };
        }
    }
}