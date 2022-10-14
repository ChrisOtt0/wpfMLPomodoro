using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMLPomodoro.Domain;

namespace wpfMLPomodoro.Controller
{
    public abstract class AbstractFileListBuilder
    {
        public abstract void GenerateFileNamesInA();

        public abstract void GenerateFileNamesInB();

        //  get the complete FileLists-object
        public abstract FileLists GetFileLists();
    }
}
