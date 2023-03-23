using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace lib
{
    class BFS
    {
        private List<char> finalPath;
        private int numOfTreasures;
        private int numOfNodesVisited;
        private double executionTime;
        private GraphNode startGraph;
        private List<GraphNode> visitedNodeSequence; // urutan node yang dikunjungi
        private Queue<ElementQueue> queue;

        public BFS(int numOfTreasures, GraphNode startGraph)
        {
            this.finalPath = new List<char>();
            this.numOfTreasures = numOfTreasures;
            this.numOfNodesVisited = 0;
            this.executionTime = 0;
            this.startGraph = startGraph;
            this.visitedNodeSequence = new List<GraphNode>();
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
        public int getNumOfNodesVisited()
        {
            return this.numOfNodesVisited;
        }
        public double getExecutionTime()
        {
            return this.executionTime;
        }
        public GraphNode getStartGraph()
        {
            return this.startGraph;
        }
        public List<GraphNode> getVisitedNodeSequence()
        {
            return this.visitedNodeSequence;
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
        public void setNumOfNodesVisited(int numOfNodesVisited)
        {
            this.numOfNodesVisited = numOfNodesVisited;
        }
        public void setExecutionTime(double executionTime)
        {
            this.executionTime = executionTime;
        }
        public void setStartGraph(GraphNode startGraph)
        {
            this.startGraph = startGraph;
        }
        public void addVisitedNodeSequence(GraphNode visitedNode)
        {
            this.visitedNodeSequence.Add(visitedNode);
        }
        public void setQueue(Queue<ElementQueue> queue)
        {
            this.queue = queue;
        }

        // Method

        public void runBFSAlgorithm(int row, int col)
        {
            var watch = Stopwatch.StartNew(); // timer
            GraphNode tempStartNode = this.startGraph;
            int remainingTreasures = this.numOfTreasures;
            while (remainingTreasures != 0)
            {
                Console.Write("Sisa treasure: ");
                Console.WriteLine(remainingTreasures);
                // Matrix visited = new Matrix(row, col);
                List<char> newPath = new List<char>();
                List<GraphNode> newNodePath = new List<GraphNode>();
                newNodePath.Add(tempStartNode);

                ElementQueue firstElement = new ElementQueue(row, col, newPath, newNodePath, tempStartNode, 0);
                queue.Enqueue(firstElement);
                bool found = false;
                while (!found)
                {

                    ElementQueue headElement = this.queue.Dequeue();
                    Console.WriteLine("===========> MENGECEK KOORDINAT :");
                    Console.Write("x: ");
                    Console.WriteLine(headElement.route.node.getCoordinate().x);
                    Console.Write("y: ");
                    Console.WriteLine(headElement.route.node.getCoordinate().y);
                    // Check if the headElement's last node is treasure
                    if (headElement.route.node.isTreasure())
                    {
                        found = true;
                        finalPath.AddRange(headElement.route.path);
                        Console.WriteLine("========= KOORDINAT TREASURE ==========");
                        Console.Write("x: ");
                        Console.WriteLine(headElement.route.node.getCoordinate().x);
                        Console.Write("y: ");
                        Console.WriteLine(headElement.route.node.getCoordinate().y);

                        if (remainingTreasures == 1)
                        {
                            this.addVisitedNodeSequence(headElement.route.node);
                            this.setNumOfNodesVisited(this.getNumOfNodesVisited() + 1);
                            Console.WriteLine("treasure terakhir");
                            foreach (GraphNode n in headElement.route.nodePath)
                            {
                                n.setVisited(n.getVisited() + 1);
                            }
                        }
                        else
                        {
                            Console.WriteLine("treasure masih ada");
                            for (int i = 0; i < headElement.route.nodePath.Count - 1; i++)  // the last element is considered not visited because it'll be done in the next iteration
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
                        this.addVisitedNodeSequence(headElement.route.node);
                        this.setNumOfNodesVisited(this.getNumOfNodesVisited() + 1);
                        Console.WriteLine("not treasure");
                        headElement.visitedNodes.setElement(headElement.route.node.getCoordinate().x, headElement.route.node.getCoordinate().y, 1);
                    }
                    headElement.route.node.setTreasure(false);

                    // RIGHT NODE
                    if (!found && headElement.route.node.getRight() != null && headElement.visitedNodes.getElement(headElement.route.node.getRight().getCoordinate().x, headElement.route.node.getRight().getCoordinate().y) == 0)
                    {
                        Console.Write("Masuk Kanan ");
                        // Initialize new ElementQueue
                        ElementQueue newElement = new ElementQueue();
                        newElement.route.path = new List<char>(headElement.route.path);
                        Console.WriteLine(headElement.route.path.Count);
                        Console.WriteLine("Kanan");

                        newElement.route.path.Add('R');

                        newElement.route.nodePath = new List<GraphNode>(headElement.route.nodePath);
                        newElement.route.nodePath.Add(headElement.route.node.getRight());

                        newElement.route.node = headElement.route.node.getRight();

                        newElement.visitedNodes = headElement.visitedNodes;

                        // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // DOWN NODE
                    if (!found && headElement.route.node.getDown() != null && headElement.visitedNodes.getElement(headElement.route.node.getDown().getCoordinate().x, headElement.route.node.getDown().getCoordinate().y) == 0)
                    {
                        Console.Write("Masuk Bawah ");
                        // Initialize new ElementQueue
                        ElementQueue newElement = new ElementQueue();
                        newElement.route.path = new List<char>(headElement.route.path);
                        Console.WriteLine(headElement.route.path.Count);
                        Console.WriteLine("Bawah");
                        newElement.route.path.Add('D');

                        newElement.route.nodePath = new List<GraphNode>(headElement.route.nodePath);
                        newElement.route.nodePath.Add(headElement.route.node.getDown());

                        newElement.route.node = headElement.route.node.getDown();

                        newElement.visitedNodes = headElement.visitedNodes;

                        // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // LEFT NODE
                    if (!found && headElement.route.node.getLeft() != null && headElement.visitedNodes.getElement(headElement.route.node.getLeft().getCoordinate().x, headElement.route.node.getLeft().getCoordinate().y) == 0)
                    {
                        Console.Write("Masuk Kiri ");
                        // Initialize new ElementQueue
                        ElementQueue newElement = new ElementQueue();
                        newElement.route.path = new List<char>(headElement.route.path);
                        Console.WriteLine(headElement.route.path.Count);
                        Console.WriteLine("Kiri");
                        newElement.route.path.Add('L');

                        newElement.route.nodePath = new List<GraphNode>(headElement.route.nodePath);
                        newElement.route.nodePath.Add(headElement.route.node.getLeft());

                        newElement.route.node = headElement.route.node.getLeft();

                        newElement.visitedNodes = headElement.visitedNodes;

                        // Enqueue newElement to queue
                        queue.Enqueue(newElement);
                    }

                    // UP NODE
                    if (!found && headElement.route.node.getUp() != null && headElement.visitedNodes.getElement(headElement.route.node.getUp().getCoordinate().x, headElement.route.node.getUp().getCoordinate().y) == 0)
                    {
                        Console.Write("Masuk Atas ");
                        // Initialize new ElementQueue
                        ElementQueue newElement = new ElementQueue();
                        newElement.route.path = new List<char>(headElement.route.path);
                        Console.WriteLine(headElement.route.path.Count);
                        Console.WriteLine("Atas");
                        newElement.route.path.Add('U');

                        newElement.route.nodePath = new List<GraphNode>(headElement.route.nodePath);
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

            Console.Write("Waktu: ");
            Console.Write(this.executionTime);
            Console.WriteLine(" ms");
        }

        public void runTSPBFSAlgorithm(int row, int col)
        {
            var watch = Stopwatch.StartNew(); // timer
            // startNode itu tujuan
            GraphNode startNode = new GraphNode(startGraph.getCoordinate(), startGraph.getVisited(), startGraph.isTreasure(), startGraph.getRight(), startGraph.getDown(), startGraph.getLeft(), startGraph.getUp());
            this.runBFSAlgorithm(row, col);
            GraphNode tempStartNode = this.visitedNodeSequence[this.visitedNodeSequence.Count - 1];
            int remainingTreasures = this.numOfTreasures;
            // 
            // Matrix visited = new Matrix(row, col);
            List<char> newPath = new List<char>();
            List<GraphNode> newNodePath = new List<GraphNode>();
            // newNodePath.Add(tempStartNode);

            ElementQueue firstElement = new ElementQueue(row, col, newPath, newNodePath, tempStartNode, 0);
            queue.Enqueue(firstElement);
            bool found = false;
            int x = 0;
            while (!found && this.queue.Count > 0)
            {
                ElementQueue headElement = this.queue.Dequeue();
                // Check if the headElement's last node is treasure
                if (headElement.route.node.getCoordinate().x == startNode.getCoordinate().x && headElement.route.node.getCoordinate().y == startNode.getCoordinate().y)
                {
                    found = true;

                    finalPath.AddRange(headElement.route.path);
                    Console.WriteLine("========= KOORDINAT START NODE ==========");
                    Console.Write("x: ");
                    Console.WriteLine(headElement.route.node.getCoordinate().x);
                    Console.Write("y: ");
                    Console.WriteLine(headElement.route.node.getCoordinate().y);


                    this.addVisitedNodeSequence(headElement.route.node);
                    this.setNumOfNodesVisited(this.getNumOfNodesVisited() + 1);
                    Console.WriteLine("treasure terakhir");
                    foreach (GraphNode n in headElement.route.nodePath)
                    {
                        n.setVisited(n.getVisited() + 1);
                    }


                    tempStartNode = headElement.route.node;
                    remainingTreasures--;
                    queue.Clear();

                }
                else
                {
                    this.addVisitedNodeSequence(headElement.route.node);
                    this.setNumOfNodesVisited(this.getNumOfNodesVisited() + 1);
                    Console.WriteLine("not treasure");
                    headElement.visitedNodes.setElement(headElement.route.node.getCoordinate().x, headElement.route.node.getCoordinate().y, 1);
                }
                headElement.route.node.setTreasure(false);

                // RIGHT NODE
                if (!found && headElement.route.node.getRight() != null && headElement.visitedNodes.getElement(headElement.route.node.getRight().getCoordinate().x, headElement.route.node.getRight().getCoordinate().y) == 0)
                {
                    Debug.Write("Masuk Kanan ");
                    // Initialize new ElementQueue
                    ElementQueue newElement = new ElementQueue();
                    newElement.route.path = new List<char>(headElement.route.path);
                    Debug.WriteLine(headElement.route.path.Count);
                    Debug.WriteLine("Kanan");

                    newElement.route.path.Add('R');

                    newElement.route.nodePath = new List<GraphNode>(headElement.route.nodePath);
                    newElement.route.nodePath.Add(headElement.route.node.getRight());

                    newElement.route.node = headElement.route.node.getRight();

                    newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                    queue.Enqueue(newElement);
                }

                // DOWN NODE
                if (!found && headElement.route.node.getDown() != null && headElement.visitedNodes.getElement(headElement.route.node.getDown().getCoordinate().x, headElement.route.node.getDown().getCoordinate().y) == 0)
                {
                    Console.Write("Masuk Bawah ");
                    // Initialize new ElementQueue
                    ElementQueue newElement = new ElementQueue();
                    newElement.route.path = new List<char>(headElement.route.path);
                    Console.WriteLine(headElement.route.path.Count);
                    Console.WriteLine("Bawah");
                    newElement.route.path.Add('D');

                    newElement.route.nodePath = new List<GraphNode>(headElement.route.nodePath);
                    newElement.route.nodePath.Add(headElement.route.node.getDown());

                    newElement.route.node = headElement.route.node.getDown();

                    newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                    queue.Enqueue(newElement);
                }

                // LEFT NODE
                if (!found && headElement.route.node.getLeft() != null && headElement.visitedNodes.getElement(headElement.route.node.getLeft().getCoordinate().x, headElement.route.node.getLeft().getCoordinate().y) == 0)
                {
                    Console.Write("Masuk Kiri ");
                    // Initialize new ElementQueue
                    ElementQueue newElement = new ElementQueue();
                    newElement.route.path = new List<char>(headElement.route.path);
                    Console.WriteLine(headElement.route.path.Count);
                    Console.WriteLine("Kiri");
                    newElement.route.path.Add('L');

                    newElement.route.nodePath = new List<GraphNode>(headElement.route.nodePath);
                    newElement.route.nodePath.Add(headElement.route.node.getLeft());

                    newElement.route.node = headElement.route.node.getLeft();

                    newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                    queue.Enqueue(newElement);
                }

                // UP NODE
                if (!found && headElement.route.node.getUp() != null && headElement.visitedNodes.getElement(headElement.route.node.getUp().getCoordinate().x, headElement.route.node.getUp().getCoordinate().y) == 0)
                {
                    Console.Write("Masuk Atas ");
                    // Initialize new ElementQueue
                    ElementQueue newElement = new ElementQueue();
                    newElement.route.path = new List<char>(headElement.route.path);
                    Console.WriteLine(headElement.route.path.Count);
                    Console.WriteLine("Atas");
                    newElement.route.path.Add('U');

                    newElement.route.nodePath = new List<GraphNode>(headElement.route.nodePath);
                    newElement.route.nodePath.Add(headElement.route.node.getUp());

                    newElement.route.node = headElement.route.node.getUp();

                    newElement.visitedNodes = headElement.visitedNodes;

                    // Enqueue newElement to queue
                    queue.Enqueue(newElement);
                }
                x++;

            } // found


            // Finishing
            watch.Stop();
            this.executionTime = watch.ElapsedMilliseconds;

            Console.Write("Path menuju treasure: ");
            foreach (char dir in this.finalPath)
            {
                Console.Write(dir);
            }
            Console.WriteLine("");

            Console.Write("Waktu: ");
            Console.Write(this.executionTime);
            Console.WriteLine(" ms");
        }



    }
}