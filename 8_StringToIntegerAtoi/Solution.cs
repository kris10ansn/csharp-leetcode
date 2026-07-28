namespace P8;

public class Solution
{
    public int MyAtoi(string str)
    {
        if (str.Length == 0) return 0;

        long result = 0;
        int sign = 1;

        int i = 0;

        while (i < str.Length && str[i] == ' ')
        {
            i++;
        }

        if (i >= str.Length) return 0;

        if (str[i] is '+' or '-')
        {
            sign = str[i] == '+' ? 1 : -1;
            i++;
        }

        for (; i < str.Length && char.IsDigit(str[i]); i++)
        {
            char c = str[i];
            int digit = c - '0';

            result *= 10;
            result += digit * sign;

            if (result > int.MaxValue) return int.MaxValue;
            if (result < int.MinValue) return int.MinValue;
        }

        return (int)result;
    }
}
