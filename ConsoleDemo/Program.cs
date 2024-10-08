using System;
using System.Globalization;
using BytesSizeToHumanReadableFormat;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

ulong bytes = 18446744073709000000;
Console.WriteLine(bytes.BytesToHumanReadableFormat());

// System.Console.WriteLine();
// var t = 9223372036854775807 / 1152921504606846976;

// System.Console.WriteLine(t);

// decimal numerator = 9223372036854775807m;
// decimal denominator = 1152921504606846976m;
// decimal result = numerator / denominator;
// Console.WriteLine(result);

// bytes = 18446744073709551615;
// decimal eb = 1152921504606846976; // 1 petabyte in bytes (2^50)
// decimal d = (decimal)bytes / eb;
// Console.WriteLine(d.ToString("F15")); // Display the result rounded to 15 decimal places

// Console.WriteLine(((double)bytes).ToString());
