using System;
using System.Text;

namespace LeetCode.Q1_Q9
{
    class Q5
    {
        //给你一个字符串 s，找到 s 中最长的回文子串。
        public string LongestPalindrome(string s)
        {
            if (s.Length < 2)
            {
                return s;
            }
            //预处理
            StringBuilder sb = new StringBuilder("$");
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append('#');
                sb.Append(s[i]);
            }
            sb.Append("#@");
            int len = sb.Length;
            int[] p = new int[len];
            int center = 0, right = 0;
            int maxLength = -1;
            int index = 0;
            for (int i = 1; i < len - 1; i++)
            {
                p[i] = right > i ? Math.Min(p[2 * center - i], right - i) : 1;
                while (sb[i + p[i]] == sb[i - p[i]])
                {
                    p[i]++;
                }
                if (right < p[i] + i)
                {
                    right = p[i] + i;
                    center = i;
                }
                if (maxLength < p[i] - 1)
                {
                    maxLength = p[i] - 1;
                    index = i;
                }
            }
            int start = (index - maxLength) / 2;
            return s.Substring(start, maxLength);
        }
    }
}
