using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q10_Q19
{
    
    class Q16
    {
        //给定一个包括 n 个整数的数组 nums 和 一个目标值 target。找出 nums 中的三个整数，使得它们的和与 target 最接近。
        //返回这三个数的和。假定每组输入只存在唯一答案。
        public int ThreeSumClosest(int[] nums, int target)
        {
            Array.Sort(nums);
            int numsLen = nums.Length;
            int ans = 10000;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }
                int left = i + 1, right = numsLen - 1;
                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];
                    if (Math.Abs(sum - target) < Math.Abs(ans - target))
                    {
                        ans = sum;
                    }
                    if (sum == target)
                    {
                        return target;
                    }else if (sum > target)
                    {
                        right--;
                        while (left < right && nums[right] == nums[right + 1])
                        {
                            right--;
                        }
                    }
                    else
                    {
                        left++;
                        while (left < right && nums[left] == nums[left - 1])
                        {
                            left++;
                        }
                    }
                }
            }
            return ans;
        }
    }
}
