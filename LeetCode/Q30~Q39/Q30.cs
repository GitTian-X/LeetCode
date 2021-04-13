using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q30_Q39
{
    class Q30
    {
        //给定一个字符串 s 和一些长度相同的单词 words。找出 s 中恰好可以由 words 中所有单词串联形成的子串的起始位置。
        //注意子串要与 words 中的单词完全匹配，中间不能有其他字符，但不需要考虑 words 中单词串联的顺序
        public IList<int> FindSubstring(string s, string[] words)
        {
            List<int> res = new List<int>();
            int wordNum = words.Length;
            if (wordNum == 0)
            {
                return res;
            }
            int wordLen = words[0].Length;
            Dictionary<string, int> allWords = new Dictionary<string, int>();
            foreach (var w in words)
            {
                if (allWords.ContainsKey(w))
                {
                    allWords[w]++;
                }
                else
                {
                    allWords.Add(w, 0);
                }
            }
            //所有移动分成wordLen种情况
            for (int j = 0; j < wordLen; j++)
            {
                Dictionary<string, int> hasWords = new Dictionary<string, int>();
                int num = 0;
                //每次移动一个单词的长度
                for (int i = j; i < s.Length - wordLen * wordNum + 1; i += wordLen)
                {
                    bool hasRemoved = false;
                    while (num < wordNum)
                    {
                        string word = s.Substring(i + num * wordLen, wordLen);
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
                                    string firstWord = s.Substring(i + removeNum * wordLen, wordLen);
                                    hasWords[firstWord]--;
                                    removeNum++;
                                }
                                num = num - removeNum + 1;
                                i += (removeNum - 1) * wordLen;
                                break;
                            }
                        }
                        else
                        {
                            hasWords.Clear();
                            i += num * wordLen;
                            num = 0;
                            break;
                        }
                        num++;
                    }
                    if (num == wordNum)
                    {
                        res.Add(i);
                    }
                    if (num > 0 && !hasRemoved)
                    {
                        string firstWord = s.Substring(i, wordLen);
                        hasWords[firstWord]--;
                        num--;
                    }
                }
            }
            return res;
        }
    }
}
