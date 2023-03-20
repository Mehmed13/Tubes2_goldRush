using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace lib
{
    class BFS1 
    {
        private List<char> finalPath;
        private int numOfTreasures;
        private double executionTime;
        private GraphNode startGraph;
        private Queue<ElementQueue> queue;

        public BFS1(int numOfTreasures, GraphNode startGraph) 
        {
            this.finalPath = new List<char>();
            this.numOfTreasures = numOfTreasures;
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

        public void runBFS1Algorithm(int row, int col)
        {
            var watch = Stopwatch.StartNew(); // timer
            GraphNode tempStartNode = this.startGraph;
            int remainingTreasures = this.numOfTreasures;

            while (remainingTreasures != 0) 
            {   
                Matrix visited = new Matrix(row, col);
                List<char> newPath = new List<char>();
                List<GraphNode> newNodePath = new List<GraphNode>();
                newNodePath.Add(tempStartNode);

                ElementQueue firstElement = new ElementQueue(row, col, newPath, newNodePath, tempStartNode, 0);
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
                            for (int i = 0; i < headElement.route.nodePath.Count - 2; i++)  // the last element is considered not visited because it'll be done in the next iteration
                            {
                                headElement.route.nodePath[i].setVisited(headElement.route.nodePath[i].getVisited() + 1);
                            }
                        }
                        tempStartNode = headElement.route.node; 
                        remainingTreasures--;
                        queue.Clear();

                    } 
                    else 
                    {
                        headElement.visitedNodes.setElement(headElement.route.node.getCoordinate().x, headElement.route.node.getCoordinate().y, 1);
                    }


                    // RIGHT NODE
                    if (!found && headElement.route.node.getRight() != null && headElement.visitedNodes.getElement(headElement.route.node.getRight().getCoordinate().x, headElement.route.node.getRight().getCoordinate().y) == 0)
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement = new ElementQueue();
                        newElement.route.path = headElement.route.path;
                        newElement.route.path.Add('R');

                        newElement.route.nodePath = headElement.route.nodePath;
                        newElement.route.nodePath.Add(headElement.route.node.getRight());

                        newElement.route.node = headElement.route.node.getRight();

                        newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // DOWN NODE
                    if (!found && headElement.route.node.getDown() != null && headElement.visitedNodes.getElement(headElement.route.node.getDown().getCoordinate().x, headElement.route.node.getDown().getCoordinate().y) == 0)
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement = new ElementQueue();
                        newElement.route.path = headElement.route.path;
                        newElement.route.path.Add('D');

                        newElement.route.nodePath = headElement.route.nodePath;
                        newElement.route.nodePath.Add(headElement.route.node.getDown());

                        newElement.route.node = headElement.route.node.getDown();

                        newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // LEFT NODE
                    if (!found && headElement.route.node.getLeft() != null && headElement.visitedNodes.getElement(headElement.route.node.getLeft().getCoordinate().x, headElement.route.node.getLeft().getCoordinate().y) == 0)
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement = new ElementQueue();
                        newElement.route.path = headElement.route.path;
                        newElement.route.path.Add('L');

                        newElement.route.nodePath = headElement.route.nodePath;
                        newElement.route.nodePath.Add(headElement.route.node.getLeft());

                        newElement.route.node = headElement.route.node.getLeft();

                        newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // UP NODE
                    if (!found && headElement.route.node.getUp() != null && headElement.visitedNodes.getElement(headElement.route.node.getUp().getCoordinate().x, headElement.route.node.getUp().getCoordinate().y) == 0)
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement = new ElementQueue();
                        newElement.route.path = headElement.route.path;
                        newElement.route.path.Add('U');

                        newElement.route.nodePath = headElement.route.nodePath;
                        newElement.route.nodePath.Add(headElement.route.node.getUp());

                        newElement.route.node = headElement.route.node.getUp();

                        newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }
                    
                } // found

            }
            // Finishing
            watch.Stop();
            this.executionTime = watch.ElapsedMilliseconds; 

            Console.Write("Path menuju treasure: ");
            foreach (char dir in this.finalPath)
            {
                Console.Write(dir);
            }
            Console.WriteLine("");

            Console.WriteLine("Waktu: ", this.executionTime);
        }

    }
}