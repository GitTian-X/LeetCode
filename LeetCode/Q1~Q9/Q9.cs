namespace LeetCode.Q1_Q9
{
    class Q9
    {
        //给你一个整数 x ，如果 x 是一个回文整数，返回 true ；否则，返回 false 。
        //回文数是指正序（从左向右）和倒序（从右向左）读都是一样的整数。例如，121 是回文，而 123 不是。
        public bool IsPalindrome(int x)
        {
            if (x < 0)
            {
                return false;
            }
            if (x < 10)
            {
                return true;
            }
            if (x % 10 == 0)
            {
                return false;
            }
            int temp = 0;
            while (x >= temp)
            {
                if (x / 10 == temp || x == temp)
                {
                    return true;
                }
                int remainder = x % 10;
                temp = temp * 10 + remainder;
                x /= 10;
            }
            return false;
        }
    }
}
