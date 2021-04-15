using System;

namespace LeetCode.Q30_Q39
{
    class Q31
    {
        //实现获取 下一个排列 的函数，算法需要将给定数字序列重新排列成字典序中下一个更大的排列。
        //如果不存在下一个更大的排列，则将数字重新排列成最小的排列（即升序排列）。
        //必须 原地 修改，只允许使用额外常数空间。
        public void NextPermutation(int[] nums)
        {
            int len = nums.Length;
            if (len < 2)
            {
                return;
            }
            void Swap(int left, int right)
            {
                int temp = nums[left];
                nums[left] = nums[right];
                nums[right] = temp;
            }
            void Reverse(int start)
            {
                Array.Reverse(nums, start, len - start);
            }
            for (int i = nums.Length - 2; i >= 0 ; i--)
            {
                for (int j = nums.Length - 1; j > i; j--)
                {
                    if (nums[i] < nums[j])
                    {
                        Swap(i, j);
                        Reverse(i+ 1);
                        return;
                    }
                }
            }
            Array.Reverse(nums);
        }
    }
}
