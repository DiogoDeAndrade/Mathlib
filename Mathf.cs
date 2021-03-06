﻿using System;

namespace Mathlib
{
    public static class Mathf
    {
#if NET5_0
        public static float PI = MathF.PI;

        public static float Sin(float a) => MathF.Sin(a);
        public static float Cos(float a) => MathF.Cos(a);
        public static float Tan(float a) => MathF.Tan(a); 
        public static float Asin(float a) => MathF.Asin(a);
        public static float Abs(float v) => MathF.Abs(v);
        public static float Min(float a, float b) => MathF.Min(a, b);
        public static float Max(float a, float b) => MathF.Max(a, b);
        public static float Sqrt(float v) => MathF.Sqrt(v);
        public static float Floor(float v) => MathF.Floor(v);
        public static int FloorToInt(float v) => (int)MathF.Floor(v);
        public static int CeilToInt(float v) => (int)MathF.Ceiling(v);
        public static float Round(float v) => MathF.Round(v);
        public static float Atan2(float y, float x) => MathF.Atan2(y, x);
        public static float CopySign(float x, float y) => MathF.CopySign(x, y);
        public static float Pow(float x, float y) => MathF.Pow(x, y);
        public static float Fract(float v) => v - (float)MathF.Truncate(v);
#else
        public static float PI = (float)Math.PI;

        public static float Sin(float a) => (float)Math.Sin(a);
        public static float Cos(float a) => (float)Math.Cos(a);
        public static float Tan(float a) => (float)Math.Tan(a);
        public static float Asin(float a) => (float)Math.Asin(a);
        public static float Abs(float v) => (float)Math.Abs(v);
        public static float Min(float a, float b) => (float)Math.Min(a, b);
        public static float Max(float a, float b) => (float)Math.Max(a, b);
        public static int Min(int a, int b) => Math.Min(a, b);
        public static int Max(int a, int b) => Math.Max(a, b);
        public static float Sqrt(float v) => (float)Math.Sqrt(v);
        public static float Floor(float v) => (float)Math.Floor(v);
        public static int FloorToInt(float v) => (int)Math.Floor(v);
        public static int CeilToInt(float v) => (int)Math.Ceiling(v);
        public static float Round(float v) => (float)Math.Round(v);
        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);
        public static float CopySign(float x, float y) => (float)((y < 0) ? (-Math.Abs(x)) : (Math.Abs(x)));
        public static float Pow(float x, float y) => (float)Math.Pow(x, y);
        public static float Fract(float v) => v - (float)Math.Truncate(v);
#endif

        public static float Deg2Rad = (PI * 2.0f) / 360.0f;
        public static float Rad2Deg = 360.0f / (PI * 2.0f);
        public static int Clamp(int v, int a, int b) => (v < a) ? (a) : ((v > b) ? (b) : (v));
        public static float Clamp(float v, float a, float b) => (v < a) ? (a) : ((v > b) ? (b) : (v));
        public static float Clamp01(float v) => (v < 0) ? (0) : ((v > 1) ? (1) : (v));
        public static byte Min(byte a, byte b) => (a < b) ? (a) : (b);
        public static byte Max(byte a, byte b) => (a > b) ? (a) : (b);

        public static float Lerp(float v1, float v2, float t) => v1 + (v2 - v1) * t;

        // Perlin noise implementation (taken from Unity: https://github.com/Unity-Technologies/UnityCsReference/blob/master/Modules/TreeEditor/Includes/Perlin.cs)
        public static float Perlin(Vector2 p) => Perlin(p.x, p.y);
        public static float Perlin(Vector3 p) => Perlin(p.x, p.y, p.z);

        public static float Perlin(float x)
        {
            int bx0, bx1;
            float rx0, rx1, sx, u, v;

            if (!perlinInit) InitPerlin();
            setup(x, out bx0, out bx1, out rx0, out rx1);

            sx = s_curve(rx0);
            u = rx0 * g1[p[bx0]];
            v = rx1 * g1[p[bx1]];

            return (lerp(sx, u, v));
        }

        public static float Perlin(float x, float y)
        {
            int bx0, bx1, by0, by1, b00, b10, b01, b11;
            float rx0, rx1, ry0, ry1, sx, sy, a, b, u, v;
            int i, j;

            if (!perlinInit) InitPerlin();
            setup(x, out bx0, out bx1, out rx0, out rx1);
            setup(y, out by0, out by1, out ry0, out ry1);

            i = p[bx0];
            j = p[bx1];

            b00 = p[i + by0];
            b10 = p[j + by0];
            b01 = p[i + by1];
            b11 = p[j + by1];

            sx = s_curve(rx0);
            sy = s_curve(ry0);

            u = at2(rx0, ry0, g2[b00, 0], g2[b00, 1]);
            v = at2(rx1, ry0, g2[b10, 0], g2[b10, 1]);
            a = lerp(sx, u, v);

            u = at2(rx0, ry1, g2[b01, 0], g2[b01, 1]);
            v = at2(rx1, ry1, g2[b11, 0], g2[b11, 1]);
            b = lerp(sx, u, v);

            return lerp(sy, a, b);
        }

        public static float Perlin(float x, float y, float z)
        {
            int bx0, bx1, by0, by1, bz0, bz1, b00, b10, b01, b11;
            float rx0, rx1, ry0, ry1, rz0, rz1, sy, sz, a, b, c, d, t, u, v;
            int i, j;

            if (!perlinInit) InitPerlin();
            setup(x, out bx0, out bx1, out rx0, out rx1);
            setup(y, out by0, out by1, out ry0, out ry1);
            setup(z, out bz0, out bz1, out rz0, out rz1);

            i = p[bx0];
            j = p[bx1];

            b00 = p[i + by0];
            b10 = p[j + by0];
            b01 = p[i + by1];
            b11 = p[j + by1];

            t = s_curve(rx0);
            sy = s_curve(ry0);
            sz = s_curve(rz0);

            u = at3(rx0, ry0, rz0, g3[b00 + bz0, 0], g3[b00 + bz0, 1], g3[b00 + bz0, 2]);
            v = at3(rx1, ry0, rz0, g3[b10 + bz0, 0], g3[b10 + bz0, 1], g3[b10 + bz0, 2]);
            a = lerp(t, u, v);

            u = at3(rx0, ry1, rz0, g3[b01 + bz0, 0], g3[b01 + bz0, 1], g3[b01 + bz0, 2]);
            v = at3(rx1, ry1, rz0, g3[b11 + bz0, 0], g3[b11 + bz0, 1], g3[b11 + bz0, 2]);
            b = lerp(t, u, v);

            c = lerp(sy, a, b);

            u = at3(rx0, ry0, rz1, g3[b00 + bz1, 0], g3[b00 + bz1, 2], g3[b00 + bz1, 2]);
            v = at3(rx1, ry0, rz1, g3[b10 + bz1, 0], g3[b10 + bz1, 1], g3[b10 + bz1, 2]);
            a = lerp(t, u, v);

            u = at3(rx0, ry1, rz1, g3[b01 + bz1, 0], g3[b01 + bz1, 1], g3[b01 + bz1, 2]);
            v = at3(rx1, ry1, rz1, g3[b11 + bz1, 0], g3[b11 + bz1, 1], g3[b11 + bz1, 2]);
            b = lerp(t, u, v);

            d = lerp(sy, a, b);

            return lerp(sz, c, d);
        }

        // Helpers for Perlin noise
        const int B = 0x100;
        const int BM = 0xff;
        const int N = 0x1000;

        static bool  perlinInit = false;
        static int[] p = new int[B + B + 2];
        static float[,] g3 = new float[B + B + 2, 3];
        static float[,] g2 = new float[B + B + 2, 2];
        static float[] g1 = new float[B + B + 2];

        static float s_curve(float t)
        {
            return t * t * (3.0F - 2.0F * t);
        }

        static float lerp(float t, float a, float b)
        {
            return a + t * (b - a);
        }

        static void setup(float value, out int b0, out int b1, out float r0, out float r1)
        {
            float t = value + N;
            b0 = ((int)t) & BM;
            b1 = (b0 + 1) & BM;
            r0 = t - (int)t;
            r1 = r0 - 1.0F;
        }

        static float at2(float rx, float ry, float x, float y) { return rx * x + ry * y; }
        static float at3(float rx, float ry, float rz, float x, float y, float z) { return rx * x + ry * y + rz * z; }

        static void normalize2(ref float x, ref float y)
        {
            float s;

            s = (float)Math.Sqrt(x * x + y * y);
            x = x / s;
            y = y / s;
        }

        static void normalize3(ref float x, ref float y, ref float z)
        {
            float s;
            s = (float)Math.Sqrt(x * x + y * y + z * z);
            x = x / s;
            y = y / s;
            z = z / s;
        }

        static void InitPerlin()
        {
            int i, j, k;
            System.Random rnd = new System.Random(0);

            for (i = 0; i < B; i++)
            {
                p[i] = i;
                g1[i] = (float)(rnd.Next(B + B) - B) / B;

                for (j = 0; j < 2; j++)
                    g2[i, j] = (float)(rnd.Next(B + B) - B) / B;
                normalize2(ref g2[i, 0], ref g2[i, 1]);

                for (j = 0; j < 3; j++)
                    g3[i, j] = (float)(rnd.Next(B + B) - B) / B;


                normalize3(ref g3[i, 0], ref g3[i, 1], ref g3[i, 2]);
            }

            while (--i != 0)
            {
                k = p[i];
                p[i] = p[j = rnd.Next(B)];
                p[j] = k;
            }

            for (i = 0; i < B + 2; i++)
            {
                p[B + i] = p[i];
                g1[B + i] = g1[i];
                for (j = 0; j < 2; j++)
                    g2[B + i, j] = g2[i, j];
                for (j = 0; j < 3; j++)
                    g3[B + i, j] = g3[i, j];
            }

            perlinInit = true;
        }
    }
}
