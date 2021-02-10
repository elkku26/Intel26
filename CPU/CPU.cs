using System;
using System.IO;

namespace CPU
{

    /// <summary>
    /// Prepares the CPU for initialization by reading config files and such
    /// </summary>
    static class PrepSystem
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Cpu cpu = new Cpu("/home/eliasm/Documents/Projects/Intel26/Test Program", "Space Invaders.ch8");
            cpu.InitSystem();
        }

    }
    
    /// <summary>
    /// The actual CPU class itself, in charge of running the binary
    /// </summary>
    class Cpu
    {
        private string RunDirectory;
        private string BinName;

        /// <summary>
        /// Constructor for CPU Class
        /// </summary>
        /// <param name="runDirectory">The directory from which the binary is run. Should end in a slash or InitSystem() may fail.</param>
        /// <param name="binName">The name of the binary that is to be run.</param>
        public Cpu(string runDirectory, string binName)
        {
            this.RunDirectory = runDirectory;
            this.BinName = binName;
        }
        
        /// <summary>
        /// In charge of loading the program data into memory
        /// </summary>
        /// <param name="path">The path from which the program data is to be loaded.</param>
        /// <returns>A byte array containing the desired data.</returns>
        public byte[] LoadData(string path)
        {
            Console.WriteLine("Loading data");
            return File.ReadAllBytes(path);
        }
        
        /// <summary>
        /// Initializes the CPU by loading data into memory, setting registers, and beginning execution
        /// </summary>
        public void InitSystem()
        {
            byte[] memory = LoadData(RunDirectory + BinName);
        }



    }
}
