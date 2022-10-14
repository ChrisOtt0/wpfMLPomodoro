using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMLPomodoro.Foundation
{
    public class StringOperations
    {
        const int EXTENSIONLENGTH = 4;
        public static string getFileName(string path)
        {
            // a) skipping the extension .txt (4 chars)
            string name = path.Substring(0, path.Length - EXTENSIONLENGTH);

            // b) skipping the front part
            int startPos = name.LastIndexOf('\\') + 1;
            name = name.Substring(startPos, name.Length - startPos);

            return name;
        }

        // deprecated
        /*
        public static List<string> StemStrings(List<string> input)
        {
            List<string> output = new List<string>(input);

            for (int i = 0; i < input.Count; i++)
            {
                string cWord = input[i];
                string withoutS = input[i].Substring(0, input[i].Length - 1);

                if (cWord.EndsWith('s') && output.Contains(withoutS))
                {
                    output.Remove(input[i]);
                    Trace.WriteLine(input[i]);
                }
            }

            return output;
        }
        */
    }
}
