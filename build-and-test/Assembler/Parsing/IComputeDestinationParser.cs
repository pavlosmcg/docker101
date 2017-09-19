using Assembler.Instructions;

namespace Assembler.Parsing
{
    public interface IComputeDestinationParser
    {
        ComputeDestinationType ParseComputeDestination(string destination);
    }
}