using SFML.Graphics;
using SFML.Window;

namespace AGWAE
{
    internal abstract class Scene
    {
        public abstract string Name { get; protected set; }

        public virtual void Start() { }
        public virtual void Update(HashSet<Keyboard.Key> pressedKeys) { }
        public virtual void FixedUpdate(HashSet<Keyboard.Key> pressedKeys) { }
        public virtual void HandleClick(MouseButtonEventArgs e) { }
        public virtual void HandleClickRelease(MouseButtonEventArgs e) { }
        public virtual void HandleMouseMoved(MouseMoveEventArgs e) { }
        public abstract void Draw(RenderWindow window);
    }
}
