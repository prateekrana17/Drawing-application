using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafPack
{
    class Circle : Shape //class Circle inherited from the abstract class Shape
    {
        private Point centerPoint; // Stores the coordinates of the center of the circle.
        private Point oppositePoint; // Stores the coordinates of the radius point of the circle.
        private readonly int radius; // Stores the radius of the circle.
        private Brush mybrush;

        // Constructor for the Circle class.
        public Circle(Point centerPoint, Point oppositePoint)
        {
            this.centerPoint = centerPoint;
            this.oppositePoint = oppositePoint;

            // Calculating the radius
            radius = Math.Abs(centerPoint.X - oppositePoint.X);
        }

        //drawing  the circle from first principles using Bresenham Circle drawing algorithm by filling pixels
        
        public override void Draw(Graphics g, Pen pen)
        {

            if(pen.Color == Color.Black)
            {
                mybrush = new SolidBrush(Color.Black);
            }
            else
            {
                mybrush = new SolidBrush(Color.Blue);
            }

            //  Sets x to 0
            int x = 0;
            
            // Sets y to the radius
            int y = radius;

            // Decision parameter
            int d = 3 - 2 * radius;

            // Increments x and sets pixels until x is bigger than y.
            while(y >= x)
            {
                // Sets pixels 
                DrawCircle(g, x, y);
                
                // If the decision parameter is more than 0, y is decremented.
                if(d > 0)
                {
                    y--;

                    // update the value of d
                    d = d + 4 * (x - y) + 10;
                }
                else
                {
                    // Recalculates the decision parameter.
                    d = d + 4 * x + 6;
                }

                x++;
            }
        }

        // setting pixels in each octant
        
        public void DrawCircle(Graphics g, int x, int y)
        {
            SetPixel(g, new Point(centerPoint.X + x, centerPoint.Y + y));
            SetPixel(g, new Point(centerPoint.X - x, centerPoint.Y + y));
            SetPixel(g, new Point(centerPoint.X + x, centerPoint.Y - y));
            SetPixel(g, new Point(centerPoint.X - x, centerPoint.Y - y));
            SetPixel(g, new Point(centerPoint.X + y, centerPoint.Y + x));
            SetPixel(g, new Point(centerPoint.X - y, centerPoint.Y + x));
            SetPixel(g, new Point(centerPoint.X + y, centerPoint.Y - x));
            SetPixel(g, new Point(centerPoint.X - y, centerPoint.Y - x));
        }

        // Method fills in one pixel on the screen
        public void SetPixel(Graphics g, Point pixel)
        {
            g.FillRectangle(mybrush, pixel.X, pixel.Y, 1, 1);
        }

        
        // Moves the circle by overriding the Move function of Shape.
        
        public override void Move(int xMove, int yMove)
        {
            centerPoint.X = centerPoint.X + xMove;
            centerPoint.Y = centerPoint.Y + yMove;
            oppositePoint.X = oppositePoint.X + xMove;
            oppositePoint.Y = oppositePoint.Y + yMove;
        }

        
        // Rotates the drawn circle.
        
        public override void Rotate(int angle_in_degress)
        {
            centerPoint = RotatePoint(angle_in_degress, centerPoint, centerPoint);
            oppositePoint = RotatePoint(angle_in_degress, oppositePoint, centerPoint);
        }
    }
}
