using System;
using System.Collections.Generic;
namespace lib
{
    public class Utility
    {
        public static void Main(string[] args)
        {
            // Coordinate pNode = new Coordinate();
            // pNode.x = 1;
            // pNode.y = 2;
            // // GraphNode node1(pNode, 0, false);
            // Console.WriteLine(pNode.x);
            // Console.WriteLine(pNode.y);

            // Inisialisasi posisi 
            Coordinate position1 = new Coordinate();
            position1.x = 0;
            position1.y = 0;
            Coordinate position2 = new Coordinate();
            position2.x = 0;
            position2.y = 1;
            Coordinate position3 = new Coordinate();
            position3.x = 0;
            position3.y = 2;
            Coordinate position4 = new Coordinate();
            position4.x = 0;
            position4.y = 3;
            Coordinate position5 = new Coordinate();
            position5.x = 1;
            position5.y = 1;
            Coordinate position6 = new Coordinate();
            position6.x = 1;
            position6.y = 3;
            Coordinate position7 = new Coordinate();
            position7.x = 1;
            position7.y = 2;

            // Creating Nodes
            GraphNode node1 = new GraphNode(position1);
            GraphNode node2 = new GraphNode(position2);
            node1.setRight(node2);
            node2.setLeft(node1);
            GraphNode node3 = new GraphNode(position3);
            node2.setRight(node3);
            node3.setLeft(node2);
            GraphNode node4 = new GraphNode(position4);
            node3.setRight(node4);
            node4.setLeft(node3);
            GraphNode node5 = new GraphNode(position5);
            node5.setTreasure(true);
            node2.setDown(node5);
            node5.setUp(node2);

            GraphNode node6 = new GraphNode(position6);
            node4.setDown(node6);
            node6.setUp(node4);
            node6.setTreasure(true);

            GraphNode node7 = new GraphNode(position7);
            node6.setLeft(node7);
            node5.setRight(node7);
            node7.setLeft(node5);
            node7.setRight(node6);
            // Collecting nodes become a graph that represented by list of nodes
            List<GraphNode> graph = new List<GraphNode>() { node1, node2, node3, node4, node5, node6, node7 };

            // Creating DFS object
            DFS cek = new DFS(2, graph);
            cek.runDFSAlgorithm();
            foreach (char dir in cek.getPath())
            {
                Console.Write(dir);
            }

        }
    }
}
