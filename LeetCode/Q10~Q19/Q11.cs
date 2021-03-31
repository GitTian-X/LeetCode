using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q10_Q19
{
    //给你 n 个非负整数 a1，a2，...，an，每个数代表坐标中的一个点(i, ai) 。在坐标内画 n 条垂直线，垂直线 i 的两个端点分别为(i, ai) 和(i, 0) 。找出其中的两条线，使得它们与 x 轴共同构成的容器可以容纳最多的水。
    //说明：你不能倾斜容器。
    class Q11
    {
        public int MaxArea(int[] height)
        {
            int left = 0, right = height.Length - 1;
            int ans = 0;
            while (left < right)
            {
                if (height[left] <= height[right])
                {
                    ans = Math.Max(ans, height[left] * (right - left));
                    left++;
                }
                else
                {
                    ans = Math.Max(ans, height[right] * (right - left));
                    right--;
                }
            }
            return ans;
        }
    }
}
