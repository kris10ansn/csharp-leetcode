namespace P4;

public class Solution
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        if (nums1.Length == 0) return Median(nums2);
        if (nums2.Length == 0) return Median(nums1);


        int combinedLength = nums1.Length + nums2.Length;
        int half = (combinedLength + 1) / 2;

        int[] shorter = nums1.Length < nums2.Length ? nums1 : nums2;
        int[] longer = nums1.Length < nums2.Length ? nums2 : nums1;

        int low = 0;
        int high = shorter.Length;


        while (low <= high)
        {
            int i = (low + high) / 2;
            int j = half - i;

            int L1 = i <= 0 ? int.MinValue : shorter[i - 1];
            int R1 = i >= shorter.Length ? int.MaxValue : shorter[i];

            int L2 = j <= 0 ? int.MinValue : longer[j - 1];
            int R2 = j >= longer.Length ? int.MaxValue : longer[j];

            bool success = L1 <= R2 && L2 <= R1;

            if (success && combinedLength % 2 == 0)
            {
                return Avg(Math.Max(L1, L2), Math.Min(R1, R2));
            }
            else if (success)
            {
                return Math.Max(L1, L2);
            }
            else if (L2 > R1)
            {
                low = i + 1;
            }
            else
            {
                high = i - 1;
            }
        }

        throw new Exception("Something went wrong");
    }

    public double Avg(int a, int b)
    {
        return (double)(a + b) / 2;
    }

    public double Median(int[] array)
    {
        if (array.Length % 2 == 0)
        {
            return Avg(array[array.Length / 2 - 1], array[array.Length / 2]);
        }

        return array[array.Length / 2];
    }

}
