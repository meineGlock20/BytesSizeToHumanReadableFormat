using System;
using System.Globalization;

namespace BytesSizeToHumanReadableFormat.Core
{
    internal class Format
    {
        /// <summary>
        /// Formats the byte size into a human-readable string based on the specified culture.
        /// </summary>
        /// <param name="culture">The culture to use for formatting.</param>
        /// <param name="bytes">The size in bytes to format.</param>
        /// <param name="d">The size in the appropriate unit (KB, MB, GB, etc.) to format.</param>
        /// <param name="roundToDecimalPlaces">The number of decimal places to round to.</param>
        /// <param name="useThousandsSeparator">Whether to use a thousands separator in the formatted string.</param>
        /// <returns>A string representing the formatted byte size in a human-readable format.</returns>
        public static string ByCulture(CultureInfo culture,
            ulong bytes,
            double d,
            RoundToDecimalPlaces roundToDecimalPlaces,
            bool useThousandsSeparator,
            SizeFormats sizeFormat)
        {
            // Format the string for the culture. Bytes will never have decimal places.
            // REM: "#,#,0" 0 is used for bytes to force a 0 to display if the result == 0, otherwise it would be blank.
            // REM: The new string of # is for removing trailing zeros.

            // If the sizeFormat is not Auto, then the suffix string will be the enum value.
            string suffix = sizeFormat != SizeFormats.Auto ? sizeFormat.ToString() : string.Empty;

            int decimalPlaces = (int)roundToDecimalPlaces;
            string result;
            switch (bytes)
            {
                case ulong n when n < BytesSizeToHumanReadableFormat.KB:
                    suffix = sizeFormat == SizeFormats.Auto ? SizeFormats.B.ToString() : suffix;
                    result = useThousandsSeparator ? $"{d.ToString("#,#,0", culture)} {suffix}" : $"{d.ToString("0", culture)} {suffix}";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.MB:
                    suffix = sizeFormat == SizeFormats.Auto ? SizeFormats.KB.ToString() : suffix;
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} {suffix}" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} {suffix}";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.GB:
                    suffix = sizeFormat == SizeFormats.Auto ? SizeFormats.MB.ToString() : suffix;
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} {suffix}" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} {suffix}";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.TB:
                    suffix = sizeFormat == SizeFormats.Auto ? SizeFormats.GB.ToString() : suffix;
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} {suffix}" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} {suffix}";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.PB:
                    suffix = sizeFormat == SizeFormats.Auto ? SizeFormats.TB.ToString() : suffix;
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} {suffix}" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} {suffix}";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.EB:
                    suffix = sizeFormat == SizeFormats.Auto ? SizeFormats.PB.ToString() : suffix;
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} {suffix}" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} {suffix}";
                    break;
                default:
                    suffix = sizeFormat == SizeFormats.Auto ? SizeFormats.EB.ToString() : suffix;
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} {suffix}" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} {suffix}";
                    break;
            }

            return result;
        }
    }
}