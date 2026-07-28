namespace P8;

public class Solution
{
    private readonly Dictionary<char, int> _digits = new()
    {
        ['0'] = 0,
        ['1'] = 1,
        ['2'] = 2,
        ['3'] = 3,
        ['4'] = 4,
        ['5'] = 5,
        ['6'] = 6,
        ['7'] = 7,
        ['8'] = 8,
        ['9'] = 9,
    };

    public int MyAtoi(string str)
    {
        bool numberStarted = false;
        int result = 0;
        int sign = 1;

        foreach (char c in str)
        {
            bool isDigit = _digits.TryGetValue(c, out int digit);

            if (c is '+' or '-' && !numberStarted)
            {
                numberStarted = true;
                sign = c == '+' ? 1 : -1;
                continue;
            }
            if (c == ' ' && !numberStarted) continue;

            if (!isDigit) break;

            numberStarted = true;

            try
            {
                result = checked(result * 10 + digit * sign);
            }
            catch
            {
                return sign > 0 ? int.MaxValue : int.MinValue;
            }
        }

        return result;
    }
}
