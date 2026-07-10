using Xunit;

namespace P2;

public class SolutionTests
{
    // Worked examples from the problem statement. Digits are stored in reverse
    // order, one digit per node.
    [Theory]
    [InlineData(new[] { 2, 4, 3 }, new[] { 5, 6, 4 }, new[] { 7, 0, 8 })]                            // 342 + 465 = 807
    [InlineData(new[] { 0 }, new[] { 0 }, new[] { 0 })]                                              // 0 + 0 = 0
    [InlineData(new[] { 9, 9, 9, 9, 9, 9, 9 }, new[] { 9, 9, 9, 9 }, new[] { 8, 9, 9, 9, 0, 0, 0, 1 })] // 9999999 + 9999 = 10009998
    public void AddsDescriptionExamples(int[] a, int[] b, int[] expected)
    {
        Assert.Equal(expected, ToArray(new Solution().AddTwoNumbers(Build(a), Build(b))));
    }

    // Carrying within the number, a final carry that grows the result by a node,
    // and operands of unequal length.
    [Theory]
    [InlineData(new[] { 5 }, new[] { 5 }, new[] { 0, 1 })]             // 5 + 5 = 10, carry creates a new node
    [InlineData(new[] { 9, 9 }, new[] { 1 }, new[] { 0, 0, 1 })]       // 99 + 1 = 100, carry ripples the whole way
    [InlineData(new[] { 1, 2, 3 }, new[] { 4, 5 }, new[] { 5, 7, 3 })] // 321 + 54 = 375, unequal lengths
    public void HandlesCarryAndUnequalLengths(int[] a, int[] b, int[] expected)
    {
        Assert.Equal(expected, ToArray(new Solution().AddTwoNumbers(Build(a), Build(b))));
    }

    // Builds a linked list from reverse-ordered digits, the way LeetCode stores them.
    private static ListNode? Build(int[] digits)
    {
        ListNode? head = null;
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            head = new ListNode(digits[i], head);
        }
        return head;
    }

    private static int[] ToArray(ListNode? node)
    {
        var digits = new List<int>();
        for (; node != null; node = node.next)
        {
            digits.Add(node.val);
        }
        return digits.ToArray();
    }
}
