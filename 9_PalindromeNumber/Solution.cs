namespace P9;

public class Solution
{
    public bool IsPalindrome(int x)
    {
        if (x < 0) return false;
        if (x < 10) return true;

        string str = x.ToString();

        for (int i = 0; i < str.Length / 2; i++)
        {
            int left = str[i];
            int right = str[^(i + 1)];

            if (left != right) return false;
        }

        return true;
    }
}
