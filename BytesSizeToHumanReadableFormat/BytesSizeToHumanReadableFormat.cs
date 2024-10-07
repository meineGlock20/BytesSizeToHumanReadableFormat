using System;
using System.Globalization;
using System.Linq;

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
    /// <summary>
    /// Possible number of decimal places for a double.
    /// </summary>
    public enum RoundToDecimalPlaces
    {
        Zero, One, Two, Three, Four, Five, Six, Seven, Eight,
        Nine, Ten, Eleven, Twelve, Thirteen, Fourteen, Fifteen
    };

    public enum Formats
    {
        Bytes, KB, MB, GB, TB, PB, EB
    }

    /*
    In C#, a long is a 64-bit signed integer.
     This gives it a range from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807. 
     When it comes to data storage, this range translates to several exabytes, 
     as 1 exabyte equals roughly 1 quintillion bytes. 8.192 exabytes.
    */
    public static class BytesSizeToHumanReadableFormat
    {
        const long kb = 1024;
        const long mb = 1048576;
        const long gb = 1073741824;
        const long tb = 1099511627776;
        const long pb = 1125899906842624;
        const long eb = 1152921504606846976;

        public static string BytesToHumanReadableFormat(this long bytes,
           RoundToDecimalPlaces roundToDecimalPlaces = RoundToDecimalPlaces.One,
           string culture = null,
           Formats? forcedFormat = null)
        {
            double d;
            switch (bytes)
            {
                case long n when n < kb:
                    d = bytes;
                    break;
                case long n when n < mb:
                    d = (double)bytes / kb;
                    break;
                case long n when n < gb:
                    d = (double)bytes / mb;
                    break;
                case long n when n < tb:
                    d = (double)bytes / gb;
                    break;
                case long n when n < pb:
                    d = (double)bytes / tb;
                    break;
                case long n when n < eb:
                    d = (double)bytes / pb;
                    break;
                default:
                    d = (double)bytes / eb;
                    break;
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
                    case long n when n < kb:
                        result = d == 1 ? "1 byte" : $"{d} bytes";
                        break;
                    case long n when n < mb:
                        result = $"{d} KB";
                        break;
                    case long n when n < gb:
                        result = $"{d} MB";
                        break;
                    case long n when n < tb:
                        result = $"{d} GB";
                        break;
                    case long n when n < pb:
                        result = $"{d} TB";
                        break;
                    case long n when n < eb:
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
                    case long n when n < kb:
                        result = d == 1 ? "1 byte" : $"{d.ToString("#,#,0", cultureInfo)} bytes";
                        break;
                    case long n when n < mb:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} KB";
                        break;
                    case long n when n < gb:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} MB";
                        break;
                    case long n when n < tb:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} GB";
                        break;
                    case long n when n < pb:
                        result = $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", cultureInfo)} TB";
                        break;
                    case long n when n < eb:
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