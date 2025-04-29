using System;
using System.Collections.Generic;

namespace CPU
{
    /// <summary>
    ///     A helper class for dealing with repetitive generic tasks
    /// </summary>
    internal static class CPUHelper
    {
        private static readonly Dictionary<char, byte> FlagAbbr
            = new Dictionary<char, byte>
            {
                { 'C', FlagSelector.Carry },
                { 'P', FlagSelector.Parity },
                { 'A', FlagSelector.AuxCarry },
                { 'Z', FlagSelector.Zero },
                { 'S', FlagSelector.Sign }
            };

        /// <summary>
        ///     Takes in a value and returns its parity
        /// </summary>
        /// <param name="valueToCheck"></param>
        /// <returns>Either 1 or 0 depending on the parity</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal static int ParityCounter(int valueToCheck)
        {
            if (valueToCheck > 255)
                throw new ArgumentOutOfRangeException(nameof(valueToCheck), valueToCheck,
                    $"{nameof(valueToCheck)} should be no larger than one byte (255)");

            var parityCount = 0;
            for (var i = 7; i >= 0; i--) parityCount += (valueToCheck >> i) & 1;

            //Since an even amount of ones actually leads to 1 as the parity bit, I have to do this 'plus one' nonsense.
            return (parityCount + 1) % 2;
        }


        /// <summary>
        ///     Constructs a byte that can be used to check specific CPU flags
        /// </summary>
        /// <param name="flagString">
        ///     A string comprising the flags that should be constructed
        /// </param>
        /// <returns>A byte that for</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal static byte FlagConstructor(string flagString)
        {
            var constructedFlagSelector = new byte();

            foreach (var flag in flagString)
            {
                var upperCaseFlag = char.ToUpper(flag);

                if (!FlagAbbr.ContainsKey(upperCaseFlag))
                    throw new ArgumentOutOfRangeException(nameof(flagString), flagString,
                        $"Every char in {nameof(flagString)} must be a valid flag identifier present in {nameof(FlagAbbr)}.");

                constructedFlagSelector |= FlagAbbr[upperCaseFlag];
            }

            return constructedFlagSelector;
        }

        /// <summary>
        ///     Returns the two's complement of a value
        /// </summary>
        /// <param name="valueToCheck"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal static byte GetTwosComplement(int valueToCheck)
        {
            if (valueToCheck > 255)
                throw new ArgumentOutOfRangeException(nameof(valueToCheck), valueToCheck,
                    $"{nameof(valueToCheck)} should be no larger than one byte (255)");

            return (byte)((~valueToCheck + 1) & 0xFF);
        }

        internal static void SetParity(int value)
        {
            if (ParityCounter(value & 0xFF) == 1)
                Cpu.Current.SetFlags(1, FlagSelector.Parity);
            else
                Cpu.Current.SetFlags(0, FlagSelector.Parity);
        }

        internal static void SetCarry(int value)
        {
            if (value > 255)
                Cpu.Current.SetFlags(1, FlagSelector.Carry);
            else
                Cpu.Current.SetFlags(0, FlagSelector.Carry);
        }

        internal static void SetBorrow(int value)
        {
            if (value < 255)
                Cpu.Current.SetFlags(1, FlagSelector.Carry);
            else
                Cpu.Current.SetFlags(0, FlagSelector.Carry);
        }

        internal static void SetZero(int value)
        {
            if ((value & 0xFF) == 0)
                Cpu.Current.SetFlags(1, FlagSelector.Zero);
            else
                Cpu.Current.SetFlags(0, FlagSelector.Zero);
        }

        internal static void SetSign(int value)
        {
            //Check if Sign bit should be set
            if ((value & FlagSelector.Sign) == FlagSelector.Sign)
                //Set the sign bit to 1
                Cpu.Current.SetFlags(1, FlagSelector.Sign);
            else
                //Set the sign bit to 0
                Cpu.Current.SetFlags(0, FlagSelector.Sign);
        }

        internal static void SetAux(int value1, int value2)
        {
            //check if there is a carry out of bit 3
            if ((value1 & 0xF) + (value2 & 0xF) > 15)
                Cpu.Current.SetFlags(1, FlagSelector.AuxCarry);
            else
                Cpu.Current.SetFlags(0, FlagSelector.AuxCarry);
        }
    }
}