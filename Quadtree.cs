
using System;
using System.Collections.Generic;

namespace Mathlib
{
    public class Quadtree<T> where T : class
    {
        public delegate bool SelectionCriteria(T value);

        Vector2 min;
        Vector2 max;
        int nLevels;

        struct LeafObject
        {
            public Vector2 pos;
            public T value;
        }

        class Node
        {
            public Node parent;
            public bool isLeaf;
            public Rect rect;
            public List<LeafObject> objects;
            public Node[] children;
        }

        Node rootNode;

        public Quadtree(Vector2 min, Vector2 max, int nLevels)
        {
            this.min = min;
            this.max = max;
            this.nLevels = nLevels;

            rootNode = Init(new Rect(min, max), nLevels);
        }

        public void Add(Vector2 position, T value) => Add(position.x, position.y, value);
        public void Add(float x, float y, T value)
        {
            var leafNode = GetLeafNode(rootNode, x, y);
            if (leafNode != null)
            {
                leafNode.objects.Add(new LeafObject { pos = new Vector2(x, y), value = value });
            }
        }

        public void Remove(T value)
        {
            Remove(rootNode, value);
        }


        public List<T> GetObjectsInCircle(float x, float y, float radius) => GetObjectsInCircle(new Vector2(x, y), radius);
        public List<T> GetObjectsInCircle(Vector2 p, float radius)
        {
            List<T> ret = new List<T>();

            GetObjectsInCircle(rootNode, p, radius, ret);

            return ret;
        }

        public T GetClosestObjectInCircle(float x, float y, float radius) => GetClosestObjectInCircle(new Vector2(x, y), radius);
        public T GetClosestObjectInCircle(Vector2 p, float radius)
        {
            T ret = default(T);
            float minSqrDist = radius * radius;

            GetObjectsInCircle(rootNode, p, radius, ref ret, ref minSqrDist);

            return ret;
        }
        public T GetClosestObjectInCircle(float x, float y, float radius, SelectionCriteria criteria) => GetClosestObjectInCircle(new Vector2(x, y), radius, criteria);
        public T GetClosestObjectInCircle(Vector2 p, float radius, SelectionCriteria criteria)
        {
            T ret = default(T);
            float minSqrDist = radius * radius;

            GetObjectsInCircle(rootNode, p, radius, ref ret, ref minSqrDist, criteria);

            return ret;
        }

        void GetObjectsInCircle(Node node, Vector2 p, float radius, List<T> ret)
        {
            bool includeThis = node.rect.Contains(p);

            if (!includeThis)
            {
                float dist = node.rect.DistanceSqr(p);
                if (dist <= radius * radius)
                {
                    includeThis = true;
                }
            }

            if (includeThis)
            {
                if (node.isLeaf)
                {
                    float r2 = radius * radius;

                    foreach (var obj in node.objects)
                    {
                        if (obj.pos.DistanceSqr(p) <= r2)
                        {
                            ret.Add(obj.value);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        GetObjectsInCircle(node.children[i], p, radius, ret);
                    }
                }
            }
        }


        void GetObjectsInCircle(Node node, Vector2 p, float radius, ref T ret, ref float minSqrDist)
        {
            bool includeThis = node.rect.Contains(p);

            if (!includeThis)
            {
                float dist = node.rect.DistanceSqr(p);
                if (dist <= radius * radius)
                {
                    includeThis = true;
                }
            }

            if (includeThis)
            {
                if (node.isLeaf)
                {
                    float r2 = radius * radius;

                    foreach (var obj in node.objects)
                    {
                        float d = obj.pos.DistanceSqr(p);
                        if (d <= minSqrDist)
                        {
                            ret = obj.value;
                            minSqrDist = d;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        GetObjectsInCircle(node.children[i], p, radius, ref ret, ref minSqrDist);
                    }
                }
            }
        }

        void GetObjectsInCircle(Node node, Vector2 p, float radius, ref T ret, ref float minSqrDist, SelectionCriteria criteria)
        {
            bool includeThis = node.rect.Contains(p);

            if (!includeThis)
            {
                float dist = node.rect.DistanceSqr(p);
                if (dist <= radius * radius)
                {
                    includeThis = true;
                }
            }

            if (includeThis)
            {
                if (node.isLeaf)
                {
                    float r2 = radius * radius;

                    foreach (var obj in node.objects)
                    {
                        if (!criteria(obj.value)) continue;

                        float d = obj.pos.DistanceSqr(p);
                        if (d <= minSqrDist)
                        {
                            ret = obj.value;
                            minSqrDist = d;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        GetObjectsInCircle(node.children[i], p, radius, ref ret, ref minSqrDist, criteria);
                    }
                }
            }
        }

        Node GetLeafNode(Node node, float x, float y)
        {
            if (node.rect.ContainsMinInclusive(x, y))
            {
                if (node.isLeaf) return node;
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var ret = GetLeafNode(node.children[i], x, y);
                        if (ret != null) return ret;
                    }
                }
            }

            return null;
        }

        void Remove(Node node, T value)
        {
            if (node.isLeaf)
            {
                node.objects.RemoveAll((o) => o.value == value);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Remove(node.children[i], value);                    
                }
            }
        }

        Node Init(Rect r, int nLevels, Node parent = null)
        {
            Node n = new Node
            {
                isLeaf = (nLevels == 0),
                rect = r,
                parent = parent
            };

            if (n.isLeaf)
            {
                n.objects = new List<LeafObject>();
            }
            else
            {
                n.children = new Node[4];

                float w2 = r.width * 0.5f;
                float h2 = r.height * 0.5f;

                n.children[0] = Init(new Rect(r.x1, r.y1, r.x1 + w2, r.y1 + h2), nLevels - 1, n);
                n.children[1] = Init(new Rect(r.x1 + w2, r.y1, r.x2, r.y1 + h2), nLevels - 1, n);
                n.children[2] = Init(new Rect(r.x1, r.y1 + h2, r.x1 + w2, r.y2), nLevels - 1, n);
                n.children[3] = Init(new Rect(r.x1 + w2, r.y1 + h2, r.x2, r.y2), nLevels - 1, n);
            }

            return n;
        }
    }
}
