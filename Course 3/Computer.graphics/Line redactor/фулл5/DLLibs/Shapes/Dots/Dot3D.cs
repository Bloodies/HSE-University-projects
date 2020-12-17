using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLibs.Shapes.Dots
{
    [Serializable]
    public class Dot3D: IDot
    {
        #region Fields

        public float X { get; set; }
        public float LocalX { get; set; }

        public float Y { get; set; }
        public float LocalY { get; set; }

        public float Z { get; set; }
        public float LocalZ { get; set; }

        public float UnifCoordinate { get; set; }

        #endregion

        #region Init

        public Dot3D(float x, float y, float z, float unifCoordinate)
        {
            X = x;
            LocalX = X;
            Y = y;
            LocalY = Y;
            Z = z;
            LocalZ = Z;
            UnifCoordinate = unifCoordinate;
        }

        #endregion

        #region Override 

        public override string ToString() => $"({X}, {Y}, {Z}, {UnifCoordinate})";

        #endregion

        #region Convert

        public static implicit operator Point(Dot3D dot3d)
        {
            return dot3d == null ? Point.Empty : new Point(Global.UI_Data.CONST_X_SIZE_O + (int)dot3d.X, Global.UI_Data.CONST_Y_SIZE_O - (int)dot3d.Y);
        }

        public static implicit operator Dot3D(Point point)
        {
            return point.IsEmpty ? null : new Dot3D(point.X - Global.UI_Data.CONST_X_SIZE_O, Global.UI_Data.CONST_Y_SIZE_O - point.Y, 0, 1);
        }

        #endregion

        public IDot Normalise()
        {
            X /= UnifCoordinate;
            Y /= UnifCoordinate;
            Z /= UnifCoordinate;
            UnifCoordinate /= UnifCoordinate;
            return this;
        }

        public IDot Offset(params float[] offsets)
        {
            X += offsets[0];
            Y += offsets[1];
            Z += offsets[2];
            return this;
        }
    }
}
