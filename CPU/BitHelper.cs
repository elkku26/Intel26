using System;
using System.Collections.Generic;

namespace CPU
{
    internal static class BitHelper
    {

        private static readonly Dictionary<char, byte> FlagAbbr
            = new Dictionary<char, byte>
            {
                {'C', FlagSelector.Carry},
                {'P', FlagSelector.Parity},
                {'A', FlagSelector.AuxCarry},
                {'Z', FlagSelector.Zero},
                {'S', FlagSelector.Sign}

            };

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

        internal static void SetFlag(int status, int selector, Cpu cpu)
        {
            cpu.Flags = (byte) (status == 1 ? cpu.Flags | selector : cpu.Flags & ~selector & 0xFF);
        }

        internal static byte FlagConstructor(string flagString)
        {
            var constructedFlagSelector = new byte();

            foreach (char flag in flagString)
            {
                var sanitisedFlag = Char.ToUpper(flag);
                if (!FlagAbbr.ContainsKey(sanitisedFlag))
                {
                    throw new ArgumentOutOfRangeException(nameof(flagString), flagString,
                        $"Every char in {nameof(flagString)} must be a valid flag identifier present in FlagAbbr.");
                }

                constructedFlagSelector |= FlagAbbr[sanitisedFlag];
            }

            return constructedFlagSelector;
        }
    }
}