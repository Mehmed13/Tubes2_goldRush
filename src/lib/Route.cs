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
    }
}