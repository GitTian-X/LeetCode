﻿namespace LeetCode.Q20_Q29
{
    class Q26
    {
        //给你一个有序数组 nums ，请你 原地 删除重复出现的元素，使每个元素 只出现一次 ，返回删除后数组的新长度。
        //不要使用额外的数组空间，你必须在 原地 修改输入数组 并在使用 O(1) 额外空间的条件下完成。
        public int RemoveDuplicates(int[] nums)
        {
            int ans = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                while (i + 1 < nums.Length && nums[i] == nums[i + 1])
                {
                    i++;
                }
                nums[ans++] = nums[i];
            }
            return ans;
        }
    }
}
