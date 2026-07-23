namespace P7;

public class Solution
{
    public int Reverse(int x)
    {
        var isNegative = x < 0;

        IEnumerable<char> reversed = x.ToString().ToCharArray().Skip(isNegative ? 1 : 0).Reverse();
        string str = string.Join("", reversed);

        if (int.TryParse(str, out int result))
        {
            return (isNegative ? -1 : 1) * result;
        }

        return 0;
    }
}
