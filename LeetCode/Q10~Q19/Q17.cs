using System.Collections.Generic;
using System.Text;

namespace LeetCode.Q10_Q19
{
    class Q17
    {
        //给定一个仅包含数字 2-9 的字符串，返回所有它能表示的字母组合。答案可以按 任意顺序 返回。
        //给出数字到字母的映射如下（与电话按键相同）。注意 1 不对应任何字母。
        public IList<string> LetterCombinations(string digits)
        {
            IList<string> ans = new List<string>();
            if (digits.Length == 0)
            {
                return ans;
            }
            Dictionary<char, string> map = new Dictionary<char, string>()
            {
                { '2', "abc"},
                { '3', "def"},
                { '4', "ghi"},
                { '5', "jkl"},
                { '6', "mno"},
                { '7', "pqrs"},
                { '8', "tuv"},
                { '9', "wxyz"},
            };
            StringBuilder sb = new StringBuilder();
            BackTrack(0);
            void BackTrack(int index)
            {
                if (index == digits.Length)
                {
                    ans.Add(sb.ToString());
                }
                else
                {
                    string str = map[digits[index]];
                    for (int i = 0; i < str.Length; i++)
                    {
                        sb.Append(str[i]);
                        BackTrack(index + 1);
                        sb.Remove(index, 1);
                    }
                }
            }
            return ans;
        }
    }
}
