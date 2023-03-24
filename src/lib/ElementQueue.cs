using System.Collections.Generic;
using System;
namespace lib
{
    class ElementQueue 
    {
        // Attributes
        // Fields atribut route dan viistedNodes akan terus menerus diubah saat pemrosesan
        // Demi tercapainya efisiensi, atribut-atribut ini dideklarasi secara public sesuai dengan hukum Amdahl
        public Route route;
        public Matrix visitedNodes;

        // Constructor
        public ElementQueue()
        {
            this.route = new Route();
            this.visitedNodes = new Matrix(0,0);
        }
        
        // Parameterized Constructor
        public ElementQueue(int row, int col, List<char> path, List<GraphNode> nodePath, GraphNode lastNode, int remainingTreasures)
        {
            //construct route
            this.route = new Route();
            this.route.path = path;
            this.route.nodePath = nodePath;
            this.route.node = lastNode;
            this.route.remainingTreasures = remainingTreasures;

            // construct matrix
            this.visitedNodes = new Matrix(row, col);
              
        }
    }   

}