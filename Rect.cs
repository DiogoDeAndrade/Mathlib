
namespace Mathlib
{
    // Basic implementation of a Rectangle
    public struct Rect
    {
        public float x1;
        public float y1;
        public float x2;
        public float y2;

        public Vector2 min => new Vector2(x1, y1);
        public Vector2 max => new Vector2(x2, y2);

        public Rect(float x1, float y1, float x2, float y2)
        {
            this.x1 = Mathf.Min(x1, x2);
            this.y1 = Mathf.Min(y1, y2);
            this.x2 = Mathf.Max(x1, x2);
            this.y2 = Mathf.Max(y1, y2);
        }
        public Rect(Vector2 p)
        {
            x1 = p.x;
            y1 = p.y;
            x2 = p.x;
            y2 = p.y;
        }
        public Rect(Vector2 p1, Vector2 p2)
        {
            x1 = Mathf.Min(p1.x, p2.x);
            y1 = Mathf.Min(p1.y, p2.y);
            x2 = Mathf.Max(p1.x, p2.x);
            y2 = Mathf.Max(p1.y, p2.y);
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

        public bool Contains(Vector2 p) => Contains(p.x, p.y);
        public bool Contains(float x, float y)
        {
            if ((x1 <= x) && (x2 >= x) &&
                (y1 <= y) && (y2 >= y))
            {
                return true;
            }

            return false;
        }

        public bool ContainsMinInclusive(Vector2 p) => ContainsMinInclusive(p.x, p.y);
        public bool ContainsMinInclusive(float x, float y)
        {
            if ((x1 <= x) && (x2 > x) &&
                (y1 <= y) && (y2 > y))
            {
                return true;
            }

            return false;
        }

        public float Distance(Vector2 p) => Mathf.Sqrt(DistanceSqr(p));
        public float DistanceSqr(Vector2 p)
        {
            var xDist = MinXDistance(p);
            var yDist = MinYDistance(p);
            if (Mathf.Abs(xDist) < 0.0001f)
            {
                return yDist;
            }
            else if (Mathf.Abs(yDist) < 0.0001f)
            {
                return xDist;
            }

            return xDist * xDist + yDist * yDist;
        }

        private float MinXDistance(Vector2 p)
        {
            if (x1 > p.x)
            {
                return x1 - p.x;
            }
            else if (x2 < p.x)
            {
                return p.x - x2;
            }
            else
            {
                return 0;
            }
        }

        private float MinYDistance(Vector2 p)
        {
            if (y1 > p.y)
            {
                return y1 - p.y;
            }
            else if (y2 < p.y)
            {
                return p.y - y2;
            }
            else
            {
                return 0;
            }
        }
    }
}
