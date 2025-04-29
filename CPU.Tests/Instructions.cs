using System;
using System.ComponentModel;
using NUnit.Framework;
using static CPU.CPUHelper;

namespace CPU.Tests
{
    /// <summary>
    ///     A class to test the individual instructions
    /// </summary>
    [TestFixture]
    public class InstructionTests
    {
        [TestCase(0, 130, 130, "PS")] // AllFlagsLow_ParitySignHigh
        [TestCase(255, 1, 0, "ZCPA")] // AllFlagsLow_ZeroCarryParityAuxHigh
        [TestCase(46, 116, 162, "AS")] // AllFlagsLow_AuxSignHigh
        [TestCase(0, 130, 130, "PS")] // AllFlagsLow_ParitySignHigh
        public void InstructionADDRegister(byte a, byte b, byte expected, string expectedFlags)
        {
            Cpu.Current = new Cpu { Registers = { [Register.A] = a, [Register.B] = b } };
            Instructions.Add(Register.B);

            Assert.That(Cpu.Current.Registers[Register.A], Is.EqualTo(expected));
            Assert.That(Cpu.Current.Flags, Is.EqualTo(FlagConstructor(expectedFlags)));
        }


        [Test]
        public void InstructionADDMemRef_AllFlagsLow_AllFlagsLow()
        {
            Cpu.Current = new Cpu { Memory = { [100] = 118 }, Registers = { [Register.MRef] = 100 } };

            Instructions.Add(Register.MRef);

            //Check that the value in the accumulator is correct
            Assert.That(Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]], Is.EqualTo(118));

            //Check that all flags are empty
            Assert.That(Cpu.Current.Flags, Is.Zero);
        }


        [Test]
        public void InstructionADCToRegister_CarryHigh_ParitySignHigh()
        {
            Cpu.Current = new Cpu { Registers = { [Register.B] = 131 } };

            Cpu.Current.SetFlags(1, FlagSelector.Carry);
            Instructions.Adc(Register.B);

            //Check that the value in the accumulator is correct
            Assert.That(Cpu.Current.Registers[Register.A], Is.EqualTo(132));

            //Check that the parity and sign flags are set and rest are unset
            Assert.That(Cpu.Current.Flags, Is.EqualTo(FlagConstructor("PS")));
        }

        [Test]
        public void InstructionADCMemRef_AllFlagsLow_AllFlagsLow()
        {
            Cpu.Current = new Cpu { Memory = { [100] = 118 }, Registers = { [Register.MRef] = 100 } };

            Instructions.Adc(Register.MRef);

            //Check that the value in the accumulator is correct
            Assert.That(Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]], Is.EqualTo(118));

            //Check that all flags are empty
            Assert.That(Cpu.Current.Flags, Is.Zero);
        }

        [Test]
        public void InstructionMOVFromBToC_AllFlagsLow_NoChange()
        {
            Cpu.Current = new Cpu { Registers = { [Register.B] = 6 } };
            Instructions.Mov(Register.B, Register.C);

            Assert.That(Cpu.Current.Registers[Register.C], Is.EqualTo(6));
            Assert.That(Cpu.Current.Registers[Register.B], Is.EqualTo(6));
            Assert.That(Cpu.Current.Flags, Is.Zero);
        }

        [Test]
        public void InstructionSUB_A_AllFlagsLow_ParityZeroAuxHigh()
        {
            Cpu.Current = new Cpu { Registers = { [Register.A] = 0x3E } };
            Cpu.Current.Flags = FlagConstructor("");
            Instructions.Sub(Register.A);

            Assert.That(Cpu.Current.Flags, Is.EqualTo(FlagConstructor("PZA")));
        }


        [TestCase(RegisterPair.B, (ushort)0xFFAA)]
        [TestCase(RegisterPair.D, (ushort)0xFFAA)]
        [TestCase(RegisterPair.H, (ushort)0xFFAA)]
        [TestCase(RegisterPair.SP, (ushort)0xFFAA)]
        public void InstructionLXI(int registerPair, ushort immediate)
        {
            Cpu.Current = new Cpu
            {
                Memory = { [1] = BitConverter.GetBytes(immediate)[1], [2] = BitConverter.GetBytes(immediate)[0] },
                Sp = 2048
            };
            Instructions.Lxi(registerPair);

            switch (registerPair)
            {
                case RegisterPair.B:
                    Assert.That(Cpu.Current.Registers[Register.B], Is.EqualTo(0xAA));
                    Assert.That(Cpu.Current.Registers[Register.C], Is.EqualTo(0xFF));

                    break;

                case RegisterPair.D:
                    Assert.That(Cpu.Current.Registers[Register.D], Is.EqualTo(0xAA));
                    Assert.That(Cpu.Current.Registers[Register.E], Is.EqualTo(0xFF));
                    break;

                case RegisterPair.H:
                    Assert.That(Cpu.Current.Registers[Register.H], Is.EqualTo(0xAA));
                    Assert.That(Cpu.Current.Registers[Register.L], Is.EqualTo(0xFF));

                    break;

                case RegisterPair.SP:
                    Assert.That(BitConverter.ToUInt16(Cpu.Current.Memory, Cpu.Current.Sp), Is.EqualTo(0xAAFF));
                    break;
            }
        }

        [TestCase(Register.A, 0xFA)]
        [TestCase(Register.B, 0xFA)]
        [TestCase(Register.C, 0xFA)]
        [TestCase(Register.D, 0xFA)]
        [TestCase(Register.E, 0xFA)]
        [TestCase(Register.MRef, 0xFA)]
        public void InstructionMVI(int register, byte immediate)
        {
            Cpu.Current = new Cpu { Memory = { [1] = immediate } };
            Instructions.Mvi(register);

            Assert.That(Cpu.Current.Registers[register], Is.EqualTo(immediate));
        }
    }
}