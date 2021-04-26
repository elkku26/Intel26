using NUnit.Framework;

namespace CPU.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void InstructionADD_AllFlagsLow_Success()
        {
            var cpu = new Cpu("", "");
            cpu.Pc = 0;
            cpu.Registers = new byte[8];
            cpu.Registers[Register.B] = 5;
            Instructions.Add(cpu, Register.B);

            //Check that the value in the accumulator is correct
            Assert.True(cpu.Registers[Register.A] == 5);

            //Check that the carry flag is unset as it should be
            Assert.True((cpu.Flags & FlagSelector.Carry) == 0);
        }

        [Test]
        public void BitHelperSetFlag_AllFlagsLow_SetCarryHigh()
        {
            var cpu = new Cpu("", "");
            cpu.Flags = new byte();
            BitHelper.SetFlag(1, FlagSelector.Carry, cpu);

            //Check that carry is set
            Assert.True(cpu.Flags == FlagSelector.Carry);
        }

        [Test]
        public void BitHelperFlagConstructor_CreateParityAuxSelector_Success()
        {
            var selector = BitHelper.FlagConstructor("PA");

            //check that constructed selector is correct
            Assert.True(selector == (FlagSelector.Parity | FlagSelector.AuxCarry));
        }
        
        
        [Test]
        public void BitHelperSetFlag_ParitySignHigh_SetParityLow()
        {
            var cpu = new Cpu("", "");
            cpu.Flags = FlagSelector.Parity | FlagSelector.Sign;
            BitHelper.SetFlag(0, FlagSelector.Parity, cpu);

            //Check that sign is set
            Assert.True(cpu.Flags == FlagSelector.Sign);

            //Check that rest are unset
            Assert.True((cpu.Flags & BitHelper.FlagConstructor("CPAZ")) == 0);



        }
    }
}