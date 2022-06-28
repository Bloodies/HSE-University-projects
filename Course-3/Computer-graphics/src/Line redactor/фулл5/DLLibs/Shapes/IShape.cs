using DLLibs.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using static System.Math;

namespace DLLibs.Shapes
{
    public interface IShape
    {
        void Draw(System.Drawing.Graphics graphics, Pen pen);

        float GetDistance(IDot point);

        VectorActions GetCrossingType(Point point, float sensitivity);

        void Offset(OffsetTypes offsetType, params float[] offsets);
    }
}
