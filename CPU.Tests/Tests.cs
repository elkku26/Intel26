using NUnit.Framework;
using NUnit.Framework.Constraints;
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
            var cpu = new Cpu();
            cpu.Registers[Register.B] = 130;
            Instructions.Add(cpu, Register.B);

            //Check that the value in the accumulator is correct
            Assert.That(cpu.Registers[Register.A], Is.EqualTo(130));

            //Check that the parity and sign flags are set
            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("PS")));
        }

        [Test]
        public void InstructionADDRegister_AllFlagsLow_ZeroCarryParityAuxHigh()
        {
            var cpu = new Cpu();
            cpu.Registers[Register.A] = 255;
            cpu.Registers[Register.B] = 1;
            
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
            var cpu = new Cpu();
            cpu.Registers[Register.A] = 46;
            cpu.Registers[Register.B] = 116;
            
            Instructions.Add(cpu, Register.B);
            
            //Check that the value in the accumulator is correct
            Assert.That(cpu.Registers[Register.A], Is.EqualTo(162));


            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("AS")));
            
        }

        [Test]
        public void InstructionADDMemRef_AllFlagsLow_AllFlagsLow()
        {
            var cpu = new Cpu();

            cpu.Memory[100] = 118;

            cpu.Registers[Register.MRef] = 100;

            Instructions.Add(cpu, Register.MRef);

            //Check that the value in the accumulator is correct
            Assert.That(cpu.Memory[cpu.Registers[Register.MRef]], Is.EqualTo(118));

            //Check that all flags are empty
            Assert.That(cpu.Flags, Is.Zero);
        }


        [Test]
        public void InstructionADCToRegister_CarryHigh_ParitySignHigh()
        {
            var cpu = new Cpu();
            cpu.Registers[Register.B] = 131;
            cpu.SetFlags(1, FlagSelector.Carry, cpu);
            Instructions.Adc(cpu, Register.B);

            //Check that the value in the accumulator is correct
            Assert.That(cpu.Registers[Register.A], Is.EqualTo(132));

            //Check that the parity and sign flags are set and rest are unset
            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("PS")));
        }

        [Test]
        public void InstructionMOVFromBToC_AllFlagsLow_NoChange()
        {
            var cpu = new Cpu();
            cpu.Registers[Register.B] = 6;
            Instructions.Mov(cpu, Register.B, Register.C);

            Assert.That(cpu.Registers[Register.C], Is.EqualTo(6));
            Assert.That(cpu.Registers[Register.B], Is.EqualTo(6));
            Assert.That(cpu.Flags, Is.Zero);
        }
    }

    [TestFixture]
    public class HelperTests
    {

        
        [Test]
        public void BinaryHelperSetFlag_AllFlagsLow_SetAllHigh()
        {
            var cpu = new Cpu();
            cpu.Flags = new byte();
            cpu.SetFlags(1, FlagConstructor("CPAZS"), cpu);

            //Check that carry is set
            Assert.That(cpu.Flags, Is.EqualTo(FlagConstructor("CPAZS")));
        }

        [Test]
        public void BinaryHelperFlagConstructor_CreateParityAuxSelector_Success()
        {
            var selector = FlagConstructor("PA");

            //check that constructed selector is correct
            Assert.That(selector, Is.EqualTo(FlagSelector.Parity | FlagSelector.AuxCarry));
        }


        [Test]
        public void BinaryHelperSetFlag_ParitySignHigh_SetParityLow()
        {
            var cpu = new Cpu();
            cpu.Flags = FlagConstructor("PS");
            cpu.SetFlags(0, FlagSelector.Parity, cpu);

            //Check that the correct flags are set
            //(sign is set, rest unset)
            Assert.That(cpu.Flags, Is.EqualTo(FlagSelector.Sign));

        }
        [Test]
        public void BinaryHelperParityCounter_UnevenParity_Return0()
        {
            var parity = ParityCounter(0b00011111);
            Assert.That(parity, Is.Zero);
        }
        [Test]
        public void BinaryHelperParityCounter_EvenParity_Return1()
        {
            var parity = ParityCounter(0b00001111);
            Assert.That(parity, Is.EqualTo(1));
        }
    }

}