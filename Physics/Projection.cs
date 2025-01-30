using System.Numerics;

namespace AGWAE.Physics
{
    internal class Projection(double min, double max)
    {
        public double Min { get; } = min;
        public double Max { get; } = max;

        public static Projection ProjectOntoAxis(Vector2 axis, Shape shape)
        {
            double min = Vector2.Dot(axis, shape.Vertices[0]);
            double max = min;

            for (int i = 1; i < shape.Vertices.Length; i++)
            {
                double p = Vector2.Dot(axis, shape.Vertices[i]);

                if (p < min)
                {
                    min = p;
                }
                else if (p > max)
                {
                    max = p;
                }
            }
            return new Projection(min, max);
        }

        public bool Overlaps(Projection other)
        {
            return !(Max < other.Min || Min > other.Max);
        }

        public double GetOverlap(Projection other)
        {
            if (!Overlaps(other))
                return 0;

            return Math.Min(Max, other.Max) - Math.Max(Min, other.Min);
        }
    }
}
