using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMLPomodoro.Foundation
{
    public class DirOperations
    {
        public static string GetTextDirectory()
        {
            string path = Environment.CurrentDirectory;
            bool foundProjectFolder = false;
            int subFolderSearched = 0;

            while (true)
            {
                if (Directory.GetParent(path).Name == "wpfMLPomodoro")
                {
                    path = Directory.GetParent(path).FullName;
                    foundProjectFolder = true;
                    break;
                }
                if (subFolderSearched > 5)
                    break;

                path = Directory.GetParent(path).FullName;
                subFolderSearched++;
            }

            if (foundProjectFolder)
                return path + "\\6_Resources\\";
            else
                return null;
        }
    }
}
