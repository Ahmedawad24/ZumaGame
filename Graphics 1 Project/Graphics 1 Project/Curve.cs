using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_1_Project
{
    class Curve
    {
        public List<PointF> points = new List<PointF>();
        public List<Ball> balls = new List<Ball>();
        public bool isMoveBalls = false;

        public void DrawSelf(Graphics g)
        {
            for (float t = 0; t <= 1 && points.Count > 0; t += 0.0001f)
            {
                PointF point = CalculatePoint(t);
                float lineWidth = 10;
                g.FillEllipse(Brushes.CornflowerBlue, point.X - lineWidth / 2, point.Y - lineWidth / 2, lineWidth, lineWidth);
            }

            SolidBrush brush;
            for (int i = 0; i < balls.Count; i++)
            {
                brush = new SolidBrush(balls[i].color);
                g.FillEllipse(brush, balls[i].center.X - balls[i].radius, balls[i].center.Y - balls[i].radius, balls[i].radius * 2, balls[i].radius * 2);
            }
        }

        public void MoveBalls(float speed)
        {
            if (isMoveBalls)
            {
                for (int i = 0; i < balls.Count; i++)
                {
                    balls[i].t += speed;
                    balls[i].center = CalculatePoint(balls[i].t);
                    if (balls[i].t >= 1)
                    {
                        isMoveBalls = false;
                        break;
                    }
                } 
            }
        }

        private PointF CalculatePoint(float t)
        {
            PointF point = new PointF();
            int n = points.Count - 1;
            float c;

            for (int i = 0; i < points.Count; i++)
            {
                c = GetFactorial(n) / (GetFactorial(i) * GetFactorial(n - i));
                point.X += (float)(Math.Pow(t, i) * Math.Pow(1 - t, n - i) * c * points[i].X);
                point.Y += (float)(Math.Pow(t, i) * Math.Pow(1 - t, n - i) * c * points[i].Y);
            }

            return point;
        }

        private float GetFactorial(int power)
        {
            float f = 1;

            for (int i = 2; i <= power; i++)
            {
                f *= i;
            }

            return f;
        }
    }
}
