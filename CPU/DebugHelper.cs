using System.Diagnostics;
using CPU;
using System;

namespace CPU
{

    internal static class DebugPrinter
    {
        public static void DebugPrint(int opcode, Cpu instance, string message = "")
        {
            Debug.WriteLine("Instruction: {0}\nPC: {1}", opcode, instance.Pc);
            Debug.WriteLine(message + '\n');
        }
    }

}