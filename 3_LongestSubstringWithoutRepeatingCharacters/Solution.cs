namespace P3;

public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        int longest = 0;
        int left = 0;

        Dictionary<char, int> dict = new();
        char[] chars = s.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            char c = chars[i];

            if (!dict.TryAdd(c, i) && dict.TryGetValue(c, out int index))
            {
                left = Math.Max(left, index + 1);
                dict[c] = i;
            }

            int length = i - left + 1;
            longest = Math.Max(longest, length);
        }



        return longest;
    }
}