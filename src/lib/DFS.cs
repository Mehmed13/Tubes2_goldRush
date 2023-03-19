using System;
using System.Diagnostics;
using System.collections.Generic;

namespace lib
{
    class DFS
    {
        // PRIORITAS DRIECTIONS : RIGHT, DOWN, LEFT, UP
        // Belum handling kasus harus bolak balik
        private List<char> path;
        private int numOfTreasures;
        private int numOfNodesVisited;

        private long executionTime; // in ms

        private List<GraphNode> graph;

        private Stack<Route> stack;

        // Constructor
        public DFS()
        {
            this.path = new List<char>();
            this.numOfTreasures = 0;
            this.numOfNodesVisited = 0;
            this.executionTime = 0;
            this.graph = new List<GraphNode>();
            this.stack = new Stack<Route>();
        }

        public DFS(int numOftreasures, List<GraphNode> graph, Stack<Route> stack)
        {
            this.path = new List<char>();
            this.numOfTreasures = numOftreasures;
            this.numOfNodesVisited = 0;
            this.executionTime = 0;
            this.graph = graph;
            this.stack = stack;
        }

        // Getter
        public List<char> getPath()
        {
            return this.path;
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
        public List<GraphNode> getGraph()
        {
            return this.graph;
        }
        public Stack<Route> getStack()
        {
            return this.stack;
        }

        // Setter
        public void setPath(List<char> path)
        {
            this.path = path;
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
        public void setGraph(List<GraphNode> graph)
        {
            this.graph = graph;
        }
        public void setStack(Stack<Route> stack)
        {
            this.stack = stack;
        }

        // Method
        public void runDFSAlgorithm()
        {
            // Inisialization

            var watch = Stopwatch.StartNew(); // timer
            int remainingTreasures = this.numOfTreasures;
            if (this.graph[0].isTreasure())
            {
                remainingTreasures--;
            }

            Route tempRoute;
            List<char> tempPath = new List<char>();
            List<GraphNode> tempNodePath = new List<GraphNode>() { this.graph[0] };
            this.graph[0].setVisited(1); // Telah dikunjungi
            tempRoute.node = this.graph[0];
            tempRoute.nodePath = tempNodePath;
            tempRoute.path = tempPath;
            tempRoute.remainingTreasures = remainingTreasures;
            this.stack.Push(tempRoute);

            // Loop

            while (remainingTreasures != 0) // Akan looping hingga remaining treasures = 0
            {
                if (this.stack.Count > 0) // Kasus Normal
                {
                    // Pengecekan currentNode, yaitu Peek pada stack
                    currentNode = this.stack.Peek().node;
                    currentPath = this.stack.Peek().path;
                    currentNodePath = this.stack.Peek().nodePath;
                    currentRemainingTreasures = this.stack.Peek().remainingTreasures;

                    if (currentNode.getVisited() > 0)
                    { // Jika sudah pernah dikunjungi, pop route dari stack
                        Route currentRoute = this.stack.Pop();
                    }
                    else
                    {
                        // Evaluasi currentNode
                        currentNode.setVisited(currentNode.getVisited() + 1); // Increment numOfvisited
                        if (currentNode.isTreasure())
                        { // Jika merupakan treasure
                            currentRemainingTreasures--;
                            if (currentRemainingTreasures == 0) // Jika DFS selesai
                            {
                                // Set remaining Treasures 0 dan tempPath, tempNodePath menjadi current
                                remainingTreasures = 0;
                                tempPath = currentPath;
                                tempNodePath = currentNodePath;

                                continue; // skip agar keluar loop
                            }
                        }

                        // Tambahkan tetangga yang belum pernah visited ke dalam stack
                        // Pada stack, prioritas tertinggi ditambahkan terakhir

                        // Tetangga Atas
                        GraphNode upNeighbour = currentNode.getUp();
                        if (upNeighbour != null) // Tetangga atas
                        {
                            if (upNeighbour.getVisited() == 0)
                            {
                                Route tempUpRoute;
                                tempUpRoute.node = upNeighbour;
                                tempUpRoute.path = currentPath.Add('U');
                                tempUpRoute.nodePath = currentNodePath.Add(upNeighbour);
                                tempUpRoute.remainingTreasures = currentRemainingTreasures;

                                this.stack.Push(tempUpRoute);
                            }
                        }

                        // Tetangga Kiri
                        GraphNode leftNeighbour = currentNode.getLeft();
                        if (leftNeighbour != null) // Tetangga atas
                        {
                            if (leftNeighbour.getVisited() == 0)
                            {
                                Route tempLeftRoute;
                                tempLeftRoute.node = leftNeighbour;
                                tempLeftRoute.path = currentPath.Add('L');
                                tempLeftRoute.nodePath = currentNodePath.Add(leftNeighbour);
                                tempLeftRoute.remainingTreasures = currentRemainingTreasures;

                                this.stack.Push(tempLeftRoute);
                            }
                        }

                        // Tetangga Bawah
                        GraphNode downNeighbour = currentNode.getDown();
                        if (downNeighbour != null) // Tetangga atas
                        {
                            if (downNeighbour.getVisited() == 0)
                            {
                                Route tempDownRoute;
                                tempDownRoute.node = downNeighbour;
                                tempDownRoute.path = currentPath.Add('L');
                                tempDownRoute.nodePath = currentNodePath.Add(downNeighbour);
                                tempDownRoute.remainingTreasures = currentRemainingTreasures;

                                this.stack.Push(tempDownRoute);
                            }
                        }

                        // Tetangga Kanan
                        GraphNode rightNeighbour = currentNode.getRight();
                        if (rightNeighbour != null) // Tetangga atas
                        {
                            if (rightNeighbour.getVisited() == 0)
                            {
                                Route tempRightRoute;
                                tempRightRoute.node = rightNeighbour;
                                tempRightRoute.path = currentPath.Add('L');
                                tempRightRoute.nodePath = currentNodePath.Add(rightNeighbour);
                                tempRightRoute.remainingTreasures = currentRemainingTreasures;

                                this.stack.Push(tempRightRoute);
                            }
                        }
                    }
                }
                else
                { // Jika sudah dicek semua tetapi belum menemukan semua treasures

                }
            }


            // Finishing
            watch.Stop();
            this.executionTime = watch.ElapsedMilliseconds; // get the execution time in ms
            this.path = tempPath;
            this.numOfNodesVisited = tempNodePath.Count;

        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("DFS Algorithm!");
        }
    }
}