using System;


namespace GraphNode
{
    class GraphNode
    {
        // Field
        private int value;
        private int visited;
        private GraphNode right;
        private GraphNode down;
        private GraphNode left;
        private GraphNode up;

        // Constructor
        public GraphNode()
        { // default
            this.value = 0;
            this.visited = 0;
            this.right = null;
            this.down = null;
            this.left = null;
            this.up = null;
        }

        public GraphNode(int value)
        { // value
            this.value = value;
            this.visited = 0;
            this.right = null;
            this.down = null;
            this.left = null;
            this.up = null;
        }

        public GraphNode(int value, int visited)
        { // value, visited
            this.value = value;
            this.visited = visited;
            this.right = null;
            this.down = null;
            this.left = null;
            this.up = null;
        }

        public GraphNode(int value, int visited, GraphNode right, GraphNode down, GraphNode left, GraphNode up)
        { // value, visited, right, down, left, up
            this.value = value;
            this.visited = visited;
            this.right = right;
            this.down = down;
            this.left = left;
            this.up = up;
        }


        // Getter
        public int getValue()
        {
            return this.value;
        }
        public int getVisited()
        {
            return this.visited;
        }

        public GraphNode getRight()
        {
            return this.right;
        }

        public GraphNode getDown()
        {
            return this.down;
        }
        public GraphNode getLeft()
        {
            return this.left;
        }
        public GraphNode getUp()
        {
            return this.up;
        }

        // Setter
        public void setValue(int value)
        {
            this.value = value;
        }
        public void setVisited(int visited)
        {
            this.visited = visited;
        }
        public void setRight(GraphNode right)
        {
            this.right = right;
        }
        public void setDown(GraphNode down)
        {
            this.down = down;
        }
        public void setLeft(GraphNode left)
        {
            this.left = left;
        }
        public void setUp(GraphNode up)
        {
            this.up = up;
        }

        static void Main(string[] args)
        {
            GraphNode node1 = new GraphNode(1);
            GraphNode node2 = new GraphNode();
            GraphNode node3 = new GraphNode(3, 1);
            GraphNode node4 = new GraphNode(4, 0, node1, node2, node3, null);
            Console.WriteLine(node4.getValue());
            Console.WriteLine(node4.getVisited());
            Console.WriteLine(node4.getRight().getValue());
            Console.WriteLine(node4.getDown().getValue());
            Console.WriteLine(node4.getLeft().getValue());

        }
    }
}
