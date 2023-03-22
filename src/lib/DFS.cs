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
        private List<char> path; // path akhir
        private int numOfTreasures;
        private int numOfNodesVisited; // jumlah node yang dikunjungi

        private double executionTime; // in ms

        private List<GraphNode> graph;

        private List<GraphNode> visitedNodeSequence; // urutan node yang dikunjungi

        private Stack<Route> stack;

        // Constructor
        public DFS()
        {
            this.path = new List<char>();
            this.numOfTreasures = 0;
            this.numOfNodesVisited = 0;
            this.executionTime = 0;
            this.graph = new List<GraphNode>();
            this.visitedNodeSequence = new List<GraphNode>();
            this.stack = new Stack<Route>();
        }

        public DFS(int numOftreasures, List<GraphNode> graph)
        {
            this.path = new List<char>();
            this.numOfTreasures = numOftreasures;
            this.numOfNodesVisited = 0;
            this.executionTime = 0;
            this.graph = graph;
            this.visitedNodeSequence = new List<GraphNode>();
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

        public List<GraphNode> getVisitedNodeSequence()
        {
            return this.visitedNodeSequence;
        }

        public Stack<Route> getStack()
        {
            Stack<Route> ctsack;
            ctsack = new Stack<Route>(this.stack);
            return ctsack;
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

        public void setVisitedNodeSequence(List<GraphNode> visitedNodeSequence)
        {
            this.visitedNodeSequence = visitedNodeSequence;
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
            foreach (GraphNode node in this.graph)
            {
                Debug.WriteLine("======================");
                Debug.WriteLine("Node: ");
                Debug.WriteLine(node.getCoordinate().x + " " + node.getCoordinate().y);
                Debug.WriteLine("Is Treasure: " + node.isTreasure().ToString());
                Debug.WriteLine("Tetangga: ");

                if (node.getRight() != null)
                {
                    Debug.Write("Kanan: ");
                    Debug.WriteLine(node.getRight().getCoordinate().x + " " + node.getRight().getCoordinate().y);
                }
                if (node.getDown() != null)
                {
                    Debug.Write("Bawah: ");
                    Debug.WriteLine(node.getDown().getCoordinate().x + " " + node.getDown().getCoordinate().y);
                }
                if (node.getLeft() != null)
                {
                    Debug.Write("Kiri: ");
                    Debug.WriteLine(node.getLeft().getCoordinate().x + " " + node.getLeft().getCoordinate().y);
                }
                if (node.getUp() != null)
                {
                    Debug.Write("Atas: ");
                    Debug.WriteLine(node.getUp().getCoordinate().x + " " + node.getUp().getCoordinate().y);
                }

                Debug.WriteLine("======================");
            }
            Route tempRoute = new Route();
            List<char> tempPath = new List<char>();

            // Inisialisasi

            List<GraphNode> tempNodePath = new List<GraphNode>() { this.graph[0] };
            this.graph[0].setVisited(1); // Telah dikunjungi
            tempRoute.node = this.graph[0];
            tempRoute.nodePath = tempNodePath;
            tempRoute.path = tempPath;
            tempRoute.remainingTreasures = remainingTreasures;
            this.stack.Push(tempRoute); // Push rute pertama
            this.visitedNodeSequence.Add(this.graph[0]); // Tambahkan node pertama ke visitedNodeSequence

            // Console.WriteLine("X: " + this.stack.Peek().node.getValue().x.ToString() + " Y: " + this.stack.Peek().node.getValue().y);

            // Pengecekan tetangga untuk node pertama
            cekTetangga(this.graph[0], tempPath, tempNodePath, remainingTreasures);

            // Loop mencari semua treasures
            while (remainingTreasures != 0 && this.stack.Count>0) // Akan looping hingga remaining treasures = 0
            {
                // Pengecekan currentNode, yaitu Peek pada stack
                GraphNode currentNode = new GraphNode();
                currentNode = this.stack.Peek().node;
                List<char> currentPath = new List<char>();
                currentPath = this.stack.Peek().path;
                List<GraphNode> currentNodePath = new List<GraphNode>();

                currentNodePath = this.stack.Peek().nodePath;
                int currentRemainingTreasures = this.stack.Peek().remainingTreasures;
                // Console.WriteLine("=======");

                // foreach (char dir in currentPath)
                // {
                //     Console.Write(dir);
                // }

                // Console.Write(" ");
                // Console.WriteLine("X: " + currentNode.getCoordinate().x.ToString() + " Y: " + currentNode.getCoordinate().y);
                // Console.Write("Backtracking: ");
                // Console.WriteLine(backtracking);
                // Console.WriteLine(stopbacktracking);
                // Console.WriteLine(this.stack.Count);

                if (currentNode.getVisited() > 0 && !multiplevisited)
                { // Jika sudah pernah dikunjungi, pop route dari stack
                    Route currentRoute = new Route();
                    currentRoute = this.stack.Pop();
                    // Console.WriteLine("Remaining Treasures: " + currentRemainingTreasures.ToString());
                    if ((backtracking && !stopbacktracking) || (currentNode.isTreasure() && !backtracking) || currentNode.isIntersection()) // Jika backtracking simpan rute yang ditempuh ditambah direction dari current Node
                    {
                        if (!currentNode.isTreasure())
                        {
                            currentNode.setVisited(currentNode.getVisited() + 1);
                            this.visitedNodeSequence.Add(currentNode); // tambahkan ke visitedNodeSequence
                        }
                        backtracking = true;
                        this.stack.Peek().remainingTreasures = currentRemainingTreasures;
                        if (currentNodePath[currentNodePath.Count - 1].isNeighbourhood(currentNode))
                        {
                            currentNodePath.Add(currentNode); // Hanya ditambahkan ke node path jika bertetangga langsung
                        }
                        this.stack.Peek().nodePath = currentNodePath;
                        if (this.stack.Peek().node.getRight() == currentNodePath[currentNodePath.Count - 1]) // Backtrack ke kiri
                        {
                            currentPath.Add('L');
                            this.stack.Peek().path = currentPath;
                        }
                        else if (this.stack.Peek().node.getDown() == currentNodePath[currentNodePath.Count - 1]) // Backtrack ke atas
                        {
                            currentPath.Add('U');
                            this.stack.Peek().path = currentPath;
                        }
                        else if (this.stack.Peek().node.getLeft() == currentNodePath[currentNodePath.Count - 1]) // Backtrack ke kanan
                        {
                            currentPath.Add('R');
                            this.stack.Peek().path = currentPath;
                        }
                        else if (this.stack.Peek().node.getUp() == currentNodePath[currentNodePath.Count - 1]) // Backtrack ke bawah
                        {
                            currentPath.Add('D');
                            this.stack.Peek().path = currentPath;
                        }
                        else // Jika selanjutnya backtrack akan berhenti atau backtrack tp bukan bagian path
                        {
                            this.stack.Peek().path = currentPath;
                        }
                    }
                    else // Jika tidak sedang backtracking, atau ada sinyal untuk stop backtracking
                    {
                        if (stopbacktracking) // berada di simpang, sinyal stop backtracking menyala
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

                            // Set marker untuk stop backtracking di simpang
                            backtracking = false;
                            stopbacktracking = false;
                            multiplevisited = true;
                            currentNode.setIntersection(true);
                            this.stack.Push(currentRoute); // Push agar dievaluasi ulang dalam situasi tidak backtracking
                        }
                        else
                        { // Jika hanya berupa backtrack karena tidak ada solusi
                            if (currentNodePath[currentNodePath.Count - 1] != currentNode)
                            {
                                this.visitedNodeSequence.Add(currentNode);
                            }
                        }
                    }
                }
                else
                {
                    if (backtracking) // Harus handling oper" dari backtracking ke normal
                    {
                        // Kasus jika backtracking distop. Yaitu saat terdapat node belum dikunjungi. Pop hingga ketemu simpang
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
                    this.visitedNodeSequence.Add(currentNode);// tambahkan ke visitedNodeSequence
                    if (currentNode.isTreasure() && (currentNode.getVisited() == 1))
                    { // Jika merupakan treasure
                        currentRemainingTreasures--;
                        this.stack.Peek().remainingTreasures--;
                        if (currentRemainingTreasures == 0)
                        {
                            remainingTreasures = 0;
                            tempPath = currentPath;
                            currentNodePath.Add(currentNode);
                            tempNodePath = currentNodePath;
                            // Console.WriteLine("Remaining Treasures saat kedetect: " + currentRemainingTreasures.ToString());
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
            this.numOfNodesVisited = 0;
            foreach (GraphNode node in this.graph)
            {
                this.numOfNodesVisited += node.getVisited();
            }

        }

        // static void Main(string[] args)
        // {
        //     System.Console.WriteLine("DFS Algorithm!");

        // }
    }
}