using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLibs.Shapes.Dots
{
    [Serializable]
    public class Dot2D: IDot
    {
        #region Fields
        public float Z { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float LocalX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float LocalY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float LocalZ { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float X { get; set; }
        public float Y { get; set; }
        public float UnifCoordinate { get; set; }

        #endregion

        #region Init

        public Dot2D(float x, float y, float uc)
        {
            X = x;
            Y = y;
            UnifCoordinate = uc;
        }

        #endregion

        #region Override

        public override string ToString() => $"({X}, {Y}, {UnifCoordinate})";

        #endregion

        #region Convert

        public static implicit operator Point(Dot2D dot2d)
        {
            return dot2d == null ? Point.Empty : new Point(Global.UI_Data.CONST_X_SIZE_O + (int)dot2d.X, Global.UI_Data.CONST_Y_SIZE_O - (int)dot2d.Y) ;
        }

        public static implicit operator Dot2D(Point point)
        {
            return point.IsEmpty ? null : new Dot2D(point.X - Global.UI_Data.CONST_X_SIZE_O, Global.UI_Data.CONST_Y_SIZE_O - point.Y, 1);
        }

        #endregion

        public IDot Normalise()
        {
            X /= UnifCoordinate;
            Y /= UnifCoordinate;
            UnifCoordinate /= UnifCoordinate;
            return this;
        }

        public IDot Offset(params float[] offsets)
        {
            X += offsets[0];
            Y += offsets[1];
            return this;
        }
    }
}
