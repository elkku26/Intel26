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
            Assert.That(cpu.Registers[Register.A], Is.EqualTo(5));

            //Check that the carry flag is unset as it should be
            Assert.That(cpu.Flags & FlagSelector.Carry, Is.Zero);
        }

        [Test]
        public void BitHelperSetFlag_AllFlagsLow_SetCarryHigh()
        {
            var cpu = new Cpu("", "");
            cpu.Flags = new byte();
            BitHelper.SetFlag(1, FlagSelector.Carry, cpu);

            //Check that carry is set
            Assert.That(cpu.Flags, Is.EqualTo(FlagSelector.Carry));
        }

        [Test]
        public void BitHelperFlagConstructor_CreateParityAuxSelector_Success()
        {
            var selector = BitHelper.FlagConstructor("PA");

            //check that constructed selector is correct
            Assert.That(selector, Is.EqualTo(FlagSelector.Parity | FlagSelector.AuxCarry));
        }
        
        
        [Test]
        public void BitHelperSetFlag_ParitySignHigh_SetParityLow()
        {
            var cpu = new Cpu("", "");
            cpu.Flags = FlagSelector.Parity | FlagSelector.Sign;
            BitHelper.SetFlag(0, FlagSelector.Parity, cpu);

            //Check that sign is set
            Assert.That(cpu.Flags, Is.EqualTo(FlagSelector.Sign));

            //Check that rest are unset
            Assert.That((cpu.Flags & BitHelper.FlagConstructor("CPAZ")), Is.Zero);
        }
        [Test]
        public void BitHelperParityCounter_ParityUneven_Return0()
        {
            var parity = BitHelper.ParityCounter(0b00011111);
            Assert.That(parity, Is.EqualTo(0));
        }
        [Test]
        public void BitHelperParityCounter_ParityEven_Return1()
        {
            var parity = BitHelper.ParityCounter(0b00001111);
            Assert.That(parity, Is.EqualTo(1));
        }
    }
}