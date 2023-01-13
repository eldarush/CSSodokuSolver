using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Algoritms
{
    public static class BackTrackingCreatingFunctions
    {
        /// <summary>
        /// function that gets a value and counts the amount of activated bits in the numbe
        /// Equal to writing CountOnes from System.Numerics.BitOperations
        /// </summary>
        /// <param name="value">thevalue to be converted</param>
        /// <returns>counts the amount of activated bits in the number</returns>
        public static int GetActivatedBits(int value)
        {
            // set value to the result of a bitwise AND operation between value and value - 1 and increment the location
            int numActivatedBits = 0;
            while (value > 0)
            {
                value &= value - 1;
                numActivatedBits++;
            }
            return numActivatedBits;
        }

        /// <summary>
        /// function that determines the index of the most significant bit that is set to 1 in a binary representation of a given integer value.
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the index of the most significant bit that is set to 1</returns>
        public static int GetIndexOfMostSignificantActivatedBit(int value)
        {
            int msbIndex = 0;
            while (value != 0)
            {
                // divide value by 2 using right shift and increment the location
                value >>= 1;
                msbIndex++;
            }
            return msbIndex;
        }

        /// <summary>
        /// function that will get the row, col, and block and helper mask and will copy them into new copies of them
        /// </summary>
        /// <param name="oldRowValues">row bitmask</param>
        /// <param name="oldColValues">col bitmask</param>
        /// <param name="oldBlockValues">block bitmask</param>
        /// <param name="oldHelperMask">helper bitmask</param>
        /// <param name="newRowValues">new row bitmask</param>
        /// <param name="newColValues">new col bitmask</param>
        /// <param name="newBlockValues">new block bitmask</param>
        /// <param name="newHelperMask">new helper bitmask</param>
        public static void CopyBitMasks(int[] oldRowValues, int[] oldColValues, int[] oldBlockValues, int[] oldHelperMask,
             out int[] newRowValues, out int[] newColValues, out int[] newBlockValues, out int[] newHelperMask)
        {
            // make the new row, col, and block values use the old one's memeory space
            newRowValues = oldRowValues;
            newColValues = oldColValues;
            newBlockValues = oldBlockValues;
            newHelperMask = oldHelperMask;
        }
    }
}
