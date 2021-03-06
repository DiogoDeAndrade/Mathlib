﻿
using System.Runtime.InteropServices;

namespace Mathlib
{
    // Need to force the layout since this is used to copy data to SDL
    [StructLayout(LayoutKind.Sequential)]
    public struct Color32
    {
        public byte b;
        public byte g;
        public byte r;
        public byte a;

        public Color32(byte r, byte g, byte b, byte a = 255)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public void Set(byte r, byte g, byte b, byte a = 255)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static explicit operator Color32(Color v)
        {
            return new Color32((byte)(v.r * 255.0f), (byte)(v.g * 255.0f), (byte)(v.b * 255.0f), (byte)(v.a * 255.0f));
        }

        public override string ToString()
        {
            return $"({r},{g},{b},{a})";
        }

        static public Color32 AddClamped(Color32 a, Color32 b)
        {
            return new Color32
            {
                r = (byte)Mathf.Clamp(((int)a.r + (int)b.r), 0, 255),
                g = (byte)Mathf.Clamp(((int)a.g + (int)b.g), 0, 255),
                b = (byte)Mathf.Clamp(((int)a.b + (int)b.b), 0, 255),
                a = (byte)Mathf.Clamp(((int)a.a + (int)b.a), 0, 255)
            };
        }

        static public Color32 SubClamped(Color32 a, Color32 b)
        {
            return new Color32
            {
                r = (byte)Mathf.Clamp(((int)a.r - (int)b.r), 0, 255),
                g = (byte)Mathf.Clamp(((int)a.g - (int)b.g), 0, 255),
                b = (byte)Mathf.Clamp(((int)a.b - (int)b.b), 0, 255),
                a = (byte)Mathf.Clamp(((int)a.a - (int)b.a), 0, 255)
            };
        }
        public static Color32 Min(Color32 c1, Color32 c2)
        {
            return new Color32(Mathf.Min(c1.r, c2.r), Mathf.Min(c1.g, c2.g), Mathf.Min(c1.b, c2.b), Mathf.Min(c1.a, c2.a));
        }

        public static Color32 Max(Color32 c1, Color32 c2)
        {
            return new Color32(Mathf.Max(c1.r, c2.r), Mathf.Max(c1.g, c2.g), Mathf.Max(c1.b, c2.b), Mathf.Max(c1.a, c2.a));
        }

        public static Color32 Lerp(Color32 c1, Color32 c2, float t)
        {
            float tt = t / 255.0f;
            return new Color32((byte)((int)c1.r + ((int)c2.r - (int)c1.r) * tt),
                               (byte)((int)c1.g + ((int)c2.g - (int)c1.g) * tt),
                               (byte)((int)c1.b + ((int)c2.b - (int)c1.b) * tt),
                               (byte)((int)c1.a + ((int)c2.a - (int)c1.a) * tt));
        }

        public static Color32 black = new Color32(0, 0, 0, 255);
        public static Color32 white = new Color32(255, 255, 255, 255);
        public static Color32 grey = new Color32(128, 128, 128, 255);
        public static Color32 red = new Color32(255, 0, 0, 255);
        public static Color32 green = new Color32(0, 255, 0, 255);
        public static Color32 blue = new Color32(0, 0, 255, 255);
        public static Color32 cyan = new Color32(0, 255, 255, 255);
        public static Color32 magenta = new Color32(255, 0, 255, 255);
        public static Color32 yellow = new Color32(255, 255, 0, 255);
    }
}
