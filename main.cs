using System;
using System.Collections.Generic;
namespace lib
{

    class lain
    {
        static void Main(string[] args)
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
            position5.x = 0;
            position5.y = 4;
            Coordinate position6 = new Coordinate();
            position6.x = 0;
            position6.y = 5;
            Coordinate position7 = new Coordinate();
            position7.x = 1;
            position7.y = 1;
            Coordinate position8 = new Coordinate();
            position8.x = 1;
            position8.y = 3;
            Coordinate position9 = new Coordinate();
            position9.x = 1;
            position9.y = 5;
            Coordinate position10 = new Coordinate();
            position10.x = 1;
            position10.y = 6;
            Coordinate position11 = new Coordinate();
            position11.x = 2;
            position11.y = 1;
            Coordinate position12 = new Coordinate();
            position12.x = 2;
            position12.y = 3;
            Coordinate position13 = new Coordinate();
            position13.x = 2;
            position13.y = 5;
            Coordinate position14 = new Coordinate();
            position14.x = 3;
            position14.y = 1;
            Coordinate position15 = new Coordinate();
            position15.x = 3;
            position15.y = 5;

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
            node4.setRight(node5);
            node5.setLeft(node4);
            GraphNode node6 = new GraphNode(position6);
            node5.setRight(node6);
            node6.setLeft(node5);
            GraphNode node7 = new GraphNode(position7);
            node2.setDown(node7);
            node7.setUp(node2);
            GraphNode node8 = new GraphNode(position8);
            node4.setDown(node8);
            node8.setUp(node4);
            node8.setTreasure(true);
            GraphNode node9 = new GraphNode(position9);
            node6.setDown(node9);
            node9.setUp(node6);
            GraphNode node10 = new GraphNode(position10);
            node9.setRight(node10);
            node10.setLeft(node9);
            GraphNode node11 = new GraphNode(position11);
            node7.setDown(node11);
            node11.setUp(node7);
            node11.setTreasure(true);
            GraphNode node12 = new GraphNode(position12);
            node8.setDown(node12);
            node12.setUp(node8);
            GraphNode node13 = new GraphNode(position13);
            node9.setDown(node13);
            node13.setUp(node9);
            GraphNode node14 = new GraphNode(position14);
            node11.setDown(node14);
            node14.setUp(node11);
            GraphNode node15 = new GraphNode(position15);
            node13.setDown(node15);
            node15.setUp(node13);
            node15.setTreasure(true);




            // GraphNode node7 = new GraphNode(position7);
            // node6.setLeft(node7);
            // node5.setRight(node7);
            // node7.setLeft(node5);
            // node7.setRight(node6);
            // Collecting nodes become a graph that represented by list of nodes
            List<GraphNode> graph = new List<GraphNode>() { node1, node2, node3, node4, node5, node6, node7, node8, node9, node10, node11, node12, node13, node14, node15 };

            // Creating DFS object
            // DFS cek = new DFS(3, graph);
            // cek.runDFSAlgorithm();
            // foreach (char dir in cek.getPath())
            // {
            //     Console.Write(dir);
            // }

            // Console.WriteLine("\nNum Visited Node");
            // for (int i = 0; i < graph.Count; i++)
            // {
            //     Console.WriteLine("Node" + (i + 1).ToString() + ": " + cek.getGraph()[i].getVisited());
            // }

            // Creating BFS object
            Console.WriteLine("BFS:");
            BFS cekBFS = new BFS(3, node1);
            cekBFS.runBFSAlgorithm(4, 7);

            Console.WriteLine("\nNum Visited Node");
            for (int i = 0; i < graph.Count; i++)
            {
                Console.WriteLine("Node" + (i + 1).ToString() + ": " + graph[i].getVisited());
            }


        }
    }
}