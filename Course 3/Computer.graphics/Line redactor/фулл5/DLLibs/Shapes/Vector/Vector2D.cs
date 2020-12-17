using DLLibs.Enums;
using DLLibs.Graphics._2D;
using DLLibs.Shapes.Dots;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace DLLibs.Shapes.Vector
{
    [Serializable]
    public class Vector2D: IShape
    {
        public IDot StartPoint { get; set; }
        public IDot EndPoint { get; set; }
        public string Name { get; set; }
        public bool IsInGroup { get; set; }
        public float Length
        {
            get
            {
                var dotStart = (StartPoint as Dot2D);
                if (dotStart == null)
                {
                    throw new ArgumentNullException("Start point of the vector is not Dot2D class.");
                }

                var dotEnd = (EndPoint as Dot2D);
                if (dotEnd == null)
                {
                    throw new ArgumentNullException("End point of the vector is not Dot2D class.");
                }

                return (float)Sqrt(Pow(dotEnd.Y - dotStart.Y, 2) + Pow(dotEnd.X - dotStart.X, 2));
            }
        }

        public Vector2D(string name, IDot startPoint, IDot endPoint)
        {
            Name = name;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public Vector2D(IDot startPoint, IDot endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public void Draw(System.Drawing.Graphics graphics, Pen pen)
        {
            var points = new Point[] { StartPoint as Dot2D, EndPoint as Dot2D };
            graphics.DrawLines(pen, points);
        }

        public VectorActions GetCrossingType(Point point, float sensitivity)
        {
            var FromStartPointLen = new Vector2D(StartPoint, (Dot2D)point).Length;
            var FromEndPointLen = new Vector2D(EndPoint, (Dot2D)point).Length;

            if (FromStartPointLen <= FromEndPointLen &&
                FromStartPointLen <= sensitivity) return VectorActions.StartPoint;
            if (FromEndPointLen <= sensitivity) return VectorActions.EndPoint;
            if (GetDistance((Dot2D)point) <= sensitivity && FromStartPointLen + FromEndPointLen <= Length + sensitivity) return VectorActions.Body;

            return VectorActions.None;
        }

        public float GetDistance(IDot point) => GetSpace(point) * 2 / Length;

        public float GetSpace(IDot point) => 1f / 2 * Abs((EndPoint.Y - StartPoint.Y) * point.X - (EndPoint.X - StartPoint.X) * point.Y + EndPoint.X * StartPoint.Y - EndPoint.Y * StartPoint.X);

        public override string ToString() => $"{{{StartPoint.Y - EndPoint.Y}, {EndPoint.X - StartPoint.X}, {StartPoint.X * EndPoint.Y - EndPoint.X * StartPoint.Y}}}";

        public void Offset(OffsetTypes offsetType, params float[] offsets)
        {
            var result = Transformer2D.Offset(offsets, new IDot[] { StartPoint, EndPoint });
            StartPoint = result[0];
            EndPoint = result[1];
        }
    }
}
