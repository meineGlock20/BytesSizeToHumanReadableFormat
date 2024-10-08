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
        public static string ByCulture(CultureInfo culture, ulong bytes, double d, RoundToDecimalPlaces roundToDecimalPlaces, bool useThousandsSeparator)
        {
            // Format the string for the culture. Bytes will never have decimal places.
            // REM: "#,#,0" 0 is used for bytes to force a 0 to display if the result == 0, otherwise it would be blank.
            // REM: The new string of # is for removing trailing zeros.

            int decimalPlaces = (int)roundToDecimalPlaces;
            string result;
            switch (bytes)
            {
                case ulong n when n < BytesSizeToHumanReadableFormat.KB:
                    result = useThousandsSeparator ? $"{d.ToString("#,#,0", culture)} B" : $"{d.ToString("0", culture)} B";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.MB:
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} KB" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} KB";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.GB:
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} MB" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} MB";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.TB:
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} GB" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} GB";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.PB:
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} TB" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} TB";
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.EB:
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} PB" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} PB";
                    break;
                default:
                    result = useThousandsSeparator ? $"{d.ToString($"#,#.{new string('#', decimalPlaces)}", culture)} EB" : $"{d.ToString($"0.{new string('0', decimalPlaces)}", culture)} EB";
                    break;
            }

            return result;
        }
    }
}