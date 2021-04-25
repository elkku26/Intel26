using System;

namespace CPU
{
    public static class BitHelper
    {
        public static int ParityCounter(int valueToCheck)
        {

            if (valueToCheck > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(valueToCheck), valueToCheck ,"'valueToCheck' should be no larger than one byte (255)");
            }
            
            int parityCount = 0;
            for (int i = 7; i >= 0; i--)
            {
                parityCount += valueToCheck >> i & 1;
            }
            
            //Since an even amount of ones actually leads to 1 as the parity bit, I have to do this 'plus one' nonsense.
            return parityCount + 1 % 2;
        }

        public static int ZeroChecker(int valueToCheck)
        {
            return -1;
        }
    }
}