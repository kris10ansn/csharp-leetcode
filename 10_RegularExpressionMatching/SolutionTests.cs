using Xunit;
using Xunit.Abstractions;

namespace P10;

public class SolutionTests(ITestOutputHelper output) : ConsoleCapturingTest(output)
{
    // Worked examples straight from the problem description.
    [Theory]
    [InlineData("aa", "a", false)]   // "a" does not cover the whole "aa"
    [InlineData("aa", "a*", true)]   // "a*" repeats 'a' once to make "aa"
    [InlineData("ab", ".*", true)]   // ".*" is zero or more of any character
    public void HandlesDescriptionExamples(string s, string p, bool expected)
    {
        Assert.Equal(expected, new Solution().IsMatch(s, p));
    }

    // Plain literals: every character must line up, and the match is anchored to
    // the whole string.
    [Theory]
    [InlineData("abc", "abc", true)]
    [InlineData("abc", "abd", false)]
    public void MatchesExactLiterals(string s, string p, bool expected)
    {
        Assert.Equal(expected, new Solution().IsMatch(s, p));
    }

    // '.' matches any single character, but still exactly one — the pattern must
    // consume the entire string.
    [Theory]
    [InlineData("abc", "a.c", true)]   // '.' stands in for 'b'
    [InlineData("abc", "...", true)]   // three wildcards for three characters
    [InlineData("ab", "...", false)]   // three wildcards cannot match only two chars
    public void DotMatchesAnySingleCharacter(string s, string p, bool expected)
    {
        Assert.Equal(expected, new Solution().IsMatch(s, p));
    }

    // '*' means zero or more of the preceding element, so it can erase a token
    // entirely.
    [Theory]
    [InlineData("ab", "c*ab", true)]  // "c*" matches zero 'c's
    [InlineData("aa", "a*", true)]    // "a*" matches two 'a's
    public void StarMatchesZeroOrMore(string s, string p, bool expected)
    {
        Assert.Equal(expected, new Solution().IsMatch(s, p));
    }

    // '*' combined with '.' and the classic mississippi backtracking cases.
    [Theory]
    [InlineData("mississippi", ".*", true)]           // ".*" swallows everything
    [InlineData("mississippi", "mis*is*p*.", false)]  // "p*." cannot line up the tail
    [InlineData("mississippi", "mis*is*ip*.", true)]  // the correct grouping does match
    public void StarWithDotAndBacktracking(string s, string p, bool expected)
    {
        Assert.Equal(expected, new Solution().IsMatch(s, p));
    }

    // Greedy stars must give characters back so a later token can match.
    [Theory]
    [InlineData("aaa", "a*a", true)]       // "a*" yields one 'a' so the trailing 'a' matches
    [InlineData("aaa", "ab*a*c*a", true)]  // several zero-width stars around real matches
    [InlineData("aaa", "aaaa", false)]     // pattern is longer than the string
    [InlineData("aab", "c*a*b", true)]     // "c*" zero, "a*" two, then 'b'
    public void StarBacktracksToConsumeEntireString(string s, string p, bool expected)
    {
        Assert.Equal(expected, new Solution().IsMatch(s, p));
    }
}
