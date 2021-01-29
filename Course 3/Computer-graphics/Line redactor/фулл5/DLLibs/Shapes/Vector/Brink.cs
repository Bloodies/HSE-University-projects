using DLLibs.Enums;
using DLLibs.Shapes.Dots;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLibs.Shapes.Vector
{
    public class Brink : IShape
    {
        public List<IShape> Data { get; } = new List<IShape>();
        public float A { get; private set; }
        public float B { get; private set; }
        public float C { get; private set; }
        public float D { get; private set; }
        private string Name;

        public Brink(string name)
        {
            Name = name;
        }

        public void FindEquation(bool turnOn3d)
        {
            if (Data.Count < 3) return;
            if (turnOn3d)
            {
                var first = (Dot3D)((Vector3D)Data[0]).StartPoint;
                var second = (Dot3D)((Vector3D)Data[0]).EndPoint;
                var third = default(Dot3D);
                foreach (var shape in Data)
                {
                    var startPoint = (Dot3D)((Vector3D)shape).StartPoint;
                    if (
                        (
                            startPoint.X != first.X ||
                            startPoint.Y != first.Y ||
                            startPoint.LocalZ != first.LocalZ
                        ) &&
                        (
                            startPoint.X != second.X ||
                            startPoint.Y != second.Y ||
                            startPoint.LocalZ != second.LocalZ
                        )
                       )
                    {
                        third = startPoint;
                        break;
                    }
                    var endPoint = (Dot3D)((Vector3D)shape).EndPoint;
                    if (
                        (
                            endPoint.X != first.X ||
                            endPoint.Y != first.Y ||
                            endPoint.LocalZ != first.LocalZ
                        ) &&
                        (
                            endPoint.X != second.X ||
                            endPoint.Y != second.Y ||
                            endPoint.LocalZ != second.LocalZ
                        )
                       )
                    {
                        third = endPoint;
                        break;
                    }
                }
                A = first.Y * second.LocalZ +
                    first.LocalZ * third.Y +
                    second.Y * third.LocalZ -
                    third.Y * second.LocalZ -
                    first.Y * third.LocalZ -
                    second.Y * first.LocalZ;
                B = -(
                    first.X * second.LocalZ +
                    third.X * first.LocalZ +
                    second.X * third.LocalZ -
                    third.X * second.LocalZ -
                    first.X * third.LocalZ -
                    second.X * first.LocalZ
                    );
                C = first.X * second.Y +
                    third.X * first.LocalZ +
                    second.X * third.LocalZ -
                    third.X * second.LocalZ -
                    first.X * third.LocalZ -
                    second.X * first.LocalZ;
                D = -(
                    first.X * second.Y * third.LocalZ +
                    third.X * first.Y * second.LocalZ +
                    second.X * third.Y * first.LocalZ -
                    third.X * second.Y * first.LocalZ -
                    first.X * third.Y * second.LocalZ -
                    second.X * first.Y * third.LocalZ
                    );
            }
        }

        public void DefineMatrix(Dot3D middle)
        {
            var sign = middle.X * A + middle.Y * B + middle.LocalZ * C + D;
            if (sign < 0)
            {
                A = 0 - A;
                B = 0 - B;
                C = 0 - C;
                D = 0 - D;
            }
        }

        public bool ShouldBeDraw(float source, Dot3D point)
        {
            var sign = point.X * A + point.Y * B + (point.LocalZ - source) * C;
            return sign >= 0;
        }

        public void Draw(System.Drawing.Graphics graphics, Pen pen)
        {
            foreach (var i in Data)
            {
                i.Draw(graphics, pen);
            }
        }

        public float GetDistance(IDot point)
        {
            var minDistance = float.MaxValue;

            foreach (var i in Data)
            {
                var distance = i.GetDistance(point);

                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            return minDistance;
        }

        public VectorActions GetCrossingType(Point point, float sensitivity)
        {
            foreach (var i in Data)
            {
                if (i.GetCrossingType(point, sensitivity) != VectorActions.None)
                {
                    return VectorActions.Body;
                }
            }

            return VectorActions.None;
        }

        public void Offset(OffsetTypes offsetType, params float[] offsets)
        {
            foreach (var i in Data)
            {
                i.Offset(offsetType, offsets);
            }
        }
    }
}
