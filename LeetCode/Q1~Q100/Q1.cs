using System.Collections.Generic;

namespace LeetCode.Q1_Q100
{
    class Q1
    {
        /*
         * 给定一个整数数组 nums 和一个整数目标值 target，请你在该数组中找出和为目标值的那两个整数，并返回它们的数组下标。
         * 你可以假设每种输入只会对应一个答案。但是，数组中同一个元素在答案里不能重复出现。
         * 你可以按任意顺序返回答案。
         */
        public int[] TwoSum(int[] nums, int target)
        {
            int tempTarget = -1;
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (map.TryGetValue(nums[i], out int index))
                {
                    return new int[] { index, i };
                }
                else
                {
                    tempTarget = target - nums[i];
                    map.Add(tempTarget, i);
                }
            }
            return null;
        }
    }
}
