using Xunit;

namespace P7;

public class SolutionTests
{
    // Worked examples straight from the problem description.
    [Theory]
    [InlineData(123, 321)]
    [InlineData(-123, -321)]
    [InlineData(120, 21)]
    public void HandlesDescriptionExamples(int x, int expected)
    {
        Assert.Equal(expected, new Solution().Reverse(x));
    }

    // Small / single-digit inputs and zero.
    [Theory]
    [InlineData(0, 0)]
    [InlineData(7, 7)]
    [InlineData(-7, -7)]
    public void HandlesSingleDigitAndZero(int x, int expected)
    {
        Assert.Equal(expected, new Solution().Reverse(x));
    }

    // Trailing zeros are dropped once the digits are reversed.
    [Theory]
    [InlineData(100, 1)]
    [InlineData(1000, 1)]
    [InlineData(-120, -21)] // sign is preserved, trailing zero dropped
    [InlineData(-100, -1)]
    public void DropsTrailingZeros(int x, int expected)
    {
        Assert.Equal(expected, new Solution().Reverse(x));
    }

    // Constraint: reversing must stay within [-2^31, 2^31 - 1] (int32); if the
    // reversed value overflows that range the result is 0.
    [Theory]
    [InlineData(1534236469, 0)]  // reverses to 9646324351 > int.MaxValue
    [InlineData(2147483647, 0)]  // int.MaxValue reverses to 7463847412 -> overflow
    [InlineData(-2147483648, 0)] // int.MinValue reverses to -8463847412 -> overflow
    [InlineData(1000000003, 0)]  // reverses to 3000000001 > int.MaxValue
    public void ReturnsZeroOnOverflow(int x, int expected)
    {
        Assert.Equal(expected, new Solution().Reverse(x));
    }

    // Boundary: reversals that land just inside the int32 range must NOT be zeroed.
    [Theory]
    [InlineData(1463847412, 2147483641)]   // reverses to exactly int.MaxValue - 6
    [InlineData(-1463847412, -2147483641)] // negative counterpart, still in range
    [InlineData(2147483641, 1463847412)]   // the reverse of the case above
    public void KeepsValuesThatStayInRange(int x, int expected)
    {
        Assert.Equal(expected, new Solution().Reverse(x));
    }
}
