namespace P4;

public class Solution
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        var combinedHalf = new List<int>();
        int length = nums1.Length + nums2.Length;

        int i1 = 0;
        int i2 = 0;

        for (int index = 0; index < length / 2 + 1; index++)
        {
            int lowest;

            bool indiciesInRange = i1 < nums1.Length && i2 < nums2.Length;
            bool i2OOR = i2 >= nums2.Length;

            if ((indiciesInRange && nums1[i1] < nums2[i2]) || i2OOR)
            {
                lowest = nums1[i1++];
            }
            else
            {
                lowest = nums2[i2++];
            }

            combinedHalf.Add(lowest);
        }

        if (length % 2 == 0)
        {
            return Avg(combinedHalf[^1], combinedHalf[^2]);
        }

        return combinedHalf[^1];
    }

    public double Avg(int a, int b)
    {
        return (double)(a + b) / 2;
    }

}
