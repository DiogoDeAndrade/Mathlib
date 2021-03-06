﻿
namespace Mathlib
{
    public struct Color
    {
        public float r, g, b, a;

        public Color(float v)
        {
            r = v;
            g = v;
            b = v;
            a = 1;
        }

        public Color(float r, float g, float b, float a = 1.0f)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public void Set(float r, float g, float b, float a = 1.0f)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static explicit operator Color(Color32 v)
        {
            return new Color(v.r / 255.0f, v.g / 255.0f, v.b / 255.0f, v.a / 255.0f);
        }
        public static implicit operator Color(Vector3 v)
        {
            return new Color(v.x, v.y, v.z, 1.0f);
        }
        public static implicit operator Color(Vector4 v)
        {
            return new Color(v.x, v.y, v.z, v.w);
        }
        public static Color operator +(Color a, Color b) => new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        public static Color operator -(Color a, Color b) => new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
        public static Color operator -(Color a, float v) => new Color(a.r - v, a.g - v, a.b - v, a.a - v);
        public static Color operator *(Color v, float s) => new Color(v.r * s, v.g * s, v.b * s, v.a * s);
        public static Color operator *(float s, Color v) => new Color(v.r * s, v.g * s, v.b * s, v.a * s);
        public static Color operator *(Color c1, Color c2) => new Color(c1.r * c2.r, c1.g * c2.g, c1.b * c2.b, c1.a * c2.a);
        public static Color operator /(Color v, float s) => new Color(v.r / s, v.g / s, v.b / s, v.a / s);

        public Color saturated => new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), Mathf.Clamp01(a));
        public bool needsHDR => (r < 0) || (r > 1) || (g < 0) || (g > 1) || (b < 0) || (b > 1) || (a < 0) || (a > 1);

        public float magnitude
        {
            get { return Mathf.Sqrt(r * r + g * g + b * b + a * a); }
        }

        public Color normalized
        {
            get { return this * (1.0f / magnitude); }
        }

        public override string ToString()
        {
            return $"({r:F3},{g:F3},{b:F3},{a:F3})";
        }

        public static Color Lerp(Color c1, Color c2, float t)
        {
            return c1 + (c2 - c1) * t;
        }

        public static Color Min(Color c1, Color c2)
        {
            return new Color(Mathf.Min(c1.r, c2.r), Mathf.Min(c1.g, c2.g), Mathf.Min(c1.b, c2.b), Mathf.Min(c1.a, c2.a));
        }

        public static Color Max(Color c1, Color c2)
        {
            return new Color(Mathf.Max(c1.r, c2.r), Mathf.Max(c1.g, c2.g), Mathf.Max(c1.b, c2.b), Mathf.Max(c1.a, c2.a));
        }

        public static Color black = new Color(0, 0, 0, 1);
        public static Color white = new Color(1, 1, 1, 1);
        public static Color grey = new Color(0.5f, 0.5f, 0.5f, 1);
        public static Color red = new Color(1, 0, 0, 1);
        public static Color green = new Color(0, 1, 0, 1);
        public static Color blue = new Color(0, 0, 1, 1);
        public static Color cyan = new Color(0, 1, 1, 1);
        public static Color magenta = new Color(1, 0, 1, 1);
        public static Color yellow = new Color(1, 1, 0, 1);
        public static Color unityBlue = new Color(0.34f, 0.42f, 0.56f, 1.0f);
    }
}
