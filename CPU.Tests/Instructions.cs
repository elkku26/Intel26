﻿using NUnit.Framework;
using static CPU.BinaryHelper;

namespace CPU.Tests
{
    /// <summary>
    /// A class to test the individual instructions
    /// </summary>
    [TestFixture]
    public class InstructionTests
    {
        [Test]
        public void InstructionADDRegister_AllFlagsLow_ParitySignHigh()
        {
            var cpu = new Cpu {Registers = {[Register.B] = 130}};
            Instructions.Add(cpu, Register.B);

            //Check that the value in the accumulator is correct
            Assert.That(cpu.Registers[Register.A], Is.EqualTo(130));

            //Check that the parity and sign flags are set
            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("PS")));
        }

        [Test]
        public void InstructionADDRegister_AllFlagsLow_ZeroCarryParityAuxHigh()
        {
            var cpu = new Cpu {Registers = {[Register.A] = 255, [Register.B] = 1}};

            Instructions.Add(cpu, Register.B);

            //Check that the value in the accumulator is correct
            // (255 + 1) & 0xFF = 0
            Assert.That(cpu.Registers[Register.A], Is.EqualTo(0));

            //Check that Zero and Carry flags are high
            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("ZCPA")));
        }

        [Test]
        public void InstructionADDRegister_AllFlagsLow_AuxSignHigh()
        {
            var cpu = new Cpu {Registers = {[Register.A] = 46, [Register.B] = 116}};

            Instructions.Add(cpu, Register.B);

            //Check that the value in the accumulator is correct
            Assert.That(cpu.Registers[Register.A], Is.EqualTo(162));


            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("AS")));

        }

        [Test]
        public void InstructionADDMemRef_AllFlagsLow_AllFlagsLow()
        {
            var cpu = new Cpu {Memory = {[100] = 118}, Registers = {[Register.MRef] = 100}};

            Instructions.Add(cpu, Register.MRef);

            //Check that the value in the accumulator is correct
            Assert.That(cpu.Memory[cpu.Registers[Register.MRef]], Is.EqualTo(118));

            //Check that all flags are empty
            Assert.That(cpu.Flags, Is.Zero);
        }


        [Test]
        public void InstructionADCToRegister_CarryHigh_ParitySignHigh()
        {
            var cpu = new Cpu {Registers = {[Register.B] = 131}};
            cpu.SetFlags(1, FlagSelector.Carry, cpu);
            Instructions.Adc(cpu, Register.B);

            //Check that the value in the accumulator is correct
            Assert.That(cpu.Registers[Register.A], Is.EqualTo(132));

            //Check that the parity and sign flags are set and rest are unset
            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("PS")));
        }

        [Test]
        public void InstructionADCMemRef_AllFlagsLow_AllFlagsLow()
        {
            var cpu = new Cpu {Memory = {[100] = 118}, Registers = {[Register.MRef] = 100}};

            Instructions.Adc(cpu, Register.MRef);

            //Check that the value in the accumulator is correct
            Assert.That(cpu.Memory[cpu.Registers[Register.MRef]], Is.EqualTo(118));

            //Check that all flags are empty
            Assert.That(cpu.Flags, Is.Zero);
        }

        [Test]
        public void InstructionMOVFromBToC_AllFlagsLow_NoChange()
        {
            var cpu = new Cpu {Registers = {[Register.B] = 6}};
            Instructions.Mov(cpu, Register.B, Register.C);

            Assert.That(cpu.Registers[Register.C], Is.EqualTo(6));
            Assert.That(cpu.Registers[Register.B], Is.EqualTo(6));
            Assert.That(cpu.Flags, Is.Zero);
        }

        [Test]
        public void InstructionSUB_A_AllFlagsLow_ParityZeroAuxHigh()
        {
            var cpu = new Cpu {Registers = {[Register.A] = 0x3E}};
            Instructions.Sub(cpu, Register.A);

            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("PZA")));
        }
    }

}