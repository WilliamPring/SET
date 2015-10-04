using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETPaint
{
    class CrossHair
    {
        private Point centre = new Point(0, 0);
        private int radius = 10;
        private Color penColor = Color.Black;


        public Point Centre
        {
            get { return centre; }
            set { centre = value; }
        }

        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public CrossHair()
        {
        }

        public CrossHair(Point Centre)
        {
            this.Centre = Centre;
        }

        public bool IsInside(Point testPoint)
        {
            bool result = false;

            if (Math.Sqrt(Math.Pow((testPoint.X - centre.X), 2) + Math.Pow((testPoint.Y - centre.Y), 2)) <= radius)
            {
                result = true;
            }

            return result;
        }

        public void DrawCrossHair(Graphics g, Point Centre)
        {
            this.Centre = Centre;
            Point TopLeft = new Point(Centre.X - Radius, Centre.Y - Radius);
            Size RectSize = new Size(radius * 2, radius * 2);
            Point VerticalLineTop = new Point(Centre.X, Centre.Y - Radius);
            Point VerticalLineBottom = new Point(Centre.X, Centre.Y + Radius);
            Point HorizontalLineLeft = new Point(Centre.X - Radius, Centre.Y);
            Point HorizontalLineRight = new Point(Centre.X + Radius, Centre.Y);
            Pen pen = new Pen(penColor);
            g.DrawLine(pen, VerticalLineTop, VerticalLineBottom);
            g.DrawLine(pen, HorizontalLineLeft, HorizontalLineRight);
            g.DrawEllipse(pen, new Rectangle(TopLeft, RectSize));
        }

    }
}

