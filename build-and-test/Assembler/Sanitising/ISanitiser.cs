namespace Assembler.Sanitising
{
    public interface ISanitiser
    {
        string[] Sanitise(string[] lines);
    }
}