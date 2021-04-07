using System.Collections.Generic;

namespace LeetCode.Q20_Q29
{
    class Q22
    {
        //数字 n 代表生成括号的对数，请你设计一个函数，用于能够生成所有可能的并且 有效的 括号组合。
        public IList<string> GenerateParenthesis(int n)
        {
            void Helper(IList<string> ans, int left, int right, string str)
            {
                if (left == 0 && right == 0)
                {
                    ans.Add(str);
                    return;
                }
                if (left == right)
                {
                    Helper(ans, left - 1, right, str + "(");
                }
                else if(left < right)
                {
                    if (left > 0)
                    {
                        Helper(ans, left - 1, right, str + "(");
                    }
                    Helper(ans, left, right - 1, str + ")");
                }
            }
            IList<string> ans = new List<string>();
            Helper(ans, n, n, "");
            return ans;
        }
    }
}
