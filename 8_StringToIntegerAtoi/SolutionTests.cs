using Xunit;
using Xunit.Abstractions;

namespace P8;

public class SolutionTests(ITestOutputHelper output) : ConsoleCapturingTest(output)
{
    // Worked examples straight from the problem description.
    [Theory]
    [InlineData("42", 42)]              // plain digits
    [InlineData(" -042", -42)]          // leading space, sign, leading zeros dropped
    [InlineData("1337c0d3", 1337)]      // stops at the first non-digit
    [InlineData("0-1", 0)]              // "0" is read, then '-' stops it
    [InlineData("words and 987", 0)]    // starts with a non-digit → 0
    public void HandlesDescriptionExamples(string s, int expected)
    {
        Assert.Equal(expected, new Solution().MyAtoi(s));
    }

    // Step 1 + 2: leading whitespace is skipped, then an optional sign is read.
    [Theory]
    [InlineData("   -42", -42)]         // several leading spaces before the sign
    [InlineData("+1", 1)]               // explicit '+' sign
    public void SkipsWhitespaceAndReadsSign(string s, int expected)
    {
        Assert.Equal(expected, new Solution().MyAtoi(s));
    }

    // Step 3: leading zeros are ignored in the resulting value.
    [Theory]
    [InlineData("0032", 32)]
    public void IgnoresLeadingZeros(string s, int expected)
    {
        Assert.Equal(expected, new Solution().MyAtoi(s));
    }

    // Step 3: reading stops at (or never starts because of) a non-digit; a sign
    // must immediately follow the whitespace or nothing is read.
    [Theory]
    [InlineData("abc", 0)]              // non-digit start
    [InlineData(".1", 0)]              // '.' is not a digit or sign
    [InlineData("+-12", 0)]            // two signs → the second breaks it, no digits read
    [InlineData("  +  413", 0)]        // space between sign and digits breaks it
    public void StopsAtNonDigits(string s, int expected)
    {
        Assert.Equal(expected, new Solution().MyAtoi(s));
    }

    // Constraint: 0 <= s.length, so the empty string is valid input → 0.
    [Fact]
    public void EmptyStringIsZero()
    {
        Assert.Equal(0, new Solution().MyAtoi(""));
    }

    // Step 4: values outside [-2^31, 2^31 - 1] are clamped to the range bounds.
    [Theory]
    [InlineData("2147483648", 2147483647)]      // 2^31, just over the max → clamp to max
    [InlineData("-2147483649", -2147483648)]    // -(2^31 + 1), just under the min → clamp to min
    [InlineData("91283472332", 2147483647)]     // far over → clamp to max
    [InlineData("-91283472332", -2147483648)]   // far under → clamp to min
    [InlineData("2147483647", 2147483647)]      // exactly int.MaxValue, unchanged
    [InlineData("-2147483648", -2147483648)]    // exactly int.MinValue, unchanged
    public void ClampsToInt32Range(string s, int expected)
    {
        Assert.Equal(expected, new Solution().MyAtoi(s));
    }
}
