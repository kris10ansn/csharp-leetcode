namespace P5;

public class Solution
{
    public string LongestPalindrome(string str)
    {
        var longest = (Start: 0, Length: 1);

        for (int i = 0; i < str.Length; i++)
        {
            var odd = FindPalindrome(str, (Left: i, Right: i));
            var even = FindPalindrome(str, (Left: i, Right: i + 1));

            var longer = odd.Length > even.Length ? odd : even;

            if (longer.Length > longest.Length)
            {
                longest = longer;
            }
        }

        return str.Substring(longest.Start, longest.Length);
    }

    private (int Start, int Length) FindPalindrome(string str, (int Left, int Right) center)
    {
        var longest = (Start: center.Left, Length: 1);

        for (int i = 0; center.Left - i >= 0 && center.Right + i < str.Length; i++)
        {

            int start = center.Left - i;
            int end = center.Right + i;
            int length = end - start + 1;

            char left = str[start];
            char right = str[end];

            if (left != right) break;

            if (length > longest.Length)
            {
                longest = (Start: start, Length: length);
            }
        }

        return longest;
    }
}
