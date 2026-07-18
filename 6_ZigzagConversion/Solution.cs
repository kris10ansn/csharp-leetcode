using System.Text;

namespace P6;

public class Solution
{
    public string Convert(string str, int numberOfRows)
    {
        if (str.Length <= numberOfRows || numberOfRows == 1) return str;

        int row = 0;
        int delta = 1;

        var rows = new StringBuilder[numberOfRows];
        for (int i = 0; i < numberOfRows; i++) rows[i] = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];

            rows[row].Append(c);
            row += delta;

            if (row == numberOfRows - 1 || row == 0)
            {
                delta *= -1;
            }
        }

        return string.Join("", rows);
    }
}
