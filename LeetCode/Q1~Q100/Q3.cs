namespace LeetCode.Q1_Q100
{
    class Q3
    {
        public int LengthOfLongestSubstring(string s)
        {
            int start = 0, end = 0, ans = 0;
            bool[] arr = new bool[128];
            while (end < s.Length)
            {
                while (arr[s[end]])
                {
                    arr[s[start]] = false;
                    start++;
                }
                arr[s[end]] = true;
                end++;
                ans = ans >= end - start ? ans : end - start;
            }
            return ans;
        }
    }
}
