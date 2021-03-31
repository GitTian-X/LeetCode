using System.Text;

namespace LeetCode.Q1_Q9
{
    class Q6
    {
//      将一个给定字符串 s 根据给定的行数 numRows ，以从上往下、从左到右进行 Z 字形排列。
//      比如输入字符串为 "PAYPALISHIRING" 行数为 3 时，排列如下：
//      P   A   H   N
//      A P L S I I G
//      Y   I   R
//      之后，你的输出需要从左往右逐行读取，产生出一个新的字符串，比如："PAHNAPLSIIGYIR"。
        public string Convert(string s, int numRows)
        {
            if (numRows == 1)
            {
                return s;
            }
            int len = s.Length;
            int cycleLen = numRows * 2 - 2;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j + i < len; j += cycleLen)
                {
                    sb.Append(s[j + i]);
                    int temp = j + cycleLen - i;
                    if (i % (numRows - 1) != 0 && temp < len)
                    {
                        sb.Append(s[temp]);
                    }
                }
            }
            return sb.ToString();
        }
    }
}
