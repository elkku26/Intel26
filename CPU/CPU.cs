using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static CPU.DebugHelper;

namespace CPU
{
    public enum Error
    {
        FileNotFound = 1,
        InvalidPathArgument = 2,
        OutOfMemory = 3
    }

    internal readonly struct Register
    {
        public const int B = 0;
        public const int C = 1;
        public const int D = 2;
        public const int E = 3;
        public const int H = 4;

        public const int L = 5;

        //MRef is not actually a register but logically belongs here
        public const int MRef = 6;
        public const int A = 7;
    }


    internal readonly struct RegisterPair
    {
        public const int B = 0;
        public const int D = 1;
        public const int H = 2;
        public const int SP = 3;
    }

    /// <summary>
    ///     Constants that help to select specific flag bits
    /// </summary>
    internal readonly struct FlagSelector
    {
        public const int Carry = 0b1;
        public const int Parity = 0b100;
        public const int AuxCarry = 0b10000;
        public const int Zero = 0b1000000;
        public const int Sign = 0b10000000;
    }


    /// <summary>
    ///     Prepares the CPU for initialization by reading config files etc.
    /// </summary>
    internal static class PrepareCpu
    {
        /// <summary>
        ///     Entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Length == 0) Die(Error.InvalidPathArgument);

            var fullPath = args[0];

            if (!File.Exists(fullPath)) Die(Error.FileNotFound, "Please enter a file as the first argument.");

            var cpu = new Cpu(fullPath);
            cpu.StartCpu();
        }
    }

    /// <summary>
    ///     The actual CPU class itself, in charge of running the binary
    /// </summary>
    internal class Cpu
    {
        public static Cpu Current { get; internal set; }
        private readonly string _fullPath;
        public byte Flags;
        public byte[] Memory;
        public byte OpCodeByte;
        public uint Pc;
        public byte[] Registers;

        public ushort Sp;

        //public Stack<ushort> Stack;
        /// <summary>
        ///     Constructor for the CPU Class
        /// </summary>
        /// <param name="fullPath">The full path to the binary. </param>
        public Cpu(string fullPath = "")
        {
            _fullPath = fullPath;
            Flags = 0;
            Memory = new byte[64000];
            Registers = new byte[8];
            Pc = 0;
            Sp = new ushort();
            //Stack = new Stack<ushort>();
        }

        /// <summary>
        ///     In charge of loading the program data into memory
        /// </summary>
        /// <param name="path">The path from which the program data is to be loaded.</param>
        /// <returns>A byte array containing the desired data.</returns>
        private byte[] LoadData(string path)
        {
            Debug.WriteLine("Loading data");
            var programData = File.ReadAllBytes(path);
            Array.Copy(programData, 0, Memory, 0, programData.Length);
            Debug.WriteLine("Load succesful");

            return Memory;
        }

        /// <summary>
        ///     Initializes the CPU by loading data into memory, setting registers, and beginning execution
        /// </summary>
        public void StartCpu()
        {
            Current = this;
            Memory = LoadData(_fullPath);
            Debug.WriteLine("Start successful");
            while (true) Step();
        }

        public void PushStack(ushort data)
        {
            Memory[Sp - 1] = (byte)((data & 0xF0) >> 4);
            Memory[Sp - 2] = (byte)(data & 0x0F);
            Sp -= 2;
        }

        public void PushStack(byte mostSignificant, byte leastSignificant)
        {
            Memory[Sp - 1] = mostSignificant;
            Memory[Sp - 2] = leastSignificant;
            Sp -= 2;
        }

        public void Jump()
        {
            //DebugPrint("JMP", cpu);
            //Console.WriteLine("opcode:" + Convert.ToString(cpu.Memory[cpu.Pc], 2).PadLeft(8, '0'));

            var address = BitConverter.ToUInt16(Memory, (int)Pc + 1);
            BinaryPrimitives.ReverseEndianness(address);

            Pc = (uint)address - 1;
        }

        public uint Pop()
        {
            Sp--;
            return Memory[Sp + 1];
        }

        public void PushStackAndJump()
        {
            //var address = BitConverter.ToUInt16(Memory, (int)Pc + 1);
            //BinaryPrimitives.ReverseEndianness(address);

            PushStack((ushort)(Pc + 3));

            Jump();
        }

        /// <summary>
        ///     Sets and unsets CPU flags
        /// </summary>
        /// <param name="status">The status of the bits that are altered</param>
        /// <param name="selector">Marks the bits that should be set/unset</param>
        public void SetFlags(int status, int selector)
        {
            Current.Flags = (byte)(status == 1 ? Current.Flags | selector : Current.Flags & ~selector & 0xFF);
        }

        private void Step()
        {
            if (Pc == Memory.Length) Die(Error.OutOfMemory);
            OpCodeByte = Memory[Pc];

            Debug.WriteLine("Current opcode: Hex 0x{0:X}, Bin {1}", OpCodeByte,
                Convert.ToString(OpCodeByte, 2).PadLeft(8, '0'));


            switch ((OpCodeByte & 0xF0) >> 4)
            {
                case 0x0:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x00
                        case 0x0:

                            Instructions.Nop();

                            break;

                        // 0x01
                        case 0x1:

                            Instructions.Lxi(RegisterPair.B);

                            break;

                        // 0x02
                        case 0x2:

                            Instructions.Stax(RegisterPair.B);

                            break;

                        // 0x03
                        case 0x3:

                            Instructions.Inx(RegisterPair.B);

                            break;

                        // 0x04
                        case 0x4:

                            Instructions.Inr(Register.B);

                            break;

                        // 0x05
                        case 0x5:

                            Instructions.Dcr(Register.B);

                            break;

                        // 0x06
                        case 0x6:

                            Instructions.Mvi(Register.B);

                            break;

                        // 0x07
                        case 0x7:

                            Instructions.Rlc();

                            break;

                        // 0x08
                        case 0x8:

                            Instructions.Nop();

                            break;

                        // 0x09
                        case 0x9:

                            Instructions.Dad(RegisterPair.B);

                            break;

                        // 0x0A
                        case 0xA:

                            Instructions.Ldax(RegisterPair.B);

                            break;

                        // 0x0B
                        case 0xB:

                            Instructions.Dcx(RegisterPair.B);

                            break;

                        // 0x0C
                        case 0xC:

                            Instructions.Inr(Register.C);

                            break;

                        // 0x0D
                        case 0xD:

                            Instructions.Dcr(Register.C);

                            break;

                        // 0x0E
                        case 0xE:

                            Instructions.Mvi(Register.C);

                            break;

                        // 0x0F
                        case 0xF:

                            Instructions.Rrc();

                            break;
                    }

                    break;

                case 0x1:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x10
                        case 0x0:

                            Instructions.Nop();

                            break;

                        // 0x11
                        case 0x1:

                            Instructions.Lxi(RegisterPair.D);

                            break;

                        // 0x12
                        case 0x2:

                            Instructions.Stax(RegisterPair.D);

                            break;

                        // 0x13
                        case 0x3:

                            Instructions.Inx(RegisterPair.D);

                            break;

                        // 0x14
                        case 0x4:

                            Instructions.Inr(Register.D);

                            break;

                        // 0x15
                        case 0x5:

                            Instructions.Dcr(Register.D);

                            break;

                        // 0x16
                        case 0x6:

                            Instructions.Mvi(Register.D);

                            break;

                        // 0x17
                        case 0x7:

                            Instructions.Ral();

                            break;

                        // 0x18
                        case 0x8:

                            Instructions.Nop();

                            break;

                        // 0x19
                        case 0x9:

                            Instructions.Dad(RegisterPair.D);

                            break;

                        // 0x1A
                        case 0xA:

                            Instructions.Ldax(RegisterPair.D);

                            break;

                        // 0x1B
                        case 0xB:

                            Instructions.Dcx(RegisterPair.D);

                            break;

                        // 0x1C
                        case 0xC:

                            Instructions.Inr(Register.E);

                            break;

                        // 0x1D
                        case 0xD:

                            Instructions.Dcr(Register.E);

                            break;

                        // 0x1E
                        case 0xE:

                            Instructions.Mvi(Register.E);

                            break;

                        // 0x1F
                        case 0xF:

                            Instructions.Rar();

                            break;
                    }

                    break;

                case 0x2:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x20
                        case 0x0:

                            Instructions.Nop();

                            break;

                        // 0x21
                        case 0x1:

                            Instructions.Lxi(RegisterPair.H);

                            break;

                        // 0x22
                        case 0x2:

                            Instructions.Shld();

                            break;

                        // 0x23
                        case 0x3:

                            Instructions.Inx(RegisterPair.H);

                            break;

                        // 0x24
                        case 0x4:

                            Instructions.Inr(Register.H);

                            break;

                        // 0x25
                        case 0x5:

                            Instructions.Dcr(Register.H);

                            break;

                        // 0x26
                        case 0x6:

                            Instructions.Mvi(Register.H);

                            break;

                        // 0x27
                        case 0x7:

                            Instructions.Daa();

                            break;

                        // 0x28
                        case 0x8:

                            Instructions.Nop();

                            break;

                        // 0x29
                        case 0x9:

                            Instructions.Dad(RegisterPair.H);

                            break;

                        // 0x2A
                        case 0xA:

                            Instructions.Lhld();

                            break;

                        // 0x2B
                        case 0xB:

                            Instructions.Dcx(RegisterPair.H);

                            break;

                        // 0x2C
                        case 0xC:

                            Instructions.Inr(Register.L);

                            break;

                        // 0x2D
                        case 0xD:

                            Instructions.Dcr(Register.L);

                            break;

                        // 0x2E
                        case 0xE:

                            Instructions.Mvi(Register.L);

                            break;

                        // 0x2F
                        case 0xF:

                            Instructions.Cma();

                            break;
                    }

                    break;

                case 0x3:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x30
                        case 0x0:

                            Instructions.Nop();

                            break;

                        // 0x31
                        case 0x1:

                            Instructions.Lxi(RegisterPair.SP);

                            break;

                        // 0x32
                        case 0x2:

                            Instructions.Sta();

                            break;

                        // 0x33
                        case 0x3:

                            Instructions.Inx(RegisterPair.SP);

                            break;

                        // 0x34
                        case 0x4:

                            Instructions.Inr(Register.MRef);

                            break;

                        // 0x35
                        case 0x5:

                            Instructions.Dcr(Register.MRef);

                            break;

                        // 0x36
                        case 0x6:

                            Instructions.Mvi(Register.MRef);

                            break;

                        // 0x37
                        case 0x7:

                            Instructions.Stc();

                            break;

                        // 0x38
                        case 0x8:

                            Instructions.Nop();

                            break;

                        // 0x39
                        case 0x9:

                            Instructions.Dad(RegisterPair.SP);

                            break;

                        // 0x3A
                        case 0xA:

                            Instructions.Lda();

                            break;

                        // 0x3B
                        case 0xB:

                            Instructions.Dcx(RegisterPair.SP);

                            break;

                        // 0x3C
                        case 0xC:

                            Instructions.Inr(Register.A);

                            break;

                        // 0x3D
                        case 0xD:

                            Instructions.Dcr(Register.A);

                            break;

                        // 0x3E
                        case 0xE:

                            Instructions.Mvi(Register.A);

                            break;

                        // 0x3F
                        case 0xF:

                            Instructions.Cmc();

                            break;
                    }

                    break;

                case 0x4:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x40
                        case 0x0:

                            Instructions.Mov(Register.B, Register.B);

                            break;

                        // 0x41
                        case 0x1:

                            Instructions.Mov(Register.B, Register.C);

                            break;

                        // 0x42
                        case 0x2:

                            Instructions.Mov(Register.B, Register.D);

                            break;

                        // 0x43
                        case 0x3:

                            Instructions.Mov(Register.B, Register.E);

                            break;

                        // 0x44
                        case 0x4:

                            Instructions.Mov(Register.B, Register.H);

                            break;

                        // 0x45
                        case 0x5:

                            Instructions.Mov(Register.B, Register.L);

                            break;

                        // 0x46
                        case 0x6:

                            Instructions.Mov(Register.B, Register.MRef);

                            break;

                        // 0x47
                        case 0x7:

                            Instructions.Mov(Register.B, Register.A);

                            break;

                        // 0x48
                        case 0x8:

                            Instructions.Mov(Register.C, Register.B);

                            break;

                        // 0x49
                        case 0x9:

                            Instructions.Mov(Register.C, Register.C);

                            break;

                        // 0x4A
                        case 0xA:

                            Instructions.Mov(Register.C, Register.D);

                            break;

                        // 0x4B
                        case 0xB:

                            Instructions.Mov(Register.C, Register.E);

                            break;

                        // 0x4C
                        case 0xC:

                            Instructions.Mov(Register.C, Register.H);

                            break;

                        // 0x4D
                        case 0xD:

                            Instructions.Mov(Register.C, Register.L);

                            break;

                        // 0x4E
                        case 0xE:

                            Instructions.Mov(Register.C, Register.MRef);

                            break;

                        // 0x4F
                        case 0xF:

                            Instructions.Mov(Register.C, Register.A);

                            break;
                    }

                    break;

                case 0x5:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x50
                        case 0x0:

                            Instructions.Mov(Register.D, Register.B);

                            break;

                        // 0x51
                        case 0x1:

                            Instructions.Mov(Register.D, Register.C);

                            break;

                        // 0x52
                        case 0x2:

                            Instructions.Mov(Register.D, Register.D);

                            break;

                        // 0x53
                        case 0x3:

                            Instructions.Mov(Register.D, Register.E);

                            break;

                        // 0x54
                        case 0x4:

                            Instructions.Mov(Register.D, Register.H);

                            break;

                        // 0x55
                        case 0x5:

                            Instructions.Mov(Register.D, Register.L);

                            break;

                        // 0x56
                        case 0x6:

                            Instructions.Mov(Register.D, Register.MRef);

                            break;

                        // 0x57
                        case 0x7:

                            Instructions.Mov(Register.D, Register.A);

                            break;

                        // 0x58
                        case 0x8:

                            Instructions.Mov(Register.E, Register.B);

                            break;

                        // 0x59
                        case 0x9:

                            Instructions.Mov(Register.E, Register.C);

                            break;

                        // 0x5A
                        case 0xA:

                            Instructions.Mov(Register.E, Register.D);

                            break;

                        // 0x5B
                        case 0xB:

                            Instructions.Mov(Register.E, Register.E);

                            break;

                        // 0x5C
                        case 0xC:

                            Instructions.Mov(Register.E, Register.H);

                            break;

                        // 0x5D
                        case 0xD:

                            Instructions.Mov(Register.E, Register.L);

                            break;

                        // 0x5E
                        case 0xE:

                            Instructions.Mov(Register.E, Register.MRef);

                            break;

                        // 0x5F
                        case 0xF:

                            Instructions.Mov(Register.E, Register.A);

                            break;
                    }

                    break;

                case 0x6:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x60
                        case 0x0:

                            Instructions.Mov(Register.H, Register.B);

                            break;

                        // 0x61
                        case 0x1:

                            Instructions.Mov(Register.H, Register.C);

                            break;

                        // 0x62
                        case 0x2:

                            Instructions.Mov(Register.H, Register.D);

                            break;

                        // 0x63
                        case 0x3:

                            Instructions.Mov(Register.H, Register.E);

                            break;

                        // 0x64
                        case 0x4:

                            Instructions.Mov(Register.H, Register.H);

                            break;

                        // 0x65
                        case 0x5:

                            Instructions.Mov(Register.H, Register.L);

                            break;

                        // 0x66
                        case 0x6:

                            Instructions.Mov(Register.H, Register.MRef);

                            break;

                        // 0x67
                        case 0x7:

                            Instructions.Mov(Register.H, Register.A);

                            break;

                        // 0x68
                        case 0x8:

                            Instructions.Mov(Register.L, Register.B);

                            break;

                        // 0x69
                        case 0x9:

                            Instructions.Mov(Register.L, Register.C);

                            break;

                        // 0x6A
                        case 0xA:

                            Instructions.Mov(Register.L, Register.D);

                            break;

                        // 0x6B
                        case 0xB:

                            Instructions.Mov(Register.L, Register.E);

                            break;

                        // 0x6C
                        case 0xC:

                            Instructions.Mov(Register.L, Register.H);

                            break;

                        // 0x6D
                        case 0xD:

                            Instructions.Mov(Register.L, Register.L);

                            break;

                        // 0x6E
                        case 0xE:

                            Instructions.Mov(Register.L, Register.MRef);

                            break;

                        // 0x6F
                        case 0xF:

                            Instructions.Mov(Register.L, Register.A);

                            break;
                    }

                    break;

                case 0x7:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x70
                        case 0x0:

                            Instructions.Mov(Register.MRef, Register.B);

                            break;

                        // 0x71
                        case 0x1:

                            Instructions.Mov(Register.MRef, Register.C);

                            break;

                        // 0x72
                        case 0x2:

                            Instructions.Mov(Register.MRef, Register.D);

                            break;

                        // 0x73
                        case 0x3:

                            Instructions.Mov(Register.MRef, Register.E);

                            break;

                        // 0x74
                        case 0x4:

                            Instructions.Mov(Register.MRef, Register.H);

                            break;

                        // 0x75
                        case 0x5:

                            Instructions.Mov(Register.MRef, Register.L);

                            break;

                        // 0x76
                        case 0x6:

                            Instructions.Hlt();

                            break;

                        // 0x77
                        case 0x7:

                            Instructions.Mov(Register.MRef, Register.A);

                            break;

                        // 0x78
                        case 0x8:

                            Instructions.Mov(Register.A, Register.B);

                            break;

                        // 0x79
                        case 0x9:

                            Instructions.Mov(Register.A, Register.C);

                            break;

                        // 0x7A
                        case 0xA:

                            Instructions.Mov(Register.A, Register.D);

                            break;

                        // 0x7B
                        case 0xB:

                            Instructions.Mov(Register.A, Register.E);

                            break;

                        // 0x7C
                        case 0xC:

                            Instructions.Mov(Register.A, Register.H);

                            break;

                        // 0x7D
                        case 0xD:

                            Instructions.Mov(Register.A, Register.L);

                            break;

                        // 0x7E
                        case 0xE:

                            Instructions.Mov(Register.A, Register.MRef);

                            break;

                        // 0x7F
                        case 0xF:

                            Instructions.Mov(Register.A, Register.A);

                            break;
                    }

                    break;

                case 0x8:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x80
                        case 0x0:

                            Instructions.Add(Register.B);

                            break;

                        // 0x81
                        case 0x1:

                            Instructions.Add(Register.C);

                            break;

                        // 0x82
                        case 0x2:

                            Instructions.Add(Register.D);

                            break;

                        // 0x83
                        case 0x3:

                            Instructions.Add(Register.E);

                            break;

                        // 0x84
                        case 0x4:

                            Instructions.Add(Register.H);

                            break;

                        // 0x85
                        case 0x5:

                            Instructions.Add(Register.L);

                            break;

                        // 0x86
                        case 0x6:

                            Instructions.Add(Register.MRef);

                            break;

                        // 0x87
                        case 0x7:

                            Instructions.Add(Register.A);

                            break;

                        // 0x88
                        case 0x8:

                            Instructions.Adc(Register.B);

                            break;

                        // 0x89
                        case 0x9:

                            Instructions.Adc(Register.C);

                            break;

                        // 0x8A
                        case 0xA:

                            Instructions.Adc(Register.D);

                            break;

                        // 0x8B
                        case 0xB:

                            Instructions.Adc(Register.E);

                            break;

                        // 0x8C
                        case 0xC:

                            Instructions.Adc(Register.H);

                            break;

                        // 0x8D
                        case 0xD:

                            Instructions.Adc(Register.L);

                            break;

                        // 0x8E
                        case 0xE:

                            Instructions.Adc(Register.MRef);

                            break;

                        // 0x8F
                        case 0xF:

                            Instructions.Adc(Register.A);

                            break;
                    }

                    break;

                case 0x9:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0x90
                        case 0x0:

                            Instructions.Sub(Register.B);

                            break;

                        // 0x91
                        case 0x1:

                            Instructions.Sub(Register.C);

                            break;

                        // 0x92
                        case 0x2:

                            Instructions.Sub(Register.D);

                            break;

                        // 0x93
                        case 0x3:

                            Instructions.Sub(Register.E);

                            break;

                        // 0x94
                        case 0x4:

                            Instructions.Sub(Register.H);

                            break;

                        // 0x95
                        case 0x5:

                            Instructions.Sub(Register.L);

                            break;

                        // 0x96
                        case 0x6:

                            Instructions.Sub(Register.MRef);

                            break;

                        // 0x97
                        case 0x7:

                            Instructions.Sub(Register.A);

                            break;

                        // 0x98
                        case 0x8:

                            Instructions.Sbb(Register.B);

                            break;

                        // 0x99
                        case 0x9:

                            Instructions.Sbb(Register.C);

                            break;

                        // 0x9A
                        case 0xA:

                            Instructions.Sbb(Register.D);

                            break;

                        // 0x9B
                        case 0xB:

                            Instructions.Sbb(Register.E);

                            break;

                        // 0x9C
                        case 0xC:

                            Instructions.Sbb(Register.H);

                            break;

                        // 0x9D
                        case 0xD:

                            Instructions.Sbb(Register.L);

                            break;

                        // 0x9E
                        case 0xE:

                            Instructions.Sbb(Register.MRef);

                            break;

                        // 0x9F
                        case 0xF:

                            Instructions.Sbb(Register.A);

                            break;
                    }

                    break;

                case 0xA:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0xA0
                        case 0x0:

                            Instructions.Ana(Register.B);

                            break;

                        // 0xA1
                        case 0x1:

                            Instructions.Ana(Register.C);

                            break;

                        // 0xA2
                        case 0x2:

                            Instructions.Ana(Register.D);

                            break;

                        // 0xA3
                        case 0x3:

                            Instructions.Ana(Register.E);

                            break;

                        // 0xA4
                        case 0x4:

                            Instructions.Ana(Register.H);

                            break;

                        // 0xA5
                        case 0x5:

                            Instructions.Ana(Register.L);

                            break;

                        // 0xA6
                        case 0x6:

                            Instructions.Ana(Register.MRef);

                            break;

                        // 0xA7
                        case 0x7:

                            Instructions.Ana(Register.A);

                            break;

                        // 0xA8
                        case 0x8:

                            Instructions.Xra(Register.B);

                            break;

                        // 0xA9
                        case 0x9:

                            Instructions.Xra(Register.C);

                            break;

                        // 0xAA
                        case 0xA:

                            Instructions.Xra(Register.D);

                            break;

                        // 0xAB
                        case 0xB:

                            Instructions.Xra(Register.E);

                            break;

                        // 0xAC
                        case 0xC:

                            Instructions.Xra(Register.H);

                            break;

                        // 0xAD
                        case 0xD:

                            Instructions.Xra(Register.L);

                            break;

                        // 0xAE
                        case 0xE:

                            Instructions.Xra(Register.MRef);

                            break;

                        // 0xAF
                        case 0xF:

                            Instructions.Xra(Register.A);

                            break;
                    }

                    break;

                case 0xB:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0xB0
                        case 0x0:

                            Instructions.Ora(Register.B);

                            break;

                        // 0xB1
                        case 0x1:

                            Instructions.Ora(Register.C);

                            break;

                        // 0xB2
                        case 0x2:

                            Instructions.Ora(Register.D);

                            break;

                        // 0xB3
                        case 0x3:

                            Instructions.Ora(Register.E);

                            break;

                        // 0xB4
                        case 0x4:

                            Instructions.Ora(Register.H);

                            break;

                        // 0xB5
                        case 0x5:

                            Instructions.Ora(Register.L);

                            break;

                        // 0xB6
                        case 0x6:

                            Instructions.Ora(Register.MRef);

                            break;

                        // 0xB7
                        case 0x7:

                            Instructions.Ora(Register.A);

                            break;

                        // 0xB8
                        case 0x8:

                            Instructions.Cmp(Register.B);

                            break;

                        // 0xB9
                        case 0x9:

                            Instructions.Cmp(Register.C);

                            break;

                        // 0xBA
                        case 0xA:

                            Instructions.Cmp(Register.D);

                            break;

                        // 0xBB
                        case 0xB:

                            Instructions.Cmp(Register.E);

                            break;

                        // 0xBC
                        case 0xC:

                            Instructions.Cmp(Register.H);

                            break;

                        // 0xBD
                        case 0xD:

                            Instructions.Cmp(Register.L);

                            break;

                        // 0xBE
                        case 0xE:

                            Instructions.Cmp(Register.MRef);

                            break;

                        // 0xBF
                        case 0xF:

                            Instructions.Cmp(Register.A);

                            break;
                    }

                    break;

                case 0xC:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0xC0
                        case 0x0:

                            Instructions.Rnz();

                            break;

                        // 0xC1
                        case 0x1:

                            Instructions.Pop(RegisterPair.B);

                            break;

                        // 0xC2
                        case 0x2:

                            Instructions.Jnz();

                            break;

                        // 0xC3
                        case 0x3:

                            Instructions.Jmp();

                            break;

                        // 0xC4
                        case 0x4:

                            Instructions.Cnz();

                            break;

                        // 0xC5
                        case 0x5:

                            Instructions.Push(RegisterPair.B);

                            break;

                        // 0xC6
                        case 0x6:

                            Instructions.Adi();

                            break;

                        // 0xC7
                        case 0x7:

                            Instructions.Rst(0);

                            break;

                        // 0xC8
                        case 0x8:

                            Instructions.Rz();

                            break;

                        // 0xC9
                        case 0x9:

                            Instructions.Ret();

                            break;

                        // 0xCA
                        case 0xA:

                            Instructions.Jz();

                            break;

                        // 0xCB
                        case 0xB:

                            Instructions.Jmp();

                            break;

                        // 0xCC
                        case 0xC:

                            Instructions.Cz();

                            break;

                        // 0xCD
                        case 0xD:

                            Instructions.Call();

                            break;

                        // 0xCE
                        case 0xE:

                            Instructions.Aci();

                            break;

                        // 0xCF
                        case 0xF:

                            Instructions.Rst(1);

                            break;
                    }

                    break;

                case 0xD:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0xD0
                        case 0x0:

                            Instructions.Rnc();

                            break;

                        // 0xD1
                        case 0x1:

                            Instructions.Pop(RegisterPair.D);

                            break;

                        // 0xD2
                        case 0x2:

                            Instructions.Jnc();

                            break;

                        // 0xD3
                        case 0x3:

                            Instructions.Out();

                            break;

                        // 0xD4
                        case 0x4:

                            Instructions.Cnc();

                            break;

                        // 0xD5
                        case 0x5:

                            Instructions.Push(RegisterPair.D);

                            break;

                        // 0xD6
                        case 0x6:

                            Instructions.Sui();

                            break;

                        // 0xD7
                        case 0x7:

                            Instructions.Rst(2);

                            break;

                        // 0xD8
                        case 0x8:

                            Instructions.Rc();

                            break;

                        // 0xD9
                        case 0x9:

                            Instructions.Ret();

                            break;

                        // 0xDA
                        case 0xA:

                            Instructions.Jc();

                            break;

                        // 0xDB
                        case 0xB:

                            Instructions.In();

                            break;

                        // 0xDC
                        case 0xC:

                            Instructions.Cc();

                            break;

                        // 0xDD
                        case 0xD:

                            Instructions.Call();

                            break;

                        // 0xDE
                        case 0xE:

                            Instructions.Sbi();

                            break;

                        // 0xDF
                        case 0xF:

                            Instructions.Rst(3);

                            break;
                    }

                    break;

                case 0xE:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0xE0
                        case 0x0:

                            Instructions.Rpo();

                            break;

                        // 0xE1
                        case 0x1:

                            Instructions.Pop(RegisterPair.H);

                            break;

                        // 0xE2
                        case 0x2:

                            Instructions.Jpo();

                            break;

                        // 0xE3
                        case 0x3:

                            Instructions.Xthl();

                            break;

                        // 0xE4
                        case 0x4:

                            Instructions.Cpo();

                            break;

                        // 0xE5
                        case 0x5:

                            Instructions.Push(RegisterPair.H);

                            break;

                        // 0xE6
                        case 0x6:

                            Instructions.Ani();

                            break;

                        // 0xE7
                        case 0x7:

                            Instructions.Rst(4);

                            break;

                        // 0xE8
                        case 0x8:

                            Instructions.Rpe();

                            break;

                        // 0xE9
                        case 0x9:

                            Instructions.Pchl();

                            break;

                        // 0xEA
                        case 0xA:

                            Instructions.Jpe();

                            break;

                        // 0xEB
                        case 0xB:

                            Instructions.Xchg();

                            break;

                        // 0xEC
                        case 0xC:

                            Instructions.Cpe();

                            break;

                        // 0xED
                        case 0xD:

                            Instructions.Call();

                            break;

                        // 0xEE
                        case 0xE:

                            Instructions.Xri();

                            break;

                        // 0xEF
                        case 0xF:

                            Instructions.Rst(5);

                            break;
                    }

                    break;

                case 0xF:
                    switch (OpCodeByte & 0xF)
                    {
                        // 0xF0
                        case 0x0:

                            Instructions.Rp();

                            break;

                        // 0xF1
                        case 0x1:

                            //TODO: registerPair should be PSW but I haven't figured out what that is yet
                            Instructions.Pop(0);

                            break;

                        // 0xF2
                        case 0x2:

                            Instructions.Jp();

                            break;

                        // 0xF3
                        case 0x3:

                            Instructions.Di();

                            break;

                        // 0xF4
                        case 0x4:

                            Instructions.Cp();

                            break;

                        // 0xF5
                        case 0x5:

                            //TODO: registerPair should be PSW but I haven't figured out what that is yet
                            Instructions.Push(0);

                            break;

                        // 0xF6
                        case 0x6:

                            Instructions.Ori();

                            break;

                        // 0xF7
                        case 0x7:

                            Instructions.Rst(6);

                            break;

                        // 0xF8
                        case 0x8:

                            Instructions.Rm();

                            break;

                        // 0xF9
                        case 0x9:

                            Instructions.Sphl();

                            break;

                        // 0xFA
                        case 0xA:

                            Instructions.Jm();

                            break;

                        // 0xFB
                        case 0xB:

                            Instructions.Ei();

                            break;

                        // 0xFC
                        case 0xC:

                            Instructions.Cm();

                            break;

                        // 0xFD
                        case 0xD:

                            Instructions.Call();

                            break;

                        // 0xFE
                        case 0xE:

                            Instructions.Cpi();

                            break;

                        // 0xFF
                        case 0xF:

                            Instructions.Rst(7);

                            break;
                    }

                    break;
            }


            //TODO: Incrementing PC like this might cause issues with things like JMP, watch out
            Pc++;
        }
    }
}