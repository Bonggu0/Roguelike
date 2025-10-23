using UnityEngine;


public class InputHandler : MonoBehaviour
{
    public GridManager gridManager;
    public PathfinderVisualizer visualizer;
    public Vector2Int startPos = new Vector2Int(-1, -1);
    public Vector2Int goalPos = new Vector2Int(-1, -1);


    void Start()
    {
        if (!gridManager) gridManager = FindFirstObjectByType<GridManager>();
        if (!visualizer) visualizer = FindFirstObjectByType<PathfinderVisualizer>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 왼클릭: 토글 벽
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(wp.x);
            int y = Mathf.RoundToInt(wp.y);
            if (!gridManager.InBounds(x, y)) return;
            var c = gridManager.grid[x, y];
            c.SetWalkable(!c.walkable);
        }


        if (Input.GetKeyDown(KeyCode.S)) // S: 시작 지정 (마우스 위치)
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(wp.x);
            int y = Mathf.RoundToInt(wp.y);
            if (!gridManager.InBounds(x, y)) return;
            startPos = new Vector2Int(x, y);
            Debug.Log("Start set: " + startPos);
        }


        if (Input.GetKeyDown(KeyCode.G)) // G: 목표 지정
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(wp.x);
            int y = Mathf.RoundToInt(wp.y);
            if (!gridManager.InBounds(x, y)) return;
            goalPos = new Vector2Int(x, y);
            Debug.Log("Goal set: " + goalPos);
        }


        if (Input.GetKeyDown(KeyCode.Space)) // Space: 경로 시각화
        {
            if (startPos.x < 0 || goalPos.x < 0) { Debug.Log("Start/Goal 미지정"); return; }
            visualizer.ShowPath((startPos.x, startPos.y), (goalPos.x, goalPos.y));
        }


        if (Input.GetKeyDown(KeyCode.C)) visualizer.ClearColors();
    }
}