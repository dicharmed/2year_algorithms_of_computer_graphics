using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace аф
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {  }
        private void pictureBox1_Click(object sender, EventArgs e)
        {    }
        private void Form1_Load(object sender, EventArgs e)
        {       
        }

//--------------------АФФИННЫЕ ПРЕОБРАЗОВАНИЯ----------------------//
        private void Crd()//ось координат
        {
            //оси x,y
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            //высота и ширина 
            float w = pictureBox1.Bounds.Width;
            float h = pictureBox1.Bounds.Height;

            //x 
            Pen pen = new Pen(Color.Black, 1);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            //+-20 отступ от края рамки pictureBox1
            g.DrawLine(pen, 0 + 20, h / 2, w - 20, h / 2);
            g.DrawString("x", new Font("Caliber", 11), Brushes.Black, new PointF(w - 25, (h / 2) + 10));

            //y
            Pen penn = new Pen(Color.Black, 1);
            penn.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            g.DrawLine(penn, w / 2, 0, w / 2, h);
            g.DrawString("y", new Font("Caliber", 11), Brushes.Black, new PointF((w / 2) - 20, 10));
            
        }
  
        private void square_build()//построить квадрат
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Crd();//рисуем ось           
            //квадрат
            Pen pn = new Pen(Color.Black, 1);

            if (textBox1.Text == "" && textBox2.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Введите параметры квадрата!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                ////получаем данные квадрата от пользователя
                int Ax = Int32.Parse(textBox1.Text);
                int Ay = Int32.Parse(textBox2.Text);
                int a = Int32.Parse(textBox4.Text);

                // Create rectangle.
                Rectangle rect = new Rectangle(Ax, Ay, a, a);
                g.DrawRectangle(pn, rect);
                g.DrawString("A", new Font("Caliber", 11), Brushes.Black, new PointF(Ax - 20, Ay - 20));
                g.DrawString("B", new Font("Caliber", 11), Brushes.Black, new PointF(Ax + a + 5, Ay - 20));
                g.DrawString("C", new Font("Caliber", 11), Brushes.Black, new PointF(Ax - 20, Ay + a));
                g.DrawString("D", new Font("Caliber", 11), Brushes.Black, new PointF(Ax + a + 5, Ay + a));

            }
        }        

        private void button1_Click(object sender, EventArgs e)//кнопка построить квадрат
        {
            square_build();
        }

        private void rotate()//поворот
        {
            //координаты квадрата
            int a = Int32.Parse(textBox4.Text); //сторона квадрата
                                                //A
            int Axrout = Int32.Parse(textBox1.Text);
            int Ayrout = Int32.Parse(textBox2.Text);
            //B
            int Bx = Axrout + a;
            int By = Ayrout;
            //C
            int Cx = Axrout;
            int Cy = Ayrout + a;
            //D
            int Dx = Axrout + a;
            int Dy = Ayrout + a;

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Crd();//рисуем ось

            //Координаты первоначального квадрата
            PointF[] o = new PointF[] { new PointF(Axrout, Ayrout), new PointF(Bx, By), new PointF(Dx, Dy), new PointF(Cx, Cy) };
            //угол от пользователя
            int alpha = Int32.Parse(textBox7.Text);
            PointF r = new PointF(Axrout, Ayrout); // точка относительно которой поворачиваем
            double angleRadian = alpha * Math.PI / 180; //переводим угол в радианты
            PointF[] qw = new PointF[o.Length]; //для хранения новых координат квадрата
            for (int j = 0; j < o.Length; j++)
            {
                //X = (x — x0) *cos(alpha) — (y — y0) *sin(alpha) + x0;
                //Y = (x — x0) *sin(alpha) + (y — y0) *cos(alpha) + y0;
                float x = (float)((o[j].X - r.X) * Math.Cos(angleRadian) - (o[j].Y - r.Y) * Math.Sin(angleRadian) + r.X);
                float y = (float)((o[j].X - r.X) * Math.Sin(angleRadian) + (o[j].Y - r.Y) * Math.Cos(angleRadian) + r.Y);
                qw[j] = new PointF(x, y);
            }
            //Рисуем повернутый объект
            g.DrawPolygon(Pens.Black, qw);
            g.DrawString("A", new Font("Caliber", 11), Brushes.Black, qw[0]);
            g.DrawString("B", new Font("Caliber", 11), Brushes.Black, qw[1]);
            g.DrawString("C", new Font("Caliber", 11), Brushes.Black, qw[3]);
            g.DrawString("D", new Font("Caliber", 11), Brushes.Black, qw[2]);
        }

        private void move()//сдвиг
        {
            int k = Int32.Parse(textBox5.Text);//коэффициент сдвига
            //координаты квадрата
            int a = Int32.Parse(textBox4.Text); //сторона квадрата
            //A
            int Axrout = Int32.Parse(textBox1.Text);
            int Ayrout = Int32.Parse(textBox2.Text);
            //B
            int Bx = Axrout + a;
            int By = Ayrout;
            //C
            int Cx = Axrout;
            int Cy = Ayrout + a;
            //D
            int Dx = Axrout + a;
            int Dy = Ayrout + a;

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Crd();//рисуем ось

            //Координаты первоначального квадрата
            PointF[] o = new PointF[] { new PointF(Axrout, Ayrout), new PointF(Bx, By), new PointF(Dx, Dy), new PointF(Cx, Cy) };
           
            PointF[] qw = new PointF[o.Length]; //для хранения новых координат квадрата
            for (int j = 0; j < o.Length; j++)
            {
                
                float x = (float)(o[j].X + k);
                float y = (float)(o[j].Y);
                qw[j] = new PointF(x, y);
            }
            //Рисуем повернутый объект
            g.DrawPolygon(Pens.Black, qw);
            g.DrawString("A", new Font("Caliber", 11), Brushes.Black, qw[0]);
            g.DrawString("B", new Font("Caliber", 11), Brushes.Black, qw[1]);
            g.DrawString("C", new Font("Caliber", 11), Brushes.Black, qw[3]);
            g.DrawString("D", new Font("Caliber", 11), Brushes.Black, qw[2]);


        }

        private void dilatation_vertical()//растяжение
        {
            int step = Int32.Parse(textBox3.Text); //коэффициент растяжения
                                                   //координаты квадрата
            int a = Int32.Parse(textBox4.Text); //сторона квадрата
           // a *= step;
            //A
            int Axrout = Int32.Parse(textBox1.Text);
            int Ayrout = Int32.Parse(textBox2.Text);
            //B
            int Bx = Axrout + a;
            int By = Ayrout;
            //C
            int Cx = Axrout;
            int Cy = Ayrout + a;
            //D
            int Dx = Axrout + a;
            int Dy = Ayrout + a;

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Crd();//рисуем ось

            //Координаты первоначального квадрата
            PointF[] o = new PointF[] { new PointF(Axrout, Ayrout), new PointF(Bx, By), new PointF(Dx, Dy), new PointF(Cx, Cy) };
            g.DrawPolygon(Pens.Black, o);
            PointF[] qw = new PointF[o.Length]; //для хранения новых координат квадрата
            //растяжение. точки C,D переносятся на k=3 по оси y
            o[2].Y*=step;
            o[3].Y*=step;
            for (int j = 0; j < o.Length; j++)
            {

                float x = (float)(o[j].X);
                float y = (float)(o[j].Y);
           
              
                qw[j] = new PointF(x, y);
            }
            //Рисуем повернутый объект
            g.DrawPolygon(Pens.Red, qw);
            g.DrawString("A", new Font("Caliber", 11), Brushes.Black, qw[0]);
            g.DrawString("B", new Font("Caliber", 11), Brushes.Black, qw[1]);
            g.DrawString("C", new Font("Caliber", 11), Brushes.Black, qw[3]);
            g.DrawString("D", new Font("Caliber", 11), Brushes.Black, qw[2]);

        }

        private void dilatation_horizontal()//растяжение
        {
            int step = Int32.Parse(textBox3.Text); //коэффициент растяжения
                                                   //координаты квадрата
            int a = Int32.Parse(textBox4.Text); //сторона квадрата
             // a *= step;
             //A
            int Axrout = Int32.Parse(textBox1.Text);
            int Ayrout = Int32.Parse(textBox2.Text);
            //B
            int Bx = Axrout + a;
            int By = Ayrout;
            //C
            int Cx = Axrout;
            int Cy = Ayrout + a;
            //D
            int Dx = Axrout + a;
            int Dy = Ayrout + a;

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Crd();//рисуем ось

            //Координаты первоначального квадрата
            PointF[] o = new PointF[] { new PointF(Axrout, Ayrout), new PointF(Bx, By), new PointF(Dx, Dy), new PointF(Cx, Cy) };
            g.DrawPolygon(Pens.Black, o);
            PointF[] qw = new PointF[o.Length]; //для хранения новых координат квадрата
            //растяжение. точки B,D переносятся на k=3 по оси x
            o[1].X *= step;
            o[2].X*=step;
            for (int j = 0; j < o.Length; j++)
            {

                float x = (float)(o[j].X);
                float y = (float)(o[j].Y);


                qw[j] = new PointF(x, y);
            }
            //Рисуем повернутый объект
            g.DrawPolygon(Pens.Red, qw);
            g.DrawString("A", new Font("Caliber", 11), Brushes.Black, qw[0]);
            g.DrawString("B", new Font("Caliber", 11), Brushes.Black, qw[1]);
            g.DrawString("C", new Font("Caliber", 11), Brushes.Black, qw[3]);
            g.DrawString("D", new Font("Caliber", 11), Brushes.Black, qw[2]);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//по нажатию на "сдвиг" квадрат возвращается в первоначальное положение
        {
            square_build();
            groupBox5.Enabled = true;//включаем коэффициент сдвига 
            groupBox4.Enabled = false;//отключаем параметры растяжения
            groupBox3.Enabled = false;//отключаем градус поворота  
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)//по нажатию на "растяжение" квадрат возвращается в первоначальное положение
        {
            square_build();
            groupBox4.Enabled = true;//включаем параметры растяжения
            groupBox3.Enabled = false;//отключаем градус поворота
            groupBox5.Enabled = false;//отключаем коэффициент сдвига  
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)//по нажатию на "поворот" квадрат возвращается в первоначальное положение
        {
            square_build();
            groupBox3.Enabled = true;//включаем градус поворота
            groupBox4.Enabled = false;//отключаем параметры растяжения
            groupBox5.Enabled = false;//отключаем коэффициент сдвига          
        }

//---------контроль выбора только один чекбокс------------------------------------------------------//
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           square_build();

           if (checkBox1.Checked==true)
            {
                checkBox2.Checked = false;
                //checkBox3.Checked = false;
                //checkBox4.Checked = false;
            };
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            square_build();

            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                //checkBox3.Checked = false;
                //checkBox4.Checked = false;
            };
        }
        //private void checkBox3_CheckedChanged(object sender, EventArgs e)
        //{
        //    square_build();

        //    if (checkBox3.Checked == true)
        //    {
        //        checkBox1.Checked = false;
        //        checkBox2.Checked = false;
        //        checkBox4.Checked = false;
        //    };
        //}
        //private void checkBox4_CheckedChanged(object sender, EventArgs e)
        //{
        //    square_build();

        //    if (checkBox4.Checked == true)
        //    {
        //        checkBox1.Checked = false;
        //        checkBox2.Checked = false;
        //        checkBox3.Checked = false;
        //    };
        //}
//---------контроль выбора только один чекбокс------------------------------------------------------//

        private void button2_Click(object sender, EventArgs e)//выполнить
        {
            if (radioButton1.Checked == true)//сдвиг
            {
                move();
            };
            if (radioButton2.Checked == true)//растяжение
            {
                if (checkBox1.Checked == true)//растяжение по вертикали
                {
                    dilatation_vertical();
                };
                if (checkBox2.Checked == true)//растяжение по горизонтали
                {
                    dilatation_horizontal();
                };
                //if (checkBox3.Checked == true)//растяжение по диагонали AD
                //{ };
                //if (checkBox4.Checked == true)//растяжение по диагонали BC
                //{ };

            };
            if (radioButton3.Checked == true)//поворот
            {
                rotate();
            };

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
