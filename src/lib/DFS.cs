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
        public bool inTrack(GraphNode node, List<GraphNode> nodePath)
        {
            foreach (GraphNode nodeInPath in nodePath)
            {
                if (nodeInPath == node)
                {
                    return true;
                }
            }
            return false;
        }

        private void cekTetangga(GraphNode currentNode, List<char> currentPath, List<GraphNode> currentNodePath, int currentRemainingTreasures)
        {
            int maxVisited = 1;
            // if (backAndForth)
            // {
            //     maxVisited = 2;
            // }
            // else
            // {
            //     maxVisited = 1;
            // }

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

        public void resetVisitedNodes()
        {
            foreach (GraphNode node in this.graph)
            {
                node.setVisited(0);
            }
        }

        public void runDFSAlgorithm()
        {
            // Inisialization

            var watch = Stopwatch.StartNew(); // timer
            bool backtracking = false;
            bool stopbacktracking = false;
            bool multiplevisited = false;
            int remainingTreasures = this.numOfTreasures;
            if (this.graph[0].isTreasure())
            {
                remainingTreasures--;
            }

            Route tempRoute = new Route();
            List<char> tempPath = new List<char>();
            List<int> visitAccumulation = new List<int>(); // Menyimpan akumulasi jumlah visit
            // Inisialisasi
            foreach (GraphNode node in this.graph)
            {
                visitAccumulation.Add(node.getVisited());
            }


            List<GraphNode> tempNodePath = new List<GraphNode>() { this.graph[0] };
            this.graph[0].setVisited(1); // Telah dikunjungi
            tempRoute.node = this.graph[0];
            tempRoute.nodePath = tempNodePath;
            tempRoute.path = tempPath;
            tempRoute.remainingTreasures = remainingTreasures;
            this.stack.Push(tempRoute); // Push rute pertama

            // Console.WriteLine("X: " + this.stack.Peek().node.getValue().x.ToString() + " Y: " + this.stack.Peek().node.getValue().y);

            // Pengecekan tetangga untuk node pertama
            cekTetangga(this.graph[0], tempPath, tempNodePath, remainingTreasures);

            // Loop mencari semua treasures
            while (remainingTreasures != 0) // Akan looping hingga remaining treasures = 0
            {
                // Pengecekan currentNode, yaitu Peek pada stack
                GraphNode currentNode = new GraphNode();
                currentNode = this.stack.Peek().node;
                List<char> currentPath = new List<char>();
                currentPath = this.stack.Peek().path;
                List<GraphNode> currentNodePath = new List<GraphNode>();
                currentNodePath = this.stack.Peek().nodePath;
                int currentRemainingTreasures = this.stack.Peek().remainingTreasures;

                // foreach (char dir in currentPath)
                // {
                //     Console.Write(dir);
                // }

                // Console.Write(" ");
                // Console.WriteLine("X: " + currentNode.getCoordinate().x.ToString() + " Y: " + currentNode.getCoordinate().y);
                // Console.Write("Backtracking: ");
                // Console.WriteLine(backtracking);
                // Console.WriteLine(this.stack.Count);

                if (currentNode.getVisited() > 0 && !multiplevisited)
                { // Jika sudah pernah dikunjungi, pop route dari stack
                    Route currentRoute = new Route();
                    currentRoute = this.stack.Pop();
                    // Console.WriteLine("Remaining Treasures: " + currentRemainingTreasures.ToString());
                    if ((backtracking && !stopbacktracking) || currentNode.isTreasure()) // Jika backtracking simpan rute yang ditempuh ditambah direction dari current Node
                    {
                        if (!currentNode.isTreasure())
                        {
                            currentNode.setVisited(currentNode.getVisited() + 1);
                        }
                        backtracking = true;
                        this.stack.Peek().remainingTreasures = currentRemainingTreasures;
                        currentNodePath.Add(currentNode);
                        this.stack.Peek().nodePath = currentNodePath;
                        if (this.stack.Peek().node.getRight() == currentRoute.node) // Backtrack ke kiri
                        {
                            currentPath.Add('L');
                            this.stack.Peek().path = currentPath;
                        }
                        else if (this.stack.Peek().node.getDown() == currentRoute.node) // Backtrack ke atas
                        {
                            currentPath.Add('U');
                            this.stack.Peek().path = currentPath;
                        }
                        else if (this.stack.Peek().node.getLeft() == currentRoute.node) // Backtrack ke kanan
                        {
                            currentPath.Add('R');
                            this.stack.Peek().path = currentPath;
                        }
                        else if (this.stack.Peek().node.getUp() == currentRoute.node) // Backtrack ke bawah
                        {
                            currentPath.Add('D');
                            this.stack.Peek().path = currentPath;
                        }
                        else // Jika selanjutnya backtrack akan berhenti
                        {
                            this.stack.Peek().path = currentPath;
                        }
                    }
                    else
                    {
                        // Tidak melakukan apa"
                        if (stopbacktracking) // berada di simpang
                        {
                            // Lakukan pengisian path yang sesuai dikaitkan dengan node backtrack sebelum simpang
                            if (currentNode.getRight() == currentNodePath[currentNodePath.Count - 1])
                            {
                                currentPath.Add('L');
                            }
                            else if (currentNode.getDown() == currentNodePath[currentNodePath.Count - 1])
                            {
                                currentPath.Add('U');
                            }
                            else if (currentNode.getLeft() == currentNodePath[currentNodePath.Count - 1])
                            {
                                currentPath.Add('R');
                            }
                            else if (currentNode.getUp() == currentNodePath[currentNodePath.Count - 1])
                            {
                                currentPath.Add('D');
                            }
                            currentNodePath.Add(currentNode);

                            backtracking = false;
                            stopbacktracking = false;
                            multiplevisited = true;
                            // // Evaluasi currentNode
                            // currentNode.setVisited(currentNode.getVisited() + 1); // Increment numOfvisited
                            // if (currentNode.isTreasure())
                            // { // Jika merupakan treasure
                            //     currentRemainingTreasures--;
                            //     if (currentRemainingTreasures == 0)
                            //     {
                            //         remainingTreasures = 0;
                            //         continue;
                            //     }
                            // }
                            // Console.WriteLine("Remaining Treasures: " + currentRemainingTreasures.ToString());
                            // if (currentNode.isNeighbourVisitAbleExist())
                            // {
                            //     // Pengecekan tetangga untuk node yang sedang dikunjungi
                            //     cekTetangga(currentNode, currentPath, currentNodePath, currentRemainingTreasures);
                            // }
                            // else
                            // {
                            //     this.stack.Peek().remainingTreasures = currentRemainingTreasures;
                            //     this.stack.Peek().nodePath = currentNodePath;
                            //     this.stack.Peek().path = currentPath;
                            // }
                            this.stack.Push(currentRoute);
                        }
                    }
                }
                else
                {
                    if (backtracking) // Harus handling oper" dari backtracking ke normal
                    {
                        // Kasus jika backtracking distop. Yaitu saat terdapat node belum dikunjungi. Cari node yang menghubungkan (simpang)
                        // I guess ada kasus yang tak mungkin karena prioritas
                        // Node yg tidak backtrack berada di kanan simpang
                        // Node yg tidak backtrack di bawah simpang jika tak ada node backtrack di kanan
                        // Node yg tidak backtra
                        stopbacktracking = true;
                        Route currentRoute = new Route();
                        currentRoute = this.stack.Pop();
                        this.stack.Peek().path = currentPath;
                        this.stack.Peek().nodePath = currentNodePath;
                        this.stack.Peek().remainingTreasures = currentRemainingTreasures;
                        // Console.Write("Visited: ");
                        // Console.WriteLine(currentNode.getVisited().ToString());
                        continue;
                    }

                    multiplevisited = false;
                    // Evaluasi currentNode
                    currentNode.setVisited(currentNode.getVisited() + 1); // Increment numOfvisited
                    if (currentNode.isTreasure())
                    { // Jika merupakan treasure
                        currentRemainingTreasures--;
                        this.stack.Peek().remainingTreasures--;
                        if (currentRemainingTreasures == 0)
                        {
                            remainingTreasures = 0;
                            tempPath = currentPath;
                            currentNodePath.Add(currentNode);
                            tempNodePath = currentNodePath;
                            continue;
                        }
                    }

                    // Console.WriteLine("Remaining Treasures: " + currentRemainingTreasures.ToString());
                    if (currentNode.isNeighbourVisitAbleExist())
                    {
                        // Pengecekan tetangga untuk node yang sedang dikunjungi
                        // Console.WriteLine("Ko iyo ko");
                        cekTetangga(currentNode, currentPath, currentNodePath, currentRemainingTreasures);
                    }
                    else
                    {
                        this.stack.Peek().remainingTreasures = currentRemainingTreasures;
                        this.stack.Peek().nodePath = currentNodePath;
                        this.stack.Peek().path = currentPath;
                    }
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