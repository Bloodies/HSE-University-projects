using DLLibs.Shapes;
using DLLibs.Shapes.Dots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace DLLibs.Graphics
{
    class Transformer3D
    {
        public static IDot[] Multiply(IDot[] dots, float[] matrix)
        {
            if (matrix.Length != 16)
            {
                throw new ArgumentException("Array's length must be 16.");
            }

            for (int i = 0; i < dots.Length; ++i)
            {
                if (!(dots[i] is Dot3D))
                {
                    throw new ArgumentNullException("Dot's interface is not Dot3D class.");
                }

                dots[i].X = dots[i].X * matrix[0] + dots[i].Y * matrix[4] + dots[i].Z * matrix[8] + dots[i].UnifCoordinate * matrix[12];
                dots[i].Y = dots[i].X * matrix[1] + dots[i].Y * matrix[5] + dots[i].Z * matrix[9] + dots[i].UnifCoordinate * matrix[13];
                dots[i].Z = dots[i].X * matrix[2] + dots[i].Y * matrix[6] + dots[i].Z * matrix[10] + dots[i].UnifCoordinate * matrix[14];
                dots[i].UnifCoordinate = dots[i].X * matrix[3] + dots[i].Y * matrix[7] + dots[i].Z * matrix[11] + dots[i].UnifCoordinate * matrix[15];
            }

            return dots;
        }

        public static IDot[] MultiplyHome(IDot[] dots, float[] matrix)
        {
            if (matrix.Length != 16)
            {
                throw new ArgumentException("Array's length must be 16.");
            }

            for (int i = 0; i < dots.Length; ++i)
            {
                if (!(dots[i] is Dot3D))
                {
                    throw new ArgumentNullException("Dot's interface is not Dot3D class.");
                }

                dots[i].X = dots[i].LocalX * matrix[0] + dots[i].LocalY * matrix[4] + dots[i].LocalZ * matrix[8] + dots[i].UnifCoordinate * matrix[12];
                dots[i].Y = dots[i].LocalX * matrix[1] + dots[i].LocalY * matrix[5] + dots[i].LocalZ * matrix[9] + dots[i].UnifCoordinate * matrix[13];
                dots[i].Z = dots[i].LocalX * matrix[2] + dots[i].LocalY * matrix[6] + dots[i].LocalZ * matrix[10] + dots[i].UnifCoordinate * matrix[14];
                dots[i].UnifCoordinate = dots[i].LocalX * matrix[3] + dots[i].LocalY * matrix[7] + dots[i].LocalZ * matrix[11] + dots[i].UnifCoordinate * matrix[15];
            }

            return dots;
        }

        public static IDot[] Offset(float[] offsets, params IDot[] dots)
        {
            return Multiply(
                dots: dots,
                matrix: new float[]
                {
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    offsets[0], offsets[1], 0, 1
                }
            );
        }

        public static IDot[] Action(float[] actionMatrix, params IDot[] dots)
        {
            return Multiply(
                dots: dots,
                matrix: actionMatrix
            );
        }

        public static IDot[] HomeMoving(float phi, float tetta, float z, params IDot[] dots)
        {
            return MultiplyHome(
                dots: dots,
                matrix: new float[] { 
                    (float)Cos(phi), (float)Sin(phi)*(float)Sin(tetta), 0, (float)Sin(phi) * (float)Cos(tetta) / z,
                    0, (float)Cos(tetta), 0, -(float)Sin(tetta)/z,
                    (float)Sin(phi), -(float)Cos(phi)*(float)Sin(tetta), 0, -(float)Cos(phi)*(float)Cos(tetta) / z,
                    0, 0, 0, 1
                }
            );
        }
    }
}
