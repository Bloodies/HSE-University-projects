using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graphic_redactor.src;
using Graphic_redactor.src.Libraries;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace Graphic_redactor.src.Libraries
{
    public class editor
    {
        public int curModes;
        public int curCaptures;
        public bool isDragging;
        public int pen;
        public SLine curFigure
        {
            get { return points[CurLineIndex]; }
            set { points[CurLineIndex] = value; }
        }
        protected Point curPoint;
        protected SLine curLine;
        private int curLineIndex = -1;

        public int CurLineIndex
        {
            get { return curLineIndex; }
        }
        public const int visibility = 15;
        protected Graphics canvas;
        protected PictureBox defaultCanvas;
        protected Pen primaryPen = new Pen(Color.Blue, 2.0f);        //линия
        protected Pen secondryPen = new Pen(Color.DarkOrange, 1.0f); //лиkния
        protected Bitmap bmp;
        protected Graphics bmpGr;                                    //сглаживание
        public List<SLine> points = new List<SLine>();

        protected bool zoom = false;
        protected int prevCaptur = -1;
        public Transform aft;
        public Matrix lastMatrix;

        //bool blockDCM = false; //блокирвока отпускания левый кнопки мыши, что бы после выделения тут же не произошли другие изменения


        public void initial(PictureBox initialForm)
        {//связывание холста и пиктербокса + включение сглаживания
            canvas = initialForm.CreateGraphics();
            defaultCanvas = initialForm;
            bmp = new Bitmap(initialForm.Width, initialForm.Height);
            bmpGr = Graphics.FromImage(bmp);
            bmpGr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (true)
            {
                curModes = (int)modes.MODE_DROW;
            }
            aft = new Transform(this);



        }

        public void changeZoom(MouseEventArgs e)
        {//смена состояние (маштабирвоание/тансформация
            if (curModes == (int)modes.MODE_MOVE && curCaptures != (int)captures.TAKE_NONE)
            {
                if (zoom)
                    zoom = false;
                else
                    zoom = true;
            }

        }


        protected double d(Point a, Point b)
        {//растояние между 2 точками
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow(b.Y - a.Y, 2));
        }
        public void resetIndexLine()
        {
            curLineIndex = -1;
        }

        //отрисовка  холста с учетом надатия ЛКМ
        public void drawingDown(MouseEventArgs e)
        {//при нажатии ЛКМ на холсте

            switch (pen)
            {

                case (int)penType.line:
                    switch (curModes)
                    {
                        case (int)modes.MODE_DROW:
                            isDragging = true;
                            curPoint = e.Location;
                            curLine.a = e.Location;
                            curLine.aW = e.Location;
                            curLine.color = Color.Black;
                            curLine.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
                            break;

                        case (int)modes.MODE_MOVE:
                            int index = getLine(e.Location);
                            //если мы куда попали в фигуру
                            if (curCaptures != (int)captures.TAKE_NONE)
                            {
                                isDragging = true;
                                curLineIndex = index;
                                curPoint = e.Location;
                                curLine = points[curLineIndex];
                                //blockDCM = true;

                            }
                            break;
                        case (int)modes.MODE_DELETE:
                            curLineIndex = getLine(e.Location);
                            if (curLineIndex != -1)
                            {
                                points.RemoveAt(curLineIndex);
                                drawingSciene();
                            }
                            break;
                    }
                    break;
                case (int)penType.poligon:
                    break;
            }
        }
        //отрисовка событий с учетом отпускания ЛКМ на холсте
        public void drawingUp(MouseEventArgs e)
        {//при отпускание ЛКМ на холсте
            switch (pen)
            {
                case (int)penType.line:
                    switch (curModes)
                    {
                        case (int)modes.MODE_DROW:
                            isDragging = false;
                            curLine.b = e.Location;
                            curLine.bW = e.Location;
                            points.Add(curLine);
                            break;
                        case (int)modes.MODE_MOVE:
                            isDragging = false;
                            if (curLineIndex != -1)
                                applyMatrix(curLineIndex);
                            drawingSciene();
                            break;
                    }
                    break;
                case (int)penType.poligon:
                    break;
            }
        }
        private bool dForSquare(Point primary, Point curPoint)
        {//находится ли curPoint в окрености primary (окресность = visibility)
            foreach (Point point in getPointsTransform(primary))
                if (d(point, curPoint) <= visibility)
                    return true;
            return false;
        }

        public int getLine(Point midPoint)
        {//возвращает индекс линии, которой принадлежит данная точка
            int ptr = 0;
            foreach (SLine line in points)
            {
                //Console.WriteLine("||{0} + {1} - {2} = {4} < {3} ", d(line.aW, midPoint), d(midPoint, line.bW), d(line.aW, line.bW), visibility, d(line.aW, midPoint) + d(midPoint, line.bW) - d(line.aW, line.bW));
                if (d(line.aW, midPoint) + d(midPoint, line.bW) - d(line.aW, line.bW) < visibility || dForSquare(line.aW, midPoint) || dForSquare(line.bW, midPoint))
                {

                    if (d(line.turnPoint, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_TURN;
                    else if (d(line.aW, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_PT1;
                    else if (d(line.bW, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_PT2;
                    else
                        curCaptures = (int)captures.TAKE_CENTR;
                    return ptr;
                }

                ptr++;

            }
            curCaptures = (int)captures.TAKE_NONE;

            return -1;
        }
        protected Point[] getPointsTransform(Point primary)
        { //массив точек для трансформации (см. место где вызываю данную функцию)
            int mathVis = (int)(visibility / 2);

            Point a = new Point(primary.X - mathVis, primary.Y - mathVis);
            Point b = new Point(primary.X - mathVis, primary.Y + mathVis);
            Point c = new Point(primary.X + mathVis, primary.Y + mathVis);
            Point d = new Point(primary.X + mathVis, primary.Y - mathVis);
            Point[] arr = new Point[] { a, b, c, d };
            return arr;
        }

        public void applyMatrix(int indexLine)
        {//применить матрицу афинных преобразований, без сброса самой матрицы
            SLine tempLine = points[indexLine];
            tempLine.affinMatrix = points[indexLine].affinMatrix.Clone();

            Point[] ps = new Point[2];
            ps[0] = tempLine.a;
            ps[1] = tempLine.b;
            tempLine.affinMatrix.TransformPoints(ps);
            tempLine.aW = ps[0];
            tempLine.bW = ps[1];

            points[indexLine] = tempLine;

            return;
        }
        private void popMatrix(int indexLine)
        {//применить матрицу афинных преобразований к фигуре и сбросить матрицу афинных преобразований
            SLine tempLine = points[indexLine];
            Point[] ps = new Point[2];
            ps[0] = tempLine.a;
            ps[1] = tempLine.b;
            tempLine.affinMatrix.TransformPoints(ps);
            tempLine.a = ps[0];
            tempLine.b = ps[1];
            tempLine.aW = ps[0];
            tempLine.bW = ps[1];
            tempLine.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            points[indexLine] = tempLine;
            return;
        }

        private void popMatrix(SLine tempLine)
        {
            Point[] ps = new Point[2];
            ps[0] = tempLine.a;
            ps[1] = tempLine.b;
            tempLine.affinMatrix.TransformPoints(ps);
            tempLine.a = ps[0];
            tempLine.b = ps[1];
            tempLine.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            return;
        }
        private Point tranformPoint(float ugol, Point pTrance, Point pSelf)
        {//возвращает по некоторому эзетерическому алгоритму точку, за через которую потом будем делать поворот
            Matrix angel = new Matrix(1, 0, 0, 1, 0, 0);
            PointF tempP = pTrance;
            PointF tempPTr = pSelf;

            angel.RotateAt(ugol, tempP);
            PointF[] arrCenter = { tempPTr };
            angel.TransformPoints(arrCenter);

            pSelf.X = (int)arrCenter[0].X;
            pSelf.Y = (int)arrCenter[0].Y;
            return pSelf;

        }
        private Point[] tranformPoint(float ugol, Point pTrance, Point[] pSelf)
        {// -||- но массив
            for (int i = 0; i < pSelf.Count(); i++)
            {

                pSelf[i] = tranformPoint(ugol, pTrance, pSelf[i]);

                i++;
            }
            return pSelf;

        }
        private SLine tranformPoint(float ugol, SLine temp)
        {//100500 функция, которая пытается дать правильные координаты (тщетно)
            Point[] tempArr = { temp.a, temp.b };
            Point c = new Point(40, 40);
            tempArr = tranformPoint(ugol, temp.b, tempArr);

            temp.a = tempArr[0];
            temp.b = tempArr[1];
            return temp;


        }
        public void repaireLine(int index)
        {//востановление линии в случае, если произошло переполнение
            SLine temp = new SLine();

            temp = points[index];
            temp.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            temp.aW = curLine.a;
            temp.bW = curLine.b;
            points[index] = temp;
            applyMatrix(index);

        }

        public void drawingScieneOnly()
        {//отрисовки всех фигур + короб (без пост и пред действий)
            //потому что иногда нужна толко отрисовка без других действий
            try
            {
                foreach (SLine line in points)
                {
                    primaryPen.Color = line.color;
                    try
                    {
                        bmpGr.DrawLine(primaryPen, line.aW, line.bW);
                    }
                    catch (OverflowException)
                    {
                        applyMatrix(curLineIndex);
                    }

                }
            }
            catch (InvalidOperationException) { }

            SLine tempL = new SLine();
            //отрисовка короба
            try
            {
                if (curModes == (int)modes.MODE_MOVE && curLineIndex != -1)
                {
                    tempL = points[curLineIndex];
                    if (double.IsNaN(points[curLineIndex].aW.X) || double.IsNaN(points[curLineIndex].aW.Y) || points[curLineIndex].aW.X < -99999 ||
                    points[curLineIndex].aW.X > 99999)
                    {
                        printLine(points[curLineIndex]);
                        repaireLine(curLineIndex);
                        return;
                    }
                    try
                    {
                        int height = Math.Abs(curFigure.bW.X - curFigure.aW.X);
                        int weidth = Math.Abs(curFigure.bW.Y - curFigure.aW.Y);
                        int limit = 10;
                        if (height < limit)
                            height = limit;
                        if (weidth < limit)
                            weidth = limit;
                        Rectangle frame = new Rectangle(Math.Min(curFigure.aW.X, curFigure.bW.X), Math.Min(curFigure.aW.Y, curFigure.bW.Y),
                           height, weidth);
                        bmpGr.DrawRectangle(secondryPen, frame);
                        bmpGr.DrawPolygon(secondryPen, getPointsTransform(points[curLineIndex].bW));
                        bmpGr.DrawPolygon(secondryPen, getPointsTransform(points[curLineIndex].aW));
                    }
                    catch (OverflowException)
                    { repaireLine(curLineIndex); }
                    if (curCaptures != (int)captures.TAKE_TURN)
                    {

                        changeTurnPoint();

                    }
                    //points[curLineIndex].applyAffinMatrix();
                    applyMatrix(curLineIndex);
                    Debug.WriteLine(points[curLineIndex].ToStringMx());
                    //popMatrix(curLineIndex);
                    SLine myLine = points[curLineIndex];
                    bmpGr.DrawEllipse(secondryPen, myLine.turnPoint.X, myLine.turnPoint.Y, 5, 5);

                }
            }
            catch (StackOverflowException)
            {
                // points[curLineIndex]= curLine;
                //applyMatrix(curLineIndex);
            }



        }

        public void drawingSciene()
        {//очистка холста, отрисовка, подмена холста
            bmpGr.Clear(Color.White);

            drawingScieneOnly();
            canvas.DrawImage(bmp, 0, 0);
            return;

        }
        public void drawingSciene(SLine line)
        {//очистка холста, отрисовка, отрисовка текущей линии (которая меняем), подмена холста
            bmpGr.Clear(Color.White);
            drawingScieneOnly();
            primaryPen.Color = line.color;
            bmpGr.DrawLine(primaryPen, line.aW, line.bW);
            canvas.DrawImage(bmp, 0, 0);
            return;
        }

        public Matrix addMatrix(Matrix mx1, Matrix mx2)
        {
            float[] elem1 = mx1.Elements;
            float[] elem2 = mx1.Elements;
            //a = mx
            Matrix tmp = new Matrix(elem1[0] + elem2[0], elem1[1] + elem2[1], elem1[2] + elem2[2],
                elem1[3] + elem2[3], elem1[4] + elem2[4], elem1[5] + elem2[5]);
            return tmp;

        }
        private float scalelogic(float number)
        {
            float fract = (float)number - (float)Math.Truncate(number);
            if (number == 1)
                return number;
            if (number > 1)
            {
                number -= 2 * fract;
            }
            else
            {
                number += 2 * fract;
            }
            return number;


        }
        //для отладки поворота

        public void drawingSciene(PictureBox pictureBox1, MouseEventArgs e)
        {//отрисовка + выполнение действий пользователя (поворот, маштабирвоание, трансформ, деформ)

            if (curModes == (int)modes.MODE_DROW)
            {

                curLine.bW = e.Location;
                if (isDragging)
                    drawingSciene(curLine);
            }

            else
            {

                if (isDragging)
                {
                    if (curCaptures != prevCaptur)
                    { }// popMatrix(curLineIndex);

                    SLine templine = new SLine();
                    templine = points[curLineIndex];
                    templine.affinMatrix = points[curLineIndex].affinMatrix.Clone();
                    switch (curCaptures)
                    {
                        //меняем кординаты у перетягиваемого изображения прямо в хранилище
                        //готовимся к отрисовке
                        case (int)captures.TAKE_TURN:
                            aft.rotate(ref templine, e.Location);
                            points[curLineIndex] = templine;
                            break;

                        case (int)captures.TAKE_PT1:
                            if (zoom)
                            {//если деформ
                                popMatrix(curLineIndex);
                                curLine = points[curLineIndex];
                                curLine.a = e.Location;
                                curLine.aW = e.Location;
                                points[curLineIndex] = curLine;

                            }
                            else
                            {//если  маштаб

                                aft.scale(ref templine, e.Location, 1);
                                points[curLineIndex] = templine;

                            }
                            break;
                        case (int)captures.TAKE_PT2:
                            if (zoom)
                            {
                                popMatrix(curLineIndex);
                                curLine = points[curLineIndex];
                                curLine.b = e.Location;
                                curLine.bW = e.Location;
                                points[curLineIndex] = curLine;
                                popMatrix(curLineIndex);
                            }
                            else
                            {

                                aft.scale(ref templine, e.Location, 2);
                                points[curLineIndex] = templine;


                            }
                            break;
                        case (int)captures.TAKE_CENTR:

                            //мат!
                            //Matrix coordinans3 = new Matrix(1,0, 0,1, 
                            //    (e.Location.X - curPoint.X),
                            //    (e.Location.Y - curPoint.Y) );
                            aft.moveTo((e.Location.X - curPoint.X), (e.Location.Y - curPoint.Y), curLineIndex);
                            curPoint.X = e.Location.X;
                            curPoint.Y = e.Location.Y;
                            break;
                    }


                    drawingSciene();
                    prevCaptur = curCaptures;


                }

            }

        }

        public void pointsDebug()
        {//выводил состояние точек (да, у нас не практикуется использовать отладчик)
         // Console.WriteLine("----------------");
            int ptr = 0;
            foreach (SLine line in points)
            {
                //Console.WriteLine(ptr.ToString()+"|"+line.aW.ToString() + " " + line.bW.ToString());
                ptr++;
            }
        }

        public void safeStorage(string path)
        {
            //откроем поток для записи в файл
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();

            //сериализация
            bf.Serialize(fs, points);
            fs.Close();
        }
        private void printLine(SLine line)
        {//вывести состояние линии
         //  Console.WriteLine("!{0}, {1} - {2},{3}",  line.a.X, line.a.Y, line.b.X, line.b.Y);
         // Console.WriteLine("!!{0}, {1} - {2},{3}", line.aW.X, line.aW.Y, line.bW.X, line.bW.Y);
        }
        public void loadStorage(string path)
        {//загрузка состяония из файла
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter bf = new BinaryFormatter();
            points = (List<SLine>)bf.Deserialize(fs);
            fs.Close();



        }
        private void changeTurnPoint(ref SLine line)
        {//изменить точку поворота, ее нужна менять отдельно, каждый раз забываю почему такой апендикс
            line.turnPoint.X = (int)((line.aW.X + line.bW.X) / 2 + 20);
            line.turnPoint.Y = (int)((line.aW.Y + line.bW.Y) / 2 + 35);
            return;
        }

        private void changeTurnPoint()
        {
            SLine cur = points[curLineIndex];
            cur.turnPoint.X = cur.getRotateX();
            cur.turnPoint.Y = cur.getRotateY();
            points[curLineIndex] = cur;
        }
    }

    [Serializable]
    public struct SLine
    {//структура хранения (здаровая и не поворотливая)
        public int typeObj;
        public Color color;
        public Point a, b;
        public Point aW, bW;// нудно сделать использование их везде
        public Point turnPoint;

        public Matrix affinMatrix;

        public SLine(Point a, Point b)
        {
            turnPoint = this.a = aW = a; this.b = bW = b; typeObj = 0;
            color = Color.DeepSkyBlue;
            affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
        }
        //перегрузка метода ToString
        public override string ToString()
        {
            string str = String.Format("a={0}, b={1}. aW={2}, bW={3}", this.a, this.b,
             this.aW, this.bW);
            return str;
        }

        public string ToStringMx()
        {
            StringBuilder str = new StringBuilder("mx=");
            foreach (float obj in this.affinMatrix.Elements)
            {
                str.Append(obj.ToString() + " ");
            }
            return str.ToString();
        }

        public int getCentrX()
        {
            return (this.aW.X + this.bW.X) / 2;

        }

        public int getCentrY()
        {
            return (this.aW.Y + this.bW.Y) / 2;

        }
        public int getRotateX()
        {
            return getCentrX();

        }

        public int getRotateY()
        {
            return getCentrY();

        }



        public void applyAffinMatrix()
        {
            Point[] pArr = { this.aW, this.bW };
            this.affinMatrix.TransformPoints(pArr);
            this.aW = pArr[0];
            this.bW = pArr[1];
        }

        //SLine tempLine = points[indexLine];
        //Point[] ps = new Point[2];
        //ps[0] = tempLine.a;
        //ps[1] = tempLine.b;
        //tempLine.affinMatrix.TransformPoints(ps);
        //tempLine.aW = ps[0];
        //tempLine.bW = ps[1];

        //points[indexLine] = tempLine;
        //return;
        public double d_aW(Point eqPoint)
        {//растояние как метод 
            return Math.Sqrt(Math.Pow((this.aW.X - eqPoint.X), 2) + Math.Pow(this.aW.Y - eqPoint.Y, 2));
        }
        public double d_bW(Point eqPoint)
        {//растояние как метод 
            return Math.Sqrt(Math.Pow((this.bW.X - eqPoint.X), 2) + Math.Pow(this.bW.Y - eqPoint.Y, 2));
        }
    }
}
