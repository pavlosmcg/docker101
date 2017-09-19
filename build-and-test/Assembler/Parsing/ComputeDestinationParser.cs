using System;
using System.Linq;
using Assembler.Instructions;

namespace Assembler.Parsing
{
    public class ComputeDestinationParser : IComputeDestinationParser
    {
        public ComputeDestinationType ParseComputeDestination(string destination)
        {
            var destinationType = ComputeDestinationType.None;

            if (string.IsNullOrEmpty(destination))
                return destinationType;

            if (destination.Count(c => !(c == 'A' || c == 'M' || c == 'D')) > 0)
                throw new ArgumentException("Cannot parse compute instruction destination", destination);

            if (destination.Contains("A"))
                destinationType |= ComputeDestinationType.AddressRegister;

            if (destination.Contains("M"))
                destinationType |= ComputeDestinationType.Memory;

            if (destination.Contains("D"))
                destinationType |= ComputeDestinationType.DataRegister;

            return destinationType;
        }
    }
}