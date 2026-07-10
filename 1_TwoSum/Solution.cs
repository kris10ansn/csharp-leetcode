/*
 * https://leetcode.com/problems/two-sum/
 */


namespace P1;

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> map = new();

        for (int i = 0; i < nums.Length; i++)
        {
            int a = nums[i];
            int complement = target - a;

            if (map.TryGetValue(complement, out int complementIndex))
            {
                return [i, complementIndex];
            }

            map.TryAdd(a, i);
        }

        throw new ArgumentException("The provided array does not have a two sum");
    }
}
