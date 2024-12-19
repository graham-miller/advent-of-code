using System.Reflection;

namespace Days.Day1
{
    public class Tests
    {
        [Test]
        public void Day1()
        {
            using var input = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{GetType().Namespace}.input.txt");
            using var reader = new StreamReader(input!);
            var digitLookup = new Dictionary<string, int>
            {
                { "0", 0 },
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };
            var digits = digitLookup.Keys;

            var total = 0;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                var matches = digits
                    .Select(digit => new
                    {
                        Digit = digit,
                        FirstPosition = line!.IndexOf(digit, StringComparison.InvariantCultureIgnoreCase),
                        LastPosition = line!.LastIndexOf(digit, StringComparison.InvariantCultureIgnoreCase)
                    })
                    .Where(x => x.FirstPosition != -1)
                    .ToList();

                var first = digitLookup[matches.OrderBy(x => x.FirstPosition).First().Digit];
                var last = digitLookup[matches.OrderByDescending(x => x.LastPosition).First().Digit];

                var result = (first * 10) + last;
                total += result;

            }

            // Assert.That(total, Is.EqualTo(55172));
            Assert.That(total, Is.EqualTo(54925));
        }
    }
}