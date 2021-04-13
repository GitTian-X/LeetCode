namespace LeetCode.Q20_Q29
{
    class Q29
    {
        //给定两个整数，被除数 dividend 和除数 divisor。将两数相除，要求不使用乘法、除法和 mod 运算符。
        //返回被除数 dividend 除以除数 divisor 得到的商。
        //整数除法的结果应当截去（truncate）其小数部分，例如：truncate(8.345) = 8 以及 truncate(-2.7335) = -2
        public int Divide(int dividend, int divisor)
        {
            if (dividend == 0)
            {
                return 0;
            }
            if (divisor == 1)
            {
                return dividend;
            }
            else if (divisor == -1)
            {
                if (dividend > int.MinValue)
                {
                    return -dividend;
                }
                else
                {
                    return int.MaxValue;
                }
            }
            long a = dividend;
            long b = divisor;
            int sig = 1;
            if ((a < 0 && b > 0) || (a > 0 && b < 0))
            {
                sig = -1;
            }
            a = a > 0 ? a : -a;
            b = b > 0 ? b : -b;
            long res = Div(a, b);
            long Div(long a, long b)
            {
                if (a < b)
                {
                    return 0;
                }
                long count = 1;
                long tb = b;
                while ((tb + tb) <= a)
                {
                    count += count;
                    tb += tb;
                }
                return count + Div(a - tb, b);
            }
            if (sig > 0)
            {
                return res > int.MaxValue ? int.MaxValue : (int)res;
            }
            return -(int)res;
        }
    }
}
