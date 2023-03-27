using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafPack
{
    public partial class GrafPack : Form
    {
        
        private MainMenu myMenu;
        private ShapesList my_shapes_list;

        // status flags for various functions of the application
        bool drawSquare = false; // 1
        bool drawTriangle = false; // 2
        bool drawCircle = false; // 3
        bool selectShape = false; // 4
        bool deleteShape = false; // 5
        bool moveShape = false; // 6
        bool rotateShape = false; // 7

        // Points used to draw the shapes
        private Point one;
        private Point two;
        private Point three;

        // Drawing pens
        private Pen myBlackPen;
        private Pen myBluePen;

        // Graphics object.
        private Graphics g;

        // Stores the selected shape
        private Shape shape_Selected;

        // Stores the previous shape (shape selected before the current selected shape)
        private Shape shape_Previous;

        // Stores the number of clicks
        private int number_of_clicks;

        // Stores the index of the current shape in the ShapesList 
        private int currentIndex;
        
         
        public GrafPack()
        {
            InitializeComponent();

            // Declares object of the class ShapesList
            my_shapes_list = new ShapesList();

            // Sets the form's properties.
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.LightGoldenrodYellow;
            this.Text = "GrafPack 2D: 1824482";
            this.DoubleBuffered = true;  //double buffering on
            

            // Creates the graphics object
            g = CreateGraphics();
            
            // Pens for drawing
            myBlackPen = new Pen(Color.Black);
            myBluePen = new Pen(Color.Blue);

            
            // Creates menu items.
            MainMenu myMenu = new MainMenu();

            MenuItem createItem = new MenuItem();
            MenuItem drawSquareItem = new MenuItem();
            MenuItem drawTriangleItem = new MenuItem();
            MenuItem drawCircleItem = new MenuItem();

            MenuItem selectShapeItem = new MenuItem();
            MenuItem deleteShapeItem = new MenuItem();

            MenuItem transformShapeItem = new MenuItem();
            MenuItem moveShapeItem = new MenuItem();
            MenuItem rotateShapeItem = new MenuItem();
            MenuItem rotate45Item = new MenuItem();
            MenuItem rotate60Item = new MenuItem();

            MenuItem exitItem = new MenuItem();

            createItem.Text = "Create";
            drawSquareItem.Text = "Square";
            drawTriangleItem.Text = "Triangle";
            drawCircleItem.Text = "Circle";

            selectShapeItem.Text = "Select";
            deleteShapeItem.Text = "Delete";

            transformShapeItem.Text = "Transform";
            moveShapeItem.Text = "Move";
            rotateShapeItem.Text = "Rotate";
            rotate45Item.Text = "45 degrees";
            rotate60Item.Text = "60 degrees";

            exitItem.Text = "Exit";

            myMenu.MenuItems.Add(createItem);
            createItem.MenuItems.Add(drawSquareItem);
            createItem.MenuItems.Add(drawTriangleItem);
            createItem.MenuItems.Add(drawCircleItem);

            myMenu.MenuItems.Add(selectShapeItem);
            myMenu.MenuItems.Add(deleteShapeItem);

            myMenu.MenuItems.Add(transformShapeItem);
            transformShapeItem.MenuItems.Add(moveShapeItem);
            transformShapeItem.MenuItems.Add(rotateShapeItem);
            rotateShapeItem.MenuItems.Add(rotate45Item);
            rotateShapeItem.MenuItems.Add(rotate60Item);

            myMenu.MenuItems.Add(exitItem);
            
            this.Menu = myMenu;
            
            // Sets the number of clicks
            number_of_clicks = 0;

            // Sets the index of the ShapesList
            currentIndex = -1;

            // Sets the event handlers.
            drawSquareItem.Click += squareShapeEvent;
            drawTriangleItem.Click += triangleShapeEvent;
            drawCircleItem.Click += circleShapeEvent;
            selectShapeItem.Click += selectShapeEvent;
            deleteShapeItem.Click += deleteShapeEvent;
            moveShapeItem.Click += moveShapeEvent;
            rotate45Item.Click += rotate45Event;
            rotate60Item.Click += rotate60Event;
            exitItem.Click += exitEvent;

            MouseClick += myMouseClick;
            KeyDown += myKeyDown;
        }
        
        
         //Event handler for drawing Square
        
        private void squareShapeEvent(object sender, EventArgs e)
        {
            resetShape();

            // Resets the number of clicks
            number_of_clicks = 0;

            set_statusFlag(1);
            MessageBox.Show("Click OK and then click once each at any 2 points on the screen to draw a Square.");
        }

        
        // Event handler for drawing Triangle
        
        private void triangleShapeEvent(object sender, EventArgs e)
        {
            resetShape();

            // Resets the number of clicks
            number_of_clicks = 0;

            set_statusFlag(2);
            MessageBox.Show("Click OK and then click once each at any three points on the screen to draw a Triangle.");
        }

        
        // Event handler for drawing Circle
        private void circleShapeEvent(object sender, EventArgs e)
        {
            resetShape();

            // Resets the number of clicks
            number_of_clicks = 0;

            set_statusFlag(3);
            MessageBox.Show("Click OK and then click once each at the center and any outer point of a Circle to draw it");
        }

        
        // Event handler for selecting Shapes
        
        private void selectShapeEvent(object sender, EventArgs e)
        {
            // If the Shapes list is empty
            if (my_shapes_list.shapes_getCount == 0)
            {
                MessageBox.Show("Error: There are no shapes to select!");
                return;
            }
            else
            {
                resetShape();

                // Resets the selected shape.
                shape_Selected = null;

                // Resets the List index.
                currentIndex = -1;

                set_statusFlag(4);

                MessageBox.Show("Use the left and right arrow keys to cycle through the shapes. Click anywhere on the screen to cancel the selection!");
            }   
        }

        
        // Event handler for deleting shape

        private void deleteShapeEvent(object sender, EventArgs e)
        {
            if(selectShape == true && shape_Selected != null)
            {
                deleteSelectedShape();
                clearShape();
            }
            else
            {
                MessageBox.Show("You must select a shape to delete it!");
            }
        }

        
        // Event handler for Movement of  Shape
        
        private void moveShapeEvent(object sender, EventArgs e)
        {
            if(selectShape == true && shape_Selected != null)
            {
                MessageBox.Show("Use the arrow keys to move the selected shape.");
                moveShape = true;
            }
            else
            {
                MessageBox.Show("You must select a shape before moving it!");
            }
        }

        
        // Event handler for  rotating shapes by 45 degrees
        
        private void rotate45Event(object sender, EventArgs e)
        {
            if(selectShape == true && shape_Selected != null)
            {
                rotateShape = true;
                shapeRotation(45);
            }
            else
            {
                MessageBox.Show("You must select a shape before rotating it!");
            }
        }

        
        // Event handler for  rotating shapes by 90 degrees 
       
        private void rotate60Event(object sender, EventArgs e)
        {
            if (selectShape == true && shape_Selected != null)
            {
                rotateShape = true;
                shapeRotation(60);
            }
            else
            {
                MessageBox.Show("You must select a shape before rotating it!");
            }
        }

        
        // Event handler for closing the application
        private void exitEvent(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                // Closes the form.
                Close();
            }
        }
        

        
       
        // Mouse clicks event handler
        private void myMouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(drawSquare == true)
                {
                    DrawSquare(e);
                }
                else if(drawTriangle == true)
                {
                    DrawTriangle(e);
                }
                else if(drawCircle == true)
                {
                    DrawCircle(e);
                }
            }
        }

        // Keyboard presses event handler 
        private void myKeyDown(object sender, KeyEventArgs e)
        {
            if(selectShape == true && moveShape == false)
            {
                shapeSelection(e);
            }

            if(moveShape == true)
            {
                shapeMovement(e);
            }
        }
        
        

               
        // Draws a square
        private void DrawSquare(MouseEventArgs e)
        {
            if (number_of_clicks == 0)
            {
                // Gets the first point 
                one = new Point(e.X, e.Y);
                number_of_clicks = 1;
            }
            else
            {
                // Gets the second point
                two = new Point(e.X, e.Y);
                number_of_clicks = 0;
                set_statusFlag(0);

                // Draws the shape.
                Square newSquare = new Square(one, two);
                newSquare.Draw(g, myBlackPen);

                // Adds the drawn shape to list
                my_shapes_list.addToList(newSquare);
            }
        }

        // Draws a triangle.
        private void DrawTriangle(MouseEventArgs e)
        {
            if (number_of_clicks == 0)
            {
                one = new Point(e.X, e.Y);
                number_of_clicks = 1;
            }
            else if (number_of_clicks == 1)
            {
                two = new Point(e.X, e.Y);
                number_of_clicks = 2;
            }
            else
            {
                three = new Point(e.X, e.Y);
                number_of_clicks = 0;
                set_statusFlag(0);

                Triangle newTriangle = new Triangle(one, two, three);
                newTriangle.Draw(g, myBlackPen);

                my_shapes_list.addToList(newTriangle);
            }
        }

        // Draws a circle.
     
        private void DrawCircle(MouseEventArgs e)
        {
            if (number_of_clicks == 0)
            {
                one = new Point(e.X, e.Y);
                number_of_clicks = 1;
            }
            else
            {
                two = new Point(e.X, e.Y);
                number_of_clicks = 0;
                set_statusFlag(0);

                Circle newCircle = new Circle(one, two);
                newCircle.Draw(g, myBlackPen);

                my_shapes_list.addToList(newCircle);
            }
        }


        //Manages the shape selection
        private void shapeSelection(KeyEventArgs e)
        {
            // If the selected shape is not empty it becomes the previous shape.
            if (shape_Selected != null)
            {
                shape_Previous = shape_Selected;
            }

            if(e.KeyCode == Keys.Right)
            {
                // If the current index is less than the last shape it is moved to the next shape, otherwise
                // it is moved to the first shape.
                if (currentIndex != (my_shapes_list.shapes_getCount - 1))
                {
                    currentIndex++;
                    shape_Selected = my_shapes_list.getFromList(currentIndex);
                     selectionHighlight();
                }
                else
                {
                    currentIndex = 0;
                    shape_Selected = my_shapes_list.getFromList(currentIndex);
                     selectionHighlight();
                }
            }
            else if(e.KeyCode == Keys.Left)
            {
                // If the current index is more than 0 it is moved to the previou shape, otherwise,
                // it is moved to the last shape.
                if(currentIndex > 0)
                {
                    currentIndex--;
                    shape_Selected = my_shapes_list.getFromList(currentIndex);
                     selectionHighlight();
                }
                else
                {
                    currentIndex = my_shapes_list.shapes_getCount - 1;
                    shape_Selected = my_shapes_list.getFromList(currentIndex);
                     selectionHighlight();
                }
            }
        }

 
        // Highlights the selected shape and resets the previous one if it exists.
       
        private void  selectionHighlight()
        {
            if (shape_Previous != null)
            {
                shape_Previous.Draw(g, myBlackPen);
            }

            shape_Selected.Draw(g, myBluePen);
        }

      
        // Cancels the selection and resets the selected shape.
        private void resetShape()
        {
            set_statusFlag(0);

            if (shape_Selected != null)
            {
                shape_Selected.Draw(g, myBlackPen);
            }

            shape_Selected = null;
            shape_Previous = null;
        }

        // Clears the selected shape.
        private void clearShape()
        {
            set_statusFlag(0);
            shape_Selected = null;
            shape_Previous = null;
        }

        // Deletes the selected shape.
        private void deleteSelectedShape()
        {
            my_shapes_list.removeFromList(currentIndex);
            g.Clear(BackColor);
            shapeRedraw();
        }

        
        // Redraws the remaining shapes on the screen.
 
        private void shapeRedraw()
        {
            int numberOfShapes = my_shapes_list.shapes_getCount;

            // Cycles through the shape and redraws them.
            for (int i = 0; i < numberOfShapes; i++)
            {
                Shape shape = my_shapes_list.getFromList(i);
                shape.Draw(g, myBlackPen);
            }
        }

        // Moves the selected shape
        private void shapeMovement(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                shape_Selected.Move(10, 0);
                g.Clear(this.BackColor);
                shapeRedraw();
                shape_Selected.Draw(g, myBluePen);
            }
            else if (e.KeyCode == Keys.Left)
            {
                shape_Selected.Move(-10, 0);
                g.Clear(this.BackColor);
                shapeRedraw();
                shape_Selected.Draw(g, myBluePen);
            }
            else if (e.KeyCode == Keys.Up)
            {
                shape_Selected.Move(0, -10);
                g.Clear(this.BackColor);
                shapeRedraw();
                shape_Selected.Draw(g, myBluePen);
            }
            else if (e.KeyCode == Keys.Down)
            {
                shape_Selected.Move(0, 10);
                g.Clear(this.BackColor);
                shapeRedraw();
                shape_Selected.Draw(g, myBluePen);
            }
        }

        // Rotates the selected shape
        private void shapeRotation(int degrees)
        {
            shape_Selected.Rotate(degrees);
            g.Clear(BackColor);
            shapeRedraw();
            shape_Selected.Draw(g, myBluePen);
        }

        // Sets one function to true and all remaining functions to false
        private void set_statusFlag(int flag_Num)
        {
            drawSquare = flag_Num == 1 ? true : false;
            drawTriangle = flag_Num == 2 ? true : false;
            drawCircle = flag_Num == 3 ? true : false;
            selectShape = flag_Num == 4 ? true : false;
            deleteShape = flag_Num == 5 ? true : false;
            moveShape = flag_Num == 6 ? true : false;
            rotateShape = flag_Num == 7 ? true : false;
        }
        

        private void GrafPack_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GrafPack
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "GrafPack";
            this.Load += new System.EventHandler(this.GrafPack_Load_1);
            this.ResumeLayout(false);

        }

        private void GrafPack_Load_1(object sender, EventArgs e)
        {

        }
    }
}
