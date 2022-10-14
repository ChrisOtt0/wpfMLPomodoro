using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using wpfMLPomodoro.Foundation;
using wpfMLPomodoro.FileIO;

namespace wpfMLPomodoro.Business
{
    public class Tokenization
    {
        private const int SMALLESTWORDLENGTH = 3;
        static List<string> stopWords = new TextFile("txt").GetAllTextFromFileA(DirOperations.GetTextDirectory() + "Stopwords\\long.txt").Split(' ', '\n', '\r').ToList();

        public static List<string> Tokenize(string originalText)
        {
            List<string> words = new List<string>();
            String[] tokens = originalText.Split(' ', '\r', '\n');


            foreach (string token in tokens)
            {

                if (IsAShortWord(token))
                {
                    // skip
                }
                else
                {
                    string cleanWord = token;

                    while (HasPunctuation(cleanWord))
                    {
                        cleanWord = RemovePunctuation(token);
                    }
                    cleanWord = cleanWord.ToLower();

                    if (!stopWords.Contains(cleanWord))
                        words.Add(cleanWord);
                }
            }
            //words = StringOperations.StemStrings(words);

            return words;
        }
        public static bool IsAShortWord(string token)
        {
            if (token.Length < SMALLESTWORDLENGTH)
            {
                return true;
            }
            return false;
        }

        public static string RemovePunctuation(string token)
        {
            token = token.Trim();

            char[] punctuations = { '.', ',', '"', '?', '\n', '!', ':', ';', '[', ']', '{', '}', '(', ')', '“', '”', '´', '`', '’', '‘' };

            #region deprecated (SCO)
            /*
            for (int i = 0; i < punctuations.Length; i++)
            {
                string ch = punctuations[i].ToString();
                if (token.StartsWith(ch))
                {
                    return token.Substring(1);
                }
                else if (token.EndsWith(ch))
                {
                    return token.Substring(0, token.Length - 1);
                }
            }
            */
            #endregion 

            for (int i = 0; i < punctuations.Length; i++)
            {
                foreach (char t in token)
                {
                    if (t == punctuations[i])
                    {
                        token = token.Replace(t.ToString(), "");
                    }
                }
            }

            return token;
        }

        public static bool HasPunctuation(string token)
        {

            char[] punctuations = { '.', ',', '"', '?', '\n', '!', ':', ';', '[', ']', '{', '}', '(', ')', '“', '”', '´', '`', '’', '‘' };

            for (int i = 0; i < punctuations.Length; i++)
            {
                if (token.Contains(punctuations[i]))
                    return true;
            }

            return false;
        }
    }
}
