using Xunit;

namespace P3;

public class SolutionTests
{
    // Examples straight from the problem description.
    [Theory]
    [InlineData("abcabcbb", 3)] // answer is "abc"
    [InlineData("bbbbb", 1)]    // answer is "b"
    [InlineData("pwwkew", 3)]   // answer is "wke" (a substring, not the subsequence "pwke")
    public void HandlesDescriptionExamples(string s, int expected)
    {
        Assert.Equal(expected, new Solution().LengthOfLongestSubstring(s));
    }

    // Constraint: 0 <= s.length, so the empty string is valid input.
    [Fact]
    public void EmptyStringHasNoSubstring()
    {
        Assert.Equal(0, new Solution().LengthOfLongestSubstring(""));
    }

    [Theory]
    [InlineData("a", 1)]           // single character
    [InlineData("au", 2)]          // all characters already unique
    [InlineData("abba", 2)]        // left bound must not move backwards: "ab" then "ba"
    [InlineData("dvdf", 3)]        // best window "vdf" appears after a repeat
    [InlineData("tmmzuxt", 5)]     // best window "mzuxt" is in the middle
    public void HandlesRepeatEdgeCases(string s, int expected)
    {
        Assert.Equal(expected, new Solution().LengthOfLongestSubstring(s));
    }

    // Constraint: s consists of English letters, digits, symbols and spaces.
    [Theory]
    [InlineData(" ", 1)]            // a lone space
    [InlineData(" ab ", 3)]         // spaces count as characters: "ab " / " ab"
    [InlineData("1234567890", 10)]  // all digits, all unique
    [InlineData("a!b@c#", 6)]       // symbols, all unique
    public void HandlesNonLetterCharacters(string s, int expected)
    {
        Assert.Equal(expected, new Solution().LengthOfLongestSubstring(s));
    }
}
