using System;

namespace gcgcg
{
    public class Manhattan : IDistanceStrategy
    {
        public Manhattan()
        {}

        public double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}