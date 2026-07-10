using Xunit;

namespace P1;

public class SolutionTests
{
    // Worked examples from the problem statement. The two indices may be
    // returned in any order, so they're compared as a sorted pair.
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
    [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
    [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]
    public void FindsDescriptionExamples(int[] nums, int target, int[] expected)
    {
        AssertIndexPair(expected, new Solution().TwoSum(nums, target), nums, target);
    }

    // Constraints: 2 <= nums.length, and both values and target span -1e9..1e9,
    // so negatives and extreme magnitudes are valid input.
    [Theory]
    [InlineData(new[] { 1, 2 }, 3, new[] { 0, 1 })]                      // minimum length
    [InlineData(new[] { -3, 4, 3, 90 }, 0, new[] { 0, 2 })]             // negative + positive summing to zero
    [InlineData(new[] { -1, -2, -3, -4, -5 }, -8, new[] { 2, 4 })]      // all negative
    [InlineData(new[] { 1000000000, -1000000000 }, 0, new[] { 0, 1 })] // extreme magnitudes
    public void HandlesNegativesAndBounds(int[] nums, int target, int[] expected)
    {
        AssertIndexPair(expected, new Solution().TwoSum(nums, target), nums, target);
    }

    // The answer is unique but its order isn't defined: sort before comparing,
    // and sanity-check that the returned indices really do sum to the target.
    private static void AssertIndexPair(int[] expected, int[] actual, int[] nums, int target)
    {
        Assert.Equal(2, actual.Length);
        Assert.Equal(target, nums[actual[0]] + nums[actual[1]]);

        var sorted = (int[])actual.Clone();
        Array.Sort(sorted);
        Assert.Equal(expected, sorted);
    }
}
