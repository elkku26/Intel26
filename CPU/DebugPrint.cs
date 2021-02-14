using System.Diagnostics;

static class DebugPrinter
{
    public static void DebugPrint(int opcode, string message = "")
    {
        Debug.WriteLine("Instruction: {0}", opcode);
        Debug.WriteLine(message + '\n');
    }
}
