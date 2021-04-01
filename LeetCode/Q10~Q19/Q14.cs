using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q10_Q19
{
    class Q14
    {
        //编写一个函数来查找字符串数组中的最长公共前缀。
        //如果不存在公共前缀，返回空字符串 ""。
        public string LongestCommonPrefix(string[] strs)
        {
            var length = strs.Length;
            StringBuilder sb = new StringBuilder();
            if (length <= 0)
            {
                return "";
            }
            var first = strs[0];
            var firstLength = first.Length;
            for (int i = 0; i < firstLength; i++)
            {
                var trueCount = 0;
                foreach (var val in strs)
                {
                    if (i >= val.Length)
                        break;
                    if (val[i] == first[i])
                    {
                        trueCount++;
                    }
                }

                if (trueCount == length)
                {
                    sb.Append(first[i]);
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }
    }
}
