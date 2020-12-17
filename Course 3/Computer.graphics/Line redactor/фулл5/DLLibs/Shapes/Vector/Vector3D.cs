using DLLibs.Enums;
using DLLibs.Graphics;
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
    public class Vector3D: IShape
    {
        public IDot StartPoint { get; set; }
        public IDot EndPoint { get; set; }
        public string Name { get; set; }
        public bool IsInGroup { get; set; }
        public float Length
        {
            get
            {
                var dotStart = (StartPoint as Dot3D);
                if (dotStart == null)
                {
                    throw new ArgumentNullException("Start point of the vector is not Dot3D class.");
                }

                var dotEnd = (EndPoint as Dot3D);
                if (dotEnd == null)
                {
                    throw new ArgumentNullException("End point of the vector is not Dot3D class.");
                }

                return (float)Sqrt(Pow(dotEnd.Y - dotStart.Y, 2) + Pow(dotEnd.X - dotStart.X, 2));
            }
        }

        public Vector3D(string name, IDot startPoint, IDot endPoint)
        {
            Name = name;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public Vector3D(IDot startPoint, IDot endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public void Draw(System.Drawing.Graphics graphics, Pen pen)
        {
            var points = new Point[] { StartPoint as Dot3D, EndPoint as Dot3D };
            graphics.DrawLines(pen, points);
        }

        public VectorActions GetCrossingType(Point point, float sensitivity)
        {
            var FromStartPointLen = new Vector3D(StartPoint, (Dot3D)point).Length;
            var FromEndPointLen = new Vector3D(EndPoint, (Dot3D)point).Length;

            if (FromStartPointLen <= FromEndPointLen &&
                FromStartPointLen <= sensitivity) return VectorActions.StartPoint;
            if (FromEndPointLen <= sensitivity) return VectorActions.EndPoint;
            if (GetDistance((Dot3D)point) <= sensitivity && FromStartPointLen + FromEndPointLen <= Length + sensitivity) return VectorActions.Body;

            return VectorActions.None;
        }

        public float GetDistance(IDot point) => GetSpace(point) * 2 / Length;

        public float GetSpace(IDot point) => 1f / 2 * Abs((EndPoint.Y - StartPoint.Y) * point.X - (EndPoint.X - StartPoint.X) * point.Y + EndPoint.X * StartPoint.Y - EndPoint.Y * StartPoint.X);

        public override string ToString() => $"(X - {StartPoint.X})/{EndPoint.X - EndPoint.Y} = (Y - {StartPoint.Y})/{EndPoint.Y - StartPoint.Y} = (Z - {StartPoint.Z})/{EndPoint.Z - StartPoint.Z}";

        public void Offset(OffsetTypes offsetType, params float[] offsets)
        {
            var result = new IDot[2];
            switch (offsetType)
            {
                case OffsetTypes.Usual:
                    {
                        result = Transformer3D.Offset(offsets, new IDot[] { StartPoint, EndPoint });
                        break;
                    }
                case OffsetTypes.MatrixOffset:
                    {
                        result = Transformer3D.Action(offsets, new IDot[] { StartPoint, EndPoint });
                        break;
                    }
                case OffsetTypes.HouseOffset:
                    {
                        result = Transformer3D.HomeMoving(offsets[0], offsets[1], offsets[2], new IDot[] { StartPoint, EndPoint });
                        break;
                    }

            }
            
            StartPoint = result[0];
            EndPoint = result[1];

            StartPoint.Normalise();
            EndPoint.Normalise();
        }
    }
}
