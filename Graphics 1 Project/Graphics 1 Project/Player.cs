using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_1_Project
{
    class Player
    {
        public PointF center;
        public float radius;
        public List<PointF> points;
        public float theta;

        public Player(PointF center, float radius)
        {
            this.center = center;
            this.radius = radius;
            points = new List<PointF>();
            CreateShape();
        }

        public void DrawSelf(Graphics g)
        {
            DrawShape(g);
        }

        public void Rotate(PointF mousePosition, PointF middle)
        {
            float dx = mousePosition.X - middle.X;
            float dy = mousePosition.Y - middle.Y;
            float mouseTheta = (float)Math.Atan2(dy, dx);

            for (int i = 0; i < points.Count; i++)
            {
                points[i] = RotatePoint(points[i], middle, mouseTheta - theta);
            }

            this.theta = mouseTheta;
        }

        private PointF RotatePoint(PointF movingPoint, PointF referencePoint, float theta)
        {
            PointF temp = new PointF(movingPoint.X - referencePoint.X, movingPoint.Y - referencePoint.Y);

            PointF temp2 = new PointF();
            temp2.X = (float)(temp.X * Math.Cos(theta) - temp.Y * Math.Sin(theta));
            temp2.Y = (float)(temp.X * Math.Sin(theta) + temp.Y * Math.Cos(theta));

            temp = new PointF(temp2.X + referencePoint.X, temp2.Y + referencePoint.Y);
            return temp;
        }

        private void DrawShape(Graphics g)
        {
            Pen pen = new Pen(Color.Teal, 5);
            for (int i = 0; i < points.Count - 2; i++)
            {
                g.DrawLine(pen, points[i], points[i + 1]);
            }
            g.DrawLine(pen, points[points.Count - 2], points[0]);
            g.DrawLine(pen, points[0], points[points.Count - 1]);
        }

        private void CreateShape()
        {
            points.Add(new PointF(823f, 406f));
            points.Add(new PointF(795f, 353f));
            points.Add(new PointF(723f, 373f));
            points.Add(new PointF(723f, 439f));
            points.Add(new PointF(795f, 459f));
            points.Add(new PointF(873f, 406f));

            theta = 0;
        }
    }
}
