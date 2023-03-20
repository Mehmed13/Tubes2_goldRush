using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace lib
{
    class DFS
    {
        // PRIORITAS DRIECTIONS : RIGHT, DOWN, LEFT, UP
        // Belum handling kasus harus bolak balik + deepclone nodePath : masih bitwise copy sepertinya
        // Mekanisme 
        // Normal Flow: DFS seperti biasa
        // backAndForth Flow: DFS seperti biasa hingga ketemu node treasure tetapi udah dicek, pindah ke flow backAndForth
        //                    stack dikosongkan dan disisakan satu saja, yaitu node treasure tetapi udah dicek wktu ganti mode
        //                    Pengunjungan terhadap node yang sudah dikunjungi menjadi diperbolehkan dan dfs mencari treasure yang lain
        //                    Hal ini masih mengandung bug saat ada 3 jalur bolak balik

        //                    Maybe solved, dalam pikiranku, aku membayangkan setelah masuk backAndForth, batas maks dikunjungi itu jadi 2
        //                    Sehingga tak perlu risaukan jika terjadi backAndForth Again, karena jalur yang akan dicari masih dalam satu route yang kontinu
        private List<char> path;
        private int numOfTreasures;
        private int numOfNodesVisited;

        private double executionTime; // in ms

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

        public DFS(int numOftreasures, List<GraphNode> graph)
        {
            this.path = new List<char>();
            this.numOfTreasures = numOftreasures;
            this.numOfNodesVisited = 0;
            this.executionTime = 0;
            this.graph = graph;
            this.stack = new Stack<Route>();
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

        // Melakukan pengecekan untuk semua tetangga pada currentNode
        private void cekTetangga(GraphNode currentNode, List<char> currentPath, List<GraphNode> currentNodePath, int currentRemainingTreasures, bool backAndForth)
        {
            int maxVisited;
            if (backAndForth)
            {
                maxVisited = 2;
            }
            else
            {
                maxVisited = 1;
            }

            // Tambahkan tetangga yang belum pernah visited ke dalam stack
            // Pada stack, prioritas tertinggi ditambahkan terakhir

            // Tetangga Atas
            GraphNode upNeighbour = currentNode.getUp();
            if (upNeighbour != null) // Tetangga atas
            {
                if (upNeighbour.getVisited() < maxVisited)
                {
                    Route tempUpRoute = new Route();
                    tempUpRoute.node = upNeighbour;
                    tempUpRoute.path = new List<char>(currentPath);
                    tempUpRoute.path.Add('U');
                    tempUpRoute.nodePath = new List<GraphNode>(currentNodePath);
                    tempUpRoute.nodePath.Add(upNeighbour);
                    tempUpRoute.remainingTreasures = currentRemainingTreasures;

                    this.stack.Push(tempUpRoute);
                }
            }

            // Tetangga Kiri
            GraphNode leftNeighbour = currentNode.getLeft();
            if (leftNeighbour != null) // Tetangga kiri
            {
                if (leftNeighbour.getVisited() < maxVisited)
                {
                    Route tempLeftRoute = new Route();
                    tempLeftRoute.node = leftNeighbour;
                    tempLeftRoute.path = new List<char>(currentPath);
                    tempLeftRoute.path.Add('L');
                    tempLeftRoute.nodePath = new List<GraphNode>(currentNodePath);
                    tempLeftRoute.nodePath.Add(leftNeighbour);
                    tempLeftRoute.remainingTreasures = currentRemainingTreasures;

                    this.stack.Push(tempLeftRoute);
                }
            }

            // Tetangga Bawah
            GraphNode downNeighbour = currentNode.getDown();
            if (downNeighbour != null) // Tetangga bawah
            {
                if (downNeighbour.getVisited() < maxVisited)
                {
                    Route tempDownRoute = new Route();
                    tempDownRoute.node = downNeighbour;
                    tempDownRoute.path = new List<char>(currentPath);
                    tempDownRoute.path.Add('D');
                    tempDownRoute.nodePath = new List<GraphNode>(currentNodePath);
                    tempDownRoute.nodePath.Add(downNeighbour);
                    tempDownRoute.remainingTreasures = currentRemainingTreasures;

                    this.stack.Push(tempDownRoute);
                }
            }

            // Tetangga Kanan
            GraphNode rightNeighbour = currentNode.getRight();
            if (rightNeighbour != null) // Tetangga Kanan
            {
                if (rightNeighbour.getVisited() < maxVisited)
                {
                    Route tempRightRoute = new Route();
                    tempRightRoute.node = rightNeighbour;
                    tempRightRoute.path = new List<char>(currentPath);
                    tempRightRoute.path.Add('R');
                    tempRightRoute.nodePath = new List<GraphNode>(currentNodePath);
                    tempRightRoute.nodePath.Add(rightNeighbour);
                    tempRightRoute.remainingTreasures = currentRemainingTreasures;

                    this.stack.Push(tempRightRoute);
                }
            }
        }


        public void runDFSAlgorithm()
        {
            // Inisialization

            var watch = Stopwatch.StartNew(); // timer
            bool backAndForth = false;
            int remainingTreasures = this.numOfTreasures;
            if (this.graph[0].isTreasure())
            {
                remainingTreasures--;
            }

            Route tempRoute = new Route();
            List<char> tempPath = new List<char>();
            List<GraphNode> tempNodePath = new List<GraphNode>() { this.graph[0] };
            this.graph[0].setVisited(1); // Telah dikunjungi
            tempRoute.node = this.graph[0];
            tempRoute.nodePath = tempNodePath;
            tempRoute.path = tempPath;
            tempRoute.remainingTreasures = remainingTreasures;
            this.stack.Push(tempRoute); // Push rute pertama

            Console.WriteLine("X: " + this.stack.Peek().node.getValue().x.ToString() + " Y: " + this.stack.Peek().node.getValue().y);

            // Pengecekan tetangga untuk node pertama
            cekTetangga(this.graph[0], tempPath, tempNodePath, remainingTreasures, backAndForth);

            // Loop mencari semua treasures
            while (remainingTreasures != 0) // Akan looping hingga remaining treasures = 0
            {
                if (!backAndForth) // Kasus Normal
                {
                    // Pengecekan currentNode, yaitu Peek pada stack
                    GraphNode currentNode = new GraphNode();
                    currentNode = this.stack.Peek().node;
                    List<char> currentPath = new List<char>();
                    currentPath = this.stack.Peek().path;
                    List<GraphNode> currentNodePath = new List<GraphNode>();
                    currentNodePath = this.stack.Peek().nodePath;
                    int currentRemainingTreasures = this.stack.Peek().remainingTreasures;

                    foreach (char dir in currentPath)
                    {
                        Console.Write(dir);
                    }
                    Console.Write(" ");
                    Console.WriteLine("X: " + currentNode.getValue().x.ToString() + " Y: " + currentNode.getValue().y);
                    Console.WriteLine(this.stack.Count);

                    if (currentNode.getVisited() > 0)
                    { // Jika sudah pernah dikunjungi, pop route dari stack
                        Route currentRoute = this.stack.Pop();
                        if (currentRoute.node.isTreasure())
                        { // Terindikasi masuk backAndForth mode
                            backAndForth = true;
                            this.stack.Clear();
                            this.stack.Push(currentRoute);
                        }
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

                        // Pengecekan tetangga untuk node yang sedang dikunjungi
                        cekTetangga(currentNode, currentPath, currentNodePath, currentRemainingTreasures, backAndForth);
                    }
                }
                else // Masuk back and Forth mode
                {

                    Console.WriteLine("Ga handle");
                    break;
                }
            }


            // Finishing
            watch.Stop();
            this.executionTime = watch.ElapsedMilliseconds; // get the execution time in ms
            this.path = tempPath;
            this.numOfNodesVisited = tempNodePath.Count;

        }

        // static void Main(string[] args)
        // {
        //     System.Console.WriteLine("DFS Algorithm!");

        // }
    }
}