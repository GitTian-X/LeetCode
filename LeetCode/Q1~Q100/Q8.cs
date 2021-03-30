using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q1_Q100
{
    class Q8
    {
//      请你来实现一个 myAtoi(string s) 函数，使其能将字符串转换成一个 32 位有符号整数（类似 C/C++ 中的 atoi 函数）。
//      函数 myAtoi(string s) 的算法如下：

//      读入字符串并丢弃无用的前导空格
//      检查下一个字符（假设还未到字符末尾）为正还是负号，读取该字符（如果有）。 确定最终结果是负数还是正数。 如果两者都不存在，则假定结果为正。
//      读入下一个字符，直到到达下一个非数字字符或到达输入的结尾。字符串的其余部分将被忽略。
//      将前面步骤读入的这些数字转换为整数（即，"123" -> 123， "0032" -> 32）。如果没有读入数字，则整数为 0 。必要时更改符号（从步骤 2 开始）。
//      如果整数数超过 32 位有符号整数范围[−231, 231 − 1] ，需要截断这个整数，使其保持在这个范围内。具体来说，
//      小于 −231 的整数应该被固定为 −231 ，大于231 − 1 的整数应该被固定为 231 − 1 。
//      返回整数作为最终结果。
//      注意：
//          本题中的空白字符只包括空格字符 ' ' 。
//          除前导空格或数字后的其余字符串外，请勿忽略 任何其他字符。
        public int MyAtoi(string s)
        {
            s = s.Trim();
            if (string.IsNullOrEmpty(s) || (s[0] != '-' && s[0] != '+' && s[0] < '0' && s[0] > '9'))
            {
                return 0;
            }
            int symbol = 1;
            int startIndex = 0;
            int ans = 0;
            if (s[0] == '-' || s[0] == '+')
            {
                symbol = s[0] == '-' ? -1 : 1;
                startIndex = 1;
            }
            for (; startIndex < s.Length; startIndex++)
            {
                if (s[startIndex] >= '0' && s[startIndex] <= '9')
                {
                    int temp = ans * symbol;
                    if (temp > int.MaxValue / 10 || (temp == int.MaxValue / 10 && s[startIndex] - '0' > 7))
                    {
                        return int.MaxValue;
                    }
                    if (temp < int.MinValue / 10 || (temp == int.MinValue / 10 && s[startIndex] - '0' > 8))
                    {
                        return int.MinValue;
                    }
                    ans = ans * 10 + s[startIndex] - '0';
                }
                else
                {
                    break;
                }
            }
            return ans * symbol;
        }
    }
}
