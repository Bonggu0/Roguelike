using UnityEngine;


public class GridManager : MonoBehaviour
{
    public int width = 20;
    public int height = 12;
    public GameObject cellPrefab;
    public ACell[,] grid;


    void Start()
    {
        if (!cellPrefab) { Debug.LogError("cellPrefab 비어있음"); return; }
        GenerateGrid();
    }


    public void GenerateGrid()
    {
        grid = new ACell[width, height];
        var parent = new GameObject("Grid");
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var obj = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity, parent.transform);
                var c = obj.GetComponent<ACell>();
                c.SetPosition(x, y);
                c.SetWalkable(true);
                grid[x, y] = c;
            }
    }


    public bool InBounds(int x, int y) => x >= 0 && y >= 0 && x < width && y < height;
}