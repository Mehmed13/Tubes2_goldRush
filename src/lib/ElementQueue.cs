using System.Collections.Generic;
using System;
namespace lib
{
    class ElementQueue 
    {
        public Route route;
        public Matrix visitedNodes;

        public ElementQueue(){
            // nothing
            this.route = new Route();
            this.visitedNodes = new Matrix(0,0);
        }
        
        public ElementQueue(int row, int col, List<char> path, List<GraphNode> nodePath, GraphNode lastNode, int remainingTreasures){
            //construct route
            this.route = new Route();
            this.route.path = path;
            this.route.nodePath = nodePath;
            this.route.node = lastNode;
            this.route.remainingTreasures = remainingTreasures;

            // construct matrix
            this.visitedNodes = new Matrix(row, col);
            
            
        }

        public Route getRoute()
        {
            return this.route;
        }

        public Matrix getVisitedNodes()
        {
            return this.visitedNodes;
        }


        public void setVisitedNodes(int row, int col, int value)
        {
            this.visitedNodes.setElement(row,col,value);
        }
    }   

}