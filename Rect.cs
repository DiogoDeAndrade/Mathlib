
namespace Mathlib
{
    // Basic implementation of a Rectangle
    public struct Rect
    {
        public float x1;
        public float y1;
        public float x2;
        public float y2;

        public Rect(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
        public Rect(Vector2 p)
        {
            x1 = p.x;
            y1 = p.y;
            x2 = p.x;
            y2 = p.y;
        }
        public override string ToString()
        {
            return $"[{x1:F3},{y1:F3},{x2:F3},{y2:F3}]";
        }

        public float width
        {
            get { return Mathf.Abs(x2 - x1); }
        }
        public float height
        {
            get { return Mathf.Abs(y2 - y1); }
        }

        public void ExtendTo(Vector2 p)
        {
            x1 = Mathf.Min(x1, p.x);
            y1 = Mathf.Min(y1, p.y);
            x2 = Mathf.Max(x2, p.x);
            y2 = Mathf.Max(y2, p.y);
        }
    }
}
