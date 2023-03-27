using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GrafPack
{
    class Square : Shape   //class Square inherited from class Shape
    {
        // Two points for the top left and bottom right corners of the square
        private Point keyPt, oppPt;      

        // Constructor 
        public Square(Point keyPt, Point oppPt)
        {
            this.keyPt = keyPt;
            this.oppPt = oppPt;
        }

        // Draws the square by calculating the other 2 corners
        public override void Draw(Graphics g, Pen blackPen)
        {
           
            double xDiff, yDiff, xMid, yMid;   

            // Calculating midpointts of x and y and their range
            xDiff = oppPt.X - keyPt.X; 
            yDiff = oppPt.Y - keyPt.Y; 
            xMid = (oppPt.X + keyPt.X) / 2; 
            yMid = (oppPt.Y + keyPt.Y) / 2; 

            // Draws the square from first principles
            g.DrawLine(blackPen, (int)keyPt.X, (int)keyPt.Y, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2)); 
            g.DrawLine(blackPen, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2), (int)oppPt.X, (int)oppPt.Y);
            g.DrawLine(blackPen, (int)oppPt.X, (int)oppPt.Y, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2));
            g.DrawLine(blackPen, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2), (int)keyPt.X, (int)keyPt.Y);
        }

        
        // Moves the drawn square
        public override void Move(int xMove, int yMove)
        {
            keyPt.X = keyPt.X + xMove;
            keyPt.Y = keyPt.Y + yMove;
            oppPt.X = oppPt.X + xMove;
            oppPt.Y = oppPt.Y + yMove;
        }

        
        // Rotates the drawn square
        public override void Rotate(int angle_in_degress)
        {
            keyPt = RotatePoint(angle_in_degress, keyPt, keyPt);
            oppPt = RotatePoint(angle_in_degress, oppPt, keyPt);
        }
    }
}
