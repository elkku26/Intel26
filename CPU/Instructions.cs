using static CPU.InstructionSet;
using static CPU.DebugPrinter;

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
            DebugPrint(NOP, instance);
        }
        
        internal static void Rnz(Cpu instance)
        {
            DebugPrint(RNZ, instance);
        }
        

    }
}