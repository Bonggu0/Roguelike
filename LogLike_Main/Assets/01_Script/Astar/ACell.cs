using UnityEngine;

public class ACell : MonoBehaviour
{
    public int x, y;
    public bool walkable = true;
    SpriteRenderer sr;


    void Awake() { sr = GetComponent<SpriteRenderer>(); }


    public void SetPosition(int x, int y)
    {
        this.x = x; this.y = y;
        gameObject.name = $"Cell_{x}_{y}";
    }


    public void SetWalkable(bool w)
    {
        walkable = w;
        sr.color = walkable ? Color.white : Color.black;
    }


    public void SetColor(Color c) => sr.color = c;
}
