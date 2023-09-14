public static class RandomDataGenerator
{
    private static readonly Random random = new();

    private static readonly List<string> FirstNamePersonList = new()
    {
        "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Henry", "Ivy", "Jack", "Karen",
        "Liam", "Mia", "Noah", "Olivia", "Penelope", "Quinn", "Ryan", "Sophia", "Thomas", "Ursula", "Victor", "Wendy"
    };
    private static readonly List<string> LastNamePersonList = new()
    {
        "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor"
    };
    private static readonly List<string> CompanyNameList = new()
    {
        "Acme Corporation", "Globex Corporation", "Wayne Enterprises", "Stark Industries", "InGen", "Umbrella Corporation",
        "Tyrell Corporation", "Weyland-Yutani Corporation", "Aperture Science", "Dunder Mifflin", "Cyberdyne Systems"
    };
    private static readonly List<string> FirstNameCountryList = new()
    {
        "Ayme", "Laskovia", "Manhatan", "Starlin", "Misegonia", "Marles",
        "Ettsus", "Brazuo", "Lastorvisk", "Bufin", "Laxes"
    };
    private static readonly List<string> LastNameCountryList = new()
    {
        "Pacific", "War zone", "Mountain", "Island", "Falks", "Marles",
        "Of America", "American", "Praise", "Londes", "Lake"
    };
    private static readonly List<string> CategoryList = new()
    {
        "Fiction", "Non-Fiction", "Mystery", "Thriller", "Romance", "Sci-Fi", "Fantasy", "Horror", "Historical", "Biography",
        "Self-Help", "Cooking", "Art", "Travel", "Science", "Business", "Sports", "Comedy", "Drama"
    };
    private static readonly List<string> StreetList = new()
    {
        "Main Street", "First Avenue", "Elm Street", "Maple Avenue", "Oak Street", "Cedar Avenue", "Pine Street",
        "Broadway", "Park Avenue", "Church Street", "Washington Street", "Market Street", "High Street"
    };
    private static readonly List<string> FirstPartBookNameList = new List<string>
    {
        "The", "A", "In", "Out", "Of", "Under", "Beyond", "Behind", "Within", "Above"
    };
    private static readonly List<string> SecondPartBookNameList = new List<string>
    {
        "Secrets", "Journey", "World", "Dreams", "Power", "Quest", "Mystery", "Legacy", "Destiny", "Adventure"
    };

    public static string GenerateRandomPersonFirstName()
    {
        return GetRandomElement(FirstNamePersonList);
    }

    public static string GenerateRandomPersonLastName()
    {
        return GetRandomElement(LastNamePersonList);
    }

    public static string GenerateRandomPersonName()
    {
        string firstName = GetRandomElement(FirstNamePersonList);
        string lastName = GetRandomElement(LastNamePersonList);
        return $"{firstName} {lastName}";
    }

    public static string GenerateCountryName()
    {
        string firstName = GetRandomElement(FirstNameCountryList);
        string lastName = GetRandomElement(LastNameCountryList);
        return $"{firstName} {lastName}";
    }

    public static string GenerateRandomCompanyName()
    {
        return GetRandomElement(CompanyNameList);
    }

    public static string GenerateRandomCategory()
    {
        return GetRandomElement(CategoryList);
    }

    public static string GenerateRandomAddress()
    {
        string street = GetRandomElement(StreetList);
        int houseNumber = random.Next(1, 100);
        return $"{houseNumber} {street}";
    }

    public static string GenerateRandomBookTitle()
    {
        string firstPart = GetRandomElement(FirstPartBookNameList);
        string secondPart = GetRandomElement(SecondPartBookNameList);
        return $"{firstPart} {secondPart}";
    }

    public static DateOnly GenerateRandomDateOnly(DateTime startDate, DateTime endDate)
    {
        int totalDays  = (endDate - startDate).Days + 1;
        int randomDays = random.Next(totalDays);
        DateTime randomDate = startDate.AddDays(randomDays);
        return new DateOnly(randomDate.Year, randomDate.Month, randomDate.Day);
    }
    public static int GenerateRandomInt(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue + 1);
    }

    public static decimal GenerateRandomDecimal(decimal minValue, decimal maxValue)
    {
        decimal randomValue = (decimal)(random.NextDouble() * (double)(maxValue - minValue) + (double)minValue);
        return decimal.Round(randomValue, 2);
    }

    private static T GetRandomElement<T>(List<T> list)
    {
        int index = random.Next(0, list.Count);
        return list[index];
    }
}