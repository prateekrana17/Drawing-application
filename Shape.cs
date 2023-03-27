using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafPack
{
    abstract class Shape
    {
        // Default constructor for the class Shape
        public Shape()   
        {

        }

        // Draws the shape
        public abstract void Draw(Graphics g, Pen blackPen);

        // Offsets the position of the shape.
        public abstract void Move(int xMove, int yMove);

        // Rotates the drawn shape by angle passed as argument
        public abstract void Rotate(int angle_in_degress);

        
        // Rotates a point about the origin
        /// <param name="angle_in_degress">Angle for rotation</param>
        /// <param name="pt">Rotation point</param>
        /// <param name="origin">Origin pt</param>
        /// <returns>Rotated pt</returns>       
        public Point RotatePoint(float angle_in_degress, PointF pt, PointF origin)
        {
            // Converts the angle from degrees to radians.
            float angle_in_radians = (float) (angle_in_degress * Math.PI / 180);

            // Calculating the Sine and Cosine 
            float cosA = (float) Math.Cos(angle_in_radians);
            float sinA = (float) Math.Sin(angle_in_radians);

            // Calculates the co-ordinates of the rotated points
            float X = (cosA * (pt.X - origin.X)) - sinA * (pt.Y - origin.Y) + origin.X;
            float Y = (sinA * (pt.X - origin.X)) + cosA * (pt.Y - origin.Y) + origin.Y;

            return new Point((int) X, (int) Y);
        }
    }
}
