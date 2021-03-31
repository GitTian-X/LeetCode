namespace LeetCode.Q10_Q19
{
    //给你一个字符串 s 和一个字符规律 p，请你来实现一个支持 '.' 和 '*' 的正则表达式匹配。
    //    '.' 匹配任意单个字符
    //    '*' 匹配零个或多个前面的那一个元素
    //所谓匹配，是要涵盖 整个 字符串 s的，而不是部分字符串。
    class Q10
    {
        public bool IsMatch(string s, string p)
        {
            int m = s.Length;
            int n = p.Length;
            bool[,] f = new bool[m + 1, n + 1];
            f[0, 0] = true;

            bool Mathes(int i, int j)
            {
                if (i == 0)
                {
                    return false;
                }
                return p[j - 1] == '.' || s[i - 1] == p[j - 1];
            }

            for (int i = 0; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (p[j - 1] == '*')
                    {
                        //*匹配0个字符
                        f[i, j] = f[i, j - 2];
                        if (Mathes(i, j - 1))
                        {
                            f[i, j] = f[i, j] || f[i - 1, j];
                        }
                    }
                    else
                    {
                        if (Mathes(i, j))
                        {
                            f[i, j] = f[i - 1, j - 1];
                        }
                    }
                }
            }
            return f[m, n];
        }
    }
}
