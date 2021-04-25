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
    }
}