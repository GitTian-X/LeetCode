using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q10_Q19
{
    class Q18
    {
        //给定一个包含 n 个整数的数组 nums 和一个目标值 target，判断 nums 中是否存在四个元素 a，b，c 和 d ，使得 a + b + c + d 的值与 target 相等？找出所有满足条件且不重复的四元组。
        //注意：答案中不可以包含重复的四元组。
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            int len = nums.Length;
            if (len < 4)
            {
                return ans;
            }
            Array.Sort(nums);
            for (int i = 0; i < len - 3; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }
                if (nums[i] + nums[i + 1] + nums[i + 2] + nums[i + 3] > target)
                {
                    break;
                }
                if (nums[i] + nums[len - 1] + nums[len - 2] + nums[len - 3] < target)
                {
                    continue;
                }
                for (int j = i + 1; j < len - 2; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1])
                    {
                        continue;
                    }
                    if (nums[i] + nums[j] + nums[j + 1] + nums[j + 2] > target)
                    {
                        break;
                    }
                    if (nums[i] + nums[j] + nums[len - 2] + nums[len - 1] < target)
                    {
                        continue;
                    }
                    int left = j + 1, right = len - 1;
                    while (left < right)
                    {
                        int sum = nums[i] + nums[j] + nums[left] + nums[right];
                        if (sum == target)
                        {
                            ans.Add(new List<int>() { nums[i], nums[j], nums[left], nums[right] });
                            while (left < right && nums[left] == nums[left + 1])
                            {
                                left++;
                            }
                            left++;
                            while (left < right && nums[right] == nums[right - 1])
                            {
                                right--;
                            }
                            right--;
                        }
                        else if (sum < target)
                        {
                            left++;
                        }
                        else
                        {
                            right--;
                        }
                    }
                }
            }
            return ans;
        }
    }
}
