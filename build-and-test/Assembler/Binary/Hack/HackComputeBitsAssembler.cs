using System;
using System.Collections.Generic;

namespace Assembler.Binary.Hack
{
    public class HackComputeBitsAssembler : IHackComputeBitsAssembler
    {
        private readonly IDictionary<string, string> _hackComputationsAliases = new Dictionary<string, string>
            {
                {"-0", "0"},
                {"+0", "0"},
                {"+1", "1"},
                {"+A", "A"},
                {"+D", "D"},
                {"+M", "M"},
                {"A+0", "A"},
                {"0+A", "A"},
                {"M+0", "M"},
                {"0+M", "M"},
                {"D+0", "D"},
                {"0+D", "D"},
                {"M+D", "D+M"},
                {"M&D", "D&M"},
                {"M|D", "D|M"},
                {"A+D", "D+A"},
                {"A&D", "D&A"},
                {"A|D", "D|A"},
                {"1+D", "D+1"},
                {"1+A", "A+1"},
                {"1+M", "M+1"},
            };

        private readonly IDictionary<string, string> _hackComputations = new Dictionary<string, string>
            {
                {"0",   "0101010"},
                {"1",   "0111111"},
                {"-1",  "0111010"},
                {"D",   "0001100"},
                {"A",   "0110000"},
                {"!D",  "0001101"},
                {"!A",  "0110001"},
                {"-D",  "0001111"},
                {"-A",  "0110011"},
                {"D+1", "0011111"},
                {"A+1", "0110111"},
                {"D-1", "0001110"},
                {"A-1", "0110010"},
                {"D+A", "0000010"},
                {"D-A", "0010011"},
                {"A-D", "0000111"},
                {"D&A", "0000000"},
                {"D|A", "0010101"},
                {"M",   "1110000"},
                {"!M",  "1110001"},
                {"-M",  "1110011"},
                {"M+1", "1110111"},
                {"M-1", "1110010"},
                {"D+M", "1000010"},
                {"D-M", "1010011"},
                {"M-D", "1000111"},
                {"D&M", "1000000"},
                {"D|M", "1010101"}
            };

        public string AssembleComputeBits(string input)
        {
            string hackComp;
            if (!_hackComputationsAliases.TryGetValue(input, out hackComp))
                hackComp = input;

            string output;
            if (!_hackComputations.TryGetValue(hackComp, out output))
                throw new ArgumentException("Invalid Hack computation", input);

            return output;
        }
    }
}