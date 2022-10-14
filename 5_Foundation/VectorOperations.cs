using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMLPomodoro.Foundation
{
    public class VectorOperations
    {
        public static double GetDistance(List<bool> vector1, List<bool> vector2)
        {
            if (vector1.Count != vector2.Count)
                throw new Exception("Vector count is off.");

            double toBeRooted = 0.0;

            for (int i = 0; i < vector1.Count; i++)
            {
                int x = 0, y = 0;

                if (vector1[i])
                    x = 1;

                if (vector2[i])
                    y = 1;

                toBeRooted += Math.Pow((x - y), 2);
            }

            return Math.Sqrt(toBeRooted);
        }
    }
}
