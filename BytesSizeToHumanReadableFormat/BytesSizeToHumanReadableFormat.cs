using System;

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
                default:
                    d = (double)bytes / pb;
                    break;
            }


            // Implementation goes here
            return "";
        }
    }

}