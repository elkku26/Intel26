using NUnit.Framework;
using static CPU.CPUHelper;

namespace CPU.Tests
{
    
   /// <summary>
    /// A class to test the individual instructions
    /// </summary>
    [TestFixture]
    public class InstructionTests
    {
        [TestCase(0, 130, 130, "PS")] // AllFlagsLow_ParitySignHigh
        [TestCase(255, 1, 0, "ZCPA")] // AllFlagsLow_ZeroCarryParityAuxHigh
        [TestCase(46, 116, 162, "AS")]// AllFlagsLow_AuxSignHigh
        [TestCase(0, 130, 130, "PS")] // AllFlagsLow_ParitySignHigh
        
        public void InstructionADDRegister(byte a, byte b, byte expected, string expectedFlags)
        {
            var cpu = new Cpu { Registers = { [Register.A] = a, [Register.B] = b } };
            Instructions.Add(cpu, Register.B);

            Assert.That(cpu.Registers[Register.A], Is.EqualTo(expected));
            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor(expectedFlags)));
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