using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gonka1
{
    public partial class Form1 : Form
    {
        private Point pos;
        private bool dragging, lose = false;
        private int coinsCount = 0;
        public Form1()
        {
            InitializeComponent();

            bg1.MouseDown += MouseClickDown;
            bg1.MouseUp += MouseClickUp;
            bg1.MouseMove += MouseClickMove;
            bg2.MouseDown += MouseClickDown;
            bg2.MouseUp += MouseClickUp;
            bg2.MouseMove += MouseClickMove;
            labelLose.Visible = false;
            restartbtn.Visible = false;
            KeyPreview = true;
        }

        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            pos.X = e.X;
            pos.Y = e.Y;
        }
        private void MouseClickUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentPoint = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point (currentPoint.X - pos.X, currentPoint.Y - pos.Y + bg1.Top);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char) Keys.Escape)
            {
                this.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int speed = 10;
            bg1.Top += speed;
            bg2.Top += speed;
            coin.Top += speed;

            int enemySpeed = 13;
            enemy1.Top += enemySpeed;
            enemy2.Top += enemySpeed;
            enemy3.Top += enemySpeed;
            
            if(coin.Top >= 503)
            {
                coin.Top = -690;
                Random rand = new Random();
                coin.Left = rand.Next(70, 368);
            }

            if (bg1.Top >= 503)
            {
                bg1.Top = 0;
                bg2.Top = -503;
            }
            if (enemy1.Top >= 503)
            {
                enemy1.Top = -218;
                Random rand = new Random();
                enemy1.Left = rand.Next(70, 368);
            }
            if (enemy2.Top >= 503)
            {
                enemy2.Top = -490;
                Random rand = new Random();
                enemy2.Left = rand.Next(70, 368);
            }
            if (enemy3.Top >= 503)
            {
                enemy3.Top = -790;
                Random rand = new Random();
                enemy3.Left = rand.Next(70, 368);
            }
            if (player.Bounds.IntersectsWith(enemy1.Bounds) || player.Bounds.IntersectsWith(enemy2.Bounds) || player.Bounds.IntersectsWith(enemy3.Bounds))
            {
                timer.Enabled = false;
                labelLose.Visible = true;
                restartbtn.Visible = true;
                lose = true;
            }
            if (player.Bounds.IntersectsWith(coin.Bounds))
            {
                coin.Top = -690;
                coinsCount++;
                count.Text = ("Монеты: " + coinsCount);
                Random rand = new Random();
                coin.Left = rand.Next(70, 368);

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose) return;
            int speed = 10;
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && player.Left > 70)
            {
                player.Left -= speed;

            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D && player.Left < 368)
            {
                player.Left += speed;
            }
        }

        private void restartbtn_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            enemy1.Top = -218;
            enemy2.Top = -490;
            enemy3.Top = -790;
            coin.Top = -690;
            labelLose.Visible = false;
            restartbtn.Visible = false;
            lose = false;
            coinsCount = 0;
            count.Text = ("Монеты: " + coinsCount);



        }
    }
}
