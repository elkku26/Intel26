using System;
using System.Net;
using static CPU.InstructionSet;

namespace CPU
{

    readonly struct InstructionSet
    {
        public const int NOP = 0x00;
        public const int RNZ = 0xC0;
    }

    /// <summary>
    /// Holds all of the possible instructions
    /// </summary>
    public class Instructions
    {

        internal static void Nop(Cpu instance)
        {
            DebugPrinter.DebugPrint(NOP, instance);
        }
        
        internal static void Rnz(Cpu instance)
        {
            DebugPrinter.DebugPrint(RNZ, instance);
        }
        

    }
}