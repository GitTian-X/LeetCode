namespace LeetCode.Q20_Q29
{
    class Q28
    {
        //实现 strStr() 函数。
        //给定一个 haystack 字符串和一个 needle 字符串，在 haystack 字符串中找出 needle 字符串出现的第一个位置(从0开始)。如果不存在，则返回  -1。
        public int StrStr(string haystack, string needle)
        {
            int sourceLen = haystack.Length;
            int patLen = needle.Length;
            int[] Next()
            {
                int[] next = new int[patLen];
                next[0] = -1;
                int k = -1;
                for (int i = 1; i < patLen; i++)
                {
                    while (k != -1 && needle[k + 1] != needle[i])
                    {
                        k = next[k];
                    }
                    if (needle[k + 1] == needle[i])
                    {
                        k++;
                    }
                    next[i] = k;
                }
                return next;
            }
            int KMP()
            {
                int[] next = Next();
                int j = 0;
                for (int i = 0; i < sourceLen; i++)
                {
                    while (j > 0 && haystack[i] != needle[j])
                    {
                        j = next[j - 1] + 1;
                        if (patLen - j + i > sourceLen)
                        {
                            return -1;
                        }
                    }
                    if (haystack[i] == needle[j])
                    {
                        j++;
                    }
                    if (j == patLen)
                    {
                        return i - patLen + 1;
                    }
                }
                return -1;
            }
            if (patLen == 0)
            {
                return 0;
            }
            if (sourceLen == 0)
            {
                return -1;
            }
            return KMP();
        }
    }
}
