using System.Collections.Generic;
namespace lib
{
    class Route
    {
        // Field
        public List<char> path;

        public List<GraphNode> nodePath;
        public GraphNode node;
        public int remainingTreasures;

        // Constructor
        // public Route()
        // {
        //     this.path = new List<char>();
        //     this.nodePath = new List<GraphNode>();
        //     this.node = null;
        //     this.remainingTreasures = 0;
        // }

        // public Route(List<char> path, List<GraphNode> nodePath, GraphNode node, int remainingTreasures)
        // {
        //     this.path = path;
        //     this.nodePath = nodePath;
        //     this.node = node;
        //     this.remainingTreasures = remainingTreasures;
        // }

        // // Getter   
        // public List<char> getPath()
        // {
        //     return this.path;
        // }

        // public List<GraphNode> getNodePath()
        // {
        //     return this.nodePath;
        // }
        // public GraphNode getNode()
        // {
        //     return this.node;
        // }
        // public int getRemainingTreasures()
        // {
        //     return this.remainingTreasures;
        // }

        // // Setter
        // public void setPath(List<char> path)
        // {
        //     this.path = path;
        // }

        // public void setNodePath(List<GraphNode> nodePath)
        // {
        //     this.nodePath = nodePath;
        // }

        // public void setNode(GraphNode node)
        // {
        //     this.node = node;
        // }

        // public void setRemainingTreasures(int remainingTreasures)
        // {
        //     this.remainingTreasures = remainingTreasures;
        // }
    }
}