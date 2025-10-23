using System.Collections.Generic;
using UnityEngine;


public class PathfinderVisualizer : MonoBehaviour
{
    public AStarPathfinder pathfinder;
    public GridManager gridManager;


    void Start()
    {
        if (!pathfinder) pathfinder = FindFirstObjectByType<AStarPathfinder>();
        if (!gridManager) gridManager = FindFirstObjectByType<GridManager>();
    }


    public void ShowPath((int x, int y) s, (int x, int y) g)
    {
        ClearColors();
        var path = pathfinder.FindPath(s, g);
        if (path == null) { Debug.Log("경로 없음"); return; }
        foreach (var n in path)
            gridManager.grid[n.x, n.y].SetColor(Color.yellow);
        gridManager.grid[s.x, s.y].SetColor(Color.green);
        gridManager.grid[g.x, g.y].SetColor(Color.red);
    }


    public void ClearColors()
    {
        for (int x = 0; x < gridManager.width; x++)
            for (int y = 0; y < gridManager.height; y++)
                gridManager.grid[x, y].SetColor(gridManager.grid[x, y].walkable ? Color.white : Color.black);
    }
}