using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_1_Project
{
    public partial class Form1 : Form
    {
        Curve curve;
        Player player;
        ShootingBall shootingBall;
        bool isCreateCurve;
        int ctCreateBall = 0;
        Color[] colors;
        Random random;
        Color nextColor;
        PointF mousePosition;

        Bitmap off;
        Timer t = new Timer();

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.KeyDown += Form1_KeyDown;
            t.Tick += T_Tick;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (curve.points.Count == 0)
                    {
                        MessageBox.Show("You Must Draw The Curve");
                    }
                    else
                    {
                        isCreateCurve = false;
                        curve.isMoveBalls = true;
                    }
                    break;

                case Keys.C:
                    if (isCreateCurve) { curve.points.Clear(); }
                    break;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (curve.isMoveBalls || isCreateCurve)
            {
                player.Rotate(new PointF(e.X, e.Y), new PointF(ClientSize.Width / 2, ClientSize.Height / 2));
                mousePosition = new PointF(e.X, e.Y);
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (ctCreateBall > 15 && curve.isMoveBalls)
            {
                Color color;
                if (nextColor == Color.White)
                {
                    color = colors[random.Next(colors.Length)];
                }
                else
                {
                    color = nextColor;
                    nextColor = Color.White;
                }
                Ball ball = new Ball(curve.points[0], 25, color);
                curve.balls.Add(ball);

                ctCreateBall = 0;
            }

            curve.MoveBalls(0.003f);
            shootingBall.Move(20);
            Check();
            if (shootingBall.center.X < 0 || shootingBall.center.X > ClientSize.Width || shootingBall.center.Y < 0 || shootingBall.center.Y > ClientSize.Height && shootingBall.isMove)
            {
                DestroyShootingBall();
            }

            if (!shootingBall.isMove)
            {
                shootingBall.center = player.points[player.points.Count - 1];
            }

            ctCreateBall++;
            DrawDubb(CreateGraphics());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isCreateCurve)
            {
                curve.points.Add(new PointF(e.X, e.Y)); 
            }
            else
            {
                if (curve.isMoveBalls)
                {
                    shootingBall.CalculateValues(new PointF(e.X, e.Y)); 
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);

            curve = new Curve();
            player = new Player(new PointF(ClientSize.Width / 2, ClientSize.Height / 2), 50);
            isCreateCurve = true;
            colors = new Color[] { Color.Red, Color.Blue, Color.Orange, Color.DarkGoldenrod, Color.DarkCyan };
            random = new Random();
            shootingBall = new ShootingBall();
            shootingBall.color = colors[random.Next(colors.Length)];
            shootingBall.radius = 10;

            nextColor = Color.White;

            t.Start();
        }

        private void DestroyShootingBall()
        {
            shootingBall = new ShootingBall();
            shootingBall.color = colors[random.Next(colors.Length)];
            shootingBall.radius = 10;
        }

        private void Check()
        {
            if (shootingBall.isMove && curve.isMoveBalls)
            {
                float dx, dy;
                for (int i = 0; i < curve.balls.Count; i++)
                {
                    dx = curve.balls[i].center.X - shootingBall.center.X;
                    dy = curve.balls[i].center.Y - shootingBall.center.Y;
                    if (Math.Pow(dx, 2) + Math.Pow(dy, 2) - Math.Pow(shootingBall.radius, 2) <= 0)
                    {
                        if (shootingBall.color == curve.balls[i].color)
                        {
                            RemoveBallsAfter(i);
                            RemoveBallsBefore(i - 1);
                        }
                        else
                        {
                            AddBall(i);
                        }
                        DestroyShootingBall();
                    }
                }
            }
        }

        private void AddBall(int i)
        {
            nextColor = curve.balls[curve.balls.Count - 1].color;

            for (int j = curve.balls.Count - 1; j > i; j--)
            {
                curve.balls[j].color = curve.balls[j - 1].color;
            }

            curve.balls[i].color = shootingBall.color;
        }

        private void RemoveBallsAfter(int i)
        {
            if (i >= curve.balls.Count || curve.balls[i].color != shootingBall.color)
            {
                return;
            }

            RemoveBallsAfter(i + 1);

            curve.balls.RemoveAt(i);
        }

        private void RemoveBallsBefore(int i)
        {
            if (i < 0 || curve.balls[i].color != shootingBall.color)
            {
                return;
            }
            else
            {
                curve.balls.RemoveAt(i);
            }

            RemoveBallsBefore(i - 1);
        }

        private void DrawScene(Graphics g)
        {
            g.Clear(Color.White);

            curve.DrawSelf(g);
            player.DrawSelf(g);
            if (!isCreateCurve)
            {
                shootingBall.DrawSelf(g);
                g.DrawLine(new Pen(Color.BlueViolet, 2), player.points[0], mousePosition);
            }
        }

        private void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
