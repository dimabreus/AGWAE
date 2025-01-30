using SFML.Graphics;
using System.Numerics;

namespace AGWAE.Physics
{
    public class Shape(Vector2[] vertices)
    {
        public Vector2[] Vertices = vertices;

        public Shape(FloatRect rect)
            : this([
                    new Vector2(rect.Left, rect.Top),
                    new Vector2(rect.Left + rect.Width, rect.Top),
                    new Vector2(rect.Left + rect.Width, rect.Top + rect.Height),
                    new Vector2(rect.Left, rect.Top + rect.Height),
                ])
        {
        }

        public Vector2[] GetAxes()
        {
            Vector2[] axes = new Vector2[Vertices.Length];

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vector2 p1 = Vertices[i];
                Vector2 p2 = Vertices[i + 1 == Vertices.Length ? 0 : i + 1];

                Vector2 edge = p1 - p2;
                Vector2 normal = Perp(edge);

                axes[i] = Vector2.Normalize(normal);
            }

            return RemoveParallelAxes(axes);
        }

        public static Vector2 Perp(Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }

        private static Vector2[] RemoveParallelAxes(Vector2[] axes)
        {
            HashSet<Vector2> uniqueAxes = new(new AxisEqualityComparer());

            foreach (Vector2 axis in axes)
            {
                uniqueAxes.Add(axis);
            }

            return [.. uniqueAxes];
        }

        private class AxisEqualityComparer : IEqualityComparer<Vector2>
        {
            public bool Equals(Vector2 a, Vector2 b)
            {
                Vector2 absA = new(MathF.Abs(a.X), MathF.Abs(a.Y));
                Vector2 absB = new(MathF.Abs(b.X), MathF.Abs(b.Y));

                return absA == absB;
            }

            public int GetHashCode(Vector2 obj)
            {
                Vector2 abs = new(MathF.Abs(obj.X), MathF.Abs(obj.Y));
                return abs.GetHashCode();
            }
        }
    }
}
