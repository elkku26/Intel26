using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using static CPU.BinaryHelper;

namespace CPU.Tests
{
    [TestFixture]
    public class HelperTests
    {


        [Test]
        public void BinaryHelperSetFlag_AllFlagsLow_SetAllHigh()
        {
            var cpu = new Cpu {Flags = new byte()};
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

        [Test]
        public void BinaryHelperGetTwosComplement_Success()
        {
            var twosComplement = GetTwosComplement(10);
            Assert.That(twosComplement, Is.EqualTo(0xF6));
        }

        [Test]
        public void BinaryHelperGetTwosComplement_ArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                delegate { GetTwosComplement(0xFFF); }
            );
        }
    }


}