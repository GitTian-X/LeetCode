using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q30_Q39
{
    class Q32
    {
        //给你一个只包含 '(' 和 ')' 的字符串，找出最长有效（格式正确且连续）括号子串的长度。
        public int LongestValidParentheses(string s)
        {
            int left = 0, right = 0, maxLength = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    left++;
                }
                else
                {
                    right++;
                }
                if (left == right)
                {
                    maxLength = Math.Max(maxLength, right + left);
                }
                else if (right > left)
                {
                    left = right = 0;
                }
            }
            left = right = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '(')
                {
                    left++;
                }
                else
                {
                    right++;
                }
                if (left == right)
                {
                    maxLength = Math.Max(maxLength, right + left);
                }
                else if (right < left)
                {
                    left = right = 0;
                }
            }
            return maxLength;
        }
    }
}
