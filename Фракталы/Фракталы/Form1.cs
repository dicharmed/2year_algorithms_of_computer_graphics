using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Фракталы
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //С загрузкой формы выводим сообщение!
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text="Введите длину отрезка!";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //обработка кнопки "Построить"
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();

            //очистка окна
            g.Clear(Color.White);

            //считываем длину отрезка
            int a = Int32.Parse(textBox1.Text);

            //рассчитываем координату Х
            int x = 610 / 2 - a / 2;

            // вызываем функцию прорисовки
            DrawB(x, 10, a);
        }
        //функция прорисовки
        private void DrawB(int x, int y, int width)
        {
            Graphics g = pictureBox1.CreateGraphics();
            // выбираем цвет заливки 
            SolidBrush Black = new SolidBrush(Color.Black);
            Pen myPen = new Pen(Color.Black, 1);

            //Поставим условие вызова и прорисовки 

            if (width >= 3)
            {
                //Отрезки изображаем прямоугольниками для наглядности
                g.DrawRectangle(myPen, x, y, width, 12);
                g.FillRectangle(Black, x, y, width, 12);

                //Сдвигаемся вниз
                y = y + 40;

                //Вызываем функцию для двух полученных отрезков
                DrawB(x + width * 2 / 3, y, width / 3);
                DrawB(x, y, width / 3);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
