using Xunit;
using Xunit.Abstractions;

namespace P5;

public class SolutionTests(ITestOutputHelper output) : ConsoleCapturingTest(output)
{
    // The longest palindrome is not always unique — "babad" admits both "bab"
    // and "aba" — so most cases assert the returned value is *a* valid answer:
    // a palindrome, an actual substring of s, and of the known maximum length.
    static void AssertLongestPalindrome(string s, int expectedLength, string result)
    {
        Assert.Equal(expectedLength, result.Length);
        Assert.Contains(result, s);      // the answer is a substring of s
        Assert.True(IsPalindrome(result), $"\"{result}\" is not a palindrome");
    }

    static bool IsPalindrome(string s)
    {
        for (int i = 0, j = s.Length - 1; i < j; i++, j--)
            if (s[i] != s[j]) return false;
        return true;
    }

    // Worked examples straight from the problem description.
    [Theory]
    [InlineData("babad", 3)] // "bab" — "aba" is also accepted
    [InlineData("cbbd", 2)]  // "bb"
    public void HandlesDescriptionExamples(string s, int expectedLength)
    {
        AssertLongestPalindrome(s, expectedLength, new Solution().LongestPalindrome(s));
    }

    // When the longest palindrome is unique in value we can pin it exactly.
    [Theory]
    [InlineData("cbbd", "bb")]
    [InlineData("aa", "aa")]                              // even-length center
    [InlineData("abba", "abba")]                          // even-length, whole string
    [InlineData("racecar", "racecar")]                    // odd-length, whole string
    [InlineData("bananas", "anana")]                      // palindrome embedded in the middle
    [InlineData("abacdfgdcaba", "aba")]                   // matching ends but no long contiguous palindrome
    [InlineData("forgeeksskeegfor", "geeksskeeg")]        // long even-length palindrome inside a longer string
    public void ReturnsTheAnswerWhenItIsUnique(string s, string expected)
    {
        Assert.Equal(expected, new Solution().LongestPalindrome(s));
    }

    // Constraint: s consists of digits and English letters.
    [Theory]
    [InlineData("12321", "12321")] // all digits, odd-length palindrome
    [InlineData("a1221b", "1221")] // digits form the palindrome, letters flank it
    public void HandlesDigits(string s, string expected)
    {
        Assert.Equal(expected, new Solution().LongestPalindrome(s));
    }

    // Constraint: 1 <= s.length, so a single character is the smallest input and
    // is itself a length-1 palindrome.
    [Fact]
    public void SingleCharacterIsItsOwnPalindrome()
    {
        Assert.Equal("a", new Solution().LongestPalindrome("a"));
    }

    // Cases where several answers are equally long; only the length is pinned.
    [Theory]
    [InlineData("ab", 1)]           // no palindrome longer than one char: "a" or "b"
    [InlineData("abcda", 1)]        // matching first/last char, but nothing longer than 1
    [InlineData("aaaa", 4)]         // every character identical: the whole string
    [InlineData("ccc", 3)]          // odd run of identical characters
    [InlineData("aacabdkacaa", 3)]  // "aca" — decoy repeats elsewhere
    [InlineData("xaabacxx", 3)]     // "aba" wins over the shorter "aa"/"xx" runs
    public void FindsAMaximalPalindrome(string s, int expectedLength)
    {
        AssertLongestPalindrome(s, expectedLength, new Solution().LongestPalindrome(s));
    }
}
