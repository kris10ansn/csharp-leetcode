using Xunit;
using Xunit.Abstractions;

namespace P4;

public class SolutionTests(ITestOutputHelper output) : ConsoleCapturingTest(output)
{
    // Examples straight from the problem description.
    [Theory]
    [InlineData(new[] { 1, 3 }, new[] { 2 }, 2.0)]    // merged [1,2,3], median 2
    [InlineData(new[] { 1, 2 }, new[] { 3, 4 }, 2.5)] // merged [1,2,3,4], median (2+3)/2
    public void HandlesDescriptionExamples(int[] nums1, int[] nums2, double expected)
    {
        Assert.Equal(expected, new Solution().FindMedianSortedArrays(nums1, nums2), 5);
    }

    // Constraint: 0 <= m and 0 <= n, so one array may be empty (only their sum is >= 1).
    [Theory]
    [InlineData(new int[0], new[] { 1 }, 1.0)]         // n only, single element
    [InlineData(new[] { 2 }, new int[0], 2.0)]         // m only, single element
    [InlineData(new int[0], new[] { 2, 3 }, 2.5)]      // n only, even count
    [InlineData(new[] { 4, 5, 6 }, new int[0], 5.0)]   // m only, odd count
    public void HandlesEmptyArray(int[] nums1, int[] nums2, double expected)
    {
        Assert.Equal(expected, new Solution().FindMedianSortedArrays(nums1, nums2), 5);
    }

    // Odd total length -> the single middle element; even -> average of the two middles.
    [Theory]
    [InlineData(new[] { 1, 2 }, new[] { 3 }, 2.0)]        // [1,2,3], odd
    [InlineData(new[] { 1, 3 }, new[] { 2, 7 }, 2.5)]     // [1,2,3,7], even
    [InlineData(new[] { 3 }, new[] { 1, 2, 4, 5 }, 3.0)]  // [1,2,3,4,5], odd, interleaved
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new int[0], 3.5)] // [1..6], even
    public void HandlesOddAndEvenTotals(int[] nums1, int[] nums2, double expected)
    {
        Assert.Equal(expected, new Solution().FindMedianSortedArrays(nums1, nums2), 5);
    }

    // Disjoint ranges: every element of one array sits entirely before the other.
    [Theory]
    [InlineData(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, 3.5)] // [1..6], no interleave
    [InlineData(new[] { 4, 5, 6 }, new[] { 1, 2, 3 }, 3.5)] // same, arrays swapped
    public void HandlesDisjointRanges(int[] nums1, int[] nums2, double expected)
    {
        Assert.Equal(expected, new Solution().FindMedianSortedArrays(nums1, nums2), 5);
    }

    // Constraint: -10^6 <= nums[i] <= 10^6, including negatives and duplicates.
    [Theory]
    [InlineData(new[] { -5, -3, -1 }, new[] { -2, 0 }, -2.0)]      // [-5,-3,-2,-1,0]
    [InlineData(new[] { -1000000 }, new[] { 1000000 }, 0.0)]      // extremes average to 0
    [InlineData(new[] { 2, 2 }, new[] { 2, 2 }, 2.0)]             // all identical
    [InlineData(new[] { 1, 1, 1 }, new[] { 1, 1 }, 1.0)]         // all identical, odd total
    public void HandlesNegativesAndDuplicates(int[] nums1, int[] nums2, double expected)
    {
        Assert.Equal(expected, new Solution().FindMedianSortedArrays(nums1, nums2), 5);
    }

    // Scratch case: merged [1,1,2,2,3,3,4,4,5,6,7,8], median (3+4)/2 = 3.5.
    [Fact]
    public void MyScratchCase()
    {
        var result = new Solution().FindMedianSortedArrays(
            new[] { 1, 2, 3, 4 },
            new[] { 1, 2, 3, 4, 5, 6, 7, 8 });
        Output.WriteLine($"result = {result}");
        Assert.Equal(3.5, result, 5);
    }
}
