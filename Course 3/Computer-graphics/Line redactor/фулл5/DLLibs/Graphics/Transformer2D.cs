using DLLibs.Shapes;
using DLLibs.Shapes.Dots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLibs.Graphics._2D
{
    public static class Transformer2D
    {
        public static IDot[] Multiply(IDot[] dots, float[] matrix)
        {
            if (matrix.Length != 9)
            {
                throw new ArgumentException("Array's length must be 9.");
            }

            for(int i = 0; i < dots.Length; ++i)
            {
                if (!(dots[i] is Dot2D))
                {
                    throw new ArgumentNullException("Dot's interface is not Dot2D class.");
                }

                dots[i].X = dots[i].X * matrix[0] + dots[i].Y * matrix[3] + dots[i].UnifCoordinate * matrix[6];
                dots[i].Y = dots[i].X * matrix[1] + dots[i].Y * matrix[4] + dots[i].UnifCoordinate * matrix[7];
                dots[i].UnifCoordinate = dots[i].UnifCoordinate * matrix[2] + dots[i].Y * matrix[5] + dots[i].UnifCoordinate * matrix[8];
            }

            return dots;
        }

        public static IDot[] Offset(float[] offsets, params IDot[] dots)
        {
            return Multiply(
                dots: dots,
                matrix: new float[]
                {
                    1, 0, 0,
                    0, 1, 0,
                    offsets[0], offsets[1], 1
                }
            );
        }
    }
}
