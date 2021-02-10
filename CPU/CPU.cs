using System;
using System.IO;

namespace Intel8080
{

    /// <summary>
    /// Prepares system for initialization by reading config files and such
    /// </summary>
    static class PrepSystem
    {
        public static void Main(string[] args)
        {
            Cpu cpu = new Cpu("/home/eliasm/Documents/Projects/Intel26/Test Program", "Space Invaders.ch8");
            cpu.InitSystem();
        }

    }
    class Cpu
    {
        private string RunDirectory;
        private string BinName;

        public Cpu(string runDirectory, string binName)
        {
            this.RunDirectory = runDirectory;
            this.BinName = binName;
        }
        public byte[] LoadData(string path)
        {
            
            
            Console.WriteLine("Loading data");
            return File.ReadAllBytes(RunDirectory);
        }
        public void InitSystem()
        {
            byte[] memory = LoadData(RunDirectory + BinName);
        }



    }
}
