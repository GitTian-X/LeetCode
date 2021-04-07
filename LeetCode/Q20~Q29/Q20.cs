using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Q20_Q29
{
    class Q20
    {
        //      给定一个只包括 '('，')'，'{'，'}'，'['，']' 的字符串 s ，判断字符串是否有效。
        //      有效字符串需满足：
        //          左括号必须用相同类型的右括号闭合。
        //          左括号必须以正确的顺序闭合。
        public bool IsValid(string s)
        {
            bool IsPair(char a, char b)
            {
                var lefts = "([{";
                var rights = ")]}";
                return lefts.Contains(a) && lefts.IndexOf(a) == rights.IndexOf(b);
            }

            if (s.Length % 2 == 1)
            {
                return false;
            }
            int offset = -1;
            List<char> vs = new List<char>();
            while (offset++ < s.Length - 1)
            {
                if (vs.Count > 0 && IsPair(vs.Last(), s[offset]))
                {
                    vs.RemoveAt(vs.Count - 1);
                }
                else
                {
                    vs.Add(s[offset]);
                }
            }
            return vs.Count == 0;
        }
    }
}
