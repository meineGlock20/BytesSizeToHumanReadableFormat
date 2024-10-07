using System;
using System.Globalization;
using BytesSizeToHumanReadableFormat;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

long bytes = 1000;
Console.WriteLine(bytes.BytesToHumanReadableFormat(RoundToDecimalPlaces.Two, ""));
