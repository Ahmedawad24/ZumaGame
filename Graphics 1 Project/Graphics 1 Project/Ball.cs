using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_1_Project
{
    class Ball
    {
        public PointF center;
        public float radius;
        public float t;
        public Color color;

        public Ball(PointF center, float radius, Color color)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            t = 0;
        }
    }
}
