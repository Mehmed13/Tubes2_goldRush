using System.Collections.Generic;
namespace lib
{
    class Matrix
    {
        // Attribute
        private int row;
        private int col;
        private int[,] elements;

        // Constructor
        public Matrix(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.elements = new int[row, col];
            // nilai default element = 0
            for(int i = 0; i < this.row ; i++) 
            {
                for(int j = 0; j < this.col ; j++) 
                {
                    elements[i, j] = 0;
                }

            }

        }   
        
        // Getter
        public int getRow()
        {
            return this.row;
        }
        public int getCol()
        {
            return this.col;
        }

        public int getElement(int row, int col) 
        {
            return elements[row, col];
        }

        // Setter
        public void setElement(int row, int col, int value) 
        {
            elements[row, col] = value;
        }
        
    }
}