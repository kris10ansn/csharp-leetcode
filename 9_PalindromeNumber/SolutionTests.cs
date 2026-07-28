using Xunit;

namespace P9;

public class SolutionTests
{
    // Worked examples straight from the problem description.
    [Theory]
    [InlineData(121, true)]   // reads the same both ways
    [InlineData(-121, false)] // the '-' sign only appears on the left
    [InlineData(10, false)]   // "01" reversed
    public void HandlesDescriptionExamples(int x, bool expected)
    {
        Assert.Equal(expected, new Solution().IsPalindrome(x));
    }

    // Single digits (including 0) are trivially palindromes.
    [Theory]
    [InlineData(0, true)]
    [InlineData(7, true)]
    [InlineData(9, true)]
    public void SingleDigitsArePalindromes(int x, bool expected)
    {
        Assert.Equal(expected, new Solution().IsPalindrome(x));
    }

    // Any negative number is never a palindrome (the leading '-' has no match).
    [Theory]
    [InlineData(-1, false)]
    [InlineData(-121, false)]
    public void NegativesAreNeverPalindromes(int x, bool expected)
    {
        Assert.Equal(expected, new Solution().IsPalindrome(x));
    }

    // A positive number ending in 0 can't be a palindrome (it can't start with 0).
    [Theory]
    [InlineData(100, false)]
    [InlineData(120, false)]
    public void NumbersEndingInZeroAreNotPalindromes(int x, bool expected)
    {
        Assert.Equal(expected, new Solution().IsPalindrome(x));
    }

    // Multi-digit palindromes and non-palindromes, both even and odd length.
    [Theory]
    [InlineData(1221, true)]        // even length
    [InlineData(12321, true)]       // odd length
    [InlineData(123, false)]        // not a palindrome
    [InlineData(2147483647, false)] // int32 max, at the constraint's upper bound
    public void HandlesMultiDigitNumbers(int x, bool expected)
    {
        Assert.Equal(expected, new Solution().IsPalindrome(x));
    }
}
