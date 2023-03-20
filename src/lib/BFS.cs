using System;
using System.Diagnostics;
using System.collections.Generic;

namespace lib
{

    class BFS {         
        private int numOfTreasures;
        // private int numOfNodesVisited;
        private long executionTime;
        private GraphNode graph;
        private Queue<ElementQueue> queue;

        // Constructor
        public BFS(int numOfTreasures, GraphNode graph)
        {
            this.numOfTreasures = numOfTreasures;
            this.executionTime = 0;
            this.graph = graph;
            this.stack = new Queue<ElementQueue>();
        }

        // Getter
        public int getNumOfTreasures()
        {
            return this.numOfTreasures;
        }
        public double getExecutionTime()
        {
            return this.executionTime;
        }
        public GraphNode getGraph()
        {
            return this.graph;
        }
        public Queue<ElementQueue> getQueue()
        {
            return this.Queue;
        }

        // Setter
        public void setNumOfTreasures(int numOfTreasures)
        {
            this.numOfTreasures = numOfTreasures;
        }
        public void setExecutionTime(double executionTime)
        {
            this.executionTime = executionTime;
        }
        public void setGraph(List<GraphNode> graph)
        {
            this.graph = graph;
        }
        public void setQueue(Stack<Route> Queue)
        {
            this.Queue = Queue;
        }        

        // Method
        public void runBFSAlgorithm(int row, int col) {
            // Initialization

            var watch = Stopwatch.StartNew(); // timer

            // Enqueue
            List<char> path = {};
            List<GraphNode> nodePath = {graph};
            ElementQueue element = new ElementQueue(row, col, path, nodePath, graph, this.numOfTreasures);
            queue.Enqueue(element);

            
            while (queue.Count != 0)
            {
                // Dequeue Head
                ElementQueue headElement = queue.Dequeue();
                GraphNode lastNode = headElement.getRoute().node;
                headElement.setVisitedNodes(lastNode.getValue().x, lastNode.getValue().y, 1);

                if ((lastNode.getRight() == null || headElement.getVisitedNodes().getElement(lastNode.getRight().getValue().x, lastNode.getRight().getValue().y) == 1)
                && (lastNode.getDown() == null || headElement.getVisitedNodes().getElement(lastNode.getDown().getValue().x, lastNode.getDown().getValue().y) == 1)
                && (lastNode.getLeft() === null || headElement.getVisitedNodes().getElement(lastNode.getLeft().getValue().x, lastNode.getLeft().getValue().y) == 1) 
                && (lastNode.getUp() == null || headElement.getVisitedNodes().getElement(lastNode.getUp().getValue().x, lastNode.getUp().getValue().y) == 1))
                {
                    // Apabila path sudah buntu dan tidak mengandung treasure
                    if (headElement.getRoute().remainingTreasures == this.numOfTreasures){
                        // do nothing
                    } else {
                        queue.Enqueue(headElement);
                    }
                }

                else
                {
                    if (lastNode.getRight() != null && headElement.getVisitedNodes().getElement(lastNode.getRight().getValue().x, lastNode.getRight().getValue().y) != 1)
                    {
                        // Initialize new element 
                        ElementQueue newElement = new ElementQueue(headElement.getVisitedNodes().getRow(), 
                                                                    headElement.getVisitedNodes().getCol(), 
                                                                    headElement.getRoute().path.Add('R'),
                                                                    headElement.getRoute().nodePath.Add(headElement.getRight()),
                                                                    lastNode.getRight(),
                                                                    headElement.getRoute().remainingTreasures);
                        // Decrement remainingTreasures if the right node is treasure
                        if (headElement.getRoute().node.getRight().isTreasure()) 
                        {
                            newElement.getRoute().remainingTreasures--;
                        }
                        // Break if remaining treasure is 0
                        if(newElement.getRoute().remainingTreasures == 0)
                        {
                            Console.Write("Rute ditemukan, yakni: ");
                            foreach (char dir in newElement.getRoute().path)
                            {
                                Console.Write(dir);
                            }
                            Console.WriteLine("");
                            break;
                        }

                        queue.Enqueue(newElement);
                    }
                    if (lastNode.getDown() != null && headElement.getVisitedNodes().getElement(lastNode.getDown().getValue().x, lastNode.getDown().getValue().y) != 1)
                    {
                        // Initialize new element 
                        ElementQueue newElement = new ElementQueue(headElement.getVisitedNodes().getRow(), 
                                                                    headElement.getVisitedNodes().getCol(), 
                                                                    headElement.getRoute().path.Add('D'),
                                                                    headElement.getRoute().nodePath.Add(headElement.getDown()),
                                                                    lastNode.geDown(),
                                                                    headElement.getRoute().remainingTreasures);
                        // Decrement remainingTreasures if the right node is treasure
                        if (headElement.getRoute().node.getDown().isTreasure()) 
                        {
                            newElement.getRoute().remainingTreasures--;
                        }
                        // Break if remaining treasure is 0
                        if(newElement.getRoute().remainingTreasures == 0)
                        {
                            Console.Write("Rute ditemukan, yakni: ");
                            foreach (char dir in newElement.getRoute().path)
                            {
                                Console.Write(dir);
                            }
                            Console.WriteLine("");
                            break;
                        }

                        queue.Enqueue(newElement);
        
                    }
                    if (lastNode.getLeft() != null && headElement.getVisitedNodes().getElement(lastNode.getLeft().getValue().x, lastNode.getLeft().getValue().y) != 1)
                    {
                        // Initialize new element 
                        ElementQueue newElement = new ElementQueue(headElement.getVisitedNodes().getRow(), 
                                                                    headElement.getVisitedNodes().getCol(), 
                                                                    headElement.getRoute().path.Add('L'),
                                                                    headElement.getRoute().nodePath.Add(headElement.getLeft()),
                                                                    lastNode.getLeft(),
                                                                    headElement.getRoute().remainingTreasures);
                        // Decrement remainingTreasures if the right node is treasure
                        if (headElement.getRoute().node.getLeft().isTreasure()) 
                        {
                            newElement.getRoute().remainingTreasures--;
                        }
                        // Break if remaining treasure is 0
                        if(newElement.getRoute().remainingTreasures == 0)
                        {
                            Console.Write("Rute ditemukan, yakni: ");
                            foreach (char dir in newElement.getRoute().path)
                            {
                                Console.Write(dir);
                            }
                            Console.WriteLine("");
                            break;
                        }

                        queue.Enqueue(newElement);
                        
                    }
                    if (lastNode.getUp() != null && headElement.getVisitedNodes().getElement(lastNode.getUp().getValue().x, lastNode.getUp().getValue().y) != 1)
                    {
                        // Initialize new element 
                        ElementQueue newElement = new ElementQueue(headElement.getVisitedNodes().getRow(), 
                                                                    headElement.getVisitedNodes().getCol(), 
                                                                    headElement.getRoute().path.Add('U'),
                                                                    headElement.getRoute().nodePath.Add(headElement.getUp()),
                                                                    lastNode.getUp(),
                                                                    headElement.getRoute().remainingTreasures);
                        // Decrement remainingTreasures if the right node is treasure
                        if (headElement.getRoute().node.getUp().isTreasure()) 
                        {
                            newElement.getRoute().remainingTreasures--;
                        }
                        // Break if remaining treasure is 0
                        if(newElement.getRoute().remainingTreasures == 0)
                        {
                            Console.Write("Rute ditemukan, yakni: ");
                            foreach (char dir in newElement.getRoute().path)
                            {
                                Console.Write(dir);
                            }
                            Console.WriteLine("");
                            break;
                        }

                        queue.Enqueue(newElement);
                        
                    }
                }


            }//end while
            // Finishing
            watch.Stop();
            this.executionTime = watch.ElapsedMilliseconds; // get the execution time in ms
            Console.WriteLine("Tidak dapat jalan menuju treasure");



        }
    }


}
