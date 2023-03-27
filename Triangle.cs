using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafPack
{
    class Triangle : Shape  //Class Triangle inherited from class Shape
    {
        private Point pt1, pt2, pt3; // Three points for three corners of the triangle

        // Constructor for the class triangle
        public Triangle(Point pt1, Point pt2, Point pt3)
        {
            this.pt1 = pt1;
            this.pt2 = pt2;
            this.pt3 = pt3;
        }

        // Overriding Draw() method from abstract class Shape 
        public override void Draw(Graphics g, Pen blackPen)
        {
            // Drawing triangle from first principles
            g.DrawLine(blackPen, pt1, pt2);
            g.DrawLine(blackPen, pt2, pt3);
            g.DrawLine(blackPen, pt3, pt1);
        }

        
        // Moves the drawn triangle.
        
        public override void Move(int xMove, int yMove)
        {
            pt1.X = pt1.X + xMove;
            pt1.Y = pt1.Y + yMove;
            pt2.X = pt2.X + xMove;
            pt2.Y = pt2.Y + yMove;
            pt3.X = pt3.X + xMove;
            pt3.Y = pt3.Y + yMove;
        }

        
        // Rotates the drawn triangle
        
        public override void Rotate(int angle_in_degress)
        {
            pt1 = RotatePoint(angle_in_degress, pt1, pt1);
            pt2 = RotatePoint(angle_in_degress, pt2, pt1);
            pt3 = RotatePoint(angle_in_degress, pt3, pt1);
        }
    }
}
