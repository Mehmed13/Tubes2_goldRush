using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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

        public static char[,] readFromFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            string[] columns = lines[0].Split(' ');
            int row = lines.Length;
            int col = columns.Length;
            char[,] ret = new char[row, col];
            int cntK = 0;
            int cntT = 0;
            int cntR = 0;
            for (int i = 0; i < row; i++)
            {
                string[] values = lines[i].Trim().Split(' ');
                if (values.Length != col)
                {
                    throw new Exception(); // jika terdapat baris yang jumlah kolomnya berbeda
                }
                for (int j = 0; j < col; j++)
                {
                    if (values[j] == "K" || values[j] == "R" || values[j] == "T" || values[j] == "X")
                    {
                        ret[i, j] = char.Parse(values[j]);
                        if (values[j] == "K")
                        {
                            cntK++;
                        }
                        if (values[j] == "R")
                        {
                            cntR++;
                        }
                        if (values[j] == "T")
                        {
                            cntT++;
                        }
                    }
                    else // karakter selain yang diijinkan
                    {
                        throw new Exception();
                    }
                }
            }
            if (cntK != 1 || cntT < 1 || cntR < 1)
            { // jumlah K harus tepat 1, jumlah T harus >=1 , jumlah R harus >= 1
                throw new Exception();
            }
            return ret;
        }

        // public static void Main()
        // {
        //     char[,] matrixMap = readFromFile("../../test/test3.txt");
        //     ArrayList graphData = getGraphData(matrixMap);
        //     List<GraphNode> graph = (List<GraphNode>)graphData[0];
        //     int numOfTreasures = (int)graphData[1];
        //     DFS searchingDFS = new DFS(numOfTreasures, graph);
        //     searchingDFS.runDFSAlgorithm();

        //     TSP searchingTSP = new TSP(searchingDFS);
        //     searchingTSP.runTSPDFSAlgorithm();


        //     Console.WriteLine("Path TSP: ");
        //     List<char> pathTSP = searchingTSP.getPath();
        //     for (int i = 0; i < pathTSP.Count; i++)
        //     {
        //         Console.Write(pathTSP[i]);
        //         if (i != pathTSP.Count - 1)
        //             Console.Write("-");
        //     }

        //     Console.WriteLine();
        //     Console.WriteLine("Visited Node Sequence:");
        //     List<GraphNode> visitedNodeSequence = searchingTSP.getVisitedNodeSequence();
        //     foreach (GraphNode visitedNode in visitedNodeSequence)
        //     {
        //         Console.WriteLine(visitedNode.getCoordinate().x + " " + visitedNode.getCoordinate().y + "  :" + visitedNode.getVisited().ToString());
        //     }
        //     Console.WriteLine("Num of visitedNodes: " + searchingTSP.getNumOfNodesVisited().ToString());
        //     Console.WriteLine("TimeExecution: " + searchingTSP.getExecutionTime().ToString() + "ms");
        // }
    }
}

