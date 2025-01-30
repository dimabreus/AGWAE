using System.Numerics;

namespace AGWAE.Physics
{
    public static class SAT
    {
        public static bool CheckCollision(Shape shapeA, Shape shapeB, out Vector2 mtv)
        {
            mtv = Vector2.Zero;

            float overlap = float.MaxValue;
            Vector2 smallest = Vector2.Zero;
            Vector2[] axesA = shapeA.GetAxes();
            Vector2[] axesB = shapeB.GetAxes();

            Vector2 centerA = GetShapeCenter(shapeA);
            Vector2 centerB = GetShapeCenter(shapeB);
            Vector2 direction = centerB - centerA;

            foreach (Vector2 axis in axesA.Concat(axesB))
            {
                Projection projectionA = Projection.ProjectOntoAxis(axis, shapeA);
                Projection projectionB = Projection.ProjectOntoAxis(axis, shapeB);

                if (!projectionA.Overlaps(projectionB))
                {
                    return false;
                }

                float o = (float)projectionA.GetOverlap(projectionB);

                if (o < overlap)
                {
                    overlap = o;
                    smallest = axis;
                }
            }

            if (Vector2.Dot(smallest, direction) < 0)
            {
                smallest = -smallest; // Invert if directed inward
            }

            mtv = Vector2.Normalize(smallest) * overlap;

            return true;
        }

        private static Vector2 GetShapeCenter(Shape shape)
        {
            Vector2 center = Vector2.Zero;
            foreach (var vertex in shape.Vertices)
            {
                center += vertex;
            }
            return center / shape.Vertices.Length;
        }
    }
}
