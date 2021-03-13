using System.Diagnostics;

namespace CPU
{

    /// <summary>
    ///     A helper class for debugging
    /// </summary>
    internal static class DebugPrinter
    {
        /// <summary>
        ///     A method that prints information about the state of the program
        /// </summary>
        /// <param name="opcode">The opcode currently being evaluated</param>
        /// <param name="instance">The current CPU instance</param>
        /// <param name="message">A string containing any arbitrary data the programmer wants displayed</param>
        public static void DebugPrint(string opcode, Cpu instance, string message = "")
        {
            Debug.WriteLine("Instruction: {0}\nPC: {1}", opcode, instance.Pc);
            Debug.WriteLine(message + '\n');
        }
    }

}