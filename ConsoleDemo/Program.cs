using System;
using System.Globalization;
using BytesSizeToHumanReadableFormat;

Console.WriteLine("Fake Movie Listing With Disk Size!");
Console.WriteLine();

Console.WriteLine("📌 Default Settings:");
var movies = new List<Movie>
{
    new() { Name = "The Shawshank Redemption", Size = 867630342949 },
    new() { Name = "The Godfather", Size = 18446744073709551615 },
    new() { Name = "The Dark Knight", Size = 2208245174 },
    new() { Name = "The Godfather: Part II", Size = 750052082558 },
    new() { Name = "The Lord of the Rings", Size = 528489456732 },
    new() { Name = "Pulp Fiction", Size = 0 },
    new() { Name = "Schindler's List", Size = 1050591100 },
};

foreach (var movie in movies.OrderByDescending(x => x.Size))
{
    Console.WriteLine($"{movie.Name} - {movie.Size.BytesToHumanReadableFormat()}");
}

Console.WriteLine();
Console.WriteLine("📌 Using German culture, rounding to 4 decimal places, forcing to MB, and use a thousands seperator:");

CultureInfo culture = new("de-De");
foreach (var movie in movies.OrderByDescending(x => x.Size))
{
    Console.WriteLine($"{movie.Name} - {movie.Size.BytesToHumanReadableFormat(culture, RoundToDecimalPlaces.Four, SizeFormats.MB, useThousandsSeparator: true)}");
}

class Movie
{
    public string? Name { get; set; }
    public ulong Size { get; set; }
}

// The max value that can be returned is 16 exabytes.
// ulong bytes = 18446744073709000000;
// Console.WriteLine(bytes.BytesToHumanReadableFormat());
