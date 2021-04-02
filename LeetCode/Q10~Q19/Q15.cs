using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q10_Q19
{
    class Q15
    {
        //给你一个包含 n 个整数的数组 nums，判断 nums 中是否存在三个元素 a，b，c ，使得 a + b + c = 0 ？请你找出所有和为 0 且不重复的三元组。
        //注意：答案中不可以包含重复的三元组。
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            if (nums.Length < 3)
            {
                return ans; 
            }
            Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0)
                {
                    return ans;
                }
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }
                int temp = -nums[i];
                int k = nums.Length - 1;
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1])
                    {
                        continue;
                    }
                    while (j < k && nums[j] + nums[k] > temp)
                    {
                        k--;
                    }
                    if (j == k)
                    {
                        break;
                    }
                    if (nums[j] + nums[k] == temp)
                    {
                        List<int> tempList = new List<int>();
                        tempList.AddRange(new int[] { nums[i], nums[j], nums[k] });
                        ans.Add(tempList);
                    }
                }
            }
            return ans;
        }
    }
}
