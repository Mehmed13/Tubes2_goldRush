using System;
using System.Collections;
using System.Collections.Generic;
namespace lib
{
    public class Utility
    {
        public static int getIndex(List<GraphNode> graph, Coordinate coordinate)
        {
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].getCoordinate().x == coordinate.x && graph[i].getCoordinate().y == coordinate.y)
                {
                    return i;
                }
            }
            return -1;
        }
        public static ArrayList getGraphData(char[,] matrixMap)
        {
            int numOfTreasures = 0;
            List<GraphNode> graph = new List<GraphNode>();
            ArrayList graphData = new ArrayList(); // {graph, numOfTreasures}
            // Pengisian list of Node (graph)
            for (int i = 0; i < matrixMap.GetLength(0); i++) // baris
            {
                for (int j = 0; j < matrixMap.GetLength(1); j++) // kolom
                {
                    if (matrixMap[i, j] == 'T') // Jika cell treasure
                    {
                        numOfTreasures++;
                        GraphNode node = new GraphNode(new Coordinate(i, j), 0, true);
                        graph.Add(node);
                        makeNodesRelation(node, graph);
                    }
                    else if (matrixMap[i, j] == 'K') // Jika cell start
                    {
                        GraphNode node = new GraphNode(new Coordinate(i, j), 0, false);
                        graph.Insert(0, node);
                        makeNodesRelation(node, graph);
                    }
                    else if (matrixMap[i, j] == 'R') // Jika cell grid yang bisa diakses
                    {
                        GraphNode node = new GraphNode(new Coordinate(i, j), 0, false);
                        graph.Add(node);
                        makeNodesRelation(node, graph);
                    }
                }
            }
            graphData.Add(graph);
            graphData.Add(numOfTreasures);
            return graphData;
        }

        public static void makeNodesRelation(GraphNode node, List<GraphNode> graph)
        {
            int x = node.getCoordinate().x;
            int y = node.getCoordinate().y;
            int idx = -1;
            // Cek atas
            if (node.getUp() == null)
            {
                idx = getIndex(graph, new Coordinate(x - 1, y));
                if (idx != -1)
                {

                    node.setUp(graph[idx]);
                    graph[idx].setDown(node);
                }
            }
            // Cek bawah
            if (node.getDown() == null)
            {
                idx = getIndex(graph, new Coordinate(x + 1, y));
                if (idx != -1)
                {
                    node.setDown(graph[idx]);
                    graph[idx].setUp(node);
                }
            }
            // Cek kiri
            if (node.getLeft() == null)
            {
                idx = getIndex(graph, new Coordinate(x, y - 1));
                if (idx != -1)
                {
                    node.setLeft(graph[idx]);
                    graph[idx].setRight(node);
                }
            }
            // Cek kanan
            if (node.getRight() == null)
            {
                idx = getIndex(graph, new Coordinate(x, y + 1));
                if (idx != -1)
                {
                    node.setRight(graph[idx]);
                    graph[idx].setLeft(node);
                }
            }
        }

        public static void Main(string[] args)
        {
            char[,] matrixMap = {
                { 'X', 'X', 'T', 'X','T', 'X', 'T', 'X', 'X'},
                { 'X', 'X', 'R', 'X','R', 'X', 'R', 'X', 'X'},
                { 'K', 'R', 'R', 'R','R', 'R', 'R', 'R', 'T'},
                { 'X', 'X', 'R', 'X','R', 'X', 'R', 'X', 'X'},
                { 'X', 'X', 'T', 'X','T', 'X', 'T', 'X', 'X'}};
            ArrayList graphData = getGraphData(matrixMap);
            List<GraphNode> graph = (List<GraphNode>)graphData[0];
            int numOfTreasures = (int)graphData[1];
            // foreach (GraphNode node in graph)
            // {
            //     Console.WriteLine("======================");
            //     Console.WriteLine("Node: ");
            //     Console.WriteLine(node.getCoordinate().x + " " + node.getCoordinate().y);
            //     Console.WriteLine("Is Treasure: " + node.isTreasure().ToString());
            //     Console.WriteLine("Tetangga: ");

            //     if (node.getRight() != null)
            //     {
            //         Console.Write("Kanan: ");
            //         Console.WriteLine(node.getRight().getCoordinate().x + " " + node.getRight().getCoordinate().y);
            //     }
            //     if (node.getDown() != null)
            //     {
            //         Console.Write("Bawah: ");
            //         Console.WriteLine(node.getDown().getCoordinate().x + " " + node.getDown().getCoordinate().y);
            //     }
            //     if (node.getLeft() != null)
            //     {
            //         Console.Write("Kiri: ");
            //         Console.WriteLine(node.getLeft().getCoordinate().x + " " + node.getLeft().getCoordinate().y);
            //     }
            //     if (node.getUp() != null)
            //     {
            //         Console.Write("Atas: ");
            //         Console.WriteLine(node.getUp().getCoordinate().x + " " + node.getUp().getCoordinate().y);
            //     }

            //     Console.WriteLine("======================");
            // }
            // Console.WriteLine("Banyak Treasures: " + numOfTreasures.ToString());


            DFS searchingDFS = new DFS(numOfTreasures, graph);
            searchingDFS.runDFSAlgorithm();
            Console.WriteLine("Path DFS: ");
            List<char> pathDFS = searchingDFS.getPath();
            for (int i = 0; i < pathDFS.Count; i++)
            {
                Console.Write(pathDFS[i]);
                if (i != pathDFS.Count - 1)
                    Console.Write("-");
            }

            Console.WriteLine();
            Console.WriteLine("Visited Node Sequence:");
            List<GraphNode> visitedNodeSequence = searchingDFS.getVisitedNodeSequence();
            foreach (GraphNode visitedNode in visitedNodeSequence)
            {
                Console.WriteLine(visitedNode.getCoordinate().x + " " + visitedNode.getCoordinate().y + "  :" + visitedNode.getVisited().ToString());
            }
            Console.WriteLine("Num of visitedNodes: " + searchingDFS.getNumOfNodesVisited().ToString());
            Console.WriteLine("TimeExecution: " + searchingDFS.getExecutionTime().ToString() + "ms");
        }

    }
}

