﻿using System;
using System.IO;
using System.Net;

namespace CPU
{

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
            Cpu cpu = new Cpu("/home/eliasm/Documents/Projects/Intel26/Test Program/", "Space Invaders.ch8");
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
        private byte[] memory;
        private byte[] programData;
        private int opCode;
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
            Console.WriteLine("Loading data");
            memory = new byte[64000];
            programData = File.ReadAllBytes(path);
            for(int i = 0; i < programData.Length; i++)
            {
                Console.WriteLine(i);
                memory[i] = programData[i];
            }
            return memory;
        }
        
        /// <summary>
        /// Initializes the CPU by loading data into memory, setting registers, and beginning execution
        /// </summary>
        public void InitSystem()
        {
            byte[] memory = LoadData(_runDirectory + _binName);
        }

        public void Step()
        {
            for (int i = 0; i < memory.Length; i++)
            {
                opCode = memory[i];
            }
        }



    }
}
