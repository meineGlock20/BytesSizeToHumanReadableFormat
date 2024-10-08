using System;
using System.Globalization;
using System.Linq;
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
    public enum Formats
    {
        B, KB, MB, GB, TB, PB, EB
    }

    public static class BytesSizeToHumanReadableFormat
    {
        public const long KB = 1024;
        public const long MB = 1048576;
        public const long GB = 1073741824;
        public const long TB = 1099511627776;
        public const long PB = 1125899906842624;
        public const long EB = 1152921504606846976;

        public static string BytesToHumanReadableFormat(this ulong bytes,
           RoundToDecimalPlaces roundToDecimalPlaces = RoundToDecimalPlaces.One,
           string culture = null,
           Formats? forcedFormat = null)
        {
            double d;

            // If no format is forced, calculate the best format.
            if (forcedFormat is null)
            {
                d = Calculate.Auto(bytes);
            }
            else
            {
                d = Calculate.Forced(bytes, (Formats)forcedFormat);
            }

            // Round to the specified number of decimal places.
            int decimalPlaces = (int)roundToDecimalPlaces;
            d = Math.Round(d, decimalPlaces);

            // If culture is not passed or the passed one does not exist, use Invariant.
            CultureInfo cultureInfo = culture is null || !cultureExists() ? CultureInfo.InvariantCulture : new CultureInfo(culture);
            bool cultureExists()
            {
                return CultureInfo.GetCultures(CultureTypes.AllCultures).Any(x => x.Name.Equals(culture, StringComparison.OrdinalIgnoreCase));
            };

            // Format the string for the culture. Bytes will never have decimal places.
            // REM: "#,#,0" is used for bytes to force a 0 to display if the result == 0, otherwise it would be blank.
            // REM: The new string of # is for removing trailing zeros.
            string result;
            if (culture is null)
            {
                switch (bytes)
                {
                    case ulong n when n < KB:
                        result = d == 1 ? "1 byte" : $"{d} bytes";
                        break;
                    case ulong n when n < MB:
                        result = $"{d} KB";
                        break;
                    case ulong n when n < GB:
                        result = $"{d} MB";
                        break;
                    case ulong n when n < TB:
                        result = $"{d} GB";
                        break;
                    case ulong n when n < PB:
                        result = $"{d} TB";
                        break;
                    case ulong n when n < EB:
                        result = $"{d} PB";
                        break;
                    default:
                        result = $"{d} EB";
                        break;
                }
            }
            else
            {
                switch (bytes)
                {
                    case ulong n when n < KB:
                        result = $"{d.ToString("#,#,0", cultureInfo)} B";
                        break;
                    case ulong n when n < MB:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} KB";
                        break;
                    case ulong n when n < GB:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} MB";
                        break;
                    case ulong n when n < TB:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} GB";
                        break;
                    case ulong n when n < PB:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} TB";
                        break;
                    case ulong n when n < EB:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} PB";
                        break;
                    default:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} EB";
                        break;
                }

            }

            return result;
        }
    }
}