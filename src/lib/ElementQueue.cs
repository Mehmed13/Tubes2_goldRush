using System.collections.Generic;
using System;
namespace lib
{
    class ElementQueue 
    {
        private Route route;
        private Matrix matrixOfVisitedNodes;

        public ElementQueue(int row, int col, List<char> path, List<GraphNode> nodePath, GraphNode lastNode, int remainingTreasures){
            //construct route
            this.route.path = path;
            this.route.nodePath = nodePath;
            this.route.node = lastNode;
            this.route.remainingTreasures = remainingTreasures;

            // construct matrix
            this.matrixOfVisitedNodes = new Matrix(row, col);
            
            
        }

        public Route getRoute()
        {
            return this.route;
        }

        public Matrix getMatrixOfVisitedNodes()
        {
            return this.matrixOfVisitedNodes;
        }

        // public void setRoute(Route route)
        // {
        //     this.route = route;
        // }

        public void setMatrixOfVisitedNodes(int row, int col, int value){
            this.matrixOfVisitedNodes.setElement(row,col,value);
        }
    }   

}