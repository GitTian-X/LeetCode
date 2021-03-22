using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using static LeetCode.Solution;

namespace LeetCode
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }

    public class Solution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
            {
                return nums;
            }
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(target - nums[i]))
                {
                    return new int[] { dic[target - nums[i]], i };
                }
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], i);
                }
            }
            return null;
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 != null)
            {
                return l2;
            }
            if (l1 != null && l2 == null)
            {
                return l1;
            }
            if (l1 == null && l2 == null)
            {
                return null;
            }
            ListNode head = null;
            ListNode tail = null;
            int carry = 0;
            while (l1 != null || l2 != null)
            {
                int num1 = l1 == null ? 0 : l1.val;
                int num2 = l2 == null ? 0 : l2.val;
                int sum = num1 + num2 + carry;
                if (head == null)
                {
                    head = tail = new ListNode(sum % 10);
                }
                else
                {
                    tail.next = new ListNode(sum % 10);
                    tail = tail.next;
                }
                carry = sum / 10;
                if (l1 != null)
                {
                    l1 = l1.next;
                }
                if (l2 != null)
                {
                    l2 = l2.next;
                }
            }
            if (carry != 0)
            {
                tail.next = new ListNode(carry);
            }
            return head;
        }

        public int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            if (s.Trim().Length == 0)
            {
                return 1;
            }
            int maxLength = 0;
            int startIndex = 0;
            Dictionary<char, int> dic = new Dictionary<char, int>();
            char[] charArray = s.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (dic.ContainsKey(charArray[i]))
                {
                    if (dic[charArray[i]] < startIndex)
                    {
                        maxLength = Math.Max(maxLength, i - startIndex + 1);
                    }
                    else
                    {
                        startIndex = dic[charArray[i]] + 1;
                    }
                    dic[charArray[i]] = i;
                }
                else
                {
                    dic.Add(charArray[i], i);
                    maxLength = Math.Max(maxLength, i - startIndex + 1);
                }
            }
            return maxLength;
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
            {
                return FindMedianSortedArrays(nums2, nums1);
            }
            int shortLength = nums1.Length;
            int longLength = nums2.Length;
            int left = 0, right = shortLength;
            int firstHalfMax = 0, secondHalfMin = 0;
            while (left <= right)
            {
                int i = (left + right) / 2;
                int j = (shortLength + longLength + 1) / 2 - i;

                int nums1Min = i == 0 ? int.MinValue : nums1[i - 1];
                int nums1Max = i == shortLength ? int.MaxValue : nums1[i];
                int nums2Min = j == 0 ? int.MinValue : nums2[j - 1];
                int nums2Max = j == longLength ? int.MaxValue : nums2[j];

                if (nums1Min <= nums2Max)
                {
                    firstHalfMax = Math.Max(nums1Min, nums2Min);
                    secondHalfMin = Math.Min(nums1Max, nums2Max);
                    left++;
                }
                else
                {
                    right--;
                }
            }
            return (shortLength + longLength) % 2 == 0 ? (firstHalfMax + secondHalfMin) / 2.0 : firstHalfMax;
        }

        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            int start = 0, end = 0;
            int length = s.Length;
            char[] charArray = s.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                int len1 = ExpandAroundCenter(charArray, i, i);
                int len2 = ExpandAroundCenter(charArray, i, i + 1);
                int len = Math.Max(len1, len2);
                if (end - start < len)
                {
                    start = i - (len - 1) / 2;
                    end = i + len / 2;
                }
            }
            return s.Substring(start, end - start + 1);
        }

        private int ExpandAroundCenter(char[] array, int left, int rigth)
        {
            while (left >= 0 && rigth < array.Length && array[left] == array[rigth])
            {
                left--;
                rigth++;
            }
            return rigth - left - 1;
        }

        public string Convert(string s, int numRows)
        {
            if (numRows == 1)
            {
                return s;
            }
            StringBuilder sb = new StringBuilder();
            int length = s.Length;
            int cycleLength = numRows * 2 - 2;
            char[] charArray = s.ToCharArray();
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j + i < length; j += cycleLength)
                {
                    sb.Append(charArray[j + i]);
                    if (i > 0 && i < numRows - 1 && j + cycleLength - i < length)
                    {
                        sb.Append(charArray[j + cycleLength - i]);
                    }
                }
            }
            return sb.ToString();
        }

        public int Reverse(int x)
        {
            int res = 0;
            while (x != 0)
            {
                int remainder = x % 10;
                x /= 10;
                if (res > int.MaxValue / 10 || res == int.MaxValue && remainder > 7)
                {
                    return 0;
                }
                if (res < int.MinValue / 10 || res == int.MinValue && remainder < -8)
                {
                    return 0;
                }
                res = res * 10 + remainder;
            }
            return res;
        }

        public int MyAtoi(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return 0;
            }
            s = s.Trim();
            char[] charArray = s.ToCharArray();
            if (charArray[0] != '-' && charArray[0] < '0' && charArray[0] > '9')
            {
                return 0;
            }
            int startIndex = 0;
            int symbol = 1;
            if (charArray[0] == '-' || charArray[0] == '+')
            {
                startIndex = 1;
                symbol = charArray[0] == '-' ? -1 : 1;
            }
            int res = 0;
            for (; startIndex < charArray.Length; startIndex++)
            {
                if (charArray[startIndex] >= '0' && charArray[startIndex] <= '9')
                {
                    if (res * symbol > int.MaxValue / 10 || (res * symbol == int.MaxValue / 10 && charArray[startIndex] - '0' > 7))
                    {
                        return int.MaxValue;
                    }
                    if (res * symbol < int.MinValue / 10 || (res * symbol == int.MinValue / 10 && charArray[startIndex] - '0' > 8))
                    {
                        return int.MinValue;
                    }
                    res = res * 10 + (charArray[startIndex] - '0');
                }
                else
                    break;
            }
            return res * symbol;
        }

        public bool IsPalindrome(int x)
        {
            if (x < 0 || (x % 10 == 0 && x != 0))
            {
                return false;
            }
            int reverseNumber = 0;
            while (x > reverseNumber)
            {
                reverseNumber = reverseNumber * 10 + x % 10;
                x /= 10;
            }
            return reverseNumber == x || x == reverseNumber / 10;
        }

        public bool IsMatch(string s, string p)
        {
            char[] charArrayS = s.ToCharArray();
            char[] charArrayP = p.ToCharArray();
            int m = charArrayS.Length;
            int n = charArrayP.Length;

            bool[,] f = new bool[m + 1, n + 1];
            f[0, 0] = true;
            for (int i = 0; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (charArrayP[j - 1] == '*')
                    {
                        f[i, j] = f[i, j - 2];
                        if (Mathes(charArrayS, charArrayP, i, j - 1))
                        {
                            f[i, j] = f[i, j] || f[i - 1, j];
                        }
                    }
                    else
                    {
                        if (Mathes(charArrayS, charArrayP, i, j))
                        {
                            f[i, j] = f[i - 1, j - 1];
                        }
                    }
                }
            }
            return f[m, n];
        }

        private bool Mathes(char[] s, char[] p, int i, int j)
        {
            if (i == 0)
            {
                return false;
            }
            if (p[j - 1] == '.')
            {
                return true;
            }
            return s[i - 1] == p[j - 1];
        }

        public int MaxArea(int[] height)
        {
            int left = 0;
            int right = height.Length - 1;
            int maxArea = 0;
            while (left <= right)
            {
                if (height[left] <= height[right])
                {
                    maxArea = Math.Max(maxArea, height[left] * (right - left));
                    left++;
                }
                else
                {
                    maxArea = Math.Max(maxArea, height[right] * (right - left));
                    right--;
                }
            }
            return maxArea;
        }
        public string IntToRoman(int num)
        {
            int[] nums = new int[] { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
            string[] romans = new string[] { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            StringBuilder sb = new StringBuilder();
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                while (num >= nums[i])
                {
                    num -= nums[i];
                    sb.Append(romans[i]);
                }
            }
            return sb.ToString();
        }
        public int RomanToInt(string s)
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>() { { "I", 1 },{ "IV", 3}, { "V", 5}, { "IX", 8 }, { "X", 10 }, { "XL", 30 },
                {"L", 50 }, {"XC", 80 }, { "C", 100}, { "CD", 300}, { "D", 500}, { "CM", 800}, { "M", 1000} };
            int res = keyValuePairs[s.Substring(0, 1)];
            for (int i = 1; i < s.Length; i++)
            {
                string two = s.Substring(i - 1, 2);
                string one = s.Substring(i, 1);
                res += keyValuePairs.ContainsKey(two) ? keyValuePairs[two] : keyValuePairs[one];
            }
            return res;
        }
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
            {
                return "";
            }
            StringBuilder one = new StringBuilder(strs[0]);
            while (one.Length >= 0)
            {
                bool isChild = true;
                for (int i = 1; i < strs.Length; i++)
                {
                    if (!strs[i].StartsWith(one.ToString()))
                    {
                        isChild = false;
                        one.Remove(one.Length - 1, 1);
                        break;
                    }
                }
                if (isChild)
                {
                    return one.ToString();
                }
            }
            return "";
        }
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (nums.Length < 3)
            {
                return res;
            }
            Array.Sort(nums);
            int numsLength = nums.Length;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }
                int tmp = -nums[i];
                int k = numsLength - 1;
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1])
                    {
                        continue;
                    }
                    while (j < k && nums[j] + nums[k] > tmp)
                    {
                        k--;
                    }
                    if (j == k)
                    {
                        break;
                    }
                    if (nums[j] + nums[k] == tmp)
                    {
                        List<int> tmpList = new List<int>();
                        tmpList.Add(nums[i]);
                        tmpList.Add(nums[j]);
                        tmpList.Add(nums[k]);
                        res.Add(tmpList);
                    }
                }
            }
            return res;
        }
        public int ThreeSumClosest(int[] nums, int target)
        {
            Array.Sort(nums);
            int res = 100000;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }
                int left = i + 1;
                int right = nums.Length - 1;
                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];
                    if (sum == target)
                    {
                        return sum;
                    }
                    if (Math.Abs(sum - target) < Math.Abs(res - target))
                    {
                        res = sum;
                    }
                    if (sum > target)
                    {
                        right--;
                        while (left < right && nums[right] == nums[right + 1])
                        {
                            right--;
                        }
                    }
                    else
                    {
                        left++;
                        while (left < right && nums[left] == nums[left - 1])
                        {
                            left++;
                        }
                    }
                }
            }
            return res;
        }
        public IList<string> LetterCombinations(string digits)
        {
            IList<string> res = new List<string>();
            if (digits.Length == 0)
            {
                return res;
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
            BackTrack(res, map, digits, 0, new StringBuilder());
            return res;
        }

        private void BackTrack(IList<string> res, Dictionary<char, string> map, string digits, int v, StringBuilder stringBuilder)
        {
            if (v == digits.Length)
            {
                res.Add(stringBuilder.ToString());
            }
            else
            {
                char digit = digits.ToCharArray()[v];
                string s = map[digit];
                for (int i = 0; i < s.Length; i++)
                {
                    stringBuilder.Append(s[i]);
                    BackTrack(res, map, digits, v + 1, stringBuilder);
                    stringBuilder.Remove(v, 1);
                }
            }
        }
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (nums.Length < 4)
            {
                return res;
            }
            Array.Sort(nums);
            int numsLength = nums.Length;
            for (int i = 0; i < numsLength - 3; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }
                if (nums[i] + nums[i + 1] + nums[i + 2] + nums[i + 3] > target)
                {
                    break;
                }
                if (nums[i] + nums[numsLength - 1] + nums[numsLength - 2] + nums[numsLength - 3] < target)
                {
                    continue;
                }
                for (int j = i + 1; j < numsLength - 2; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1])
                    {
                        continue;
                    }
                    if (nums[i] + nums[j] + nums[j + 1] + nums[j + 2] > target)
                    {
                        break;
                    }
                    if (nums[i] + nums[j] + nums[numsLength - 2] + nums[numsLength - 1] < target)
                    {
                        continue;
                    }
                    int left = j + 1, right = numsLength - 1;
                    while (left < right)
                    {
                        int sum = nums[i] + nums[j] + nums[left] + nums[right];
                        if (sum == target)
                        {
                            res.Add(new List<int>() { nums[i], nums[j], nums[left], nums[right] });
                            while (left < right && nums[left] == nums[left + 1])
                            {
                                left++;
                            }
                            left++;
                            while (left < right && nums[right] == nums[right - 1])
                            {
                                right--;
                            }
                            right--;
                        }
                        else if (sum < target)
                        {
                            left++;
                        }
                        else
                        {
                            right--;
                        }
                    }
                }
            }
            return res;
        }
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode dumy = new ListNode(0, head);
            ListNode slow = dumy;
            ListNode fast = head;
            for (int i = 0; i < n; i++)
            {
                fast = fast.next;
            }
            while (fast != null)
            {
                fast = fast.next;
                slow = slow.next;
            }
            slow.next = slow.next.next;
            return dumy.next;
        }
        public bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return true;
            }
            if (s.Length % 2 == 1)
            {
                return false;
            }
            Dictionary<char, char> keyValuePairs = new Dictionary<char, char>()
            {
                {')', '(' },
                {'}', '{' },
                {']', '[' }
            };
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (keyValuePairs.ContainsKey(s[i]))
                {
                    if (stack.Count == 0 || stack.Pop() != keyValuePairs[s[i]])
                    {
                        return false;
                    }
                }
                else
                {
                    stack.Push(s[i]);
                }
            }
            return stack.Count == 0;
        }
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode head = new ListNode();
            ListNode res = head;
            while (l1 != null && l2 != null)
            {
                if (l1.val >= l2.val)
                {
                    head.next = new ListNode(l2.val);
                    l2 = l2.next;
                }
                else
                {
                    head.next = new ListNode(l1.val);
                    l1 = l1.next;
                }
                head = head.next;
            }
            head.next = l1 == null ? l2 : l1;
            return res.next;
        }
        public IList<string> GenerateParenthesis(int n)
        {
            IList<string> res = new List<string>();
            generateParenthesis(res, n, n, "");
            return res;
        }
        private void generateParenthesis(IList<string> res, int left, int right, string tmp)
        {
            if (left == 0 && right == 0)
            {
                res.Add(tmp);
                return;
            }
            if (left == right)
            {
                generateParenthesis(res, left - 1, right, tmp + "(");
            }
            else if (left < right)
            {
                if (left > 0)
                {
                    generateParenthesis(res, left - 1, right, tmp + "(");
                }
                generateParenthesis(res, left, right - 1, tmp + ")");
            }
        }
        public ListNode MergeKLists(ListNode[] lists)
        {
            return Merge(lists, 0, lists.Length - 1);
        }
        private ListNode Merge(ListNode[] lists, int l, int r)
        {
            if (l == r)
            {
                return lists[l];
            }
            if (l > r)
            {
                return null;
            }
            int mid = (l + r) / 2;
            return MergeTwoLists(Merge(lists, l, mid), Merge(lists, mid + 1, r));
        }
        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            ListNode newHead = head.next;
            head.next = SwapPairs(newHead.next);
            newHead.next = head;
            return newHead;
        }
        public ListNode SwapPairs1(ListNode head)
        {
            ListNode dummyHead = new ListNode(0);
            dummyHead.next = head;
            ListNode tmp = dummyHead;
            while (tmp.next != null && tmp.next.next != null)
            {
                ListNode l1 = tmp.next;
                ListNode l2 = tmp.next.next;
                tmp.next = l2;
                l1.next = l2.next;
                l2.next = l1;
                tmp = l1;
            }
            return dummyHead.next;
        }
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode hair = new ListNode(0);
            hair.next = head;
            ListNode pre = hair;
            while (head != null)
            {
                ListNode tail = pre;
                for (int i = 0; i < k; i++)
                {
                    tail = tail.next;
                    if (tail == null)
                    {
                        return hair.next;
                    }
                }
                ListNode nex = tail.next;
                ListNode[] reverse = MyReverse(head, tail);
                head = reverse[0];
                tail = reverse[1];
                pre.next = head;
                tail.next = nex;
                pre = tail;
                head = tail.next;
            }
            return hair.next;
        }

        private ListNode[] MyReverse(ListNode head, ListNode tail)
        {
            ListNode prev = tail.next;
            ListNode p = head;
            while (prev != tail)
            {
                ListNode nex = p.next;
                p.next = prev;
                prev = p;
                p = nex;
            }
            return new ListNode[] { tail, head };
        }
        public int RemoveDuplicates(int[] nums)
        {
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                while (i + 1 < nums.Length && nums[i] == nums[i + 1])
                {
                    i++;
                }
                nums[res++] = nums[i];
            }
            return res;
        }
        public int RemoveElement(int[] nums, int val)
        {
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != val)
                {
                    nums[res] = nums[i];
                    res++;
                }
            }
            return res;
        }
        public int StrStr(string haystack, string needle)
        {
            return haystack.IndexOf(needle);
        }
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
            if (sig > 0)
            {
                return res > int.MaxValue ? int.MaxValue : (int)res;
            }
            return -(int)res;
        }

        private long Div(long a, long b)
        {
            if (a < b)
            {
                return 0;
            }
            long count = 1;
            long tb = b;
            while ((tb + tb) <= a)
            {
                count = count + count;
                tb = tb + tb;
            }
            return count + Div(a - tb, b);
        }
        public IList<int> FindSubstring(string s, string[] words)
        {
            List<int> res = new List<int>();
            int wordsNum = words.Length;
            if (wordsNum == 0)
            {
                return res;
            }
            int wordLen = words[0].Length;
            Dictionary<string, int> allWords = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (allWords.ContainsKey(word))
                {
                    allWords[word]++;
                }
                else
                {
                    allWords.Add(word, 0);
                }
            }
            for (int i = 0; i < wordLen; i++)
            {
                Dictionary<string, int> hasWords = new Dictionary<string, int>();
                int num = 0;
                for (int j = i; j < s.Length - wordsNum * wordLen + 1; j += wordLen)
                {
                    bool hasRemoved = false;
                    while (num < wordsNum)
                    {
                        string word = s.Substring(j + num * wordLen, wordLen);
                        if (allWords.ContainsKey(word))
                        {
                            if (hasWords.ContainsKey(word))
                            {
                                hasWords[word]++;
                            }
                            else
                            {
                                hasWords.Add(word, 0);
                            }
                            if (hasWords[word] > allWords[word])
                            {
                                hasRemoved = true;
                                int removeNum = 0;
                                while (hasWords[word] > allWords[word])
                                {
                                    string firstWorld = s.Substring(j + removeNum * wordLen, wordLen);
                                    hasWords[firstWorld]--;
                                    removeNum++;
                                }
                                num = num - removeNum + 1;
                                j = j + (removeNum - 1) * wordLen;
                                break;
                            }
                        }
                        else
                        {
                            hasWords.Clear();
                            j = j + num * wordLen;
                            num = 0;
                            break;
                        }
                        num++;
                    }
                    if (num == wordsNum)
                    {
                        res.Add(j);
                    }
                    if (num > 0 && !hasRemoved)
                    {
                        string firstWord = s.Substring(j, wordLen);
                        hasWords[firstWord]--;
                        num--;
                    }
                }
            }
            return res;
        }
        public void NextPermutation(int[] nums)
        {
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                for (int j = nums.Length - 1; j > i; j--)
                {
                    if (nums[j] > nums[i])
                    {
                        Swap(nums, i, j);
                        Reverse(nums, i + 1);
                        return;
                    }
                }
            }
            Array.Sort(nums);
        }
        private void Swap(int[] nums, int left, int right)
        {
            int tmp = nums[right];
            nums[right] = nums[left];
            nums[left] = tmp;
        }
        private void Reverse(int[] nums, int start)
        {
            int right = nums.Length - 1;
            while (start < right)
            {
                Swap(nums, start, right);
                start++;
                right--;
            }
        }
        public int LongestValidParentheses(string s)
        {
            int left = 0, right = 0, maxLength = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    left++;
                }
                else
                {
                    right++;
                }
                if (left == right)
                {
                    maxLength = Math.Max(maxLength, right + left);
                }
                else if (right > left)
                {
                    left = right = 0;
                }
            }
            left = right = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '(')
                {
                    left++;
                }
                else
                {
                    right++;
                }
                if (left == right)
                {
                    maxLength = Math.Max(maxLength, right + left);
                }
                else if (right < left)
                {
                    left = right = 0;
                }
            }
            return maxLength;
        }
        public int Search(int[] nums, int target)
        {
            int len = nums.Length;
            if (target < nums[0])
            {
                for (int i = len - 1; i > 0; i--)
                {
                    if (target == nums[i])
                    {
                        return i;
                    }
                    if (nums[i] < nums[i - 1])
                    {
                        return -1;
                    }
                }
                return -1;
            }
            else if (target > nums[0])
            {
                for (int i = 0; i < len - 1; i++)
                {
                    if (target == nums[i])
                    {
                        return i;
                    }
                    if (nums[i] > nums[i + 1])
                    {
                        return -1;
                    }
                }
                return nums[len - 1] == target ? len - 1 : -1;
            }
            else
            {
                return 0;
            }
        }
        public int[] SearchRange(int[] nums, int target)
        {
            int left = BinarySeatch(nums, target, true);
            int right = BinarySeatch(nums, target, false) - 1;
            if (left <= right && right < nums.Length && nums[left] == target && nums[right] == target)
            {
                return new int[] { left, right };
            }
            return new int[] { -1, -1 };
        }
        public int BinarySeatch(int[] nums, int target, bool lower)
        {
            int left = 0, ans = nums.Length, right = ans - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (target < nums[mid] || (lower && target <= nums[mid]))
                {
                    right = mid - 1;
                    ans = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return ans;
        }
        public int SearchInsert(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (target > nums[mid])
                {
                    left = mid + 1;
                }
                else if (target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    return mid;
                }
            }
            return left;
        }
        public bool IsValidSudoku(char[][] board)
        {
            int[] wow = new int[9];
            int mux1, mux2, mux3, boxIndex;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == '.')
                    {
                        continue;
                    }
                    mux1 = 0x01 << (board[i][j] - '1');
                    mux2 = 0x01 << 9 << (board[i][j] - '1');
                    mux3 = 0x01 << 9 << 9 << (board[i][j] - '1');
                    boxIndex = (i / 3) * 3 + j / 3;
                    if ((wow[i] & mux1) != mux1 && (wow[j] & mux2) != mux2 && (wow[boxIndex] & mux3) != mux3)
                    {
                        wow[i] = wow[i] | mux1;
                        wow[j] = wow[j] | mux2;
                        wow[boxIndex] = wow[boxIndex] | mux3;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private int[] line = new int[9];
        private int[] column = new int[9];
        private int[,] block = new int[3, 3];
        private bool valid = false;
        private List<int[]> spaces = new List<int[]>();
        public void SolveSudoku(char[][] board)
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (board[i][j] != '.')
                    {
                        int digit = board[i][j] - '0' - 1;
                        Flip(i, j, digit);
                    }
                }
            }

            while (true)
            {
                bool modified = false;
                for (int i = 0; i < 9; ++i)
                {
                    for (int j = 0; j < 9; ++j)
                    {
                        if (board[i][j] == '.')
                        {
                            int mask = ~(line[i] | column[j] | block[i / 3, j / 3]) & 0x1ff;
                            if ((mask & (mask - 1)) == 0)
                            {
                                int digit = BitCount(mask - 1);
                                Flip(i, j, digit);
                                board[i][j] = (char)(digit + '0' + 1);
                                modified = true;
                            }
                        }
                    }
                }
                if (!modified)
                {
                    break;
                }
            }

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (board[i][j] == '.')
                    {
                        spaces.Add(new int[] { i, j });
                    }
                }
            }
            DFS(board, 0);
        }
        private void DFS(char[][] board, int pos)
        {
            if (pos == spaces.Count)
            {
                valid = true;
                return;
            }

            int[] space = spaces[pos];
            int i = space[0], j = space[1];
            int mask = ~(line[i] | column[j] | block[i / 3, j / 3]) & 0x1ff;
            for (; mask != 0 && !valid; mask &= (mask - 1))
            {
                int digitMask = mask & (-mask);
                int digit = BitCount(digitMask - 1);
                Flip(i, j, digit);
                board[i][j] = (char)(digit + '0' + 1);
                DFS(board, pos + 1);
                Flip(i, j, digit);
            }
        }

        private void Flip(int i, int j, int digit)
        {
            line[i] ^= (1 << digit);
            column[j] ^= (1 << digit);
            block[i / 3, j / 3] ^= (1 << digit);
        }
        private int BitCount(int n)
        {
            int count = 0;
            while (n != 0)
            {
                count++;
                n &= (n - 1);
            }
            return count;
        }
        public string CountAndSay(int n)
        {
            StringBuilder pre;
            StringBuilder cur = new StringBuilder("1");
            for (int i = 1; i < n; i++)
            {
                pre = cur;
                cur = new StringBuilder();
                int start = 0, end = 0;
                while (end < pre.Length)
                {
                    while (end < pre.Length && pre[start] == pre[end])
                    {
                        end++;
                    }
                    cur.Append($"{end - start}{pre[start]}");
                    start = end;
                }
            }
            return cur.ToString();
        }
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            int len = candidates.Length;
            IList<IList<int>> res = new List<IList<int>>();
            if (len == 0)
            {
                return res;
            }
            Array.Sort(candidates);
            Stack<int> path = new Stack<int>();
            DFS(candidates, 0, len, target, path, res);
            return res;
        }

        private void DFS(int[] candidates, int begin, int len, int target, Stack<int> path, IList<IList<int>> res)
        {
            if (target == 0)
            {
                res.Add(new List<int>(path));
                return;
            }
            for (int i = begin; i < len; i++)
            {
                if (target - candidates[i] < 0)
                {
                    break;
                }
                path.Push(candidates[i]);
                DFS(candidates, i, len, target - candidates[i], path, res);
                path.Pop();
            }
        }

        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            int len = candidates.Length;
            IList<IList<int>> res = new List<IList<int>>();
            if (len == 0)
            {
                return res;
            }
            Array.Sort(candidates);
            Stack<int> path = new Stack<int>();
            DFS2(candidates, 0, len, target, path, res);
            return res;
        }
        private void DFS2(int[] candidates, int begin, int len, int target, Stack<int> path, IList<IList<int>> res)
        {
            if (target == 0)
            {
                res.Add(new List<int>(path));
                return;
            }
            for (int i = begin; i < len; i++)
            {
                if (target - candidates[i] < 0)
                {
                    break;
                }
                if (i > begin && candidates[i] == candidates[i - 1])
                {
                    continue;
                }
                path.Push(candidates[i]);
                DFS2(candidates, i + 1, len, target - candidates[i], path, res);
                path.Pop();
            }
        }
        public int FirstMissingPositive(int[] nums)
        {
            bool hasOne = false;
            int last = -1;
            int len = nums.Length;
            if (len == 0)
            {
                return 1;
            }
            Array.Sort(nums);
            for (int i = 0; i < len; i++)
            {
                if (nums[i] < 1)
                {
                    continue;
                }
                if (nums[i] == 1)
                {
                    hasOne = true;
                    last = 1;
                }
                if (nums[i] > 1)
                {
                    if (!hasOne) return 1;
                    if (nums[i] - last > 1)
                    {
                        return last + 1;
                    }
                    last = nums[i];
                }
            }
            return Math.Max(1, nums[len - 1] + 1);
        }
        public int Trap(int[] height)
        {
            int leftMax = 0, rightMax = 0;
            int left = 0, right = height.Length - 1;
            int ans = 0;
            while (left < right)
            {
                if (height[left] <= height[right])
                {
                    if (height[left] >= leftMax)
                    {
                        leftMax = height[left];
                    }
                    else
                    {
                        ans += (leftMax - height[left]);
                    }
                    left++;
                }
                else
                {
                    if (height[right] >= rightMax)
                    {
                        rightMax = height[right];
                    }
                    else
                    {
                        ans += (rightMax - height[right]);
                    }
                    right--;
                }
            }
            return ans;
        }
        public string Multiply(string num1, string num2)
        {
            if (num1.Equals("0") || num2.Equals("0"))
            {
                return "0";
            }
            int m = num1.Length, n = num2.Length;
            int[] ansAry = new int[m + n];
            for (int i = m - 1; i >= 0; i--)
            {
                int x = num1[i] - '0';
                for (int j = n - 1; j >= 0; j--)
                {
                    int y = num2[j] - '0';
                    ansAry[i + j + 1] += x * y;
                }
            }
            for (int i = m + n - 1; i > 0; i--)
            {
                ansAry[i - 1] += ansAry[i] / 10;
                ansAry[i] %= 10;
            }
            int index = ansAry[0] == 0 ? 1 : 0;
            StringBuilder ans = new StringBuilder();
            while (index < m + n)
            {
                ans.Append(ansAry[index]);
                index++;
            }
            return ans.ToString();
        }
        public bool IsMatch2(string s, string p)
        {
            int sRight = s.Length, pRight = p.Length;
            while (sRight > 0 && pRight > 0 && p[pRight - 1] != '*')
            {
                if (CharMatch(s[sRight - 1], p[pRight - 1]))
                {
                    sRight--;
                    pRight--;
                }
                else
                    return false;
            }
            if (pRight == 0)
            {
                return sRight == 0;
            }
            int sIndex = 0, pIndex = 0;
            int sRecord = -1, pRecord = -1;
            while (sIndex < sRight && pIndex < pRight)
            {
                if (p[pIndex] == '*')
                {
                    pIndex++;
                    sRecord = sIndex;
                    pRecord = pIndex;
                }
                else if (CharMatch(s[sIndex], p[pIndex]))
                {
                    sIndex++;
                    pIndex++;
                }
                else if (sRecord != -1 && sRecord + 1 < sRight)
                {
                    sRecord++;
                    sIndex = sRecord;
                    pIndex = pRecord;
                }
                else
                {
                    return false;
                }
            }
            return AllStar(p, pIndex, pRight);
        }

        private bool AllStar(string p, int pIndex, int pRight)
        {
            for (int i = pIndex; i < pRight; i++)
            {
                if (p[i] != '*')
                {
                    return false;
                }
            }
            return true;
        }

        private bool CharMatch(char v1, char v2)
        {
            return v1 == v2 || v2 == '?';
        }
        public int Jump(int[] nums)
        {
            int ans = 0;
            int end = 0;
            int maxPos = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                maxPos = Math.Max(nums[i] + i, maxPos);
                if (i == end)
                {
                    end = maxPos;
                    ans++;
                }
            }
            return ans;
        }
        public IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            List<int> path = new List<int>();
            path.AddRange(nums);
            int len = nums.Length;
            BackTrack2(len, path, ans, 0);
            return ans;
        }

        private void BackTrack2(int len, List<int> path, IList<IList<int>> ans, int first)
        {
            if (first == len)
            {
                ans.Add(new List<int>(path));
            }
            for (int i = first; i < len; i++)
            {
                Swap(path, first, i);
                BackTrack2(len, path, ans, first + 1);
                Swap(path, first, i);
            }
        }
        private void Swap(List<int> path, int index1, int index2)
        {
            int tmp = path[index1];
            path[index1] = path[index2];
            path[index2] = tmp;
        }
        private bool[] vis;
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            int len = nums.Length;
            IList<IList<int>> ans = new List<IList<int>>();
            List<int> path = new List<int>();
            vis = new bool[len];
            Array.Sort(nums);
            BackTrack3(nums, ans, 0, path);
            return ans;
        }

        private void BackTrack3(int[] nums, IList<IList<int>> ans, int index, List<int> path)
        {
            if (index == nums.Length)
            {
                ans.Add(new List<int>(path));
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (vis[i] || (i > 0 && nums[i] == nums[i - 1] && !vis[i - 1]))
                {
                    continue;
                }
                path.Add(nums[i]);
                vis[i] = true;
                BackTrack3(nums, ans, index + 1, path);
                vis[i] = false;
                path.RemoveAt(index);
            }
        }
        public void Rotate(int[][] matrix)
        {
            int len = matrix.Length;
            for (int i = 0; i < (len + 1) / 2; i++)
            {
                for (int j = 0; j < len / 2; j++)
                {
                    int tmp = matrix[len - 1 - j][i];
                    matrix[len - 1 - j][i] = matrix[len - 1 - i][len - j - 1];
                    matrix[len - 1 - i][len - j - 1] = matrix[j][len - 1 - i];
                    matrix[j][len - 1 - i] = matrix[i][j];
                    matrix[i][j] = tmp;
                }
            }
        }
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            IList<IList<string>> ans = new List<IList<string>>();
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            foreach (var str in strs)
            {
                char[] charArr = str.ToCharArray();
                Array.Sort(charArr);
                string key = new string(charArr);
                List<string> lis = dic.GetValueOrDefault(key, new List<string>());
                lis.Add(str);
                if (dic.ContainsKey(key))
                {
                    dic[key] = lis;
                }
                else
                {
                    dic.Add(key, lis);
                }
            }
            return new List<IList<string>>(dic.Values);
        }
        public double MyPow(double x, int n)
        {
            long N = n;
            return n >= 0 ? QuickMul(x, N) : 1.0 / QuickMul(x, -N);
        }
        private double QuickMul(double x, long n)
        {
            double ans = 1.0;
            while (n > 0)
            {
                if (n % 2 == 1)
                {
                    ans *= x;
                }
                x *= x;
                n /= 2;
            }
            return ans;
        }
        public IList<IList<string>> SolveNQueens(int n)
        {
            int[] queens = new int[n];
            Array.Fill(queens, -1);
            IList<IList<string>> solutions = new List<IList<string>>();
            Solve(solutions, queens, n, 0, 0, 0, 0);
            return solutions;
        }

        private void Solve(IList<IList<string>> solutions, int[] queens, int n, int row, int column, int diagonals1, int diagonals2)
        {
            if (row == n)
            {
                List<string> board = GenerateBoard(queens, n);
                solutions.Add(board);
            }
            else
            {
                int availablePositions = ((1 << n) - 1) & (~(column | diagonals1 | diagonals2));
                while (availablePositions != 0)
                {
                    int position = availablePositions & (-availablePositions);
                    availablePositions &= (availablePositions - 1);
                    int c = BitCount(position - 1);
                    queens[row] = c;
                    Solve(solutions, queens, n, row + 1, column | position, (diagonals1 | position) << 1, (diagonals2 | position) >> 1);
                    queens[row] = -1;
                }
            }
        }

        private List<string> GenerateBoard(int[] queens, int n)
        {
            List<string> board = new List<string>();
            for (int i = 0; i < n; i++)
            {
                char[] row = new char[n];
                Array.Fill(row, '.');
                row[queens[i]] = 'Q';
                board.Add(new string(row));
            }
            return board;
        }
        public int TotalNQueens(int n)
        {
            return Solve(n, 0, 0, 0, 0);
        }

        private int Solve(int n, int row, int column, int diagonals1, int diagonals2)
        {
            if (row == n)
            {
                return 1;
            }
            else
            {
                int count = 0;
                int availablePositions = ((1 << n) - 1) & (~(column | diagonals1 | diagonals2));
                while (availablePositions != 0)
                {
                    int position = availablePositions & (-availablePositions);
                    availablePositions &= (availablePositions - 1);
                    count += Solve(n, row + 1, column | position, (diagonals1 | position) << 1, (diagonals2 | position) >> 1);
                }
                return count;
            }
        }
        public int MaxSubArray(int[] nums)
        {
            int pre = 0, maxAns = nums[0];
            foreach (var num in nums)
            {
                pre = Math.Max(pre + num, num);
                maxAns = Math.Max(maxAns, pre);
            }
            return maxAns;
        }
        public IList<int> SpiralOrder(int[][] matrix)
        {
            IList<int> ans = new List<int>();
            if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
            {
                return ans;
            }
            int rows = matrix.Length, column = matrix[0].Length;
            int left = 0, right = column - 1, top = 0, bottom = rows - 1;
            while (left <= right && top <= bottom)
            {
                for (int i = left; i <= right; i++)
                {
                    ans.Add(matrix[top][i]);
                }
                for (int j = top + 1; j <= bottom; j++)
                {
                    ans.Add(matrix[j][right]);
                }
                if (left < right && top < bottom)
                {
                    for (int i = right - 1; i > left; i--)
                    {
                        ans.Add(matrix[bottom][i]);
                    }
                    for (int row = bottom; row > top; row--)
                    {
                        ans.Add(matrix[row][left]);
                    }
                }
                left++;
                right--;
                top++;
                bottom--;
            }
            return ans;
        }
        public bool CanJump(int[] nums)
        {
            int maxLength = 0;
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                if (i <= maxLength)
                {
                    maxLength = Math.Max(maxLength, nums[i] + i);
                    if (maxLength >= n - 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public int[][] Merge(int[][] intervals)
        {
            if (intervals.Length == 0)
            {
                return new int[0][];
            }
            Array.Sort(intervals, (int[] num1, int[] num2) =>
            {
                return num1[0] - num2[0];
            });
            List<int[]> merged = new List<int[]>();
            for (int i = 0; i < intervals.Length; i++)
            {
                int L = intervals[i][0], R = intervals[i][1];
                if (merged.Count == 0 || merged[merged.Count - 1][1] < L)
                {
                    merged.Add(new int[] { L, R });
                }
                else
                {
                    merged[merged.Count - 1][1] = Math.Max(R, merged[merged.Count - 1][1]);
                }
            }
            return merged.ToArray();
        }
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            int left = newInterval[0];
            int right = newInterval[1];
            bool placed = false;
            List<int[]> ans = new List<int[]>();
            foreach (var interval in intervals)
            {
                if (interval[0] > right)
                {
                    if (!placed)
                    {
                        ans.Add(new int[] { left, right });
                        placed = true;
                    }
                    ans.Add(interval);
                }
                else if (interval[1] < left)
                {
                    ans.Add(interval);
                }
                else
                {
                    left = Math.Min(left, interval[0]);
                    right = Math.Max(right, interval[1]);
                }
            }
            if (!placed)
            {
                ans.Add(new int[] { left, right });
            }
            return ans.ToArray();
        }
        public int LengthOfLastWord(string s)
        {
            int ans = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] != ' ')
                {
                    ans++;
                }
                else if (ans != 0)
                {
                    return ans;
                }
            }
            return ans;
        }
        public int[][] GenerateMatrix(int n)
        {
            int[][] ans = new int[n][];
            int left = 0, right = n - 1, top = 0, bottom = n - 1;
            int start = 1, target = n * n;
            for (int i = 0; i < n; i++)
            {
                ans[i] = new int[n];
            }
            while (start <= target)
            {
                for (int i = left; i <= right; i++)
                {
                    ans[top][i] = start++;
                }
                top++;
                for (int i = top; i <= bottom; i++)
                {
                    ans[i][right] = start++;
                }
                right--;
                for (int i = right; i >= left; i--)
                {
                    ans[bottom][i] = start++;
                }
                bottom--;
                for (int i = bottom; i >= top; i--)
                {
                    ans[i][left] = start++;
                }
                left++;
            }
            return ans;
        }
        public string GetPermutation(int n, int k)
        {
            k--;
            int[] factorial = new int[n];
            factorial[0] = 1;
            for (int i = 1; i < n; i++)
            {
                factorial[i] = factorial[i - 1] * i;
            }
            List<int> nums = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                nums.Add(i);
            }
            StringBuilder ans = new StringBuilder();
            for (int i = n - 1; i >= 0; i--)
            {
                int index = k / factorial[i];
                ans.Append(nums[index]);
                nums.RemoveAt(index);
                k -= index * factorial[i];
            }
            return ans.ToString();
        }
        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null)
            {
                return null;
            }
            if (head.next == null)
            {
                return head;
            }
            ListNode oldTail = head;
            int n;
            for (n = 1; oldTail.next != null; n++)
            {
                oldTail = oldTail.next;
            }
            oldTail.next = head;
            ListNode newTail = head;
            for (int i = 0; i < n - k % n - 1; i++)
            {
                newTail = newTail.next;
            }
            ListNode ans = newTail.next;
            newTail.next = null;
            return ans;
        }
        public int UniquePaths(int m, int n)
        {
            long ans = 1;
            for (int i = n, y = 1; y < m; i++, y++)
            {
                ans = ans * i / y;
            }
            return (int)ans;
        }
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int n = obstacleGrid.Length;
            int m = obstacleGrid[0].Length;
            int[] f = new int[m];
            f[0] = obstacleGrid[0][0] == 0 ? 1 : 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (obstacleGrid[i][j] == 1)
                    {
                        f[j] = 0;
                        continue;
                    }
                    if (j - 1 >= 0 && obstacleGrid[i][j - 1] == 0)
                    {
                        f[j] += f[j - 1];
                    }
                }
            }
            return f[m - 1];
        }
        public int MinPathSum(int[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0)
            {
                return 0;
            }
            int rows = grid.Length;
            int column = grid[0].Length;
            int[,] dp = new int[rows, column];
            dp[0, 0] = grid[0][0];
            for (int i = 1; i < rows; i++)
            {
                dp[i, 0] = dp[i - 1, 0] + grid[i][0];
            }
            for (int i = 1; i < column; i++)
            {
                dp[0, i] = dp[0, i - 1] + grid[0][i];
            }
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < column; j++)
                {
                    dp[i, j] = Math.Min(dp[i, j - 1], dp[i - 1, j]) + grid[i][j];
                }
            }
            return dp[rows - 1, column - 1];
        }
        public bool IsNumber(string s)
        {
            int state = 0;
            int finals = 0b101101000;
            int[][] transfer = new int[][]
            {
                new int[]{0, 1, 6, 2, -1 },
                new int[]{-1, -1, 6, 2, -1 },
                new int[]{-1, -1, 3, -1, -1 },
                new int[]{8, -1, 3, -1, 4 },
                new int[]{-1, 7, 5, -1, -1 },
                new int[]{8, -1, 5, -1, -1 },
                new int[]{8, -1, 6, 3, 4 },
                new int[]{-1, -1, 5, -1, -1 },
                new int[]{8, -1, -1, -1, -1 },
            };
            char[] ss = s.ToCharArray();
            for (int i = 0; i < ss.Length; i++)
            {
                int id = Make(ss[i]);
                if (id < 0)
                {
                    return false;
                }
                state = transfer[state][id];
                if (state < 0)
                {
                    return false;
                }
            }
            return (finals & (1 << state)) > 0;
        }
        private int Make(char c)
        {
            return c switch
            {
                ' ' => 0,
                '+' => 1,
                '-' => 1,
                '.' => 3,
                'e' => 4,
                _ => (c >= 48 && c <= 57) ? 2 : -1
            };
        }
        public int[] PlusOne(int[] digits)
        {
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                digits[i]++;
                digits[i] %= 10;
                if (digits[i] != 0)
                {
                    return digits;
                }
            }
            digits = new int[digits.Length + 1];
            digits[0] = 1;
            return digits;
        }
        public string AddBinary(string a, string b)
        {
            StringBuilder ans = new StringBuilder();
            int length = Math.Max(a.Length, b.Length);
            int carry = 0;
            for (int i = 0; i < length; i++)
            {
                carry += i < a.Length ? a[a.Length - 1 - i] - '0' : 0;
                carry += i < b.Length ? b[b.Length - 1 - i] - '0' : 0;
                ans.Append((char)(carry % 2 + '0'));
                carry /= 2;
            }
            if (carry > 0)
            {
                ans.Append('1');
            }
            char[] c = ans.ToString().ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }
        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            IList<string> ans = new List<string>();
            int cnt = 0, bg = 0;
            for (int i = 0; i < words.Length; i++)
            {
                cnt += words[i].Length + 1;
                if (i + 1 == words.Length || cnt + words[i + 1].Length > maxWidth)
                {
                    ans.Add(FullWords(words, bg, i, maxWidth, i + 1 == words.Length));
                    bg = i + 1;
                    cnt = 0;
                }
            }
            return ans;
        }
        private string FullWords(string[] words, int bg, int ed, int maxWidth, bool lastLine = false)
        {
            int wordCount = ed - bg + 1;
            //空格数 = 总长减去单词后默认的空格,+1是为了处理本行最后一个单词带的空格
            int spaceCount = maxWidth + 1 - wordCount;
            for (int i = bg; i <= ed; i++)
            {
                spaceCount -= words[i].Length;
            }
            int spaceSuffix = 1;
            int spaceAvg = (wordCount == 1) ? 1 : spaceCount / (wordCount - 1);
            int spaceExtra = (wordCount == 1) ? 0 : spaceCount % (wordCount - 1);
            StringBuilder ans = new StringBuilder();
            for (int i = bg; i < ed; i++)
            {
                ans.Append(words[i]);
                if (lastLine)
                {
                    ans.Append(' ');
                    continue;
                }
                for (int j = 0; j < spaceSuffix + spaceAvg + ((i - bg) < spaceExtra ? 1 : 0); j++)
                {
                    ans.Append(' ');
                }
            }
            ans.Append(words[ed]);
            int len = ans.Length;
            for (int i = 0; i < maxWidth - len; i++)
            {
                ans.Append(' ');
            }
            return ans.ToString();
        }
        public int MySqrt(int x)
        {
            if (x == 0)
            {
                return 0;
            }
            double C = x, x0 = x;
            while (true)
            {
                double xi = 0.5 * (x0 + C / x0);
                if (Math.Abs(x0 - xi) < 1e-7)
                {
                    break;
                }
                x0 = xi;
            }
            return (int)x0;
        }
        public int ClimbStairs(int n)
        {
            int p = 0, q = 0, r = 1;
            for (int i = 1; i <= n; i++)
            {
                p = q;
                q = r;
                r = q + p;
            }
            return r;
        }
        public string SimplifyPath(string path)
        {
            string[] pathArray = path.Split("/");
            StringBuilder res = new StringBuilder();
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < pathArray.Length; i++)
            {
                if (pathArray[i].Length == 0 || pathArray[i].Equals("."))
                {
                    continue;
                }
                if (stack != null && stack.Count != 0)
                {
                    if (pathArray[i].Equals(".."))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(pathArray[i]);
                    }
                }
                else
                {
                    if (!pathArray[i].Equals(".."))
                    {
                        stack.Push(pathArray[i]);
                    }
                }
            }
            if (stack == null || stack.Count == 0)
            {
                return "/";
            }
            while (stack != null && stack.Count > 0)
            {
                res.Insert(0, stack.Pop());
                res.Insert(0, "/");
            }
            return res.ToString();
        }
        public int MinDistance(string word1, string word2)
        {
            int m = word1.Length;
            int n = word2.Length;
            if (m * n == 0)
            {
                return m + n;
            }
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
            {
                dp[i, 0] = i;
            }
            for (int j = 1; j <= n; j++)
            {
                dp[0, j] = j;
            }
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    dp[i, j] = 1 + Math.Min(Math.Min(dp[i - 1, j], dp[i, j - 1]), dp[i - 1, j - 1] - (word1[i - 1] == word2[j - 1] ? 1 : 0));
                }
            }
            return dp[m, n];
        }
        public void SetZeroes(int[][] matrix)
        {
            bool isCol = false;
            int row = matrix.Length;
            int col = matrix[0].Length;
            for (int i = 0; i < row; i++)
            {
                if (matrix[i][0] == 0)
                {
                    isCol = true;
                }
                for (int j = 1; j < col; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        matrix[i][0] = 0;
                        matrix[0][j] = 0;
                    }
                }
            }
            for (int i = 1; i < row; i++)
            {
                for (int j = 1; j < col; j++)
                {
                    if (matrix[i][0] == 0 || matrix[0][j] == 0)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }
            if (matrix[0][0] == 0)
            {
                for (int i = 0; i < col; i++)
                {
                    matrix[0][i] = 0;
                }
            }
            if (isCol)
            {
                for (int i = 0; i < row; i++)
                {
                    matrix[i][0] = 0;
                }
            }
        }
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int m = matrix.Length;
            if (m == 0)
            {
                return false;
            }
            int n = matrix[0].Length;
            int left = 0, right = m * n - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (target == matrix[mid / n][mid % n])
                {
                    return true;
                }
                else if (target < matrix[mid / n][mid % n])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return false;
        }
        public void SortColors(int[] nums)
        {
            int n = nums.Length;
            int p0 = 0, p2 = n - 1;
            for (int i = 0; i <= p2; i++)
            {
                while (i <= p2 && nums[i] == 2)
                {
                    int tmp = nums[i];
                    nums[i] = nums[p2];
                    nums[p2] = tmp;
                    p2--;
                }
                if (nums[i] == 0)
                {
                    int tmp = nums[i];
                    nums[i] = nums[p0];
                    nums[p0] = tmp;
                    p0++;
                }
            }
        }
        public string MinWindow(string s, string t)
        {
            int sLen = s.Length;
            int tLen = t.Length;
            if (sLen == 0 || tLen == 0 || sLen < tLen)
            {
                return "";
            }
            int[] tFreq = new int[128];
            foreach (var c in t)
            {
                tFreq[c]++;
            }
            int distance = tLen;
            int minLen = sLen + 1;
            int begin = 0;
            int left = 0;
            int right = 0;
            while (right < sLen)
            {
                char charRight = s[right];
                if (tFreq[charRight] > 0)
                {
                    distance--;
                }
                tFreq[charRight]--;
                right++;
                while (distance == 0)
                {
                    if (right - left < minLen)
                    {
                        minLen = right - left;
                        begin = left;
                    }
                    char charLeft = s[left];
                    if (tFreq[charLeft] == 0)
                    {
                        distance++;
                    }
                    tFreq[charLeft]++;
                    left++;
                }
            }
            if (minLen == sLen + 1)
            {
                return "";
            }
            return s.Substring(begin, minLen);
        }
        List<int> tmp77 = new List<int>();
        IList<IList<int>> ans77 = new List<IList<int>>();
        public IList<IList<int>> Combine(int n, int k)
        {
            DFS3(1, n, k);
            return ans77;
        }
        private void DFS3(int cur, int n, int k)
        {
            if (tmp77.Count + (n - cur + 1) < k)
            {
                return;
            }
            if (tmp77.Count == k)
            {
                ans77.Add(new List<int>(tmp77));
                return;
            }
            tmp77.Add(cur);
            DFS3(cur + 1, n, k);
            tmp77.RemoveAt(tmp77.Count - 1);
            DFS3(cur + 1, n, k);
        }
        public IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            IList<int> tmp = new List<int>();
            DFS4(res, tmp, 0, nums);
            return res;
        }
        private void DFS4(IList<IList<int>> res, IList<int> tmp, int cur, int[] nums)
        {
            if (cur == nums.Length)
            {
                res.Add(new List<int>(tmp));
                return;
            }
            tmp.Add(nums[cur]);
            DFS4(res, tmp, cur + 1, nums);
            tmp.RemoveAt(tmp.Count - 1);
            DFS4(res, tmp, cur + 1, nums);
        }
        public bool Exist(char[][] board, string word)
        {
            int h = board.Length, w = board[0].Length;
            bool[,] visited = new bool[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    bool flag = Check(board, visited, i, j, word, 0);
                    if (flag)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool Check(char[][] board, bool[,] visited, int i, int j, string word, int v)
        {
            if (board[i][j] != word[v])
            {
                return false;
            }
            else if (v == word.Length - 1)
            {
                return true;
            }
            visited[i, j] = true;
            int[,] directions = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
            bool result = false;
            for (int k = 0; k < directions.Length / 2; k++)
            {
                int newi = i + directions[k, 0], newj = j + directions[k, 1];
                if (newi >= 0 && newi < board.Length && newj >= 0 && newj < board[0].Length)
                {
                    if (!visited[newi, newj])
                    {
                        bool flag = Check(board, visited, newi, newj, word, v + 1);
                        if (flag)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            visited[i, j] = false;
            return result;
        }
        public int RemoveDuplicates1(int[] nums)
        {
            int j = 1, count = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1])
                {
                    count++;
                }
                else
                {
                    count = 1;
                }
                if (count <= 2)
                {
                    nums[j++] = nums[i];
                }
            }
            return j;
        }
        public bool Search1(int[] nums, int target)
        {
            if (nums == null)
            {
                return false;
            }
            int left = 0, right = nums.Length - 1;
            while (left <= right)
            {
                if (nums[left] == nums[right] && left != right)
                {
                    left++;
                    continue;
                }
                int mid = left + (right - left) / 2;
                if (nums[mid] == target)
                {
                    return true;
                }
                if (nums[left] <= nums[mid])
                {
                    if (nums[left] <= target && nums[mid] > target)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
                else
                {
                    if (nums[right] >= target && nums[mid] < target)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
            }
            return false;
        }
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            ListNode dummy = new ListNode(-1);
            dummy.next = head;
            ListNode slow = dummy;
            ListNode fast = head;
            while (fast != null)
            {
                if (fast.val != fast.next?.val)
                {
                    if (slow.next == fast)
                    {
                        slow = fast;
                    }
                    else
                    {
                        slow.next = fast.next;
                    }
                }
                fast = fast.next;
            }
            return dummy.next;
        }
        public ListNode DeleteDuplicates1(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            ListNode dummy = new ListNode(-1);
            dummy.next = head;
            ListNode slow = dummy;
            ListNode fast = head;
            while (fast != null)
            {
                if (fast.val != fast.next?.val)
                {
                    if (slow.next != fast)
                    {
                        slow.next = fast;
                    }
                    slow = fast;
                }
                fast = fast.next;
            }
            return dummy.next;
        }
        public int LargestRectangleArea(int[] heights)
        {
            int n = heights.Length;
            int[] left = new int[n];
            int[] right = new int[n];
            Array.Fill(right, n);
            Stack<int> monoStack = new Stack<int>();
            for (int i = 0; i < n; i++)
            {
                while (monoStack.Count != 0 && heights[monoStack.Peek()] >= heights[i])
                {
                    right[monoStack.Peek()] = i;
                    monoStack.Pop();
                }
                left[i] = monoStack.Count == 0 ? -1 : monoStack.Peek();
                monoStack.Push(i);
            }
            int ans = 0;
            for (int i = 0; i < n; i++)
            {
                ans = Math.Max(ans, (right[i] - left[i] - 1) * heights[i]);
            }
            return ans;
        }
        public int MaximalRectangle(char[][] matrix)
        {
            int m = matrix.Length;
            if (m == 0)
            {
                return 0;
            }
            int n = matrix[0].Length;
            int[,] left = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == '1')
                    {
                        left[i, j] = j == 0 ? 1 : left[i, j - 1] + 1;
                    }
                }
            }
            int ret = 0;
            for (int j = 0; j < n; j++)
            {
                int[] up = new int[m];
                int[] down = new int[m];
                Stack<int> stack = new Stack<int>();
                for (int i = 0; i < m; i++)
                {
                    while (stack.Count != 0 && left[stack.Peek(), j] >= left[i, j])
                    {
                        stack.Pop();
                    }
                    up[i] = stack.Count == 0 ? -1 : stack.Peek();
                    stack.Push(i);
                }
                stack.Clear();
                for (int i = m - 1; i >= 0; i--)
                {
                    while (stack.Count != 0 && left[stack.Peek(), j] >= left[i, j])
                    {
                        stack.Pop();
                    }
                    down[i] = stack.Count == 0 ? m : stack.Peek();
                    stack.Push(i);
                }

                for (int i = 0; i < m; i++)
                {
                    int height = down[i] - up[i] - 1;
                    int area = height * left[i, j];
                    ret = Math.Max(ret, area);
                }
            }
            return ret;
        }
        public ListNode Partition(ListNode head, int x)
        {
            ListNode small = new ListNode(0);
            ListNode smallHead = small;
            ListNode large = new ListNode(0);
            ListNode largeHead = large;
            while (head != null)
            {
                if (head.val < x)
                {
                    small.next = head;
                    small = small.next;
                }
                else
                {
                    large.next = head;
                    large = large.next;
                }
                head = head.next;
            }
            large.next = null;
            small.next = largeHead.next;
            return smallHead.next;
        }
        public bool IsScramble(string s1, string s2)
        {
            if (s1.Length != s2.Length)
            {
                return false;
            }
            if (s1.Equals(s2))
            {
                return true;
            }
            int[] letters = new int[26];
            for (int i = 0; i < s1.Length; i++)
            {
                letters[s1[i] - 'a']++;
                letters[s2[i] - 'a']--;
            }
            for (int i = 0; i < 26; i++)
            {
                if (letters[i] != 0)
                {
                    return false;
                }
            }
            for (int i = 1; i < s1.Length; i++)
            {
                //对应情况 1 ，判断 S1 的子树能否变为 S2 相应部分
                if (IsScramble(s1.Substring(0, i), s2.Substring(0, i)) && IsScramble(s1.Substring(i), s2.Substring(i)))
                {
                    return true;
                }
                //对应情况 2 ，S1 两个子树先进行了交换，然后判断 S1 的子树能否变为 S2 相应部分
                if (IsScramble(s1.Substring(i), s2.Substring(0, s2.Length - i)) &&
                   IsScramble(s1.Substring(0, i), s2.Substring(s2.Length - i)))
                {
                    return true;
                }
            }
            return false;
        }
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int p1 = m - 1;
            int p2 = n - 1;
            int p = m + n - 1;
            while (p1 >= 0 && p2 >= 0)
            {
                nums1[p--] = nums1[p1] < nums2[p2] ? nums2[p2--] : nums1[p1--];
            }
            Array.Copy(nums2, 0, nums1, 0, p2 + 1);
        }
        public IList<int> GrayCode(int n)
        {
            IList<int> res = new List<int>();
            res.Add(0);
            int head = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = res.Count - 1; j >= 0; j--)
                {
                    res.Add(head + res[j]);
                }
                head <<= 1;
            }
            return res;
        }
        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            Array.Sort(nums);
            IList<IList<int>> lists = new List<IList<int>>();
            int subsetNum = 1 << nums.Length;
            for (int i = 0; i < subsetNum; i++)
            {
                IList<int> list = new List<int>();
                bool illegal = false;
                for (int j = 0; j < nums.Length; j++)
                {
                    //当前位是 1
                    if ((i >> j & 1) == 1)
                    {
                        //当前是重复数字，并且前一位是 0，跳过这种情况
                        if (j > 0 && nums[j] == nums[j - 1] && (i >> (j - 1) & 1) == 0)
                        {
                            illegal = true;
                            break;
                        }
                        else
                        {
                            list.Add(nums[j]);
                        }
                    }
                }
                if (!illegal)
                {
                    lists.Add(list);
                }

            }
            return lists;
        }
        public int NumDecodings(string s)
        {
            if (s[0] == '0')
            {
                return 0;
            }
            int pre = 1, curr = 1;
            for (int i = 1; i < s.Length; i++)
            {
                int tmp = curr;
                if (s[i] == '0')
                {
                    if (s[i - 1] == '1' || s[i - 1] == '2')
                    {
                        curr = pre;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (s[i - 1] == '1' || (s[i - 1] == '2' && s[i] >= '1' && s[i] <= '6'))
                {
                    curr += pre;
                }
                pre = tmp;
            }
            return curr;
        }
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            if (head == null)
            {
                return null;
            }
            ListNode cur = head, prev = null;
            while (m > 1)
            {
                prev = cur;
                cur = cur.next;
                m--;
                n--;
            }
            ListNode con = prev, tail = cur;
            ListNode third = null;
            while (n > 0)
            {
                third = cur.next;
                cur.next = prev;
                prev = cur;
                cur = third;
                n--;
            }
            if (con != null)
            {
                con.next = prev;
            }
            else
            {
                head = prev;
            }
            tail.next = cur;
            return head;
        }
        public IList<string> RestoreIpAddresses(string s)
        {
            IList<string> ans = new List<string>();
            int[] segments = new int[4];
            DFS5(ans, s, 0, 0, segments);
            return ans;
        }
        private void DFS5(IList<string> ans, string s, int segId, int segStart, int[] segments)
        {
            if (segId == 4)
            {
                if (segStart == s.Length)
                {
                    StringBuilder ip = new StringBuilder();
                    for (int i = 0; i < 4; i++)
                    {
                        ip.Append(segments[i]);
                        if (i != 3)
                        {
                            ip.Append('.');
                        }
                    }
                    ans.Add(ip.ToString());
                }
                return;
            }
            if (segStart == s.Length)
            {
                return;
            }
            if (s[segStart] == '0')
            {
                segments[segId] = 0;
                DFS5(ans, s, segId + 1, segStart + 1, segments);
            }
            int addr = 0;
            for (int segEnd = segStart; segEnd < s.Length; segEnd++)
            {
                addr = addr * 10 + (s[segEnd] - '0');
                if (addr > 0 && addr <= 0xFF)
                {
                    segments[segId] = addr;
                    DFS5(ans, s, segId + 1, segEnd + 1, segments);
                }
                else
                {
                    break;
                }
            }
        }
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
        public IList<int> InorderTraversal(TreeNode root)
        {
            IList<int> ans = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            while (root != null || stack.Count != 0)
            {
                while (root != null)
                {
                    stack.Push(root);
                    root = root.left;
                }
                root = stack.Pop();
                ans.Add(root.val);
                root = root.right;
            }
            return ans;
        }
        public IList<TreeNode> GenerateTrees(int n)
        {
            if (n == 0)
            {
                return new List<TreeNode>();
            }
            return GenerateTrees(1, n);
        }
        private List<TreeNode> GenerateTrees(int start, int end)
        {
            List<TreeNode> allTrees = new List<TreeNode>();
            if (start > end)
            {
                allTrees.Add(null);
                return allTrees;
            }
            for (int i = start; i <= end; i++)
            {
                List<TreeNode> leftTrees = GenerateTrees(start, i - 1);
                List<TreeNode> rightTrees = GenerateTrees(i + 1, end);
                foreach (var left in leftTrees)
                {
                    foreach (var right in rightTrees)
                    {
                        TreeNode curTree = new TreeNode(i);
                        curTree.left = left;
                        curTree.right = right;
                        allTrees.Add(curTree);
                    }
                }
            }
            return allTrees;
        }
        public int NumTrees(int n)
        {
            long ans = 1;
            for (int i = 0; i < n; i++)
            {
                ans = ans * 2 * (2 * i + 1) / (i + 2);
            }
            return (int)ans;
        }
        public bool IsInterleave(string s1, string s2, string s3)
        {
            int n = s1.Length, m = s2.Length, t = s3.Length;
            if (n + m != t)
            {
                return false;
            }
            bool[] f = new bool[m + 1];
            f[0] = true;
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    int p = i + j - 1;
                    if (i > 0)
                    {
                        f[j] = f[j] && s1[i - 1] == s3[p];
                    }
                    if (j > 0)
                    {
                        f[j] = f[j] || (f[j - 1] && s2[j - 1] == s3[p]);
                    }
                }
            }
            return f[m];
        }
        public bool IsValidBST(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            double min = -double.MaxValue;
            while (stack.Count != 0 || root != null)
            {
                while (root != null)
                {
                    stack.Push(root);
                    root = root.left;
                }
                root = stack.Pop();
                if (root.val <= min)
                {
                    return false;
                }
                min = root.val;
                root = root.right;
            }
            return true;
        }
        public void RecoverTree(TreeNode root)
        {
            TreeNode x = null, y = null, pred = null, predecessor = null;
            while (root != null)
            {
                if (root.left != null)
                {
                    predecessor = root.left;
                    while (predecessor.right != null && predecessor.right != root)
                    {
                        predecessor = predecessor.right;
                    }
                    if (predecessor.right == null)
                    {
                        predecessor.right = root;
                        root = root.left;
                    }
                    else
                    {
                        if (pred != null && root.val < pred.val)
                        {
                            y = root;
                            if (x == null)
                            {
                                x = pred;
                            }
                        }
                        pred = root;
                        predecessor.right = null;
                        root = root.right;
                    }
                }
                else
                {
                    if (pred != null && root.val < pred.val)
                    {
                        y = root;
                        if (x == null)
                        {
                            x = pred;
                        }
                    }
                    pred = root;
                    root = root.right;
                }
            }
            Swap(x, y);
        }
        private void Swap(TreeNode x, TreeNode y)
        {
            int tmp = x.val;
            x.val = y.val;
            y.val = tmp;
        }
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
            {
                return true;
            }
            else if (p == null || q == null)
            {
                return false;
            }
            else if (p.val != q.val)
            {
                return false;
            }
            else
            {
                return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
            }
        }
        public bool IsSymmetric(TreeNode root)
        {
            return Check(root, root);
        }

        private bool Check(TreeNode left, TreeNode right)
        {
            if (left == null && right == null)
            {
                return true;
            }
            if (left == null || right == null)
            {
                return false;
            }
            return left.val == right.val && Check(left.left, right.right) && Check(left.right, right.left);
        }
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null)
            {
                return res;
            }
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                List<int> level = new List<int>();
                int currentLevelSize = queue.Count;
                for (int i = 1; i <= currentLevelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    level.Add(node.val);
                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }
                res.Add(level);
            }
            return res;
        }
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null)
            {
                return res;
            }
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            bool isOrderLeft = true;
            while (queue.Count != 0)
            {
                List<int> level = new List<int>();
                int currentLevelSize = queue.Count;
                for (int i = 1; i <= currentLevelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    if (isOrderLeft)
                    {
                        level.Add(node.val);
                    }
                    else
                    {
                        level.Insert(0, node.val);
                    }
                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }
                res.Add(level);
                isOrderLeft = !isOrderLeft;
            }
            return res;
        }
        public int MaxDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                int left = MaxDepth(root.left);
                int right = MaxDepth(root.right);
                return Math.Max(left, right) + 1;
            }
        }
        private Hashtable indexHashtable = new System.Collections.Hashtable();
        public TreeNode BuildTree1(int[] preorder, int[] inorder)
        {
            int n = preorder.Length;
            for (int i = 0; i < n; i++)
            {
                indexHashtable.Add(inorder[i], i);
            }
            return MyBuildTree(preorder, inorder, 0, n - 1, 0, n - 1);
        }

        private TreeNode MyBuildTree(int[] preorder, int[] inorder, int preorderLeft, int preorderRight, int inorderLeft, int inorderRight)
        {
            if (preorderLeft > preorderRight)
            {
                return null;
            }
            // 前序遍历中的第一个节点就是根节点
            int preorder_root = preorderLeft;
            // 在中序遍历中定位根节点
            int inorder_root = (int)indexHashtable[preorder[preorder_root]];

            // 先把根节点建立出来
            TreeNode root = new TreeNode(preorder[preorder_root]);
            // 得到左子树中的节点数目
            int size_left_subtree = inorder_root - inorderLeft;
            // 递归地构造左子树，并连接到根节点
            // 先序遍历中「从 左边界+1 开始的 size_left_subtree」个元素就对应了中序遍历中「从 左边界 开始到 根节点定位-1」的元素
            root.left = MyBuildTree(preorder, inorder, preorderLeft + 1, preorderLeft + size_left_subtree, inorderLeft, inorder_root - 1);
            // 递归地构造右子树，并连接到根节点
            // 先序遍历中「从 左边界+1+左子树节点数目 开始到 右边界」的元素就对应了中序遍历中「从 根节点定位+1 到 右边界」的元素
            root.right = MyBuildTree(preorder, inorder, preorderLeft + size_left_subtree + 1, preorderRight, inorder_root + 1, inorderRight);
            return root;
        }

        public TreeNode BuildTree(int[] inorder, int[] postorder)
        {
            if (postorder == null || postorder.Length == 0)
            {
                return null;
            }
            TreeNode root = new TreeNode(postorder[postorder.Length - 1]);
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            int inorderIndex = inorder.Length - 1;
            for (int i = postorder.Length - 2; i >= 0; i--)
            {
                int postorderVal = postorder[i];
                TreeNode node = stack.Peek();
                if (node.val != inorder[inorderIndex])
                {
                    node.right = new TreeNode(postorderVal);
                    stack.Push(node.right);
                }
                else
                {
                    while (stack.Count != 0 && stack.Peek().val == inorder[inorderIndex])
                    {
                        node = stack.Pop();
                        inorderIndex--;
                    }
                    node.left = new TreeNode(postorderVal);
                    stack.Push(node.left);
                }
            }
            return root;
        }
        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null)
            {
                return res;
            }
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            bool isOrderLeft = true;
            while (queue.Count != 0)
            {
                List<int> level = new List<int>();
                int currentLevelSize = queue.Count;
                for (int i = 1; i <= currentLevelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    level.Add(node.val);
                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }
                res.Insert(0, level);
                isOrderLeft = !isOrderLeft;
            }
            return res;
        }
        public TreeNode SortedArrayToBST(int[] nums)
        {
            return Helper(nums, 0, nums.Length - 1);
        }
        private TreeNode Helper(int[] nums, int left, int right)
        {
            if (left > right)
            {
                return null;
            }
            int mid = (left + right + 1) / 2;
            TreeNode root = new TreeNode(nums[mid]);
            root.left = Helper(nums, left, mid - 1);
            root.right = Helper(nums, mid + 1, right);
            return root;
        }
        private ListNode globalHead;
        public TreeNode SortedListToBST(ListNode head)
        {
            globalHead = head;
            int len = GetLengthOfListNode(head);
            return Helper(0, len - 1);
        }

        private TreeNode Helper(int left, int right)
        {
            if (left > right)
            {
                return null;
            }
            int mid = (left + right + 1) / 2;
            TreeNode root = new TreeNode();
            root.left = Helper(left, mid - 1);
            root.val = globalHead.val;
            globalHead = globalHead.next;
            root.right = Helper(mid + 1, right);
            return root;
        }

        private int GetLengthOfListNode(ListNode head)
        {
            int len = 0;
            while (head != null)
            {
                len++;
                head = head.next;
            }
            return len;
        }
        public bool IsBalanced(TreeNode root)
        {
            return Height(root) >= 0;
        }
        private int Height(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            int leftHeight = Height(root.left);
            int rightHeight = Height(root.right);
            if (leftHeight == -1 || rightHeight == -1 || Math.Abs(leftHeight - rightHeight) > 1)
            {
                return -1;
            }
            else
            {
                return Math.Max(leftHeight, rightHeight) + 1;
            }
        }
        public int MinDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            if (root.left == null && root.right == null)
            {
                return 1;
            }
            int minHeight = int.MaxValue;
            if (root.left != null)
            {
                minHeight = Math.Min(MinDepth(root.left), minHeight);
            }
            if (root.right != null)
            {
                minHeight = Math.Min(MinDepth(root.right), minHeight);
            }
            return minHeight + 1;
        }
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
            {
                return false;
            }
            if (root.left == null && root.right == null)
            {
                return targetSum == root.val;
            }
            return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
        }
        private IList<IList<int>> res = new List<IList<int>>();
        List<int> path = new List<int>();
        public IList<IList<int>> PathSum(TreeNode root, int targetSum)
        {
            DFS(root, targetSum);
            return res;
        }
        private void DFS(TreeNode root, int sum)
        {
            if (root == null)
            {
                return;
            }
            path.Add(root.val);
            sum -= root.val;
            if (root.left == null && root.right == null && sum == 0)
            {
                res.Add(new List<int>(path));
            }
            DFS(root.left, sum);
            DFS(root.right, sum);
            path.RemoveAt(path.Count - 1);
        }
        public void Flatten(TreeNode root)
        {
            TreeNode curr = root;
            while (curr != null)
            {
                if (curr.left != null)
                {
                    TreeNode next = curr.left;
                    TreeNode predecessor = next;
                    while (predecessor.right != null)
                    {
                        predecessor = predecessor.right;
                    }
                    predecessor.right = curr.right;
                    curr.left = null;
                    curr.right = next;
                }
                curr = curr.right;
            }
        }
        public int NumDistinct(string s, string t)
        {
            int sLen = s.Length;
            int tLen = t.Length;
            int[,] dp = new int[tLen + 1, sLen + 1];
            for (int i = 0; i < sLen + 1; i++)
            {
                dp[0, i] = 1;
            }
            for (int i = 1; i < tLen + 1; i++)
            {
                for (int j = 1; j < sLen + 1; j++)
                {
                    if (t[i - 1] == s[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + dp[i, j - 1];
                    }
                    else
                    {
                        dp[i, j] = dp[i, j - 1];
                    }
                }
            }
            return dp[tLen, sLen];
        }
        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node next;
            public IList<Node> neighbors;
            public Node random;
            public Node()
            {
                val = 0;
                neighbors = new List<Node>();
            }
            public Node(int _val)
            {
                val = _val;
                next = null;
                random = null;
                neighbors = new List<Node>();
            }
            public Node(int _val, List<Node> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }

            public Node(int _val, Node _left, Node _right, Node _next)
            {
                val = _val;
                left = _left;
                right = _right;
                next = _next;
            }
        }
        public Node Connect(Node root)
        {
            if (root == null)
            {
                return root;
            }
            Node leftmost = root;
            while (leftmost.left != null)
            {
                Node head = leftmost;
                while (head != null)
                {
                    head.left.next = head.right;
                    if (head.next != null)
                    {
                        head.right.next = head.next.left;
                    }
                    head = head.next;
                }
                leftmost = leftmost.left;
            }
            return root;
        }
        Node last = null;
        Node nextStart = null;
        public Node Connect1(Node root)
        {
            if (root == null)
            {
                return root;
            }
            Node start = root;
            while (start != null)
            {
                last = null;
                nextStart = null;
                for (Node p = start; p != null; p = p.next)
                {
                    if (p.left != null)
                    {
                        Handle(p.left);
                    }
                    if (p.right != null)
                    {
                        Handle(p.right);
                    }
                }
                start = nextStart;
            }
            return root;
        }
        private void Handle(Node p)
        {
            if (last != null)
            {
                last.next = p;
            }
            if (nextStart == null)
            {
                nextStart = p;
            }
            last = p;
        }
        public IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> res = new List<IList<int>>();
            for (int i = 0; i < numRows; i++)
            {
                IList<int> row = new List<int>();
                for (int j = 0; j <= i; j++)
                {
                    if (j == 0 || j == i)
                    {
                        row.Add(1);
                    }
                    else
                    {
                        row.Add(res[i - 1][j] + res[i - 1][j - 1]);
                    }
                }
                res.Add(row);
            }
            return res;
        }
        public IList<int> GetRow(int rowIndex)
        {
            IList<int> res = new List<int>();
            res.Add(1);
            for (int i = 1; i <= rowIndex; i++)
            {
                res.Add((int)((long)res[i - 1] * (rowIndex - i + 1) / i));
            }
            return res;
        }
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            int n = triangle.Count;
            int[] f = new int[n];
            f[0] = triangle[0][0];
            for (int i = 1; i < n; i++)
            {
                f[i] = f[i - 1] + triangle[i][i];
                for (int j = i - 1; j > 0; j--)
                {
                    f[j] = Math.Min(f[j - 1], f[j]) + triangle[i][j];
                }
                f[0] += triangle[i][0];
            }
            int minTotal = f[0];
            for (int i = 1; i < n; i++)
            {
                minTotal = Math.Min(minTotal, f[i]);
            }
            return minTotal;
        }
        public int MaxProfit(int[] prices)
        {
            int minPrice = int.MaxValue;
            int maxProfit = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minPrice)
                {
                    minPrice = prices[i];
                }
                else if (prices[i] - minPrice > maxProfit)
                {
                    maxProfit = prices[i] - minPrice;
                }
            }
            return maxProfit;
        }
        public int MaxProfit1(int[] prices)
        {
            int ans = 0;
            int len = prices.Length;
            for (int i = 1; i < len; i++)
            {
                ans += Math.Max(prices[i] - prices[i - 1], 0);
            }
            return ans;
        }
        public int MaxProfit2(int[] prices)
        {
            int n = prices.Length;
            int buy1 = -prices[0], sell1 = 0;
            int buy2 = -prices[0], sell2 = 0;
            for (int i = 1; i < n; ++i)
            {
                buy1 = Math.Max(buy1, -prices[i]);
                sell1 = Math.Max(sell1, buy1 + prices[i]);
                buy2 = Math.Max(buy2, sell1 - prices[i]);
                sell2 = Math.Max(sell2, buy2 + prices[i]);
            }
            return sell2;
        }
        int maxSum = int.MinValue;
        public int MaxPathSum(TreeNode root)
        {
            MaxGain(root);
            return maxSum;
        }

        private int MaxGain(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            int leftGain = Math.Max(MaxGain(root.left), 0);
            int rightGain = Math.Max(MaxGain(root.right), 0);
            int price = root.val + leftGain + rightGain;
            maxSum = Math.Max(price, maxSum);
            return root.val + Math.Max(leftGain, rightGain);
        }
        public bool IsPalindrome(string s)
        {
            int left = 0, right = s.Length - 1;
            while (left < right)
            {
                while (left < right && !char.IsLetterOrDigit(s[left]))
                {
                    left++;
                }
                while (left < right && !char.IsLetterOrDigit(s[right]))
                {
                    right--;
                }
                if (left < right)
                {
                    if (char.ToLower(s[left]) != char.ToLower(s[right]))
                    {
                        return false;
                    }
                    left++;
                    right--;
                }
            }
            return true;
        }
        private const int INF = 1 << 20;
        private Dictionary<string, int> wordId = new Dictionary<string, int>();
        private List<string> idWord = new List<string>();
        private List<int>[] edges;
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            int id = 0;
            foreach (var word in wordList)
            {
                if (!wordId.ContainsKey(word))
                {
                    wordId.Add(word, id++);
                    idWord.Add(word);
                }
            }
            if (!wordId.ContainsKey(endWord))
            {
                return new List<IList<string>>();
            }
            if (!wordId.ContainsKey(beginWord))
            {
                wordId.Add(beginWord, id++);
                idWord.Add(beginWord);
            }
            edges = new List<int>[idWord.Count];
            for (int i = 0; i < idWord.Count; i++)
            {
                edges[i] = new List<int>();
            }
            for (int i = 0; i < idWord.Count; i++)
            {
                for (int j = i + 1; j < idWord.Count; j++)
                {
                    if (TransfromCheck(idWord[i], idWord[j]))
                    {
                        edges[i].Add(j);
                        edges[j].Add(i);
                    }
                }
            }
            int dest = wordId[endWord];
            List<IList<string>> res = new List<IList<string>>();
            int[] cost = new int[id];
            for (int i = 0; i < id; i++)
            {
                cost[i] = INF;
            }
            Queue<List<int>> q = new Queue<List<int>>();
            List<int> tmpBegin = new List<int>();
            tmpBegin.Add(wordId[beginWord]);
            q.Enqueue(tmpBegin);
            cost[wordId[beginWord]] = 0;
            while (q.Count != 0)
            {
                List<int> now = q.Dequeue();
                int last = now[now.Count - 1];
                if (last == dest)
                {
                    List<string> tmp = new List<string>();
                    foreach (var index in now)
                    {
                        tmp.Add(idWord[index]);
                    }
                    res.Add(tmp);
                }
                else
                {
                    for (int i = 0; i < edges[last].Count; i++)
                    {
                        int to = edges[last][i];
                        if (cost[last] + 1 <= cost[to])
                        {
                            cost[to] = cost[last] + 1;
                            List<int> tmp = new List<int>(now);
                            tmp.Add(to);
                            q.Enqueue(tmp);
                        }
                    }
                }
            }
            return res;
        }

        private bool TransfromCheck(string v1, string v2)
        {
            int diff = 0;
            for (int i = 0; i < v1.Length && diff < 2; i++)
            {
                if (v1[i] != v2[i])
                {
                    diff++;
                }
            }
            return diff == 1;
        }
        private Dictionary<string, int> wordId1 = new Dictionary<string, int>();
        private List<List<int>> edge = new List<List<int>>();
        private int nodeNum = 0;
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            foreach (var word in wordList)
            {
                AddEdge(word);
            }
            AddEdge(beginWord);
            if (!wordId1.ContainsKey(endWord))
            {
                return 0;
            }
            int[] disBegin = new int[nodeNum];
            Array.Fill(disBegin, int.MaxValue);
            int beginId = wordId1[beginWord];
            disBegin[beginId] = 0;
            Queue<int> queBegin = new Queue<int>();
            queBegin.Enqueue(beginId);
            int[] disEnd = new int[nodeNum];
            Array.Fill(disEnd, int.MaxValue);
            int endId = wordId1[endWord];
            disEnd[endId] = 0;
            Queue<int> queEnd = new Queue<int>();
            queEnd.Enqueue(endId);
            while (queBegin.Count != 0 && queEnd.Count != 0)
            {
                int queBeginSize = queBegin.Count;
                for (int i = 0; i < queBeginSize; ++i)
                {
                    int nodeBegin = queBegin.Dequeue();
                    if (disEnd[nodeBegin] != int.MaxValue)
                    {
                        return (disBegin[nodeBegin] + disEnd[nodeBegin]) / 2 + 1;
                    }
                    foreach (int it in edge[nodeBegin])
                    {
                        if (disBegin[it] == int.MaxValue)
                        {
                            disBegin[it] = disBegin[nodeBegin] + 1;
                            queBegin.Enqueue(it);
                        }
                    }
                }

                int queEndSize = queEnd.Count;
                for (int i = 0; i < queEndSize; ++i)
                {
                    int nodeEnd = queEnd.Dequeue();
                    if (disBegin[nodeEnd] != int.MaxValue)
                    {
                        return (disBegin[nodeEnd] + disEnd[nodeEnd]) / 2 + 1;
                    }
                    foreach (int it in edge[nodeEnd])
                    {
                        if (disEnd[it] == int.MaxValue)
                        {
                            disEnd[it] = disEnd[nodeEnd] + 1;
                            queEnd.Enqueue(it);
                        }
                    }
                }
            }
            return 0;
        }

        private void AddEdge(string word)
        {
            AddWord(word);
            int id1 = wordId1[word];
            char[] array = word.ToCharArray();
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                char tmp = array[i];
                array[i] = '*';
                string newWord = new string(array);
                AddWord(newWord);
                int id2 = wordId1[newWord];
                edge[id1].Add(id2);
                edge[id2].Add(id1);
                array[i] = tmp;
            }
        }

        private void AddWord(string word)
        {
            if (!wordId1.ContainsKey(word))
            {
                wordId1.Add(word, nodeNum++);
                edge.Add(new List<int>());
            }
        }
        public int LongestConsecutive(int[] nums)
        {
            HashSet<int> numSet = new HashSet<int>();
            foreach (var num in nums)
            {
                numSet.Add(num);
            }
            int longestStreak = 0;
            foreach (var num in numSet)
            {
                if (!numSet.Contains(num - 1))
                {
                    int currentNum = num;
                    int currentStreak = 1;
                    while (numSet.Contains(currentNum + 1))
                    {
                        currentNum += 1;
                        currentStreak += 1;
                    }
                    longestStreak = Math.Max(longestStreak, currentStreak);
                }
            }
            return longestStreak;
        }
        public int SumNumbers(TreeNode root)
        {
            return GetSum(root, 0);
        }
        private int GetSum(TreeNode root, int preSum)
        {
            if (root == null)
            {
                return 0;
            }
            int sum = preSum * 10 + root.val;
            if (root.left == null && root.right == null)
            {
                return sum;
            }
            else
            {
                return GetSum(root.left, sum) + GetSum(root.right, sum);
            }
        }
        int row130, height130;
        public void Solve(char[][] board)
        {
            row130 = board.Length;
            if (row130 == 0)
            {
                return;
            }
            height130 = board[0].Length;
            for (int i = 0; i < row130; i++)
            {
                DFS(board, i, 0);
                DFS(board, i, height130 - 1);
            }
            for (int i = 1; i < height130 - 1; i++)
            {
                DFS(board, 0, i);
                DFS(board, row130 - 1, i);
            }
            for (int i = 0; i < row130; i++)
            {
                for (int j = 0; j < height130; j++)
                {
                    if (board[i][j] == 'A')
                    {
                        board[i][j] = 'O';
                    }
                    else if (board[i][j] == 'O')
                    {
                        board[i][j] = 'X';
                    }
                }
            }
        }

        private void DFS(char[][] board, int i, int v)
        {
            if (i < 0 || i >= row130 || v < 0 || v >= height130 || board[i][v] != 'O')
            {
                return;
            }
            board[i][v] = 'A';
            DFS(board, i + 1, v);
            DFS(board, i - 1, v);
            DFS(board, i, v + 1);
            DFS(board, i, v - 1);
        }
        public IList<IList<string>> Partition(string s)
        {
            int len = s.Length;
            List<IList<string>> res = new List<IList<string>>();
            if (len == 0)
            {
                return res;
            }
            bool[,] dp = new bool[len, len];
            for (int right = 0; right < len; right++)
            {
                for (int left = 0; left <= right; left++)
                {
                    if (s[left] == s[right] && (right - left <= 2 || dp[left + 1, right - 1]))
                    {
                        dp[left, right] = true;
                    }
                }
            }
            List<string> stack = new List<string>();
            BackTracking(s, 0, len, dp, stack, res);
            return res;
        }

        private void BackTracking(string s, int start, int len, bool[,] dp, List<string> path, List<IList<string>> res)
        {
            if (start == len)
            {
                res.Add(new List<string>(path));
                return;
            }
            for (int i = start; i < len; i++)
            {
                if (!dp[start, i])
                {
                    continue;
                }
                path.Add(s.Substring(start, i + 1 - start));
                BackTracking(s, i + 1, len, dp, path, res);
                path.RemoveAt(path.Count - 1);
            }
        }
        public int MinCut(string s)
        {
            int len = s.Length;
            if (len < 2)
            {
                return 0;
            }
            int[] dp = new int[len];
            for (int i = 0; i < len; i++)
            {
                dp[i] = i;
            }
            bool[,] checkPalindrome = new bool[len, len];
            for (int right = 0; right < len; right++)
            {
                for (int left = 0; left <= right; left++)
                {
                    if (s[left] == s[right] && (right - left <= 2 || checkPalindrome[left + 1, right - 1]))
                    {
                        checkPalindrome[left, right] = true;
                    }
                }
            }
            for (int i = 1; i < len; i++)
            {
                if (checkPalindrome[0, i])
                {
                    dp[i] = 0;
                    continue;
                }
                for (int j = 0; j < i; j++)
                {
                    if (checkPalindrome[j + 1, i])
                    {
                        dp[i] = Math.Min(dp[i], dp[j] + 1);
                    }
                }
            }
            return dp[len - 1];
        }
        private Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
        public Node CloneGraph(Node node)
        {
            if (node == null)
            {
                return node;
            }
            if (visited.ContainsKey(node))
            {
                return visited[node];
            }
            Node cloneNode = new Node(node.val, new List<Node>());
            visited.Add(node, cloneNode);
            foreach (var neighbor in node.neighbors)
            {
                cloneNode.neighbors.Add(CloneGraph(neighbor));
            }
            return cloneNode;
        }
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int len = gas.Length;
            int spare = 0;
            int minSpare = int.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < len; i++)
            {
                spare += gas[i] - cost[i];
                if (spare < minSpare)
                {
                    minSpare = spare;
                    minIndex = i;
                }
            }
            return spare < 0 ? -1 : (minIndex + 1) % len;
        }
        public int Candy(int[] ratings)
        {
            int n = ratings.Length;
            int ret = 1;
            int inc = 1, dec = 0, pre = 1;
            for (int i = 1; i < n; i++)
            {
                if (ratings[i] >= ratings[i - 1])
                {
                    dec = 0;
                    pre = ratings[i] == ratings[i - 1] ? 1 : pre + 1;
                    ret += pre;
                    inc = pre;
                }
                else
                {
                    dec++;
                    if (dec == inc)
                    {
                        dec++;
                    }
                    ret += dec;
                    pre = 1;
                }
            }
            return ret;
        }
        public int SingleNumber(int[] nums)
        {
            int res = 0;
            foreach (var num in nums)
            {
                res ^= num;
            }
            return res;
        }
        public int SingleNumber1(int[] nums)
        {
            int seenOnce = 0, seenTwice = 0;
            foreach (var num in nums)
            {
                seenOnce = ~seenTwice & (seenOnce ^ num);
                seenTwice = ~seenOnce & (seenTwice ^ num);
            }
            return seenOnce;
        }
        public Node CopyRandomList(Node head)
        {
            if (head == null)
            {
                return null;
            }
            Node ptr = head;
            while (ptr != null)
            {
                Node newNode = new Node(ptr.val);
                newNode.next = ptr.next;
                ptr.next = newNode;
                ptr = newNode.next;
            }
            ptr = head;
            while (ptr != null)
            {
                ptr.next.random = ptr.random != null ? ptr.random.next : null;
                ptr = ptr.next.next;
            }
            Node ptrOldList = head;
            Node ptrNewList = head.next;
            Node headOld = head.next;
            while (ptrOldList != null)
            {
                ptrOldList.next = ptrOldList.next.next;
                ptrNewList.next = ptrNewList.next != null ? ptrNewList.next.next : null;
                ptrOldList = ptrOldList.next;
                ptrNewList = ptrNewList.next;
            }
            return headOld;
        }
        public bool WordBreak(string s, IList<string> wordDict)
        {
            var wordDictSet = new HashSet<string>(wordDict);
            var dp = new bool[s.Length + 1];
            dp[0] = true;
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (dp[j] && wordDictSet.Contains(s.Substring(j, i - j)))
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }
            return dp[s.Length];
        }
        public IList<string> WordBreak1(string s, IList<string> wordDict)
        {
            Dictionary<int, List<List<string>>> map = new Dictionary<int, List<List<string>>>();
            List<List<string>> wordBreaks = BackTrack(s, s.Length, new HashSet<string>(wordDict), 0, map);
            List<string> breakList = new List<string>();
            foreach (var wordBreak in wordBreaks)
            {
                breakList.Add(string.Join(" ", wordBreak));
            }
            return breakList;
        }

        private List<List<string>> BackTrack(string s, int length, HashSet<string> wordSet, int index, Dictionary<int, List<List<string>>> map)
        {
            if (!map.ContainsKey(index))
            {
                List<List<string>> wordBreaks = new List<List<string>>();
                if (index == length)
                {
                    wordBreaks.Add(new List<string>());
                }
                for (int i = index + 1; i <= length; i++)
                {
                    string word = s.Substring(index, i - index);
                    if (wordSet.Contains(word))
                    {
                        List<List<string>> nextWordBreaks = BackTrack(s, length, wordSet, i, map);
                        foreach (var nextWordBreak in nextWordBreaks)
                        {
                            List<string> wordBreak = new List<string>(nextWordBreak);
                            wordBreak.Insert(0, word);
                            wordBreaks.Add(wordBreak);
                        }
                    }
                }
                map.Add(index, wordBreaks);
            }
            return map[index];
        }
        public bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return false;
            }
            ListNode slow = head;
            ListNode fast = head.next;
            while (slow != fast)
            {
                if (fast == null || fast.next == null)
                {
                    return false;
                }
                slow = slow.next;
                fast = fast.next.next;
            }
            return true;
        }
        public ListNode DetectCycle(ListNode head)
        {
            if (head == null)
            {
                return null;
            }
            ListNode slow = head, fast = head;
            while (fast != null)
            {
                slow = slow.next;
                if (fast.next != null)
                {
                    fast = fast.next.next;
                }
                else
                {
                    return null;
                }
                if (fast == slow)
                {
                    ListNode ptr = head;
                    while (ptr != slow)
                    {
                        ptr = ptr.next;
                        slow = slow.next;
                    }
                    return ptr;
                }
            }
            return null;
        }
        public void ReorderList(ListNode head)
        {
            if (head == null)
            {
                return;
            }
            ListNode mid = MiddleNode(head);
            ListNode l1 = head;
            ListNode l2 = mid.next;
            mid.next = null;
            l2 = ReverseLIst(l2);
            MergeList(l1, l2);
        }
        private ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head;
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            return slow;
        }
        private ListNode ReverseLIst(ListNode head)
        {
            ListNode prev = null;
            ListNode curr = head;
            while (curr != null)
            {
                ListNode nextTemp = curr.next;
                curr.next = prev;
                prev = curr;
                curr = nextTemp;
            }
            return prev;
        }
        private void MergeList(ListNode l1, ListNode l2)
        {
            ListNode l1Tmp;
            ListNode l2Tmp;
            while (l1 != null && l2 != null)
            {
                l1Tmp = l1.next;
                l2Tmp = l2.next;
                l1.next = l2;
                l1 = l1Tmp;
                l2.next = l1;
                l2 = l2Tmp;
            }
        }
        public IList<int> PreorderTraversal(TreeNode root)
        {
            IList<int> res = new List<int>();
            if (root == null)
            {
                return res;
            }
            TreeNode p1 = root, p2 = null;
            while (p1 != null)
            {
                p2 = p1.left;
                if (p2 != null)
                {
                    while (p2.right != null && p2.right != p1)
                    {
                        p2 = p2.right;
                    }
                    if (p2.right == null)
                    {
                        res.Add(p1.val);
                        p2.right = p1;
                        p1 = p1.left;
                        continue;
                    }
                    else
                    {
                        p2.right = null;
                    }
                }
                else
                {
                    res.Add(p1.val);
                }
                p1 = p1.right;
            }
            return res;
        }
        public IList<int> PostorderTraversal(TreeNode root)
        {
            IList<int> res = new List<int>();
            if (root == null)
            {
                return res;
            }
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode prev = null;
            while (root != null || stack.Count != 0)
            {
                while (root != null)
                {
                    stack.Push(root);
                    root = root.left;
                }
                root = stack.Pop();
                if (root.right == null || root.right == prev)
                {
                    res.Add(root.val);
                    prev = root;
                    root = null;
                }
                else
                {
                    stack.Push(root);
                    root = root.right;
                }
            }
            return res;
        }
        public ListNode InsertionSortList(ListNode head)
        {
            if (head == null)
            {
                return head;
            }
            ListNode dummyHead = new ListNode(0);
            dummyHead.next = head;
            ListNode lastSorted = head, curr = head.next;
            while (curr != null)
            {
                if (lastSorted.val <= curr.val)
                {
                    lastSorted = lastSorted.next;
                }
                else
                {
                    ListNode prev = dummyHead;
                    while (prev.next.val <= curr.val)
                    {
                        prev = prev.next;
                    }
                    lastSorted.next = curr.next;
                    curr.next = prev.next;
                    prev.next = curr;
                }
                curr = lastSorted.next;
            }
            return dummyHead.next;
        }
        public ListNode SortList(ListNode head)
        {
            if (head == null)
            {
                return head;
            }
            int len = 0;
            ListNode node = head;
            while (node != null)
            {
                len++;
                node = node.next;
            }
            ListNode dummyHead = new ListNode(0, head);
            for (int subLen = 1; subLen < len; subLen <<= 1)
            {
                ListNode prev = dummyHead, curr = dummyHead.next;
                while (curr != null)
                {
                    ListNode head1 = curr;
                    for (int i = 1; i < subLen && curr.next != null; i++)
                    {
                        curr = curr.next;
                    }
                    ListNode head2 = curr.next;
                    curr.next = null;
                    curr = head2;
                    for (int i = 1; i < subLen && curr != null && curr.next != null; i++)
                    {
                        curr = curr.next;
                    }
                    ListNode next = null;
                    if (curr != null)
                    {
                        next = curr.next;
                        curr.next = null;
                    }
                    ListNode merged = Merge(head1, head2);
                    prev.next = merged;
                    while (prev.next != null)
                    {
                        prev = prev.next;
                    }
                    curr = next;
                }
            }
            return dummyHead.next;
        }
        private ListNode Merge(ListNode head1, ListNode head2)
        {
            ListNode dummyHead = new ListNode(0);
            ListNode temp = dummyHead, temp1 = head1, temp2 = head2;
            while (temp1 != null && temp2 != null)
            {
                if (temp1.val <= temp2.val)
                {
                    temp.next = temp1;
                    temp1 = temp1.next;
                }
                else
                {
                    temp.next = temp2;
                    temp2 = temp2.next;
                }
                temp = temp.next;
            }
            if (temp1 != null)
            {
                temp.next = temp1;
            }
            else if (temp2 != null)
            {
                temp.next = temp2;
            }
            return dummyHead.next;
        }
        public int MaxPoints(int[][] points)
        {
            if (points.Length < 3)
            {
                return points.Length;
            }
            int maxCount = 1;
            for (int i = 0; i < points.Length - 1; i++)
            {
                maxCount = Math.Max(ComputeMaxPoints(points, i), maxCount);
            }
            return maxCount;
        }
        private int horizontalLines;
        private Dictionary<string, int> linePointsDictionary = new Dictionary<string, int>();
        private int count;
        private int duplicates;
        private int ComputeMaxPoints(int[][] points, int i)
        {
            ClearStatisticData();
            for (int j = i + 1; j < points.Length; j++)
            {
                Statistic(points, i, j);
            }
            return count + duplicates;
        }

        private void Statistic(int[][] points, int i, int j)
        {
            int x1 = points[i][0], y1 = points[i][1];
            int x2 = points[j][0], y2 = points[j][1];
            if (x1 == x2 && y1 == y2)
            {
                duplicates++;
            }
            else if (y1 == y2)
            {
                horizontalLines++;
                count = Math.Max(horizontalLines, count);
            }
            else
            {
                string slope = BuildSlope(x1 - x2, y1 - y2);
                if (!linePointsDictionary.TryAdd(slope, 2))
                {
                    linePointsDictionary[slope]++;
                }
                count = Math.Max(linePointsDictionary[slope], count);
            }
        }

        private string BuildSlope(int v1, int v2)
        {
            int gcd = ComputeGcd(v1, v2);
            return v1 / gcd + "_" + v2 / gcd;
        }

        private int ComputeGcd(int v1, int v2)
        {
            if (v2 == 0)
            {
                return v1;
            }
            int r = v1 % v2;
            return ComputeGcd(v2, r);
        }

        private void ClearStatisticData()
        {
            linePointsDictionary.Clear();
            horizontalLines = 1;
            count = 1;
            duplicates = 0;
        }
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            foreach (var token in tokens)
            {
                if (token == "+" || token == "-" || token == "*" || token == "/")
                {
                    int num1 = stack.Pop();
                    int num2 = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push(num2 + num1);
                            break;
                        case "-":
                            stack.Push(num2 - num1);
                            break;
                        case "*":
                            stack.Push(num2 * num1);
                            break;
                        case "/":
                            stack.Push(num2 / num1);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    stack.Push(int.Parse(token));
                }
            }
            return stack.Pop();
        }
        public string ReverseWords(string s)
        {
            string[] res = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            for (int i = res.Length - 1; i >= 0; i--)
            {
                sb.Append(res[i]);
                if (i != 0)
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }
        public int MaxProduct(int[] nums)
        {
            int maxF = nums[0], minF = nums[0], ans = nums[0];
            int len = nums.Length;
            for (int i = 1; i < len; i++)
            {
                int mx = maxF, mn = minF;
                maxF = Math.Max(mx * nums[i], Math.Max(nums[i], mn * nums[i]));
                minF = Math.Min(mn * nums[i], Math.Min(nums[i], mx * nums[i]));
                ans = Math.Max(maxF, ans);
            }
            return ans;
        }
        public int FindMin(int[] nums)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int middle = (left + right) / 2;
                if (nums[middle] < nums[right])
                {
                    right = middle;
                }
                else
                {
                    left = middle + 1;
                }
            }
            return nums[left];
        }
        public int FindMin1(int[] nums)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int middle = left + (right - left) / 2;
                if (nums[middle] < nums[right])
                {
                    right = middle;
                }
                else if (nums[middle] > nums[right])
                {
                    left = middle + 1;
                }
                else
                {
                    right--;
                }
            }
            return nums[left];
        }
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null)
            {
                return null;
            }
            ListNode p1 = headA;
            ListNode p2 = headB;
            while (p1 != p2)
            {
                p1 = p1 == null ? headB : p1.next;
                p2 = p2 == null ? headA : p2.next;
            }
            return p1;
        }
        public int FindPeakElement(int[] nums)
        {
            return SearchPeak(nums, 0, nums.Length - 1);
        }

        private int SearchPeak(int[] nums, int l, int r)
        {
            if (l == r)
            {
                return l;
            }
            int mid = (l + r) / 2;
            if (nums[mid] > nums[mid + 1])
            {
                return SearchPeak(nums, l, mid);
            }
            return SearchPeak(nums, mid + 1, r);
        }
        public int MaximumGap(int[] nums)
        {
            int len = nums.Length;
            if (len < 2)
            {
                return 0;
            }
            long exp = 1;
            int[] buf = new int[len];
            int maxVal = nums.Max();
            while (maxVal >= exp)
            {
                int[] cnt = new int[10];
                for (int i = 0; i < len; i++)
                {
                    int digit = (nums[i] / (int)exp) % 10;
                    cnt[digit]++;
                }
                for (int i = 1; i < 10; i++)
                {
                    cnt[i] += cnt[i - 1];
                }
                for (int i = len - 1; i >= 0; i--)
                {
                    int digit = (nums[i] / (int)exp) % 10;
                    buf[cnt[digit] - 1] = nums[i];
                    cnt[digit]--;
                }
                Array.Copy(buf, 0, nums, 0, len);
                exp *= 10;
            }
            int res = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                res = Math.Max(nums[i + 1] - nums[i], res);
            }
            return res;
        }
        public int CompareVersion(string version1, string version2)
        {
            int p1 = 0, p2 = 0;
            int n1 = version1.Length, n2 = version2.Length;
            int i1, i2;
            KeyValuePair<int, int> pair;
            while (p1 < n1 || p2 < n2)
            {
                pair = GetNextChunk(version1, n1, p1);
                i1 = pair.Key;
                p1 = pair.Value;
                pair = GetNextChunk(version2, n2, p2);
                i2 = pair.Key;
                p2 = pair.Value;
                if (i1 != i2)
                {
                    return i1 > i2 ? 1 : -1;
                }
            }
            return 0;
        }

        private KeyValuePair<int, int> GetNextChunk(string version1, int n1, int p1)
        {
            if (p1 > n1 - 1)
            {
                return new KeyValuePair<int, int>(0, p1);
            }
            int i, pEnd = p1;
            while (pEnd < n1 && version1[pEnd] != '.')
            {
                pEnd++;
            }
            if (pEnd != n1 - 1)
            {
                i = int.Parse(version1.Substring(p1, pEnd - p1));
            }
            else
            {
                i = int.Parse(version1.Substring(p1, p1 - n1));
            }
            p1 = pEnd + 1;
            return new KeyValuePair<int, int>(i, p1);
        }
        public string FractionToDecimal(int numerator, int denominator)
        {
            if (numerator == 0)
            {
                return "0";
            }
            StringBuilder sb = new StringBuilder();
            if (numerator < 0 ^ denominator < 0)
            {
                sb.Append("-");
            }
            long dividend = Math.Abs((long)numerator);
            long divisor = Math.Abs((long)denominator);
            sb.Append(dividend / divisor);
            long reminder = dividend % divisor;
            if (reminder == 0)
            {
                return sb.ToString();
            }
            sb.Append(".");
            Dictionary<long, int> map = new Dictionary<long, int>();
            while (reminder != 0)
            {
                if (map.ContainsKey(reminder))
                {
                    sb.Insert(map[reminder], "(");
                    sb.Append(")");
                    break;
                }
                map.Add(reminder, sb.Length);
                reminder *= 10;
                sb.Append(reminder / divisor);
                reminder %= divisor;
            }
            return sb.ToString();
        }
        public int[] TwoSum1(int[] numbers, int target)
        {
            int left = 0, right = numbers.Length - 1;
            while (left < right)
            {
                if (numbers[left] + numbers[right] < target)
                {
                    left++;
                }
                else if (numbers[left] + numbers[right] > target)
                {
                    right--;
                }
                else
                {
                    return new int[] { left + 1, right + 1 };
                }
            }
            return null;
        }
        public string ConvertToTitle(int columnNumber)
        {
            StringBuilder sb = new StringBuilder();
            while (columnNumber != 0)
            {
                columnNumber--;
                sb.Insert(0, (char)('A' + columnNumber % 26));
                columnNumber /= 26;
            }
            return sb.ToString();
        }
        public int MajorityElement(int[] nums)
        {
            int count = 0;
            int candidate = 0;
            foreach (var num in nums)
            {
                if (count == 0)
                {
                    candidate = num;
                }
                count += (num == candidate ? 1 : -1);
            }
            return candidate;
        }
        public int TitleToNumber(string columnTitle)
        {
            int res = 0;
            for (int i = 0; i < columnTitle.Length; i++)
            {
                res *= 26;
                res += columnTitle[i] - 'A' + 1;
            }
            return res;
        }
        public int TrailingZeroes(int n)
        {
            int count = 0;
            while (n > 0)
            {
                count += n / 5;
                n /= 5;
            }
            return count;
        }
        public int CalculateMinimumHP(int[][] dungeon)
        {
            int row = dungeon.Length;
            int height = dungeon[0].Length;
            int[][] dp = new int[row + 1][];
            for (int i = 0; i <= row; i++)
            {
                dp[i] = new int[height + 1];
                Array.Fill(dp[i], int.MaxValue);
            }
            dp[row][height - 1] = dp[row - 1][height] = 1;
            for (int i = row - 1; i >= 0; i--)
            {
                for (int j = height - 1; j >= 0; j--)
                {
                    int min = Math.Min(dp[i + 1][j], dp[i][j + 1]);
                    dp[i][j] = Math.Max(min - dungeon[i][j], 1);
                }
            }
            return dp[0][0];
        }
        class LargerNumberComparator : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                string s1 = x + y;
                string s2 = y + x;
                return s2.CompareTo(s1);
            }
        }
        public string LargestNumber(int[] nums)
        {
            string[] strNums = new string[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                strNums[i] = nums[i].ToString();
            }
            Array.Sort(strNums, new LargerNumberComparator());
            if (strNums[0].Equals("0"))
            {
                return "0";
            }
            string ans = "";
            foreach (var s in strNums)
            {
                ans += s;
            }
            return ans;
        }
        public IList<string> FindRepeatedDnaSequences(string s)
        {
            int L = 10, n = s.Length;
            if (n <= L)
            {
                return new List<string>();
            }
            int a = 4, aL = (int)Math.Pow(a, L);
            Dictionary<char, int> toInt = new Dictionary<char, int>() { { 'A', 0 }, { 'C', 1 }, { 'G', 2 }, { 'T', 3 } };
            int[] nums = new int[n];
            for (int i = 0; i < n; i++)
            {
                nums[i] = toInt[s[i]];
            }
            int h = 0;
            HashSet<int> seen = new HashSet<int>();
            HashSet<string> output = new HashSet<string>();
            for (int start = 0; start < n - L + 1; start++)
            {
                if (start != 0)
                {
                    h = h * a - nums[start - 1] * aL + nums[start + L - 1];
                }
                else
                {
                    for (int i = 0; i < L; i++)
                    {
                        h = h * a + nums[i];
                    }
                }
                if (seen.Contains(h))
                {
                    output.Add(s.Substring(start, L));
                }
                seen.Add(h);
            }
            return new List<string>(output);
        }
        public int MaxProfit(int k, int[] prices)
        {
            if (prices.Length == 0)
            {
                return 0;
            }
            int len = prices.Length;
            int left = 1, right = prices.Max();
            int ans = -1;
            while (left <= right)
            {
                int c = (left + right) / 2;
                int buyCount = 0, sellCount = 0;
                int buy = -prices[0], sell = 0;
                for (int i = 0; i < len; i++)
                {
                    if (sell - prices[i] >= buy)
                    {
                        buy = sell - prices[i];
                        buyCount = sellCount;
                    }
                    if (buy + prices[i] - c >= sell)
                    {
                        sell = buy + prices[i] - c;
                        sellCount = buyCount + 1;
                    }
                }
                if (sellCount >= k)
                {
                    ans = sell + k * c;
                    left = c + 1;
                }
                else
                {
                    right = c - 1;
                }
            }
            if (ans == -1)
            {
                ans = 0;
                for (int i = 1; i < len; i++)
                {
                    ans += Math.Max(prices[i] - prices[i - 1], 0);
                }
            }
            return ans;
        }
        public void Rotate(int[] nums, int k)
        {
            k %= nums.Length;
            Reverse(nums, 0, nums.Length - 1);
            Reverse(nums, 0, k - 1);
            Reverse(nums, k, nums.Length - 1);
        }
        private void Reverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                int tmp = nums[start];
                nums[start] = nums[end];
                nums[end] = tmp;
                start++;
                end--;
            }
        }
        public uint ReverseBits(uint n)
        {
            n = (n >> 16) | (n << 16);
            n = ((n & 0xff00ff00) >> 8) | ((n & 0x00ff00ff) << 8);
            n = ((n & 0xf0f0f0f0) >> 4) | ((n & 0x0f0f0f0f) << 4);
            n = ((n & 0xcccccccc) >> 2) | ((n & 0x33333333) << 2);
            n = ((n & 0xaaaaaaaa) >> 1) | ((n & 0x55555555) << 1);
            return n;
        }
        public int HammingWeight(uint n)
        {
            int sum = 0;
            while (n != 0)
            {
                sum++;
                n &= (n - 1);
            }
            return sum;
        }
        public int Rob(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }
            int len = nums.Length;
            if (len == 1)
            {
                return nums[0]; 
            }
            int first = nums[0], second = Math.Max(nums[0], nums[1]);
            for (int i = 2; i < len; i++)
            {
                int temp = second;
                second = Math.Max(first + nums[i], second);
                first = temp;
            }
            return second;
        }
        public IList<int> RightSideView(TreeNode root)
        {
            List<int> ans = new List<int>();
            DFS(root, ans, 0);
            return ans;
        }

        private void DFS(TreeNode root, List<int> ans, int depth)
        {
            if (root == null)
            {
                return;
            }
            if (depth == ans.Count)
            {
                ans.Add(root.val);
            }
            depth++;
            DFS(root.right, ans, depth);
            DFS(root.left, ans, depth);
        }
        public int NumIslands(char[][] grid)
        {
            int count = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        DFS1(grid, i, j);
                        count++;
                    }
                }
            }
            return count;
        }

        private void DFS1(char[][] grid, int i, int j)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length || grid[i][j] == '0')
            {
                return;
            }
            grid[i][j] = '0';
            DFS1(grid, i + 1, j);
            DFS1(grid, i, j + 1);
            DFS1(grid, i - 1, j);
            DFS1(grid, i, j - 1);
        }
        public int RangeBitwiseAnd(int left, int right)
        {
            int shift = 0;
            // 找到公共前缀
            while (left < right)
            {
                left >>= 1;
                right >>= 1;
                ++shift;
            }
            return left << shift;
        }
        private HashSet<int> CycleNumbers = new HashSet<int>() { 4, 16, 37, 58, 89, 145, 42, 20, 4 };
        private int GetNext(int n)
        {
            int totalSum = 0;
            while (n > 0)
            {
                int d = n % 10;
                n /= 10;
                totalSum += d * d;
            }
            return totalSum;
        }
        public bool IsHappy(int n)
        {
            while (n != 1 && !CycleNumbers.Contains(n))
            {
                n = GetNext(n);
            }
            return n == 1;
        }
        public ListNode RemoveElements(ListNode head, int val)
        {
            ListNode newHead = new ListNode(0);
            ListNode previousNode = newHead;
            ListNode currentNode = head;

            while (currentNode != null)
            {
                if (currentNode.val != val)
                {
                    previousNode.next = currentNode;
                    previousNode = currentNode;
                }
                currentNode = currentNode.next;
            }
            //特别注意不能少了这行
            currentNode.next = null;
            return newHead.next;
        }
        public int CountPrimes(int n)
        {
            List<int> primes = new List<int>();
            int[] isPrime = new int[n];
            Array.Fill(isPrime, 1);
            for (int i = 2; i < n; i++)
            {
                if (isPrime[i] == 1)
                {
                    primes.Add(i);
                }
                for (int j = 0; j < primes.Count && i * primes[j] < n; j++)
                {
                    isPrime[i * primes[j]] = 0;
                    if (i % primes[j] == 0)
                    {
                        break;
                    }
                }
            }
            return primes.Count;
        }
        public bool IsIsomorphic(string s, string t)
        {
            if (s.Length != t.Length)
            {
                return false;
            }
            Dictionary<char, char> dic1 = new Dictionary<char, char>();
            Dictionary<char, char> dic2 = new Dictionary<char, char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (dic1.ContainsKey(s[i]))
                {
                    if (dic1[s[i]] != t[i])
                    {
                        return false;
                    }
                }
                else
                {
                    dic1.Add(s[i], t[i]);
                }
                if (dic2.ContainsKey(t[i]))
                {
                    if (dic2[t[i]] != s[i])
                    {
                        return false;
                    }
                }
                else
                {
                    dic2.Add(t[i], s[i]);
                }
            }
            return true;
        }
        public ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;
            ListNode curr = head;
            while (curr != null)
            {
                ListNode next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            return prev;
        }
        bool valid207 = true;
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            List<List<int>> edges = new List<List<int>>();
            int[] visited = new int[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                edges.Add(new List<int>());
            }
            foreach (var info in prerequisites)
            {
                edges[info[1]].Add(info[0]);
            }
            for (int i = 0; i < numCourses && valid207; i++)
            {
                if (visited[i] == 0)
                {
                    DFS(i, visited, edges);
                }
            }
            return valid207;
        }

        private void DFS(int i, int[] visited, List<List<int>> edges)
        {
            visited[i] = 1;
            foreach (var num in edges[i])
            {
                if (visited[num] == 0)
                {
                    DFS(num, visited, edges);
                    if (!valid207)
                    {
                        return;
                    }
                }
                else if (visited[num] == 1)
                {
                    valid207 = false;
                    return;
                }
            }
            visited[i] = 2;
        }
        bool valid210 = true;
        int index210;
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            List<List<int>> edges = new List<List<int>>();
            for (int i = 0; i < numCourses; i++)
            {
                edges.Add(new List<int>());
            }
            int[] visited = new int[numCourses];
            int[] result = new int[numCourses];
            index210 = numCourses - 1;
            foreach (var info in prerequisites)
            {
                edges[info[1]].Add(info[0]);
            }
            for (int i = 0; i < numCourses && valid; i++)
            {
                if (visited[i] == 0)
                {
                    DFS(i, edges, visited, result);
                }
            }
            if (!valid210)
            {
                return new int[0];
            }
            return result;
        }

        private void DFS(int i, List<List<int>> edges, int[] visited, int[] result)
        {
            visited[i] = 1;
            foreach (var num in edges[i])
            {
                if (visited[num] == 0)
                {
                    DFS(num, edges, visited, result);
                    if (!valid210)
                    {
                        return;
                    }
                }
                else if (visited[num] == 1)
                {
                    valid210 = false;
                    return;
                }
            }
            visited[i] = 2;
            result[index210--] = i;
        }
        class TrieNode
        {
            public Dictionary<char, TrieNode> Children { get; set; } = new Dictionary<char, TrieNode>();
            public string Word { get; set; }
            public TrieNode()
            {

            }
        }
        char[][] board212 = null;
        List<string> result212 = new List<string>();
        public IList<string> FindWords(char[][] board, string[] words)
        {
            TrieNode root = new TrieNode();
            foreach (var word in words)
            {
                TrieNode node = root;
                foreach (var letter in word.ToCharArray())
                {
                    if (node.Children.ContainsKey(letter))
                    {
                        node = node.Children[letter];
                    }
                    else
                    {
                        TrieNode newNode = new TrieNode();
                        node.Children.Add(letter, newNode);
                        node = newNode;
                    }
                }
                node.Word = word;
            }
            board212 = board;
            for (int row = 0; row < board.Length; row++)
            {
                for (int col = 0; col < board[row].Length; col++)
                {
                    if (root.Children.ContainsKey(board[row][col]))
                    {
                        BackTracking(row, col, root);
                    }
                }
            }
            return result212;
        }

        private void BackTracking(int row, int col, TrieNode root)
        {
            char letter = board212[row][col];
            TrieNode currNode = root.Children[letter];
            if (currNode.Word != null)
            {
                result212.Add(currNode.Word);
                currNode.Word = null;
            }
            board212[row][col] = '#';
            int[] rowOffset = { -1, 0, 1, 0 };
            int[] colOffset = { 0, 1, 0, -1 };
            for (int i = 0; i < 4; i++)
            {
                int newRow = row + rowOffset[i];
                int newCol = col + colOffset[i];
                if (newRow < 0 || newRow >= board212.Length ||
                    newCol < 0 || newCol >= board212[0].Length)
                {
                    continue;
                }
                if (currNode.Children.ContainsKey(board212[newRow][newCol]))
                {
                    BackTracking(newRow, newCol, currNode);
                }
            }
            board212[row][col] = letter;
            if (currNode.Children.Count == 0)
            {
                root.Children.Remove(letter);
            }
        }
    }
    public class WordDictionary
    {
        Dictionary<int, List<string>> maps;
        /** Initialize your data structure here. */
        public WordDictionary()
        {
            maps = new Dictionary<int, List<string>>();
        }

        public void AddWord(string word)
        {
            int key = word.Length;
            if (maps.ContainsKey(key))
            {
                maps[key].Add(word);
            }
            else
            {
                maps.Add(key, new List<string>() { word});
            }
        }

        public bool Search(string word)
        {
            int key = word.Length;
            if (!maps.ContainsKey(key))
            {
                return false;
            }
            List<string> temp = maps[key];
            bool flag = true;
            foreach (var str in temp)
            {
                flag = true;
                for (int i = 0; i < str.Length; i++)
                {
                    char w = word[i];
                    if (w == '.')
                    {
                        continue;
                    }
                    else
                    {
                        char s = str[i];
                        if (w == s)
                        {
                            continue;
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Trie
    {
        private bool isEnd;
        private Trie[] Nodes = new Trie[26];
        /** Initialize your data structure here. */
        public Trie()
        {

        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            Trie node = this;
            foreach (var a in word)
            {
                if (node.Nodes[a - 'a'] == null)
                {
                    node.Nodes[a - 'a'] = new Trie();
                }
                node = node.Nodes[a - 'a'];
            }
            node.isEnd = true;
        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            Trie node = this;
            foreach (var a in word)
            {
                node = node.Nodes[a - 'a'];
                if (node == null)
                    return false;
            }
            return node.isEnd;
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            Trie node = this;
            foreach (var a in prefix)
            {
                node = node.Nodes[a - 'a'];
                if (node == null)
                    return false;
            }
            return true;
        }
        public int MinSubArrayLen(int target, int[] nums)
        {
            int len = nums.Length;
            if (len == 0)
            {
                return 0;
            }
            int ans = int.MaxValue;
            int start = 0, end = 0;
            int sum = 0;
            while (end < len)
            {
                sum += nums[end];
                while (sum >= target)
                {
                    ans = Math.Min(ans, end - start + 1);
                    sum -= nums[start];
                    start++;
                }
                end++;
            }
            return ans == int.MaxValue ? 0 : ans;
        }
    }
    public class BSTIterator
    {
        Stack<TreeNode> stack;
        public BSTIterator(TreeNode root)
        {
            stack = new Stack<TreeNode>();
            LeftMin(root);
        }
        private void LeftMin(TreeNode root)
        {
            while (root != null)
            {
                stack.Push(root);
                root = root.left;
            }
        }
        public int Next()
        {
            TreeNode tmp = stack.Pop();
            if (tmp.right != null)
            {
                LeftMin(tmp.right);
            }
            return tmp.val;
        }

        public bool HasNext()
        {
            return stack.Count > 0;
        }
    }
    public class LRUCache
    {
        public class DLinkeNode
        {
            public int key;
            public int value;
            public DLinkeNode prev;
            public DLinkeNode next;
            public DLinkeNode()
            {

            }
            public DLinkeNode(int k, int val)
            {
                key = k;
                value = val;
            }
        }
        private Dictionary<int, DLinkeNode> cache = new Dictionary<int, DLinkeNode>();
        private int size;
        private int capacity;
        private DLinkeNode head, tail;

        public LRUCache(int capacity)
        {
            size = 0;
            this.capacity = capacity;
            head = new DLinkeNode();
            tail = new DLinkeNode();
            head.next = tail;
            tail.prev = head;
        }

        public int Get(int key)
        {
            bool success = cache.TryGetValue(key, out DLinkeNode node);
            if (!success)
            {
                return -1;
            }
            MoveToHead(node);
            return node.value;
        }

        public void Put(int key, int value)
        {
            bool success = cache.TryGetValue(key, out DLinkeNode node);
            if (!success)
            {
                DLinkeNode newNode = new DLinkeNode(key, value);
                cache.Add(key, newNode);
                AddToHead(newNode);
                size++;
                if (size > capacity)
                {
                    DLinkeNode tail = RemoveTail();
                    cache.Remove(tail.key);
                    size--;
                }
            }
            else
            {
                node.value = value;
                MoveToHead(node);
            }
        }

        private DLinkeNode RemoveTail()
        {
            DLinkeNode res = tail.prev;
            RemoveNode(res);
            return res;
        }

        private void MoveToHead(DLinkeNode node)
        {
            RemoveNode(node);
            AddToHead(node);
        }

        private void AddToHead(DLinkeNode node)
        {
            node.prev = head;
            node.next = head.next;
            head.next.prev = node;
            head.next = node;
        }

        private void RemoveNode(DLinkeNode node)
        {
            node.prev.next = node.next;
            node.next.prev = node.prev;
        }
    }
    public class MinStack
    {
        Stack<int[]> myStack;
        /** initialize your data structure here. */
        public MinStack()
        {
            myStack = new Stack<int[]>();
        }

        public void Push(int x)
        {
            if (myStack.Count == 0)
            {
                myStack.Push(new int[] { x, x });
            }
            else
            {
                myStack.Push(new int[] { x, Math.Min(x, myStack.Peek()[1]) });
            }
        }

        public void Pop()
        {
            myStack.Pop();
        }

        public int Top()
        {
            return myStack.Peek()[0];
        }

        public int GetMin()
        {
            return myStack.Peek()[1];
        }
    }
}
