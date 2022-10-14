using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMLPomodoro.FileIO;

namespace wpfMLPomodoro._4_FileIO
{
    internal class PDFFile : FileAdapter
    {
        public PDFFile(string fileType) : base(fileType)
        {

        }

        public override List<string> GetAllFileNames(string folderName)
        {
            throw new NotImplementedException();
        }

        public override string GetAllTextFromFileA(string fileName)
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfReader(fileName));
            LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
            string text = "";

            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
            {
                var page = pdfDocument.GetPage(i);
                text += PdfTextExtractor.GetTextFromPage(page, strategy);
            }

            return text;
        }

        public override string GetAllTextFromFileB(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
