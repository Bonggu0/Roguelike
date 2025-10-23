public class ANode
{
    public int x, y;
    public bool walkable;
    public ANode parent;
    public float g, h;
    public float F => g + h;


    public ANode(int x, int y, bool walkable)
    {
        this.x = x; this.y = y; this.walkable = walkable;
    }
}