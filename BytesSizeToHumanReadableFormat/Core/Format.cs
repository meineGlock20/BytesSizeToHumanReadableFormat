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
        /// <param name="sizeFormat">The size format to use (e.g., KB, MB, GB) or Auto.</param>
        /// <returns>A string representing the formatted byte size in a human-readable format.</returns>
        public static string ByCulture(CultureInfo culture,
            ulong bytes,
            double d,
            RoundToDecimalPlaces roundToDecimalPlaces,
            bool useThousandsSeparator,
            SizeFormats sizeFormat)
        {
            // Bytes will never have decimal places.
            // REM: "#,#,0" 0 is used for bytes to force a 0 to display if the result == 0, otherwise it would be blank.
            // REM: The new string of # is for removing trailing zeros.

            // If the sizeFormat is not Auto, then the suffix string will be the enum value.
            string suffix = sizeFormat != SizeFormats.Auto ? sizeFormat.ToString() : string.Empty;

            int decimalPlaces = (int)roundToDecimalPlaces;
            string format = useThousandsSeparator ? $"#,#,0.{new string('#', decimalPlaces)}" : $"0.{new string('#', decimalPlaces)}";
            string numberFormat = d.ToString(format, culture);

            // ✨ If the sizeFormat is not Auto, then the suffix string will be the enum value which we already have so return here.
            if (sizeFormat != SizeFormats.Auto) return $"{numberFormat} {suffix}";

            // If the sizeFormat is Auto, then the suffix string will be the best format.
            switch (bytes)
            {
                case ulong n when n < BytesSizeToHumanReadableFormat.KB:
                    suffix = SizeFormats.B.ToString();
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.MB:
                    suffix = SizeFormats.KB.ToString();
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.GB:
                    suffix = SizeFormats.MB.ToString();
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.TB:
                    suffix = SizeFormats.GB.ToString();
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.PB:
                    suffix = SizeFormats.TB.ToString();
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.EB:
                    suffix = SizeFormats.PB.ToString();
                    break;
                default:
                    suffix = SizeFormats.EB.ToString();
                    break;
            }

            return $"{numberFormat} {suffix}";
        }
    }
}