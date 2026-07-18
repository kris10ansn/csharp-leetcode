using Xunit;
using Xunit.Abstractions;

namespace P6;

public class SolutionTests(ITestOutputHelper output) : ConsoleCapturingTest(output)
{
    // Worked examples straight from the problem description.
    [Theory]
    [InlineData("PAYPALISHIRING", 3, "PAHNAPLSIIGYIR")] // the 3-row zigzag read line by line
    [InlineData("PAYPALISHIRING", 4, "PINALSIGYAHRPI")] // the 4-row zigzag
    [InlineData("A", 1, "A")]                            // a single row
    public void HandlesDescriptionExamples(string s, int numRows, string expected)
    {
        Assert.Equal(expected, new Solution().Convert(s, numRows));
    }

    // One row (numRows == 1) never zigzags, so the string is returned unchanged.
    [Theory]
    [InlineData("ABCDEFG", "ABCDEFG")]
    [InlineData("X", "X")]        // single character
    [InlineData("AB", "AB")]      // two characters, still one row
    public void SingleRowReturnsInputUnchanged(string s, string expected)
    {
        Assert.Equal(expected, new Solution().Convert(s, 1));
    }

    // When numRows >= s.Length every character sits on its own row, so nothing
    // zigzags and the string comes back unchanged.
    [Theory]
    [InlineData("ABC", 5, "ABC")] // more rows than characters
    [InlineData("ABC", 3, "ABC")] // exactly as many rows as characters
    public void RowsAtLeastLengthReturnsInputUnchanged(string s, int numRows, string expected)
    {
        Assert.Equal(expected, new Solution().Convert(s, numRows));
    }

    // Two rows: characters simply alternate between the top and bottom row.
    [Fact]
    public void TwoRowsAlternate()
    {
        Assert.Equal("PYAIHRNAPLSIIG", new Solution().Convert("PAYPALISHIRING", 2));
    }

    // Constraint: s may contain ',' and '.' alongside letters (and spaces here).
    [Fact]
    public void HandlesPunctuationCharacters()
    {
        Assert.Equal("Hoo.el,Wrdl l", new Solution().Convert("Hello, World.", 3));
    }
}
