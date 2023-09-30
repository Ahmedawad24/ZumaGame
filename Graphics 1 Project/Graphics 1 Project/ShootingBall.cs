using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_1_Project
{
    class ShootingBall
    {
        public PointF center, start, end;
        public float radius, slope, invSlope;
        public Color color;
        public bool isMove;

        public void DrawSelf(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillEllipse(brush, center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }

        public void Move(float speed)
        {
            if (isMove)
            {
                if (Math.Abs(end.X - start.X) > Math.Abs(end.Y - start.Y))
                {
                    if (end.X > start.X)
                    {
                        center.X += speed;
                        center.Y += speed * slope;
                    }
                    else
                    {
                        center.X -= speed;
                        center.Y -= speed * slope;
                    }
                }
                else
                {
                    if (end.Y > start.Y)
                    {
                        center.Y += speed;
                        center.X += speed * invSlope;
                    }
                    else
                    {
                        center.Y -= speed;
                        center.X -= speed * invSlope;
                    }
                }
            }
        }

        public void CalculateValues(PointF click)
        {
            if (!isMove)
            {
                end = click;
                start = center;
                float dx = end.X - center.X;
                float dy = end.Y - center.Y;
                slope = dy / dx;
                invSlope = dx / dy;
                radius = 25;
                isMove = true; 
            }
        }
    }
}
