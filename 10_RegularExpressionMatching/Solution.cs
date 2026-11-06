using System.Data.SqlTypes;

namespace P10;

public class Solution
{
    // WIP: 2 failing tests
    public bool IsMatch(string str, string pattern)
    {
        Console.WriteLine($"---\nSTR:{str}\nPATTERN:{pattern}\n%%%");
        int sCursor = 0;
        int pCursor = 0;

        while (pCursor < pattern.Length)
        {
            char sChar = str.ElementAtOrDefault(sCursor);
            char pChar = pattern[pCursor];

            char pCharNext = pattern.ElementAtOrDefault(pCursor + 1);

            Console.WriteLine($"pChar: {pChar}, pCharNext: {pCharNext}, s: {sChar}, pCursor: {pCursor}, sCursor: {sCursor}");

            if (!CharMatch(sChar, pChar) && pCharNext != '*')
            {
                return false;
            }

            while (CharMatch(sChar, pChar) && pCharNext == '*')
            {
                Console.WriteLine($"{pCharNext}: {sChar} (p: {pChar})");
                sCursor++;
                sChar = str.ElementAtOrDefault(sCursor);
            }


            if (pCharNext == '*')
            {
                pCursor += 2;
            }
            else
            {
                pCursor++;
                sCursor++;
            }
        }

        return sCursor >= str.Length;
    }

    private bool CharMatch(char s, char p)
    {
        if (s == default(char) || p == default(char))
        {
            return false;
        }

        if (p == '.')
        {
            return true;
        }

        return s == p;
    }

}
