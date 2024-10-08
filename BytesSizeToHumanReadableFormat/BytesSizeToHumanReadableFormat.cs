using System.Globalization;
using BytesSizeToHumanReadableFormat.Core;

/*
    MIT License

    Copyright (c) 2024 meineGlock20.

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/
namespace BytesSizeToHumanReadableFormat
{
    /*
        In C#, a ulong is a 64-bit unsigned integer.
        This gives it a range from 0 to 18,446,744,073,709,551,615. 
        That number is eighteen quintillion, four hundred forty-six quadrillion, seven hundred forty-four trillion, seventy-three billion, seven hundred nine million in English.
        18446744073709551615 / 1152921504606846976 = 16 EB.
    */

    /// <summary>
    /// Converts a byte size into a human-readable format string.
    /// Possible number of decimal places for a double.
    /// </summary>
    public enum RoundToDecimalPlaces
    {
        Zero, One, Two, Three, Four, Five, Six, Seven, Eight,
        Nine, Ten, Eleven, Twelve, Thirteen, Fourteen, Fifteen
    };

    /// <summary>
    /// Possible formats for the result.
    /// </summary>
    public enum SizeFormats
    {
        Auto, B, KB, MB, GB, TB, PB, EB
    }

    public static class BytesSizeToHumanReadableFormat
    {
        public const long KB = 1024;
        public const long MB = 1048576;
        public const long GB = 1073741824;
        public const long TB = 1099511627776;
        public const long PB = 1125899906842624;
        public const long EB = 1152921504606846976;

        /// <summary>
        /// Converts a byte size into a human-readable format string.
        /// </summary>
        /// <param name="bytes">The size in bytes to be converted.</param>
        /// <param name="culture">The culture information to format the output string. Leave null to use InvariantCulture.</param>
        /// <param name="roundToDecimalPlaces">The number of decimal places to round the result to. Default is 2. Range is 0-15. Trailing zeros are removed.</param>
        /// <param name="sizeFormat">The size format to use (e.g., KB, MB, GB). Default is Auto, which calculates the best format.</param>
        /// <param name="useThousandsSeparator">Indicates whether to use a thousands separator in the formatted string. Default is false.</param>
        /// <returns>A string representing the byte size in a human-readable format.</returns>
        public static string BytesToHumanReadableFormat(this ulong bytes,
           CultureInfo culture = null,
           RoundToDecimalPlaces roundToDecimalPlaces = RoundToDecimalPlaces.Two,
           SizeFormats sizeFormat = SizeFormats.Auto,
           bool useThousandsSeparator = false)
        {
            if (culture is null) culture = CultureInfo.InvariantCulture;

            double d;

            // If no size format is forced, calculate the best format.
            if (sizeFormat == SizeFormats.Auto)
            {
                d = Calculate.Auto(bytes);
            }
            else
            {
                d = Calculate.Forced(bytes, sizeFormat);
            }

            return Format.ByCulture(culture, bytes, d, roundToDecimalPlaces, useThousandsSeparator);
        }
    }
}