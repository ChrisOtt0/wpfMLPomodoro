using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMLPomodoro.Domain
{
    public class Result : IComparable<Result>
    {
        double distance;
        string belongsTo;

        public Result(double distance, string belongsTo)
        {
            this.distance = distance;
            this.belongsTo = belongsTo;
        }

        public double Distance => distance;

        public string BelongsTo => belongsTo;

        public int CompareTo(Result? other)
        {
            if (other == null)
                return 0;
            if (other.distance == distance)
                return 0;
            else if (other.distance > distance)
                return -1;
            else
                return 1;
        }
    }
}
