using System;
using System.Diagnostics;

namespace CPU
{
    /// <summary>
    ///     A helper class for debugging
    /// </summary>
    internal static class DebugHelper
    {
        /// <summary>
        ///     A method that prints information about the state of the program
        /// </summary>
        /// <param name="opcode">The opcode currently being evaluated</param>
        /// <param name="instance">The current CPU instance</param>
        /// <param name="message">A string containing any arbitrary data the programmer wants displayed</param>
        public static void DebugPrint(string opcode, Cpu cpu, string message = "")
        {
            Debug.WriteLine("PC: {1}\nInstruction: {0}", opcode, cpu.Pc);
            Debug.WriteLine(message + '\n');
        }

        public static void Die(Error err, string msg = "")
        {
            Console.Error.WriteLine($"Failed with error code {err}");
            Console.Error.WriteLine(msg);
            Environment.Exit((int)err);
        }
    }
}