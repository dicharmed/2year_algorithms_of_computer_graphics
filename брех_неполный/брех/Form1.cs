using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace брех
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {        }
        //Метод, устанавливающий пиксел на форме с заданными цветом и прозрачностью
        private static void PutPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        //Статический метод, реализующий отрисовку 4-связной линии
        static public void Bresenham4Line(Graphics g, Color clr, int x0, int y0,
                                                                 int x1, int y1)
        {
            //Изменения координат
            int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
            int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
            //Направление приращения
            int sx = (x1 >= x0) ? (1) : (-1);
            int sy = (y1 >= y0) ? (1) : (-1);

            if (dy < dx)
            {
                int d = (dy << 1) - dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                PutPixel(g, clr, x0, y0, 255);
                int x = x0 + sx;
                int y = y0;
                for (int i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    x++;
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                PutPixel(g, clr, x0, y0, 255);
                int x = x0;
                int y = y0 + sy;
                for (int i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    y++;
                }
            }
        }
        public static void BresenhamCircle(Graphics g, Color clr, int _x, int _y, int radius)
        {
            int x = 0, y = radius, gap = 0, delta = (2 - 2 * radius);
            while (y >= 0)
            {
                PutPixel(g, clr, _x + x, _y + y, 255);
                PutPixel(g, clr, _x + x, _y - y, 255);
                PutPixel(g, clr, _x - x, _y - y, 255);
                PutPixel(g, clr, _x - x, _y + y, 255);
                gap = 2 * (delta + y) - 1;
                if (delta < 0 && gap <= 0)
                {
                    x++;
                    delta += 2 * x + 1;
                    continue;
                }
                if (delta > 0 && gap > 0)
                {
                    y--;
                    delta -= 2 * y + 1;
                    continue;
                }
                x++;
                delta += 2 * (x - y);
                y--;
            }
        }
        
        public static void BresenhamEllipse(Graphics g, Color clr, int x, int y, int a, int b)
        {
            int col, row;
            long a_square, b_square, two_a_square, two_b_square, four_a_square, four_b_square, d;

            b_square = b * b;
            a_square = a * a;
            row = b;
            col = 0;
            two_a_square = a_square << 1;
            four_a_square = a_square << 2;
            four_b_square = b_square << 2;
            two_b_square = b_square << 1;
            d = two_a_square * ((row - 1) * (row)) + a_square + two_b_square * (1 - a_square);
            while (a_square * (row) > b_square * (col))
            {
                PutPixel(g,clr, col + x, row + y, 255);
                PutPixel(g,clr, col + x, y - row,255);
                PutPixel(g,clr, x - col, row + y, 255);
                PutPixel(g,clr, x - col, y - row, 255);
                if (d >= 0)
                {
                    row--;
                    d -= four_a_square * (row);
                }
                d += two_b_square * (3 + (col << 1));
                col++;
            }
            d = two_b_square * (col + 1) * col + two_a_square * (row * (row - 2) + 1) + (1 - two_a_square) * b_square;
            
            while ((row)+1>0)
            {
                PutPixel(g, clr, col + x, row + y, 255);
                PutPixel(g, clr, col + x, y - row, 255);
                PutPixel(g, clr, x - col, row + y, 255);
                PutPixel(g, clr, x - col, y - row, 255);
                if (d <= 0)
                {
                    col++;
                    d += four_b_square * col;
                }
                row--;
                d += two_a_square * (3 - (row << 1));
            }
        }
        private void button1_Click(object sender, EventArgs e)//click BUILD
        {
            if(radioButton1.Checked==true)
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.Clear(Color.White);
                Bresenham4Line(g, Color.Black, 10, 34,
                    pictureBox1.ClientSize.Width - 10, pictureBox1.ClientSize.Height - 10);
            }
            if(radioButton2.Checked==true)
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.Clear(Color.White);
                BresenhamCircle(g, Color.Black,pictureBox1.ClientSize.Width/2, 
                    pictureBox1.ClientSize.Height/2, 90);
            }
            if (radioButton3.Checked == true)
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.Clear(Color.White);
                BresenhamEllipse(g, Color.Black, pictureBox1.ClientSize.Width / 2,
                    pictureBox1.ClientSize.Height / 2, 90,30);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
