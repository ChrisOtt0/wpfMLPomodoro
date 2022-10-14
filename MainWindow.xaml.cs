using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfMLPomodoro._4_FileIO;
using wpfMLPomodoro.Business;
using wpfMLPomodoro.Controller;
using wpfMLPomodoro.Domain;
using wpfMLPomodoro.FileIO;
using wpfMLPomodoro.Foundation;

namespace wpfMLPomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableData ob = new ObservableData();
        KnowledgeBuilder kb = new KnowledgeBuilder();
        FileAdapter fA = new TextFile("txt");
        Categorization c;
        Knowledge k;
        BagOfWords bof;
        FileLists fl;
        Vectors vectors;
        long trainingTime;
        int amountOfNeigbors = 9;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ob;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start timer for trainTimeTextBox
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // initiate learning process
            kb.Train();

            // stop timer and set trainTimeTextBox
            timer.Stop();
            trainingTime = timer.ElapsedMilliseconds;
            trainTimeTextBox.Text = "Time: " + trainingTime + " ms";

            // getting whole knowledge found in ClassA and ClassB
            k = kb.GetKnowledge();

            // getting parts of the knowledge
            bof = k.GetBagOfWords();
            fl = k.GetFileLists();
            vectors = k.GetVectors();

            // updating ob based on gained knowledge
            ob.ObservableA = new ObservableCollection<string>(fl.GetA().Select(StringOperations.getFileName));
            ob.ObservableB = new ObservableCollection<string>(fl.GetB().Select(StringOperations.getFileName));
            ob.ObservableAB = new ObservableCollection<string>(fl.GetBoth().Select(StringOperations.getFileName));
            ob.ObservableDictionaryList = new ObservableCollection<string>(bof.GetEntriesInDictionary());
            ob.ObservableTokens = new ObservableCollection<int>(k.GetTokenCount().Tokens);
            
            BtnTest.IsEnabled = true;
            BtnCategorize.IsEnabled = true;
            BtnPDF.IsEnabled = true;

            //Trace.WriteLine(bof.GetAllWordsInDictionary().ToList()[0]);

            //// Test Vector (dumb fucking MSTest and NUnitTest)
            //Vectors v = k.GetVectors();
            //List<List<bool>> vA = v.VectorsInA;
            //List<bool> vA1 = vA[0];
            //string output = "Test:\nVectorCount: " + vA1.Count + ", bofCount: " + bof.GetAllWordsInDictionary().Count + "\n\n";
            //List<string> words = bof.GetEntriesInDictionary();
            //for (int i = 50; i < 100; i++)
            //{
            //    output += "v: " + vA1[i] + ", bof: " + words[i] + "\n";
            //}
            //MessageBox.Show(output);

            ////Test VectorOperation
            //List<bool> v1 = new List<bool> { true, true, false, true, false, false, true };
            //List<bool> v2 = new List<bool> { true, true, false, true, false, false, true };
            //double distance = VectorOperations.GetDistance(v1, v2);
            //MessageBox.Show("Calculated distance: " + distance.ToString());

            //// Test stemming algorithm -- does not work after actual implementation (deprecated)
            //List<string> before = bof.GetAllWordsInDictionary().ToList();
            //List<string> after = StringOperations.StemStrings(before);
            //string output = "Legnth - Before: " + before.Count + ", After: " + after.Count + "\n\n";
            //for (int i = 0; i < output.Length - 1; i++)
            //{
            //    output += "Before: " + before[i] + ", After: " + after[i] + "\n";
            //}
            //MessageBox.Show(output);

            //// Test stemming post-refactoring
            //int expected = 1403;
            //int after = bof.GetEntriesInDictionary().Count;
            //MessageBox.Show((expected == after).ToString());


            //// Stuff for later ////
            //// Arrange //
            //List<bool> currentV = new List<bool>();
            //string text;
            //string txt = "albert-pujols-angels-contract.txt";
            //string expected = "ClassA", actual = "";

            //// Act //
            //text = fA.GetAllTextFromFileA(path + txt);
            //List<string> words = Tokenization.Tokenize(text);

            //foreach (string key in bof.GetAllWordsInDictionary())
            //{
            //    if (words.Contains(key))
            //        currentV.Add(true);
            //    else
            //        currentV.Add(false);
            //}
        }

        // Test button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            c = new Categorization(vectors, amountOfNeigbors);

            string path = DirOperations.GetTextDirectory() + "Tests\\";
            Dictionary<string, string> tests = new Dictionary<string, string>
            {
                { "cincinnati-bengals.txt", "ClassA" },
                { "liverpools-title-tilts.txt", "ClassA" },
                { "mlb-playoffs.txt", "ClassA" },
                { "nba-draft-2023.txt", "ClassA" },
                { "wild-card-series.txt", "ClassA" },
                //{ "albert-pujols-angels-contract.txt", "ClassA" },
                //{ "draymond-jordan-poole-situation.txt", "ClassA" },
                //{ "esl-sport-reading.txt", "ClassA" },
                //{ "maiden-lpga-tour-title.txt", "ClassA" },
                //{ "unforgiving-league.txt", "ClassA" },

                //{ "androcles-and-the-lion.txt", "ClassB" },
                //{ "cindarella.txt", "ClassB" },
                //{ "little-red-riding-hood.txt", "ClassB" },
                { "rapunzel.txt", "ClassB" },
                //{ "the-leap-frog.txt", "ClassB" },
                { "beauty-and-the-beast.txt", "ClassB" },
                { "goldilocks.txt", "ClassB" },
                { "rumpelstiltskin.txt", "ClassB" },
                //{ "the-princess-and-the-pea.txt", "ClassB" },
                { "the-wicked-prince.txt", "ClassB" }
            };
            string output = "";

            foreach (KeyValuePair<string, string> kvp in tests)
            {
                List<bool> currentVector = new List<bool>();
                string txt = kvp.Key;
                string expected = kvp.Value;
                string text = fA.GetAllTextFromFileA(path + expected + "\\" + txt);
                List<string> wordsInFile = Tokenization.Tokenize(text);
                List<string> words = bof.GetAllWordsInDictionary().ToList();

                foreach (string word in words)
                {
                    if (wordsInFile.Contains(word))
                        currentVector.Add(true);
                    else
                        currentVector.Add(false);
                }

                // use magic controller to perform the thingy
                output += txt + ": " + c.Categorize(currentVector) + "\n";
            }

            MessageBox.Show(output);
        }

        // Categorize
        private void BtnCategorize_Click(object sender, RoutedEventArgs e)
        {
            c = new Categorization(vectors, amountOfNeigbors);

            List<bool> currentVector = new List<bool>();
            string txt = inputTextBox.Text;
            List<string> wordsInText = Tokenization.Tokenize(txt);
            List<string> words = bof.GetAllWordsInDictionary().ToList();

            foreach (string word in words)
            {
                if (wordsInText.Contains(word))
                    currentVector.Add(true);
                else
                    currentVector.Add(false);
            }

            MessageBox.Show("Input text belongs to: " + c.Categorize(currentVector));
        }

        // Upload PDF text button
        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".pdf"; // Required file extension 
            fileDialog.Filter = "PDF documents (.pdf)|*.pdf"; // Optional file extensions

            bool result = (bool)fileDialog.ShowDialog();

            if (result)
            {
                // Insert FileAdapter PDFFile to read
                FileAdapter pdfAdapter = new PDFFile("pdf");
                string text = pdfAdapter.GetAllTextFromFileA(fileDialog.FileName);
                inputTextBox.Text = text;
            }
        }
    }
}
