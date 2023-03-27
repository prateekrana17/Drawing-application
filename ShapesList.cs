using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafPack
{
    class ShapesList
    {
        
        private List<Shape> list_of_shapes; // All drawn shapes are stored in this list 

        // Returns the number of shapes in the list
        public int shapes_getCount
        {
            get
            {
                return list_of_shapes.Count;
            }
        }
        

       
        // Constructor of class ShapesList
        
        public ShapesList()
        {
            list_of_shapes = new List<Shape>();
        }
        

       
        // Adds the drawn shape to the list.
       
        public void addToList(Shape shape)
        {
            list_of_shapes.Add(shape);
        }

        
        //Returns the shape at provided index from the list
        public Shape getFromList(int index)
        {
            return list_of_shapes.ElementAt(index);
        }

        //removes the shape at index from the list
        public void removeFromList(int index)
        {
            list_of_shapes.RemoveAt(index);
        }
        
    }
}
