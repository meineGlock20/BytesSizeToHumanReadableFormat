# Bytes Size to Human Readable Format

This is a .NET Standard class library that provides an extension for transforming a ulong of bytes into a human-readable format. It is primarily designed for measuring disk sizes. For example, passing 867630342949 will return the string "808.4 GB".

Key features include:

- Localization support via the optional Culture parameter.
- Customizable decimal places (0 to 15).
- Ability to force formatting to a specific unit (e.g., MB, GB, PB).
- Optional thousands separator for readability.
- Handles up to 16 EXABYTES.

## Installation

You can install the lastest package via NuGet:

`dotnet add package MeineGlock.BytesSizeToHumanReadableFormat --version 1.0.0`

## Usage

```csharp
Console.WriteLine("📌 Default Settings:");
foreach (var movie in movies.OrderByDescending(x => x.Size))
{
    Console.WriteLine($"{movie.Name} - {movie.Size.BytesToHumanReadableFormat()}");
}
```

Results
```
📌 Default Settings:
The Godfather - 16 EB
The Shawshank Redemption - 808.04 GB
The Godfather: Part II - 698.54 GB
The Lord of the Rings - 492.19 GB
Schindler's List - 1001.92 MB
The Dark Knight - 215.65 KB
Pulp Fiction - 0 B
```

Full Implementation Example (See the console demo)
```csharp
using System.Globalization;
using BytesSizeToHumanReadableFormat;

Console.WriteLine("Fake Movie Listing With Disk Size!");
Console.WriteLine();

// Create a fake list of movies with their disk size in bytes.
var movies = new List<Movie>
{
    new() { Name = "The Shawshank Redemption", Size = 867630342949 },
    new() { Name = "The Godfather", Size = 18446744073709551615 },
    new() { Name = "The Dark Knight", Size = 220824 },
    new() { Name = "The Godfather: Part II", Size = 750052082558 },
    new() { Name = "The Lord of the Rings", Size = 528489456732 },
    new() { Name = "Pulp Fiction", Size = 0 },
    new() { Name = "Schindler's List", Size = 1050591100 },
};

Console.WriteLine("📌 Default Settings:");
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
```

Results
```
📌 Default Settings:
The Godfather - 16 EB
The Shawshank Redemption - 808.04 GB
The Godfather: Part II - 698.54 GB
The Lord of the Rings - 492.19 GB
Schindler's List - 1001.92 MB
The Dark Knight - 215.65 KB
Pulp Fiction - 0 B

📌 Using German culture, rounding to 4 decimal places, forcing to MB, and use a thousands seperator:
The Godfather - 17.592.186.044.416 MB
The Shawshank Redemption - 827.436,7742 MB
The Godfather: Part II - 715.305,4071 MB
The Lord of the Rings - 504.006,8214 MB
Schindler's List - 1.001,9217 MB
The Dark Knight - 0,2106 MB
Pulp Fiction - 0 MB
```

## Home Page
https://github.com/meineGlock20/BytesSizeToHumanReadableFormat

## License

This project is licensed under the MIT License.

![](https://img.shields.io/badge/License-MIT-blue.svg)

https://github.com/meineGlock20/BytesSizeToHumanReadableFormat/blob/main/BytesSizeToHumanReadableFormat/LICENSE
