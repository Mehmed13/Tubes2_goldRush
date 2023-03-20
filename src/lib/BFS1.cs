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

        public void runBFS1Algorithm(int row, int col)
        {
            var watch = Stopwatch.StartNew(); // timer
            GraphNode tempStartNode = this.startGraph;
            int remainingTreasures = this.numOfTreasures;

            while (remainingTreasures != 0) 
            {   
                Matrix visited = Matrix(row, col);
                ElementQueue firstElement = ElementQueue(row, col, {}, {tempStartNode}, tempStartNode, 0);
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
                            for (int i = 0; i < headElement.route.nodePath - 2; i++)  // the last element is considered not visited because it'll be done in the next iteration
                            {
                                n.setVisited(n.getVisited() + 1);
                            }
                        }
                        tempStartNode = headElement.route.node; 
                        remainingTreasures--;
                        queue.Clear();

                    } 
                    else 
                    {
                        headElement.visitedNodes.setElement(headElement.route.node.getCoordinate().x, headElement.route.node.getCoordinate().y, 1)
                    }


                    // RIGHT NODE
                    if (!found && headElement.node.getRight() != null && headElement.visitedNodes.getElement(headElement.node.getRight().getCoordinate().x, headElement.node.getRight().getCoordinate().y) == 0)
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement;
                        newElement.path = headElement.path;
                        newElement.path.Add('R');

                        newElement.nodePath = headElement.nodePath;
                        newElement.nodePath.Add(headElement.node.getRight());

                        newElement.node = headElement.node.getRight();

                        newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // DOWN NODE
                    if (!found && headElement.node.getDown() != null && headElement.visitedNodes.getElement(headElement.node.getDown().getCoordinate().x, headElement.node.getDown().getCoordinate().y) == 0)
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement;
                        newElement.path = headElement.path;
                        newElement.path.Add('D');

                        newElement.nodePath = headElement.nodePath;
                        newElement.nodePath.Add(headElement.node.getDown());

                        newElement.node = headElement.node.getDown();

                        newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // LEFT NODE
                    if (!found && headElement.node.getLeft() != null && headElement.visitedNodes.getElement(headElement.node.getLeft().getCoordinate().x, headElement.node.getLeft().getCoordinate().y) == 0)
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement;
                        newElement.path = headElement.path;
                        newElement.path.Add('L');

                        newElement.nodePath = headElement.nodePath;
                        newElement.nodePath.Add(headElement.node.getLeft());

                        newElement.node = headElement.node.getLeft();

                        newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // UP NODE
                    if (!found && headElement.node.getUp() != null && headElement.visitedNodes.getElement(headElement.node.getUp().getCoordinate().x, headElement.node.getUp().getCoordinate().y) == 0)
                    {
                    // Initialize new ElementQueue
                        ElementQueue newElement;
                        newElement.path = headElement.path;
                        newElement.path.Add('U');

                        newElement.nodePath = headElement.nodePath;
                        newElement.nodePath.Add(headElement.node.getUp());

                        newElement.node = headElement.node.getUp();

                        newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }
                    
                } // found

            }
            // Finishing
            watch.Stop();
            this.executionTime = watch.ElapsedMilliseconds; 

            Console.Write('Path menuju treasure: ');
            foreach (char dir in this.finalPath)
            {
                Console.Write(dir);
            }
            Console.WriteLine("");

            Console.WriteLine('Waktu: ', this.executionTime);
        }

    }
}