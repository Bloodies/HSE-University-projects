using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab4.AffinStuff;

namespace LSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bm;
            g = Graphics.FromImage(bm);
            rules = new Dictionary<char, string>();
            draw_queue = new Queue<Edge>();
            parceFile();
           
        }
        Bitmap bm;
        Graphics g;
        int main_angle;
        int dangle;
        Dictionary<char, string> rules;
        string atom;
        float f_length = 10;
        float thicknes = 10;

        Queue<Edge> draw_queue;
        Queue<int> save_rand_angle;
        Random r = new Random();

        public void parceFile(string fname = "..//..//settings.txt")
        {
            using (StreamReader file = new StreamReader(fname))
            {
                string ln = file.ReadLine();
                var arr = ln.Split(';');
                atom = arr[0];
                dangle = Int16.Parse(arr[1]);
                main_angle = Int16.Parse(arr[2]);
                while ((ln = file.ReadLine()) != null)
                {
                    rules[ln[0]] =  ln.Substring(3);
                }
                file.Close();
               
            }
        }
        string createIterString(string cur)
        {
            StringBuilder res = new StringBuilder();
            foreach(var x in cur)
            {
                if (rules.ContainsKey(x))
                    res.Append(rules[x]);
                else res.Append(x);
            }
            return res.ToString();
        }
        public float ScaleForward(string cur_iter, ref PointF start_point, int angle, int iter )
        {
           
            float length = f_length;
            double minx = 0;
            double maxx = 0;
            double miny = pictureBox1.Height - 10;
            double maxy = pictureBox1.Height - 10;

            Stack<PointF> save_point = new Stack<PointF>();
            Stack<int> save_angle = new Stack<int>();

            save_rand_angle = new Queue<int>();
            bool rand_on = false;
            float init_length = length;
            

            foreach (var ch in cur_iter)
            {
                if (ch == '+')
                {
                    if (rand_on)
                        angle += dangle + r.Next(-dangle, dangle) / 2;
                    else angle += dangle;

                }
                else if (ch == '-')
                {
                    if (rand_on)
                        angle -= dangle + r.Next(-dangle, dangle) / 2;
                    else angle -= dangle;
                }
                else if (ch == 'F')
                {
                    if (rand_on)
                        save_rand_angle.Enqueue(angle);

                    Edge e = EdgeByAngle(start_point, angle, length);
                    if (e.end.X > maxx)
                        maxx = e.end.X;
                    if (e.end.Y > maxy)
                        maxy = e.end.Y;
                    if (e.end.Y < miny)
                        miny = e.end.Y;
                    if (e.end.X < minx)
                        minx = e.end.X;
                    start_point = e.end;
                }
                else if (ch == '[')
                {
                    save_angle.Push(angle);
                    save_point.Push(start_point);
                }
                else if (ch == ']')
                {
                    start_point = save_point.Pop();
                    angle = save_angle.Pop();
                }
                else if (ch == '@')
                {
                    rand_on = !rand_on;
                }
                else if (ch == '(') // уменьшение 
                {
                    length -= (float)(1.0 / iter * init_length);
                }
                else if (ch == ')')
                {
                    length += (float)(1.0 / iter * init_length);
                }
                
            }
            double dx = maxx - minx;
            double propor_x = dx != 0 ? (pictureBox1.Width -1 ) / dx : -1;
            double dy = maxy - miny;
            double propor_y = dy != 0 ? (pictureBox1.Height -1) / dy : -1;
            double min_propor;
            if (propor_x < propor_y)
            {
                min_propor = propor_x;
            }
            else
            {
                min_propor = propor_y;
            }
            start_point = new PointF((float)Math.Abs(minx * min_propor),
                                        (float)Math.Abs((miny - pictureBox1.Height + 10) * min_propor));
            return length * (float)min_propor;
        }
        public void LSystem(int iter)
        {
            Color c = Color.Green;//FromArgb(128, 64, 0); // Brown
            f_length = iter;
            thicknes = 9;
            PointF cur_point = new PointF(0, pictureBox1.Height - 10);
            string cur_iter = atom;
            int cur_angle = main_angle;
            for(int i = 0; i < iter; i++)
            {
                cur_iter = createIterString(cur_iter);
            }

            f_length = ScaleForward(cur_iter, ref cur_point, cur_angle, iter);

            Stack<PointF> save_point = new Stack<PointF>();
            Stack<int> save_angle = new Stack<int>();
            bool rand_on = false;
            float init_f_length = f_length;
            float init_thicknes = thicknes;
           
            foreach (var ch in cur_iter)
            {
                if (ch == '+')
                {
                    cur_angle += dangle;
                }
                else if (ch == '-')
                {
                    cur_angle -= dangle;

                }
                else if (ch == 'F')
                {
                    if (rand_on)
                    {
                        cur_angle = save_rand_angle.Dequeue();
                    }
                    Edge e = EdgeByAngle(cur_point, cur_angle, f_length);
                    e.thicknes = thicknes;
                    e.color = c;
                    draw_queue.Enqueue(e);
                    cur_point = e.end;
                }
                else if (ch == '[')
                {
                    save_angle.Push(cur_angle);
                    save_point.Push(cur_point);
                }
                else if (ch == ']')
                {
                    cur_point = save_point.Pop();
                    cur_angle = save_angle.Pop();
                }
                else if (ch == '@')
                {
                    rand_on = !rand_on;
                }
                else if (ch == '(') // уменьшение 
                {
                    f_length -= (float)(1.0 / iter * init_f_length);
                    thicknes -= (float)(1.0 / iter * init_thicknes);
                    int green = c.G + (int)(1.0 / iter * (255 - 64));
                    green = green > 255 ? 255 : green;
                    green = green < 0 ? 0 : green;
                    c = Color.Green;//FromArgb(c.R, green, c.B);
                }
                else if (ch == ')')
                {
                    f_length += (float)(1.0 / iter * init_f_length);
                    thicknes += (float)(1.0 / iter * init_thicknes);
                    int green = c.G - (int)(1.0 / iter * (255 - 64));
                    green = green > 255 ? 255 : green;
                    green = green < 0 ? 0 : green;
                    c = Color.Green;//FromArgb(c.R, green, c.B);

                }
            }
        }

        public void DrawQueue()
        {
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bm);
            while (draw_queue.Count > 0)
            {
                var edge = draw_queue.Dequeue();
                g.DrawLine(new Pen(edge.color, edge.thicknes), edge.start, edge.end);
            }

            pictureBox1.Image = bm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int iter = (int)numericUpDown1.Value;
            LSystem(iter);
            DrawQueue();
        }
    }
}
