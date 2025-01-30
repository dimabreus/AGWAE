using AGWAE.Physics;
using SFML.Graphics;
using System.Numerics;

namespace AGWAE
{
    internal class RigidbodyObject : ColliderObject
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public float Friction { get; set; } = 0.9f;
        public float Gravity { get; set; } = 98.0665f;
        public float Bounciness { get; set; } = 0f;
        public float Mass { get; set; } = 1f;

        public bool IsGrounded { get; private set; } = false;

        public RigidbodyObject(Vector2 position, Quaternion rotation, Vector2 scale, Layer layer, Sprite sprite, Physics.Shape collider)
            : base(position, rotation, scale, layer, sprite, collider)
        {
        }

        public RigidbodyObject(Vector2 position, Quaternion rotation, Vector2 scale, Layer layer, Sprite sprite)
            : base(position, rotation, scale, layer, sprite)
        {
        }

        public RigidbodyObject(Vector2 position, Vector2 scale, Layer layer, Sprite sprite)
            : base(position, scale, layer, sprite)
        {
        }

        public RigidbodyObject(Vector2 position, Layer layer, Sprite sprite)
            : base(position, layer, sprite)
        {
        }


        public RigidbodyObject(Vector2 position, Layer layer, Sprite sprite, Physics.Shape collider)
            : base(position, layer, sprite, collider)
        {
        }

        public void UpdatePhysics(IEnumerable<ColliderObject> colliders)
        {
            Vector2 nextPosition = Position + Velocity * Time.DeltaTime;

            FloatRect nextColliderRect = new(
                nextPosition.X,
                nextPosition.Y,
                Size.X,
                Size.Y
            );

            Physics.Shape nextCollider = new(nextColliderRect);

            IsGrounded = false;
            bool isBounced = false;  // Only 1 bounce per frame.

            ColliderObject? objectOnStand = null;

            foreach (var collider in colliders)
            {
                if (collider == this) continue;

                if (SAT.CheckCollision(nextCollider, collider.Collider, out Vector2 mtv))
                {
                    if (mtv.Y > 0) // If MTV points up (we collided with the top of another object)
                    {
                        objectOnStand = collider;
                        continue; // Let all collisions be checked and applied, and only after that check if the object is still grounded.
                    }

                    ProcessCollision(mtv, ref nextCollider, ref nextPosition, ref isBounced);
                }
            }

            if (objectOnStand != null && SAT.CheckCollision(nextCollider, objectOnStand.Collider, out Vector2 mtv2))
            {
                ProcessCollision(mtv2, ref nextCollider, ref nextPosition, ref isBounced);
            }

            if (IsGrounded)
            {
                // If the object is on the ground, apply horizontal friction
                if (Velocity.X != 0)
                {
                    // Smart friction (Disabled as a game simplification factor)
                    //float frictionForce = Friction * Mass; // Friction depends on mass
                    //float friction = Math.Sign(Velocity.X) * Math.Min(Math.Abs(Velocity.X), frictionForce);
                    float friction = Friction;
                    Velocity = new Vector2(Velocity.X - friction, Velocity.Y);
                }
            }
            else // If the object is not on the ground, apply gravity
            {
                float gravityForce = Gravity * Mass;
                Velocity = new Vector2(Velocity.X, Velocity.Y + gravityForce * Time.DeltaTime);
            }

            Position = nextPosition;
        }

        private void ProcessCollision(Vector2 mtv, ref Physics.Shape nextCollider, ref Vector2 nextPosition, ref bool isBounced)
        {
            nextPosition -= mtv;

            FloatRect nextColliderRect = new(
                nextPosition.X,
                nextPosition.Y,
                Size.X,
                Size.Y
            );

            nextCollider = new(nextColliderRect);

            if (mtv.Y != 0) // If MTV points down or up
            {
                // Reset vertical velocity, accounting for bounce
                Velocity = new Vector2(Velocity.X, isBounced ? Velocity.Y : -Velocity.Y * Bounciness);
                isBounced = true;

                if (mtv.Y > 0) // If MTV points up (we collided with the top of another object)
                {
                    IsGrounded = true;
                }
            }

            if (mtv.X != 0) // If MTV points left or right
            {
                // Reset horizontal velocity, accounting for bounce
                Velocity = new Vector2(isBounced ? Velocity.X : -Velocity.X * Bounciness, Velocity.Y);
                isBounced = true;
            }
        }

    }
}
