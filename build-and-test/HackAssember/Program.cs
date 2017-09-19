using System;
using System.Reflection;
using Assembler;
using Ninject;

namespace HackAssember
{
    class Program
    {
        static void Main(string[] args)
        {
            // check for correct command line args
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please specifiy a file path");
                return;
            }

            // ninject set up
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Load(Assembly.GetExecutingAssembly());
            var assember = ninjectKernel.Get<IAssembler>();

            // do the assembling
            string[] lines = System.IO.File.ReadAllLines(args[0]);
            string[] assembledLines = assember.Assemble(lines);


            foreach (var line in assembledLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
