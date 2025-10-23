using System.Collections.Generic;
using UnityEngine;


public class AStarPathfinder : MonoBehaviour
{
    GridManager gridManager;


    void Awake() { gridManager = FindFirstObjectByType<GridManager>(); }


    ANode[,] BuildNodeMap()
    {
        var gm = gridManager;
        var nodes = new ANode[gm.width, gm.height];
        for (int x = 0; x < gm.width; x++)
            for (int y = 0; y < gm.height; y++)
                nodes[x, y] = new ANode(x, y, gm.grid[x, y].walkable);
        return nodes;
    }


    float Heuristic(ANode a, ANode b)
    {
        // Manhattan
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }


    public List<ANode> FindPath((int x, int y) start, (int x, int y) goal)
    {
        var gm = gridManager;
        if (!gm.InBounds(start.x, start.y) || !gm.InBounds(goal.x, goal.y)) return null;
        var nodes = BuildNodeMap();
        var startNode = nodes[start.x, start.y];
        var goalNode = nodes[goal.x, goal.y];
        if (!startNode.walkable || !goalNode.walkable) return null;


        var open = new PriorityQueue<ANode>();
        var openSet = new HashSet<ANode>();
        var closedSet = new HashSet<ANode>();
        startNode.g = 0;
        startNode.h = Heuristic(startNode, goalNode);
        open.Enqueue(startNode, startNode.F);
        openSet.Add(startNode);


        int[,] dirs = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };


        while (open.Count > 0)
        {
            var current = open.Dequeue();
            openSet.Remove(current);
            if (current.x == goalNode.x && current.y == goalNode.y) return ReconstructPath(current);


            closedSet.Add(current);
            for (int i = 0; i < 4; i++)
            {
                int nx = current.x + dirs[i, 0];
                int ny = current.y + dirs[i, 1];
                if (!gm.InBounds(nx, ny)) continue;
                var neighbor = nodes[nx, ny];
                if (!neighbor.walkable || closedSet.Contains(neighbor)) continue;


                float tentativeG = current.g + 1; // cost = 1 for grid
                bool better = false;
                if (!openSet.Contains(neighbor))
                {
                    neighbor.h = Heuristic(neighbor, goalNode);
                    openSet.Add(neighbor);
                    open.Enqueue(neighbor, tentativeG + neighbor.h);
                    better = true;
                }
                else if (tentativeG < neighbor.g)
                {
                    better = true;
                }


                if (better)
                {
                    neighbor.parent = current;
                    neighbor.g = tentativeG;
                }
            }
        }
        return null; // 경로 없음
    }


    List<ANode> ReconstructPath(ANode end)
    {
        var list = new List<ANode>();
        var cur = end;
        while (cur != null)
        {
            list.Add(cur);
            cur = cur.parent;
        }
        list.Reverse();
        return list;
    }
}