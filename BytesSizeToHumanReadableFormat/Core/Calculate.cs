using System;

namespace BytesSizeToHumanReadableFormat.Core
{
    internal class Calculate
    {
        /// <summary>
        /// Auto calculates the best format for the result based on the number of bytes.
        /// </summary>
        /// <param name="bytes">The number of bytes to convert.</param>
        public static double Auto(ulong bytes)
        {
            double d;
            switch (bytes)
            {
                case ulong n when n < BytesSizeToHumanReadableFormat.KB:
                    d = bytes;
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.MB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.KB;
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.GB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.MB;
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.TB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.GB;
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.PB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.TB;
                    break;
                case ulong n when n < BytesSizeToHumanReadableFormat.EB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.PB;
                    break;
                default:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.EB;
                    break;
            }
            return d;
        }

        /// <summary>
        /// Forces the result to be in the specified format.
        /// </summary>
        /// <param name="bytes">The number of bytes to convert.</param>
        /// <param name="format">The format to convert the bytes to.</param>
        public static double Forced(ulong bytes, Formats format)
        {
            double d;
            switch (format)
            {
                case Formats.B:
                    d = bytes;
                    break;
                case Formats.KB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.KB;
                    break;
                case Formats.MB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.MB;
                    break;
                case Formats.GB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.GB;
                    break;
                case Formats.TB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.TB;
                    break;
                case Formats.PB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.PB;
                    break;
                case Formats.EB:
                    d = (double)bytes / BytesSizeToHumanReadableFormat.EB;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
            return d;
        }
    }
}