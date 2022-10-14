using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using wpfMLPomodoro.Domain;
using wpfMLPomodoro.Foundation;

namespace wpfMLPomodoro.Controller
{
    public class Categorization
    {
        Vectors model;
        int k;

        public Categorization(Vectors model, int k)
        {
            this.model = model;
            this.k = k - 1;
        }

        public string Categorize(List<bool> input)
        {

            List<List<bool>> vectorsA = model.VectorsInA;
            List<List<bool>> vectorsB = model.VectorsInB;

            List<double> distancesA = new List<double>();
            List<double> distancesB = new List<double>();

            foreach (List<bool> vector in vectorsA)
            {
                distancesA.Add(VectorOperations.GetDistance(vector, input));
            }

            foreach (List<bool> vector in vectorsB)
            {
                distancesB.Add(VectorOperations.GetDistance(vector, input));
            }

            distancesA.Sort();
            distancesB.Sort();

            List<Result> allDistances = new List<Result>();

            foreach (double resA in distancesA)
                allDistances.Add(new Result(resA, "ClassA"));

            foreach (double resB in distancesB)
                allDistances.Add(new Result(resB, "ClassB"));

            allDistances.Sort();

            for (int i = 0; i <= k; i++)
            {
                Trace.WriteLine(allDistances[i].BelongsTo + ": " + allDistances[i].Distance);
            }

            List<string> results = new List<string>();
            foreach (Result r in allDistances) { results.Add(r.BelongsTo); }

            int countA = 0;
            int countB = 0;

            for (int i = 0; i <= k; i++)
            {
                if (results[i] == "ClassA")
                    countA += 1;
                else
                    countB += 1;
            }

            Trace.WriteLine(countA + " - " + countB);

            if (countA > countB)
                return "Sports";
            else
                return "Fairy Tales";
        }
    }
}
