
public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        int longest = 0;

        int startIndex = 0;

        HashSet<char> hashSet = new();
        char[] chars = s.ToCharArray();

        for (int endIndex = 0; endIndex < chars.Length; endIndex++)
        {
            char c = chars[endIndex];

            while (!hashSet.Add(c) && startIndex < chars.Length)
            {
                hashSet.Remove(chars[startIndex]);
                startIndex += 1;
            }

            int length = endIndex - startIndex + 1;

            if (length > longest)
            {
                longest = length;
            }
        }

        return longest;
    }
}