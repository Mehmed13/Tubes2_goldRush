using System;
using System.Diagnostics;
using System.collections.Generic;

namespace lib
{
    class BFS1 
    {
        private List<char> finalPath;
        private int numOfTreasures;
        private long executionTime;
        private GraphNode startGraph;
        private Queue<ElementQueue> queue;

        public BFS1(int numOfTreasures, GraphNode startGraph) 
        {
            this.finalPath = new List<char>();
            this.numOfTreasures = numOftreasures;
            this.executionTime = 0;
            this.startGraph = startGraph;
            this.queue = new Queue<ElementQueue>();
        }

        // Getter
        public List<char> getFinalPath()
        {
            return this.finalPath;
        }
        public int getNumOfTreasures()
        {
            return this.numOfTreasures;
        }
        public double getExecutionTime()
        {
            return this.executionTime;
        }
        public GraphNode getStartGraph()
        {
            return this.startGraph;
        }
        public Queue<ElementQueue> getQueue()
        {
            return this.queue;
        }

        // Setter
        public void setFinalPath(List<char> finalPath)
        {
            this.finalPath = finalPath;
        }
        public void setNumOfTreasures(int numOfTreasures)
        {
            this.numOfTreasures = numOfTreasures;
        }
        public void setExecutionTime(double executionTime)
        {
            this.executionTime = executionTime;
        }
        public void setStartGraph(GraphNode startGraph)
        {
            this.startGraph = startGraph;
        }
        public void setQueue(Queue<ElementQueue> queue)
        {
            this.queue = queue;
        }

        // Method

        public void runBFSAlgorithm(int row, int col)
        {
            var watch = Stopwatch.StartNew(); // timer
            int remainingTreasures = this.numOfTreasures;

            while (remainingTreasures != 0) 
            {   
                Matrix visited = Matrix(row, col);
                ElementQueue firstElement = ElementQueue(row, col, {}, {startGraph}, startGraph, 0);
                queue.Enqueue(firstElement);
                bool found = false;
                while (!found) 
                {
                    ElementQueue headElement = this.queue.Dequeue();

                    // Check if the headElement's last node is treasure
                    if (headElement.route.node.isTreasure())
                    {
                        found = true;
                        finalPath.AddRange(headElement.route.path);
                        remainingTreasures--;
                        if (remainingTreasures == 1) 
                        {
                            foreach (GraphNode n in headElement.route.nodePath) 
                            {
                                n.setVisited(n.getVisited() + 1);
                            }
                            headElement.route.node.setVisited(headElement.route.node.getVisited()+1);
                        } 
                        else 
                        {
                            for (int i = 0; i < headElement.route.nodePath - 2; i++)  // the last element is considered not visited because it'll be done in the next iteration
                            {
                                n.setVisited(n.getVisited() + 1);
                            }
                        }
                    } 
                    else 
                    {
                        // headElement.route.node.setVisited(headElement.route.node.getVisited()+1); // tambahin
                        headElement.visitedNodes.setElement(headElement.route.node.getValue().x, headElement.route.node.getValue().y, 1)
                    }



                    if (!found && headElement.node.getRight() != null && headElement.visitedNodes.getElement(headElement.node.getRight().getValue().x, headElement.node.getLeft().getValue().y))
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement;
                        newElement.path = headElement.path;
                        newElement.path.Add('R');

                        newElement.nodePath = headElement.nodePath;
                        newElement.nodePath.Add(headElement.node.getRight());

                        newElement.node = headElement.node.getRight();
                        
                    // Check if the right node is treasure
                        if (headElement.node.getRight().isTreasure())
                        {
                            found = True;


                        }

                    }
                    

                    
                } 


            }





        }












    }
}