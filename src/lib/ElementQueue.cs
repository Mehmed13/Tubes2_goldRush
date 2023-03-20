using System.collections.Generic;
using System;
namespace lib
{
    class ElementQueue 
    {
        private Route route;
        private Matrix visitedNodes;

        public ElementQueue(int row, int col, List<char> path, List<GraphNode> nodePath, GraphNode lastNode, int remainingTreasures){
            //construct route
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

        // public void setRoute(Route route)
        // {
        //     this.route = route;
        // }

        public void setVisitedNodes(int row, int col, int value){
            this.visitedNodes.setElement(row,col,value);
        }
    }   

}