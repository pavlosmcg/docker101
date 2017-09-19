using Assembler.Instructions;

namespace Assembler.Parsing
{
    public interface IInstructionParser
    {
        /// <returns>An instruction struct of the type in which this parser specialises.</returns>
        IInstruction ParseInstruction(string line);
    }
}