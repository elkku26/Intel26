using System;
using System.Diagnostics;

namespace CPU
{



    struct InstructionSet
    {
        public const int Nop = 0x00;
    }

    /// <summary>
    /// Holds all of the possible instructions
    /// </summary>
    public class Instructions
    {

        internal static void Nop(Cpu instance)
        {
            DebugPrinter.DebugPrint(0, "Tere");
        }

    }
}