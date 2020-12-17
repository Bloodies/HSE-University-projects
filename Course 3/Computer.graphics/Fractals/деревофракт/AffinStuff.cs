using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public enum Position { Left, Right };

    static class AffinStuff
    {
        public class Edge
        {
            public Edge(PointF s, PointF e)
            {
                start = s;
                end = e;
            }
            public double Length()
            {
                float dx = start.X - end.X;
                float dy = start.Y - end.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }
            public PointF MiddlePoint()
            {
                return new PointF((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            }
            public bool Same(Edge e)
            {
                return (start == e.start && end == e.end) || (start == e.end && end == e.start);
            }

            public Edge()
            {
                start = end = new PointF(-1, -1);
            }
            public bool IsFake()
            {
                return start == end;
            }
            public static bool operator== (Edge e1, Edge e2)
            {
                return e1.start == e2.start && e1.end == e2.end;
            }
            public static bool operator!= (Edge e1, Edge e2)
            {
                return e1.start != e2.start || e1.end != e2.end;
            }

            public PointF start;

            public PointF end;
            public float thicknes;
            public Color color;

            public Position WherePoint(PointF p)
            {
                float vector_x = end.X - start.X;
                float vector_y = end.Y - start.Y;
                float point_vector_x = p.X - start.X;
                float point_vector_y = p.Y - start.Y;
                if ((vector_y * point_vector_x - vector_x * point_vector_y) > 0)
                    return Position.Left;
                else return Position.Right;
            }
        }

        //Класс полигона
        public class Polygon
        {
            public Polygon(List<PointF> ps)
            {
                points = ps;
                convex = IsConvex();
            }

            public Polygon(PointF start_point)
            {
                points = new List<PointF>();
                points.Add(start_point);
            }

            public void AddEdge(Edge e)
            {
                if (!points.Contains(e.end))
                    points.Add(e.end);
            }
            public void Draw(ref Graphics g)
            {
                Pen p = new Pen(Color.Black, 1);
                g.DrawPolygon(p, points.ToArray());
            }
            

            public List<PointF> points;
            bool convex;

            private bool IsConvex()
            {
                var arrpoint = points.ToArray();
                for (var i = 0; i < arrpoint.Length; i++)
                {
                    Edge edge;
                    if (i == arrpoint.Length - 1)
                    {
                        edge = new Edge(arrpoint[i], arrpoint[0]);
                    }
                    else
                    {
                        edge = new Edge(arrpoint[i], arrpoint[i + 1]);
                    }
                    Position pos = Position.Left; ;
                    for (int j = 0; j < arrpoint.Length; j++)
                    {
                        if (arrpoint[j] != edge.start && arrpoint[j] != edge.end)
                        {

                            if (j == 0)
                                pos = edge.WherePoint(arrpoint[j]);
                            else if (pos != edge.WherePoint(arrpoint[j]))
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }

            public bool IsPointInside(Point p)
            {
                Edge ray = new Edge(p, new Point(p.X + 1000, p.Y)); // TODO: come up with somth better, than 1000
                if (EdgeIntersectWithPoly(ray) % 2 == 0)
                    return false;
                else return true;
                    
            }
            private int EdgeIntersectWithPoly(Edge e)
            {
                var arr_point = points.ToArray();
                int intersect_counter = 0;
                for (int i = 0; i < arr_point.Length; i++)
                {
                    Edge edge;
                    Point p = new Point();
                    if (i == arr_point.Length - 1)
                        edge = new Edge(arr_point[i], arr_point[0]);
                    else
                        edge = new Edge(arr_point[i], arr_point[i + 1]);

                    if (CheckEdgesForIntersection(edge, e, ref p)) // TODO: add clause if e intersect Poly's point
                        intersect_counter++;
                }
                return intersect_counter;
            }
        }


        static public List<Polygon> DeloneTriangulation(List<Point> points)
        {
            LinkedList<Edge> list_edges = new LinkedList<Edge>();
            List<Polygon> triangles = new List<Polygon>();
            Edge start = FirstEdge(points);
            Stack<Edge> alive = new Stack<Edge>();
            alive.Push(start);
            while(alive.Count != 0)
            {
                Edge cur = alive.Pop();
                Point cur_point = FindNextDelonePoint(points, cur);
                if (cur_point == new Point(-1, -1))
                    continue;
                //alive_points.Remove(cur_point);
                Edge new_edge1 = new Edge(cur.start, cur_point);
                Edge new_edge2 = new Edge(cur_point, cur.end);
                if (list_edges.Where((e) => e.Same(new_edge1)).Count() != 1)
                {
                    list_edges.AddLast(new_edge1);
                    alive.Push(new_edge1);
                }
                if (list_edges.Where((e) => e.Same(new_edge2)).Count() != 1)
                {
                    list_edges.AddLast(new_edge2);
                    alive.Push(new_edge2);
                }
                triangles.Add(new Polygon(new List<PointF>() { cur.start, cur.end, cur_point }));

            }
            return triangles;
        }
        static public Point FindNextDelonePoint(List<Point> points, Edge cur)
        {
            int min = Int16.MaxValue;
            Point res = new Point(-1,-1);
            foreach(var x in points)
            {
                if (cur.WherePoint(x) == Position.Left || cur.start == x || cur.end == x)
                    continue;
                int dist = DistToCenter(cur, x);
                if (dist < min)
                {
                    min = dist;
                    res = x;
                }
            }
            return res;
        }

        static public int DistToCenter(Edge edge, Point p)
        {
            PointF center = CircleCenterByPoints(edge.start, edge.end, p);
            Edge radius = new Edge(center, p);
            if (edge.WherePoint(center) == Position.Left)
                return -(int)radius.Length();
            else return (int)radius.Length();
        }
        // return tg of the polar angle of the point p
        static public double PolarAngle(Point center, Point p)
        {
            int dx = p.X - center.X;
            int dy = p.Y - center.Y;
            if (dx > 0)
                return Math.Atan((double)dy / dx);
            else return Math.PI + Math.Atan( (double)dy / dx);
        }
        //find point with smallest Y and X
        static public Point FirstPoint(List<Point> points)
        {
            Point res = points.First();
            foreach(var x in points)
            {
                if (x.Y < res.Y)
                    res = x;
                else if (x.Y == res.Y && x.X < res.X)
                    res = x;
            }
            return res;
        }

        static public Edge FirstEdge(List<Point> points)
        {
            Point first_point = FirstPoint(points);
            double min_tg = Double.MaxValue;
            Point second_point = first_point;
            foreach (var x in points)
            {
                if (x == first_point)
                    continue;
                double tg = PolarAngle(first_point, x);
                if (tg < min_tg)
                {
                    second_point = x;
                    min_tg = tg;
                }
            }
            return new Edge(first_point, second_point);
        }
        static public PointF CircleCenterByPoints(PointF p1, PointF p2, PointF p3)
        {
            float xy1_pow = p1.X * p1.X + p1.Y * p1.Y;
            float xy2_pow = p2.X * p2.X + p2.Y * p2.Y;
            float xy3_pow = p3.X * p3.X + p3.Y * p3.Y;
            float[,] matr_d = new float[3, 3] { { p1.X, p1.Y, 1 },{p2.X, p2.Y, 1 }, {p3.X, p3.Y, 1 } };
            float[,] matr_x = new float[3, 3] { {xy1_pow, p1.Y, 1 }, {xy2_pow, p2.Y, 1 }, {xy3_pow, p3.Y, 1 } };
            float[,] matr_y = new float[3, 3] { { xy1_pow, p1.X, 1 }, { xy2_pow, p2.X, 1 }, { xy3_pow, p3.X, 1 } };
            float d = (float)(2 * Det(matr_d));
            float x = (float)((1.0 / d) * Det(matr_x));
            float y = (float)(-(1.0 / d) * Det(matr_y));
            return new PointF(x, y);
           
        }
        // return det of matrix 3*3
        static public float Det(float[,] matr)
        {
            float m1 = matr[0, 0] * matr[1, 1] * matr[2, 2];
            float m2 = matr[1, 0] * matr[2, 1] * matr[0, 2];
            float m3 = matr[2, 0] * matr[0, 1] * matr[1, 2];
            float h1 = matr[0, 2] * matr[1, 1] * matr[2, 0];
            float h2 = matr[0, 0] * matr[2, 1] * matr[1, 2];
            float h3 = matr[1, 0] * matr[0, 1] * matr[2, 2];
            return m1 + m2 + m3 - h1 - h2 - h3;
        }

        static public Edge EdgeByAngle(PointF start, int angle, float length)
        {
            double x = (Math.Cos((angle * Math.PI) / 180) * length);
            double y = (Math.Sin((angle * Math.PI) / 180) * length);
            return new Edge(new PointF(start.X, start.Y), new PointF(start.X + (float)x, start.Y + (float)y));
        }
        static public double MultVectors(PointF v1, PointF v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        static public bool CheckEdgesForIntersection(Edge e1, Edge e2, ref Point res)
        {
            float x1 = e1.start.X;
            float y1 = e1.start.Y;

            float x2 = e1.end.X;
            float y2 = e1.end.Y;

            float x3 = e2.start.X;
            float y3 = e2.start.Y;

            float x4 = e2.end.X;
            float y4 = e2.end.Y;

            PointF v_e2se2e = new PointF(x4 - x3, y4 - y3);
            PointF v_e2se1s = new PointF(x1 - x3, y1 - y3);
            PointF v_e2se1e = new PointF(x2 - x3, y2 - y3);
            PointF v_e1se1e = new PointF(x2 - x1, y2 - y1);
            PointF v_e1se2s = new PointF(x3 - x1, y3 - y1);
            PointF v_e1se2e = new PointF(x4 - x1, y4 - y1);

            double v1 = MultVectors(v_e2se2e, v_e2se1s);
            double v2 = MultVectors(v_e2se2e, v_e2se1e);
            double v3 = MultVectors(v_e1se1e, v_e1se2s);
            double v4 = MultVectors(v_e1se1e, v_e1se2e);

            double mult1 = v1 * v2;
            double mult2 = v3 * v4;

            if (mult1 < 0 && mult2 < 0)
            {
                double a1 = y2 - y1;
                double b1 = x1 - x2;
                double c1 = x1 * (y1 - y2) + y1 * (x2 - x1);

                double a2 = y4 - y3;
                double b2 = x3 - x4;
                double c2 = x3 * (y3 - y4) + y3 * (x4 - x3);
                double det = a1 * b2 - a2 * b1;
                double detx = c2 * b1 - c1 * b2;
                double dety = c1 * a2 - a1 * c2;
                res = new Point((int)(detx / det), (int)(dety / det));
                return true;
            }

            return false;

        }

        //Перемножение матриц
        static public double[,] MatrixMultiplication(double[,] matrixA, double[,] matrixB)
        {
            if (matrixA.ColumnsCount() != matrixB.RowsCount())
            {
                throw new Exception("Умножение не возможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");
            }

            var matrixC = new double[matrixA.RowsCount(), matrixB.ColumnsCount()];

            for (var i = 0; i < matrixA.RowsCount(); i++)
            {
                for (var j = 0; j < matrixB.ColumnsCount(); j++)
                {
                    matrixC[i, j] = 0;

                    for (var k = 0; k < matrixA.ColumnsCount(); k++)
                    {
                        matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return matrixC;
        }

        static public bool SamePoint(Point p1, Point p2)
        {
            if (Math.Abs(p1.X - p2.X) <= 3 && Math.Abs(p1.Y - p2.Y) <= 3)
                return true;
            return false;
        }

        static public bool EdgeHasPoint(Point p, Edge e)
        {
            double squared_sdx = (e.start.X - p.X) * (e.start.X - p.X);
            double squared_sdy = (e.start.Y - p.Y) * (e.start.Y - p.Y);
            double squared_edx = (e.end.X - p.X) * (e.end.X - p.X);
            double squared_edy = (e.end.Y - p.Y) * (e.end.Y - p.Y);
            double squared_edge_dist_x = (e.end.X - e.start.X) * (e.end.X - e.start.X);
            double squared_edge_dist_y = (e.end.Y - e.start.Y) * (e.end.Y - e.start.Y);

            double kek = Math.Sqrt(squared_sdx + squared_sdy) + Math.Sqrt(squared_edx + squared_edy)
                - Math.Sqrt(squared_edge_dist_x + squared_edge_dist_y);
            if (Math.Abs(kek) < 1)
                return true;
            return false;
        }

        static public void RotateEdge(ref Edge e, double angle)
        {
            angle = angle * (Math.PI / 180.0);
            PointF center = new PointF(e.start.X + (e.end.X - e.start.X) / 2, e.start.Y + (e.end.Y - e.start.Y) / 2);

            var a = center.X;
            var b = center.Y;
            var p = e.start;
            var matr1 = new double[1, 3] { { p.X, p.Y, 1 } };
            var matr2 = new double[3, 3] { { Math.Cos(angle), Math.Sin(angle), 0 }, { -Math.Sin(angle), Math.Cos(angle), 0 }, { -a * Math.Cos(angle) + b * Math.Sin(angle) + a, -a * Math.Sin(angle) - b * Math.Cos(angle) + b, 1 } };
            var res = MatrixMultiplication(matr1, matr2);
            var newstart = new Point((int)res[0, 0], (int)res[0, 1]);
            p = e.end;
            matr1 = new double[1, 3] { { p.X, p.Y, 1 } };
            matr2 = new double[3, 3] { { Math.Cos(angle), Math.Sin(angle), 0 }, { -Math.Sin(angle), Math.Cos(angle), 0 }, { -a * Math.Cos(angle) + b * Math.Sin(angle) + a, -a * Math.Sin(angle) - b * Math.Cos(angle) + b, 1 } };
            res = MatrixMultiplication(matr1, matr2);
            var newend = new Point((int)res[0, 0], (int)res[0, 1]);

            e = new Edge(newstart, newend);
        }

        static public void DrawPoint(ref Bitmap bitmap, PointF e, Color color)
        {
            if (e.X > 0 && e.X < bitmap.Width && e.Y > 0 && e.Y < bitmap.Height)
            {
                bitmap.SetPixel((int)e.X + 1, (int)e.Y, color);
                bitmap.SetPixel((int)e.X - 1, (int)e.Y, color);
                bitmap.SetPixel((int)e.X + 1, (int)e.Y + 1, color);
                bitmap.SetPixel((int)e.X - 1, (int)e.Y + 1, color);
                bitmap.SetPixel((int)e.X + 1, (int)e.Y - 1, color);
                bitmap.SetPixel((int)e.X - 1, (int)e.Y - 1, color);
                bitmap.SetPixel((int)e.X, (int)e.Y + 1, color);
                bitmap.SetPixel((int)e.X, (int)e.Y - 1, color);

                bitmap.SetPixel((int)e.X, (int)e.Y, color);
            }

        }

        static public void DrawEdge(ref Graphics g, ref Bitmap bitmap, Edge e)
        {
            Pen p = new Pen(Color.Black, 1);
            g.DrawLine(p, e.start, e.end);
            DrawPoint(ref bitmap, e.end, Color.Red);
        }

        // метод расширения для получения количества строк матрицы
        public static int RowsCount(this double[,] matrix)
        {
            return matrix.GetUpperBound(0) + 1;
        }

        // метод расширения для получения количества столбцов матрицы
        public static int ColumnsCount(this double[,] matrix)
        {
            return matrix.GetUpperBound(1) + 1;
        }
    }
}
