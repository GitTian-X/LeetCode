using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

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
            while (tmp.next !=null && tmp.next.next != null)
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
                else if(right > left)
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
                    mux2 = 0x01 << 9 <<(board[i][j] - '1');
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
            Array.Sort(intervals, (int[] num1, int[] num2) => {
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
                        ans.Add(new int[] { left, right});
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
    }
}
