namespace LeetCode.Q1_Q100
{
    class Q4
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums2.Length < nums1.Length)
            {
                return FindMedianSortedArrays(nums2, nums1);
            }
            int shortLen = nums1.Length;
            int longLen = nums2.Length;
            int start = 0, end = shortLen;
            int firstHalfMax = 0, secondHalfMin = 0;
            while (start <= end)
            {
                int i = (start + end) / 2;
                int j = (shortLen + longLen + 1) / 2 - i;
                int nums1Min = i == 0 ? int.MinValue : nums1[i - 1];
                int nums1Max = i == shortLen ? int.MaxValue : nums1[i];
                int nums2Min = j == 0 ? int.MinValue : nums2[j - 1];
                int nums2Max = j == longLen ? int.MaxValue : nums2[j];
                if (nums1Min <= nums2Max)
                {
                    if (nums1Max >= nums2Min)
                    {
                        firstHalfMax = nums1Min > nums2Min ? nums1Min : nums2Min;
                        secondHalfMin = nums1Max > nums2Max ? nums2Max : nums1Max;
                        return (shortLen + longLen) % 2 == 0 ? (firstHalfMax + secondHalfMin) / 2.0 : firstHalfMax;
                    }
                    start++;
                }
                else
                {
                    end--;
                }
            }
            return 0;
        }
    }
}
