using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using static CPU.Registers;

namespace CPU
{
    readonly struct Registers
    {
        public const int B = 0;
        public const int C = 1;
        public const int D = 2;
        public const int E = 3;
        public const int H = 4;
        public const int L = 5;
        public const int A = 7;


    }

    /// <summary>
    /// Prepares the CPU for initialization by reading config files etc.
    /// </summary>
    internal static class PrepSystem
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            DebugPrinter.DebugPrint(opcode: B);
            Cpu cpu = new Cpu("/home/eliasm/Documents/Projects/Intel26/Test Program/", "TST8080.COM");
            cpu.InitSystem();
        }

    }
    
    /// <summary>
    /// The actual CPU class itself, in charge of running the binary
    /// </summary>
    class Cpu
    { 
        private readonly string _runDirectory;
        private readonly string _binName;
        public byte[] Memory;
        public byte[] Registers;
        public int PC;
        private int OpCode;
        /// <summary>
        /// Constructor for the CPU Class
        /// </summary>
        /// <param name="runDirectory">The directory from which the binary is run. Should end in a slash or InitSystem() may fail.</param>
        /// <param name="binName">The name of the binary that is to be run.</param>
        public Cpu(string runDirectory, string binName)
        {
            this._runDirectory = runDirectory;
            this._binName = binName;
        }
        
        /// <summary>
        /// In charge of loading the program data into memory
        /// </summary>
        /// <param name="path">The path from which the program data is to be loaded.</param>
        /// <returns>A byte array containing the desired data.</returns>
        private byte[] LoadData(string path)
        {
            Debug.WriteLine("Loading data");
            Memory = new byte[64000];
            var programData = File.ReadAllBytes(path);
            Array.Copy( programData, 0, Memory, 0, programData.Length);
            
            return Memory;
        }
        
        /// <summary>
        /// Initializes the CPU by loading data into memory, setting registers, and beginning execution
        /// </summary>
        public void InitSystem()
        {
            Memory = LoadData(_runDirectory + _binName);

            Registers = new byte[7];
        }

        public void Step()
        {
            PC = 0;
        }

    }
}
