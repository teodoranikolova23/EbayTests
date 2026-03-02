using System.Globalization;
using System.Text.RegularExpressions;

namespace EbayTests.Extensions
{
    public static class StringExtensions
    {
        public static decimal ParsePrice(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new AssertionException("Could not parse price from empty text.");

            var match = Regex.Match(text, @"(\d+[.,]\d{2})");
            if (!match.Success)
                match = Regex.Match(text, @"(\d+)");

            if (!match.Success)
                throw new AssertionException($"Could not parse price from text: '{text}'");

            var number = match.Value.Replace(",", ".");

            if (!decimal.TryParse(number, NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
                throw new AssertionException($"Could not parse numeric price '{number}' from text: '{text}'");

            return value;
        }
    }
}
