using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMLPomodoro.Domain;
using wpfMLPomodoro.FileIO;

namespace wpfMLPomodoro.Controller
{
    public class FileListBuilder : AbstractFileListBuilder
    {
        const string AFOLDERNAME = "ClassA";
        const string BFOLDERNAME = "ClassB";

        private FileLists _fileLists;
        private FileAdapter _fileAdapter;

        public FileListBuilder()
        {
            _fileLists = new FileLists();

            _fileAdapter = new TextFile("txt");
        }

        public override FileLists GetFileLists()
        {
            return _fileLists;
        }

        public override void GenerateFileNamesInA()
        {
            List<string> fileNames = _fileAdapter.GetAllFileNames(AFOLDERNAME);
            _fileLists.SetA(fileNames);
        }

        public override void GenerateFileNamesInB()
        {
            List<string> fileNames = _fileAdapter.GetAllFileNames(BFOLDERNAME);
            _fileLists.SetB(fileNames);
        }
    }
}
