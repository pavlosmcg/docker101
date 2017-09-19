namespace Assembler.Instructions
{
    public enum ComputeJumpType: byte
    {
        None = 0x0,
        JGT = 0x1,
        JEQ = 0x2,
        JGE = 0x3,
        JLT = 0x4,
        JNE = 0x5,
        JLE = 0x6,
        JMP = 0x7
    }
}