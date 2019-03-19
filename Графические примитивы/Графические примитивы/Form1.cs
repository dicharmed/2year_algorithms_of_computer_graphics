using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Графические_примитивы
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //клетка 20 пикселей
        int pix = 20;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Выбираем перо "myPen" черного цвета Black
            //толщиной в 2 пикселя:
            Pen myPen = new Pen(Color.Black, 3);
            Pen myPen1 = new Pen(Color.Red, 3);
            Pen myPen2 = new Pen(Color.Green, 3);
            Pen myPen3 = new Pen(Color.Blue, 3);
            //Объявляем объект "g" класса Graphics и предоставляем
            //ему возможность рисования на pictureBox1:
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            //клетки
            int w = this.ClientSize.Width;//размеры клиентской области
            int h = this.ClientSize.Height;//размеры клиентской области
            int widthLines = 20;//Ширина клетки
            int heightLines = 20;//Высота клетки
            for (int i = 0; i < w; i += widthLines)
            {
                //Вертикальные линии
                g.DrawLine(new Pen(Brushes.Blue), new Point(i + widthLines, 0), new Point(i + widthLines, h));
                //Горизонтальные линии
                g.DrawLine(new Pen(Brushes.Blue), new Point(0, i + heightLines), new Point(w, i + heightLines));
            }

            //Рисуем верхний треугольник ракеты       
            Point p1 = new Point((5 * pix) + (pix / 2), (2 * pix));// первая точка. общая точка для двух линий
            Point p2 = new Point(4 * pix, 7 * pix);// вторая точка            
            g.DrawLine(myPen, p1, p2);// рисуем линию           

            Point p3 = new Point((7 * pix), (7 * pix));// третья точка                  
            g.DrawLine(myPen, p1, p3);// рисуем линию

            //рисуем основную часть ракеты
            Point p4 = new Point(4 * pix, 18 * pix);
            g.DrawLine(myPen, p2, p4);// рисуем линию

            Point p5 = new Point(7 * pix, 18 * pix);
            g.DrawLine(myPen, p3, p5);// рисуем линию

            g.DrawLine(myPen, p4, p5);// рисуем линию

            //рисуем боковые части ракеты
            //left
            Point p6 = new Point(4 * pix, 12 * pix);
            Point p7 = new Point(2 * pix, 15 * pix);
            g.DrawLine(myPen, p6, p7);// рисуем линию

            Point p8 = new Point(2 * pix, 19 * pix);
            g.DrawLine(myPen, p7, p8);// рисуем линию

            Point p9 = new Point(3 * pix, 19 * pix);
            g.DrawLine(myPen, p8, p9);// рисуем линию

            g.DrawLine(myPen, p9, p4);// рисуем линию

            //right
            Point p11 = new Point(7 * pix, 12 * pix);
            Point p12 = new Point(9 * pix, 15 * pix);
            g.DrawLine(myPen, p11, p12);// рисуем линию

            Point p13 = new Point(9 * pix, 19 * pix);
            g.DrawLine(myPen, p12, p13);// рисуем линию

            Point p14 = new Point(8 * pix, 19 * pix);
            g.DrawLine(myPen, p13, p14);// рисуем линию

            g.DrawLine(myPen, p14, p5);// рисуем линию

            //иллюминаторы          
            g.DrawEllipse(myPen1, 5 * pix, 7 * pix, 20, 20);
            g.DrawEllipse(myPen2, 5 * pix, 9 * pix, 20, 20);
            g.DrawEllipse(myPen3, 5 * pix, 11 * pix, 20, 20);

        }
    }
}
