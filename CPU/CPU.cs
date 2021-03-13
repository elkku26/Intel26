﻿using System;
using System.Diagnostics;
using System.IO;

namespace CPU
{
    internal readonly struct Registers
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
    ///     Prepares the CPU for initialization by reading config files etc.
    /// </summary>
    internal static class PrepSystem
    {
        /// <summary>
        ///     Entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var cpu = new Cpu("/home/eliasm/Documents/Projects/Intel26/Test Programs/", "OPCODEDEBUG.COM");
            cpu.InitSystem();
        }
    }

    /// <summary>
    ///     The actual CPU class itself, in charge of running the binary
    /// </summary>
    internal class Cpu
    {
        private readonly string _binName;
        private readonly string _runDirectory;
        private byte _opCodeByte;
        public byte[] Memory;
        public int Pc;
        public byte[] Registers;
        /// <summary>
        ///     Constructor for the CPU Class
        /// </summary>
        /// <param name="runDirectory">The directory from which the binary is run. Should end in a slash or InitSystem() may fail.</param>
        /// <param name="binName">The name of the binary that is to be run.</param>
        public Cpu(string runDirectory, string binName)
        {
            _runDirectory = runDirectory;
            _binName = binName;
        }

        /// <summary>
        ///     In charge of loading the program data into memory
        /// </summary>
        /// <param name="path">The path from which the program data is to be loaded.</param>
        /// <returns>A byte array containing the desired data.</returns>
        private byte[] LoadData(string path)
        {
            Debug.WriteLine("Loading data");
            Memory = new byte[64000];
            var programData = File.ReadAllBytes(path);
            Array.Copy(programData, 0, Memory, 0, programData.Length);

            return Memory;
        }

        /// <summary>
        ///     Initializes the CPU by loading data into memory, setting registers, and beginning execution
        /// </summary>
        public void InitSystem()
        {
            Memory = LoadData(_runDirectory + _binName);

            Registers = new byte[7];

            //temporary code for testing, not a part of the final program flow
            for (var i = 0; i < 256; i++) Step(i);
        }

        private void Step(int debugOpcode)
        {

            _opCodeByte = (byte) debugOpcode;
            //_opCodeByte = Memory[Pc];

            //Debug.WriteLine("Enter an opcode (testing purposes only)");
            //_opCodeByte = Convert.ToByte(Console.ReadLine(), 16);

            //Debug.WriteLine("Current opcode: Hex 0x{0:X}, Bin {1}", _opCodeByte, Convert.ToString(_opCodeByte, 2).PadLeft(8, '0'));


                switch ((_opCodeByte & 0xF0) >> 4)
                {
                    case 0x0:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x00
                            case 0x0:

                                Instructions.Nop(this);

                                break;

                            // 0x01
                            case 0x1:

                                Instructions.Lxi(this);

                                break;

                            // 0x02
                            case 0x2:

                                Instructions.Stax(this);

                                break;

                            // 0x03
                            case 0x3:

                                Instructions.Inx(this);

                                break;

                            // 0x04
                            case 0x4:

                                Instructions.Inr(this);

                                break;

                            // 0x05
                            case 0x5:

                                Instructions.Dcr(this);

                                break;

                            // 0x06
                            case 0x6:

                                Instructions.Mvi(this);

                                break;

                            // 0x07
                            case 0x7:

                                Instructions.Rlc(this);

                                break;

                            // 0x08
                            case 0x8:

                                Instructions.Nop(this);

                                break;

                            // 0x09
                            case 0x9:

                                Instructions.Dad(this);

                                break;

                            // 0x0A
                            case 0xA:

                                Instructions.Ldax(this);

                                break;

                            // 0x0B
                            case 0xB:

                                Instructions.Dcx(this);

                                break;

                            // 0x0C
                            case 0xC:

                                Instructions.Inr(this);

                                break;

                            // 0x0D
                            case 0xD:

                                Instructions.Dcr(this);

                                break;

                            // 0x0E
                            case 0xE:

                                Instructions.Mvi(this);

                                break;

                            // 0x0F
                            case 0xF:

                                Instructions.Rrc(this);

                                break;
                        }

                        break;

                    case 0x1:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x10
                            case 0x0:

                                Instructions.Nop(this);

                                break;

                            // 0x11
                            case 0x1:

                                Instructions.Lxi(this);

                                break;

                            // 0x12
                            case 0x2:

                                Instructions.Stax(this);

                                break;

                            // 0x13
                            case 0x3:

                                Instructions.Inx(this);

                                break;

                            // 0x14
                            case 0x4:

                                Instructions.Inr(this);

                                break;

                            // 0x15
                            case 0x5:

                                Instructions.Dcr(this);

                                break;

                            // 0x16
                            case 0x6:

                                Instructions.Mvi(this);

                                break;

                            // 0x17
                            case 0x7:

                                Instructions.Ral(this);

                                break;

                            // 0x18
                            case 0x8:

                                Instructions.Nop(this);

                                break;

                            // 0x19
                            case 0x9:

                                Instructions.Dad(this);

                                break;

                            // 0x1A
                            case 0xA:

                                Instructions.Ldax(this);

                                break;

                            // 0x1B
                            case 0xB:

                                Instructions.Dcx(this);

                                break;

                            // 0x1C
                            case 0xC:

                                Instructions.Inr(this);

                                break;

                            // 0x1D
                            case 0xD:

                                Instructions.Dcr(this);

                                break;

                            // 0x1E
                            case 0xE:

                                Instructions.Mvi(this);

                                break;

                            // 0x1F
                            case 0xF:

                                Instructions.Rar(this);

                                break;
                        }

                        break;

                    case 0x2:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x20
                            case 0x0:

                                Instructions.Nop(this);

                                break;

                            // 0x21
                            case 0x1:

                                Instructions.Lxi(this);

                                break;

                            // 0x22
                            case 0x2:

                                Instructions.Shld(this);

                                break;

                            // 0x23
                            case 0x3:

                                Instructions.Inx(this);

                                break;

                            // 0x24
                            case 0x4:

                                Instructions.Inr(this);

                                break;

                            // 0x25
                            case 0x5:

                                Instructions.Dcr(this);

                                break;

                            // 0x26
                            case 0x6:

                                Instructions.Mvi(this);

                                break;

                            // 0x27
                            case 0x7:

                                Instructions.Daa(this);

                                break;

                            // 0x28
                            case 0x8:

                                Instructions.Nop(this);

                                break;

                            // 0x29
                            case 0x9:

                                Instructions.Dad(this);

                                break;

                            // 0x2A
                            case 0xA:

                                Instructions.Lhld(this);

                                break;

                            // 0x2B
                            case 0xB:

                                Instructions.Dcx(this);

                                break;

                            // 0x2C
                            case 0xC:

                                Instructions.Inr(this);

                                break;

                            // 0x2D
                            case 0xD:

                                Instructions.Dcr(this);

                                break;

                            // 0x2E
                            case 0xE:

                                Instructions.Mvi(this);

                                break;

                            // 0x2F
                            case 0xF:

                                Instructions.Cma(this);

                                break;
                        }

                        break;

                    case 0x3:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x30
                            case 0x0:

                                Instructions.Nop(this);

                                break;

                            // 0x31
                            case 0x1:

                                Instructions.Lxi(this);

                                break;

                            // 0x32
                            case 0x2:

                                Instructions.Sta(this);

                                break;

                            // 0x33
                            case 0x3:

                                Instructions.Inx(this);

                                break;

                            // 0x34
                            case 0x4:

                                Instructions.Inr(this);

                                break;

                            // 0x35
                            case 0x5:

                                Instructions.Dcr(this);

                                break;

                            // 0x36
                            case 0x6:

                                Instructions.Mvi(this);

                                break;

                            // 0x37
                            case 0x7:

                                Instructions.Stc(this);

                                break;

                            // 0x38
                            case 0x8:

                                Instructions.Nop(this);

                                break;

                            // 0x39
                            case 0x9:

                                Instructions.Dad(this);

                                break;

                            // 0x3A
                            case 0xA:

                                Instructions.Lda(this);

                                break;

                            // 0x3B
                            case 0xB:

                                Instructions.Dcx(this);

                                break;

                            // 0x3C
                            case 0xC:

                                Instructions.Inr(this);

                                break;

                            // 0x3D
                            case 0xD:

                                Instructions.Dcr(this);

                                break;

                            // 0x3E
                            case 0xE:

                                Instructions.Mvi(this);

                                break;

                            // 0x3F
                            case 0xF:

                                Instructions.Cmc(this);

                                break;
                        }

                        break;

                    case 0x4:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x40
                            case 0x0:

                                Instructions.Mov(this);

                                break;

                            // 0x41
                            case 0x1:

                                Instructions.Mov(this);

                                break;

                            // 0x42
                            case 0x2:

                                Instructions.Mov(this);

                                break;

                            // 0x43
                            case 0x3:

                                Instructions.Mov(this);

                                break;

                            // 0x44
                            case 0x4:

                                Instructions.Mov(this);

                                break;

                            // 0x45
                            case 0x5:

                                Instructions.Mov(this);

                                break;

                            // 0x46
                            case 0x6:

                                Instructions.Mov(this);

                                break;

                            // 0x47
                            case 0x7:

                                Instructions.Mov(this);

                                break;

                            // 0x48
                            case 0x8:

                                Instructions.Mov(this);

                                break;

                            // 0x49
                            case 0x9:

                                Instructions.Mov(this);

                                break;

                            // 0x4A
                            case 0xA:

                                Instructions.Mov(this);

                                break;

                            // 0x4B
                            case 0xB:

                                Instructions.Mov(this);

                                break;

                            // 0x4C
                            case 0xC:

                                Instructions.Mov(this);

                                break;

                            // 0x4D
                            case 0xD:

                                Instructions.Mov(this);

                                break;

                            // 0x4E
                            case 0xE:

                                Instructions.Mov(this);

                                break;

                            // 0x4F
                            case 0xF:

                                Instructions.Mov(this);

                                break;
                        }

                        break;

                    case 0x5:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x50
                            case 0x0:

                                Instructions.Mov(this);

                                break;

                            // 0x51
                            case 0x1:

                                Instructions.Mov(this);

                                break;

                            // 0x52
                            case 0x2:

                                Instructions.Mov(this);

                                break;

                            // 0x53
                            case 0x3:

                                Instructions.Mov(this);

                                break;

                            // 0x54
                            case 0x4:

                                Instructions.Mov(this);

                                break;

                            // 0x55
                            case 0x5:

                                Instructions.Mov(this);

                                break;

                            // 0x56
                            case 0x6:

                                Instructions.Mov(this);

                                break;

                            // 0x57
                            case 0x7:

                                Instructions.Mov(this);

                                break;

                            // 0x58
                            case 0x8:

                                Instructions.Mov(this);

                                break;

                            // 0x59
                            case 0x9:

                                Instructions.Mov(this);

                                break;

                            // 0x5A
                            case 0xA:

                                Instructions.Mov(this);

                                break;

                            // 0x5B
                            case 0xB:

                                Instructions.Mov(this);

                                break;

                            // 0x5C
                            case 0xC:

                                Instructions.Mov(this);

                                break;

                            // 0x5D
                            case 0xD:

                                Instructions.Mov(this);

                                break;

                            // 0x5E
                            case 0xE:

                                Instructions.Mov(this);

                                break;

                            // 0x5F
                            case 0xF:

                                Instructions.Mov(this);

                                break;
                        }

                        break;

                    case 0x6:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x60
                            case 0x0:

                                Instructions.Mov(this);

                                break;

                            // 0x61
                            case 0x1:

                                Instructions.Mov(this);

                                break;

                            // 0x62
                            case 0x2:

                                Instructions.Mov(this);

                                break;

                            // 0x63
                            case 0x3:

                                Instructions.Mov(this);

                                break;

                            // 0x64
                            case 0x4:

                                Instructions.Mov(this);

                                break;

                            // 0x65
                            case 0x5:

                                Instructions.Mov(this);

                                break;

                            // 0x66
                            case 0x6:

                                Instructions.Mov(this);

                                break;

                            // 0x67
                            case 0x7:

                                Instructions.Mov(this);

                                break;

                            // 0x68
                            case 0x8:

                                Instructions.Mov(this);

                                break;

                            // 0x69
                            case 0x9:

                                Instructions.Mov(this);

                                break;

                            // 0x6A
                            case 0xA:

                                Instructions.Mov(this);

                                break;

                            // 0x6B
                            case 0xB:

                                Instructions.Mov(this);

                                break;

                            // 0x6C
                            case 0xC:

                                Instructions.Mov(this);

                                break;

                            // 0x6D
                            case 0xD:

                                Instructions.Mov(this);

                                break;

                            // 0x6E
                            case 0xE:

                                Instructions.Mov(this);

                                break;

                            // 0x6F
                            case 0xF:

                                Instructions.Mov(this);

                                break;
                        }

                        break;

                    case 0x7:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x70
                            case 0x0:

                                Instructions.Mov(this);

                                break;

                            // 0x71
                            case 0x1:

                                Instructions.Mov(this);

                                break;

                            // 0x72
                            case 0x2:

                                Instructions.Mov(this);

                                break;

                            // 0x73
                            case 0x3:

                                Instructions.Mov(this);

                                break;

                            // 0x74
                            case 0x4:

                                Instructions.Mov(this);

                                break;

                            // 0x75
                            case 0x5:

                                Instructions.Mov(this);

                                break;

                            // 0x76
                            case 0x6:

                                Instructions.Hlt(this);

                                break;

                            // 0x77
                            case 0x7:

                                Instructions.Mov(this);

                                break;

                            // 0x78
                            case 0x8:

                                Instructions.Mov(this);

                                break;

                            // 0x79
                            case 0x9:

                                Instructions.Mov(this);

                                break;

                            // 0x7A
                            case 0xA:

                                Instructions.Mov(this);

                                break;

                            // 0x7B
                            case 0xB:

                                Instructions.Mov(this);

                                break;

                            // 0x7C
                            case 0xC:

                                Instructions.Mov(this);

                                break;

                            // 0x7D
                            case 0xD:

                                Instructions.Mov(this);

                                break;

                            // 0x7E
                            case 0xE:

                                Instructions.Mov(this);

                                break;

                            // 0x7F
                            case 0xF:

                                Instructions.Mov(this);

                                break;
                        }

                        break;

                    case 0x8:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x80
                            case 0x0:

                                Instructions.Add(this);

                                break;

                            // 0x81
                            case 0x1:

                                Instructions.Add(this);

                                break;

                            // 0x82
                            case 0x2:

                                Instructions.Add(this);

                                break;

                            // 0x83
                            case 0x3:

                                Instructions.Add(this);

                                break;

                            // 0x84
                            case 0x4:

                                Instructions.Add(this);

                                break;

                            // 0x85
                            case 0x5:

                                Instructions.Add(this);

                                break;

                            // 0x86
                            case 0x6:

                                Instructions.Add(this);

                                break;

                            // 0x87
                            case 0x7:

                                Instructions.Add(this);

                                break;

                            // 0x88
                            case 0x8:

                                Instructions.Adc(this);

                                break;

                            // 0x89
                            case 0x9:

                                Instructions.Adc(this);

                                break;

                            // 0x8A
                            case 0xA:

                                Instructions.Adc(this);

                                break;

                            // 0x8B
                            case 0xB:

                                Instructions.Adc(this);

                                break;

                            // 0x8C
                            case 0xC:

                                Instructions.Adc(this);

                                break;

                            // 0x8D
                            case 0xD:

                                Instructions.Adc(this);

                                break;

                            // 0x8E
                            case 0xE:

                                Instructions.Adc(this);

                                break;

                            // 0x8F
                            case 0xF:

                                Instructions.Adc(this);

                                break;
                        }

                        break;

                    case 0x9:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0x90
                            case 0x0:

                                Instructions.Sub(this);

                                break;

                            // 0x91
                            case 0x1:

                                Instructions.Sub(this);

                                break;

                            // 0x92
                            case 0x2:

                                Instructions.Sub(this);

                                break;

                            // 0x93
                            case 0x3:

                                Instructions.Sub(this);

                                break;

                            // 0x94
                            case 0x4:

                                Instructions.Sub(this);

                                break;

                            // 0x95
                            case 0x5:

                                Instructions.Sub(this);

                                break;

                            // 0x96
                            case 0x6:

                                Instructions.Sub(this);

                                break;

                            // 0x97
                            case 0x7:

                                Instructions.Sub(this);

                                break;

                            // 0x98
                            case 0x8:

                                Instructions.Sbb(this);

                                break;

                            // 0x99
                            case 0x9:

                                Instructions.Sbb(this);

                                break;

                            // 0x9A
                            case 0xA:

                                Instructions.Sbb(this);

                                break;

                            // 0x9B
                            case 0xB:

                                Instructions.Sbb(this);

                                break;

                            // 0x9C
                            case 0xC:

                                Instructions.Sbb(this);

                                break;

                            // 0x9D
                            case 0xD:

                                Instructions.Sbb(this);

                                break;

                            // 0x9E
                            case 0xE:

                                Instructions.Sbb(this);

                                break;

                            // 0x9F
                            case 0xF:

                                Instructions.Sbb(this);

                                break;
                        }

                        break;

                    case 0xA:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0xA0
                            case 0x0:

                                Instructions.Ana(this);

                                break;

                            // 0xA1
                            case 0x1:

                                Instructions.Ana(this);

                                break;

                            // 0xA2
                            case 0x2:

                                Instructions.Ana(this);

                                break;

                            // 0xA3
                            case 0x3:

                                Instructions.Ana(this);

                                break;

                            // 0xA4
                            case 0x4:

                                Instructions.Ana(this);

                                break;

                            // 0xA5
                            case 0x5:

                                Instructions.Ana(this);

                                break;

                            // 0xA6
                            case 0x6:

                                Instructions.Ana(this);

                                break;

                            // 0xA7
                            case 0x7:

                                Instructions.Ana(this);

                                break;

                            // 0xA8
                            case 0x8:

                                Instructions.Xra(this);

                                break;

                            // 0xA9
                            case 0x9:

                                Instructions.Xra(this);

                                break;

                            // 0xAA
                            case 0xA:

                                Instructions.Xra(this);

                                break;

                            // 0xAB
                            case 0xB:

                                Instructions.Xra(this);

                                break;

                            // 0xAC
                            case 0xC:

                                Instructions.Xra(this);

                                break;

                            // 0xAD
                            case 0xD:

                                Instructions.Xra(this);

                                break;

                            // 0xAE
                            case 0xE:

                                Instructions.Xra(this);

                                break;

                            // 0xAF
                            case 0xF:

                                Instructions.Xra(this);

                                break;
                        }

                        break;

                    case 0xB:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0xB0
                            case 0x0:

                                Instructions.Ora(this);

                                break;

                            // 0xB1
                            case 0x1:

                                Instructions.Ora(this);

                                break;

                            // 0xB2
                            case 0x2:

                                Instructions.Ora(this);

                                break;

                            // 0xB3
                            case 0x3:

                                Instructions.Ora(this);

                                break;

                            // 0xB4
                            case 0x4:

                                Instructions.Ora(this);

                                break;

                            // 0xB5
                            case 0x5:

                                Instructions.Ora(this);

                                break;

                            // 0xB6
                            case 0x6:

                                Instructions.Ora(this);

                                break;

                            // 0xB7
                            case 0x7:

                                Instructions.Ora(this);

                                break;

                            // 0xB8
                            case 0x8:

                                Instructions.Cmp(this);

                                break;

                            // 0xB9
                            case 0x9:

                                Instructions.Cmp(this);

                                break;

                            // 0xBA
                            case 0xA:

                                Instructions.Cmp(this);

                                break;

                            // 0xBB
                            case 0xB:

                                Instructions.Cmp(this);

                                break;

                            // 0xBC
                            case 0xC:

                                Instructions.Cmp(this);

                                break;

                            // 0xBD
                            case 0xD:

                                Instructions.Cmp(this);

                                break;

                            // 0xBE
                            case 0xE:

                                Instructions.Cmp(this);

                                break;

                            // 0xBF
                            case 0xF:

                                Instructions.Cmp(this);

                                break;
                        }

                        break;

                    case 0xC:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0xC0
                            case 0x0:

                                Instructions.Rnz(this);

                                break;

                            // 0xC1
                            case 0x1:

                                Instructions.Pop(this);

                                break;

                            // 0xC2
                            case 0x2:

                                Instructions.Jnz(this);

                                break;

                            // 0xC3
                            case 0x3:

                                Instructions.Jmp(this);

                                break;

                            // 0xC4
                            case 0x4:

                                Instructions.Cnz(this);

                                break;

                            // 0xC5
                            case 0x5:

                                Instructions.Push(this);

                                break;

                            // 0xC6
                            case 0x6:

                                Instructions.Adi(this);

                                break;

                            // 0xC7
                            case 0x7:

                                Instructions.Rst(this);

                                break;

                            // 0xC8
                            case 0x8:

                                Instructions.Rz(this);

                                break;

                            // 0xC9
                            case 0x9:

                                Instructions.Ret(this);

                                break;

                            // 0xCA
                            case 0xA:

                                Instructions.Jz(this);

                                break;

                            // 0xCB
                            case 0xB:

                                Instructions.Jmp(this);

                                break;

                            // 0xCC
                            case 0xC:

                                Instructions.Cz(this);

                                break;

                            // 0xCD
                            case 0xD:

                                Instructions.Call(this);

                                break;

                            // 0xCE
                            case 0xE:

                                Instructions.Aci(this);

                                break;

                            // 0xCF
                            case 0xF:

                                Instructions.Rst(this);

                                break;
                        }

                        break;

                    case 0xD:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0xD0
                            case 0x0:

                                Instructions.Rnc(this);

                                break;

                            // 0xD1
                            case 0x1:

                                Instructions.Pop(this);

                                break;

                            // 0xD2
                            case 0x2:

                                Instructions.Jnc(this);

                                break;

                            // 0xD3
                            case 0x3:

                                Instructions.Out(this);

                                break;

                            // 0xD4
                            case 0x4:

                                Instructions.Cnc(this);

                                break;

                            // 0xD5
                            case 0x5:

                                Instructions.Push(this);

                                break;

                            // 0xD6
                            case 0x6:

                                Instructions.Sui(this);

                                break;

                            // 0xD7
                            case 0x7:

                                Instructions.Rst(this);

                                break;

                            // 0xD8
                            case 0x8:

                                Instructions.Rc(this);

                                break;

                            // 0xD9
                            case 0x9:

                                Instructions.Ret(this);

                                break;

                            // 0xDA
                            case 0xA:

                                Instructions.Jc(this);

                                break;

                            // 0xDB
                            case 0xB:

                                Instructions.In(this);

                                break;

                            // 0xDC
                            case 0xC:

                                Instructions.Cc(this);

                                break;

                            // 0xDD
                            case 0xD:

                                Instructions.Call(this);

                                break;

                            // 0xDE
                            case 0xE:

                                Instructions.Sbi(this);

                                break;

                            // 0xDF
                            case 0xF:

                                Instructions.Rst(this);

                                break;
                        }

                        break;

                    case 0xE:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0xE0
                            case 0x0:

                                Instructions.Rpo(this);

                                break;

                            // 0xE1
                            case 0x1:

                                Instructions.Pop(this);

                                break;

                            // 0xE2
                            case 0x2:

                                Instructions.Jpo(this);

                                break;

                            // 0xE3
                            case 0x3:

                                Instructions.Xthl(this);

                                break;

                            // 0xE4
                            case 0x4:

                                Instructions.Cpo(this);

                                break;

                            // 0xE5
                            case 0x5:

                                Instructions.Push(this);

                                break;

                            // 0xE6
                            case 0x6:

                                Instructions.Ani(this);

                                break;

                            // 0xE7
                            case 0x7:

                                Instructions.Rst(this);

                                break;

                            // 0xE8
                            case 0x8:

                                Instructions.Rpe(this);

                                break;

                            // 0xE9
                            case 0x9:

                                Instructions.Pchl(this);

                                break;

                            // 0xEA
                            case 0xA:

                                Instructions.Jpe(this);

                                break;

                            // 0xEB
                            case 0xB:

                                Instructions.Xchg(this);

                                break;

                            // 0xEC
                            case 0xC:

                                Instructions.Cpe(this);

                                break;

                            // 0xED
                            case 0xD:

                                Instructions.Call(this);

                                break;

                            // 0xEE
                            case 0xE:

                                Instructions.Xri(this);

                                break;

                            // 0xEF
                            case 0xF:

                                Instructions.Rst(this);

                                break;
                        }

                        break;

                    case 0xF:
                        switch (_opCodeByte & 0xF)
                        {
                            // 0xF0
                            case 0x0:

                                Instructions.Rp(this);

                                break;

                            // 0xF1
                            case 0x1:

                                Instructions.Pop(this);

                                break;

                            // 0xF2
                            case 0x2:

                                Instructions.Jp(this);

                                break;

                            // 0xF3
                            case 0x3:

                                Instructions.Di(this);

                                break;

                            // 0xF4
                            case 0x4:

                                Instructions.Cp(this);

                                break;

                            // 0xF5
                            case 0x5:

                                Instructions.Push(this);

                                break;

                            // 0xF6
                            case 0x6:

                                Instructions.Ori(this);

                                break;

                            // 0xF7
                            case 0x7:

                                Instructions.Rst(this);

                                break;

                            // 0xF8
                            case 0x8:

                                Instructions.Rm(this);

                                break;

                            // 0xF9
                            case 0x9:

                                Instructions.Sphl(this);

                                break;

                            // 0xFA
                            case 0xA:

                                Instructions.Jm(this);

                                break;

                            // 0xFB
                            case 0xB:

                                Instructions.Ei(this);

                                break;

                            // 0xFC
                            case 0xC:

                                Instructions.Cm(this);

                                break;

                            // 0xFD
                            case 0xD:

                                Instructions.Call(this);

                                break;

                            // 0xFE
                            case 0xE:

                                Instructions.Cpi(this);

                                break;

                            // 0xFF
                            case 0xF:

                                Instructions.Rst(this);

                                break;
                        }

                        break;
                }



            Pc++;
        }
    }
}