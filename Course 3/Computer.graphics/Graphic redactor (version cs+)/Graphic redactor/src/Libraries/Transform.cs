using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphic_redactor.src.Libraries;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Graphic_redactor.src.Libraries
{
    public class Transform
    {
        editor blob;
        public Transform(editor blob1) { blob = blob1; }
        public Transform() { }
        private double bufAngel = 0;


        //преобразует масштабирование линия + точка в линии + коеф. Х + коеф. У
        public void scale(ref SLine figure, Point curPoint, int numberPoint)
        {//TODO:
         //важно помнить про то, с какой стороны браться с difAngel
         //оптимизация?
         //отдельный консольный выход?
         //if (d(figure.aW, figure.bW) < 3 * editor.visibility)
         //      return;
            float x, y; //коэффициенты
            x = y = 0;
            double difAngel = 0;
            double curAngel = findAngel(figure);
            int curDirect = findDirection();
            float znamX = (float)(figure.aW.X - figure.bW.X);
            float znamY = (float)(figure.aW.Y - figure.bW.Y);
            if (znamX == 0)
                znamX = (float)0.01;
            if (znamY == 0)
                znamY = (float)0.01;
            if (numberPoint == 1) //если тянули за первую точку
            {

                x = (float)Math.Abs((curPoint.X - figure.bW.X) / znamX);
                y = (float)Math.Abs((curPoint.Y - figure.bW.Y) / znamY);
                difAngel = findAngel(curPoint, figure.turnPoint);
            }
            else if (numberPoint == 2) // если за вторую
            {
                x = (float)Math.Abs((curPoint.X - figure.aW.X) / znamX);
                y = (float)Math.Abs((curPoint.Y - figure.aW.Y) / znamY);
                difAngel = findAngel(figure.turnPoint, curPoint);

            }

            if (x == 0 || y == 0 || Double.IsNaN(x) || double.IsNaN(y) || double.IsInfinity(x) || double.IsInfinity(y))
                return;

            if (Math.Abs(difAngel - curAngel) > 5)
            {
                if (double.IsNaN(difAngel) || double.IsNaN(curAngel))
                    return;
                rotateCrach(ref figure, difAngel - curAngel);
                return;


            }


            //String bug = "";
            //if (Math.Abs(difAngel - curAngel) > 1)
            //    bug = "\nda";
            //blob.konsole.Print("direcrion=" + findDirection().ToString() + " angel=" + aux.substr(findAngel(figure).ToString()) +
            //    "\nx,y=" + aux.substr(x.ToString()) + " " + aux.substr(y.ToString()) +
            //    "\ndiffAngel=" + aux.substr(difAngel.ToString()) +
            //    "\ncurAngel=" + aux.substr(curAngel.ToString()) +
            //    "\ndifAngels=" + aux.substr((difAngel - curAngel).ToString()) +
            //      bug);


            scale2D(ref figure, x, y);
            return;
        }


        //масштабирование с использованием матриц (без c# WinForms api)
        private void naturalScale(ref SLine figure, float x, float y)
        {
            float dx = figure.getRotateX();
            float dy = figure.getRotateY();
            //матрица переноса центра в ноль
            Matrix amx = new Matrix(1, 0, 0, 1, -dx, -dy);
            //применяем матрицу 
            figure.affinMatrix.Multiply(amx, MatrixOrder.Append);

            //масштабирование
            amx = new Matrix(x, 0, 0, y, 0, 0);
            figure.affinMatrix.Multiply(amx, MatrixOrder.Append);

            //матрица возврощения обратно
            amx = new Matrix(1, 0, 0, 1, dx, dy);
            figure.affinMatrix.Multiply(amx, MatrixOrder.Append);
            //figure.applyAffinMatrix();
        }
        //масшатабирование  линии + коеф. Х + коеф. У в разных режимых
        public void scale2D(ref SLine figure, float x, float y)
        {
            float dx = figure.getRotateX();
            float dy = figure.getRotateY();
            if (x > 10000 || y > 10000)
                return;
            figure.affinMatrix.Translate(-dx, -dy, MatrixOrder.Append);
            figure.affinMatrix.Scale(x, y, MatrixOrder.Append);
            figure.affinMatrix.Translate(dx, dy, MatrixOrder.Append);
        }

        public void scale(ref SLine figure, float x, float y)
        {
            scale2D(ref figure, x, y);
        }



        public void rotate(Point curPoint)
        {

            SLine line = blob.curFigure;
            rotate(ref line, curPoint);
            blob.curFigure = line;
        }

        public void moveTo(float x, float y, int index)
        {
            SLine line = blob.points[index];
            line.affinMatrix = blob.points[index].affinMatrix.Clone();
            line.affinMatrix.Translate(x, y, MatrixOrder.Append);
            blob.points[index] = line;
        }
        public void moveTo(float x, float y)
        {
            moveTo(x, y, blob.CurLineIndex);

        }
        public void rotate(ref SLine figure, Point curPoint)
        {

            double angel = findAngel(blob.curFigure.turnPoint, curPoint);
            
            rotate(ref figure, angel);

        }
        public void rotate(ref SLine figure, double angel)
        {
            PointF centerPointsArr = new PointF(figure.getCentrX(), figure.getCentrY());
            if (Math.Abs(angel) > 9999 || Math.Abs(bufAngel) > 9999 || Math.Abs(angel) < 0.01) return;
            figure.affinMatrix.RotateAt((float)(bufAngel - angel), centerPointsArr, MatrixOrder.Append);
            bufAngel = angel;
            blob.drawingScieneOnly();
        }
        //используется только при граничных условиях в трансформировании
        private void rotateCrach(ref SLine figure, double angel)
        {
            PointF centerPointsArr = new PointF(figure.getCentrX(), figure.getCentrY());
            figure.affinMatrix.RotateAt((float)(-angel), centerPointsArr, MatrixOrder.Append);
            blob.drawingScieneOnly();
        }
        public void rotateCrach(ref SLine figure, Point curPoint)
        {
            double angel = findAngel(figure.aW, curPoint);
            rotateCrach(ref figure, angel);

        }

        //найти угол по индексу линии
        public int findDirection()
        {
            return findDirection(blob.curFigure.aW, blob.curFigure.bW);
        }

        //найти угол по индексу линии
        public int findDirection(ref SLine figure)
        {
            return findDirection(figure.aW, figure.bW);
        }

        public int findDirection(Point a, Point b)
        {//найти направление отрезка, проходящего через 2 данные точки
            int x, y;


            //опередяляем направление вдоль оси Х, х=0 => совпадают с осью
            if (b.X - a.X > 0)
                x = 1;
            else if (b.X - a.X < 0)
                x = -1;
            else
                x = -1;

            if (b.Y - a.Y < 0)
                y = 1;
            else if (b.Y - a.Y > 0)
                y = -1;
            else
                y = -1;

            if (x == 1 && y == 1)
                return 3;
            if (x == -1 && y == 1)
                return 4;
            if (x == -1 && y == -1)
                return 1;
            if (x == 1 && y == -1)
                return 2;
            return 0;
        }

        public double findAngel(SLine line)
        {//найти угол данной линии
            double c = d(line.aW, line.bW); //длина отрезка
            double a = line.aW.X - line.bW.X; //проекциия на Х  //поворот на 90 градусов, от того кто а, кто б
            double b = line.aW.Y - line.bW.Y; // проекция на У

            //return findAngel(a, b, c, findDirection(curLineIndex));


            return findAngel(line.aW, line.bW);


        }
        public double findAngel(Point A, Point B, Point C)
        {
            double a = d(A, B);
            double b = d(A, C);
            double c = d(C, B);

            return findAngel(a, b, c, findDirection(B, C));
        }


        public double findAngel(Point A, Point B)
        {//находит угол отрезка проход. через 2 данные точки (функция обертка)
            double c = d(A, B);
            double b = A.Y - B.Y;
            double a = A.X - B.X;
            return findAngel(a, b, c, findDirection(A, B));
        }

        public double findAngel(double a, double b, double c, int direction)
        {//находит уголл  0.000000000001
            double value = 0.001;
            if (a == 0) a = value;
            if (c == 0) c = value;
            if (b == 0) b = value;
            double p = (a + b + c) / 2;
            double R = (a * b * c) / (4 * Math.Sqrt(p * (p - a) * (p - b) * (p - c)));
            double sinAngel = b / (2 * R);
            double angel = Math.Asin(sinAngel) * (180 / Math.PI);
            sinAngel = angel;


            switch (direction)
            {
                case (2):
                    angel = angel + 180;
                    break;
                case (3):
                    angel = -1 * angel + 180;
                    break;
                case (4):
                    angel = (90 - angel) + 270;
                    break;

            }
            if (Double.IsNaN(angel) || a == value || b == value || c == value)
            {
                //blob.konsole.Print("isNan");
            }
                

            return angel;
        }
        public void handScale(string scaleCoef)
        {
            float scaleXY;
            SLine tempLine;
            try
            {
                scaleXY = (float)Convert.ToDouble(scaleCoef);
                tempLine = blob.curFigure;
            }
            catch (FormatException)
            { return; }
            catch (ArgumentOutOfRangeException)
            { return; }

            Transform aft = new Transform();
            aft.scale(ref tempLine, scaleXY, scaleXY);
            blob.curFigure = tempLine;
        }

        protected double d(Point a, Point b)
        {//растояние между 2 точками
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow(b.Y - a.Y, 2));
        }






    }
}
