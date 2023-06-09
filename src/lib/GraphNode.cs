using System;
namespace lib
{
    public class GraphNode
    {
        // Field
        private Coordinate position;
        private int visited;

        private bool treasure;

        private bool intersection;
        private GraphNode right;
        private GraphNode down;
        private GraphNode left;
        private GraphNode up;

        // Constructor
        public GraphNode()
        { // default
            this.position = null;
            this.visited = 0;
            this.treasure = false;
            this.intersection = false;
            this.right = null;
            this.down = null;
            this.left = null;
            this.up = null;
        }

        public GraphNode(Coordinate position)
        { // position
            this.position = position;
            this.visited = 0;
            this.treasure = false;
            this.intersection = false;
            this.right = null;
            this.down = null;
            this.left = null;
            this.up = null;
        }

        public GraphNode(Coordinate position, int visited, bool treasure)
        { // position, visited
            this.position = position;
            this.visited = visited;
            this.treasure = treasure;
            this.intersection = false;
            this.right = null;
            this.down = null;
            this.left = null;
            this.up = null;
        }

        public GraphNode(Coordinate position, int visited, bool treasure, GraphNode right, GraphNode down, GraphNode left, GraphNode up)
        { // position, visited, right, down, left, up
            this.position = position;
            this.visited = visited;
            this.treasure = treasure;
            this.intersection = false;
            this.right = right;
            this.down = down;
            this.left = left;
            this.up = up;
        }


        // Getter
        public Coordinate getCoordinate()
        {
            return this.position;
        }
        public int getVisited()
        {
            return this.visited;
        }

        public bool isTreasure()
        {
            return this.treasure;
        }

        public bool isIntersection()
        {
            return this.intersection;
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
        public void setValue(Coordinate position)
        {
            this.position = position;
        }
        public void setVisited(int visited)
        {
            this.visited = visited;
        }

        public void setTreasure(bool treasure)
        {
            this.treasure = treasure;
        }

        public void setIntersection(bool intersection)
        {
            this.intersection = intersection;
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

        public bool isNeighbourExist()
        {
            return (this.right != null || this.down != null || this.left != null || this.up != null);
        }

        public bool isNeighbourhood(GraphNode node)
        {
            return (this.right == node || this.down == node || this.left == node || this.up == node);
        }
        public bool isNeighbourVisitAbleExist()
        {
            if (this.isNeighbourExist())
            {
                if (this.right != null)
                {
                    if (this.right.visited == 0)
                    {
                        return true;
                    }
                }
                if (this.down != null)
                {
                    if (this.down.visited == 0)
                    {
                        return true;
                    }
                }
                if (this.left != null)
                {
                    if (this.left.visited == 0)
                    {
                        return true;
                    }
                }
                if (this.up != null)
                {
                    if (this.up.visited == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

    }
}
