using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum EdgeDirection
{
    Up,
    Down,
    Left,
    Right
}

public class Room : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public int Index = 0;

    public int RoomShapeIndex = 0;

    public void SetupRoom(Cell currentCell, RoomScriptable room)
    {
        spriteRenderer.sprite = room.roomVariations[Random.Range(0, room.roomVariations.Length)];

        if (currentCell.roomType == RoomType.Secret) return;

        var floorplan = MapGenerator.Instance.GetFloorPlan;
        var cellList = MapGenerator.Instance.GetSpawnedCells;

        switch (currentCell.roomShape)
        {
            case RoomShape.OneByOne:
                SetupOneByOne(currentCell, floorplan, cellList);
                SetCollider();
                break;

            case RoomShape.OneByTwo:
                SetupOneByTwo(currentCell, floorplan, cellList);
                SetCollider();
                break;

            case RoomShape.TwoByOne:
                SetupTwoByOne(currentCell, floorplan, cellList);
                SetCollider();
                break;

            case RoomShape.TwoByTwo:
                SetupTwoByTwo(currentCell, floorplan, cellList);
                SetCollider();
                break;

            case RoomShape.LShape:
                SetupLShapeRoom(currentCell, floorplan, cellList);
                SetCollider();
                break;

            default:
                break;
        }

    }
    private void SetCollider()
    {
        var sprite = GetComponent<SpriteRenderer>().sprite;
        var polygonCollider2D = GetComponent<EdgeCollider2D>();
        var pointsList = new List<Vector2>();
        sprite.GetPhysicsShape(0, pointsList);
        polygonCollider2D.points = pointsList.ToArray();
    }
    public void SetupOneByOne(Cell cell, int[] floorplan, List<Cell> cellList)
    {
        var currentCell = cell.cellList[0];

        TryPlaceDoor(currentCell, new Vector2(0, 1.75f), EdgeDirection.Up, floorplan, cellList, cell);
        TryPlaceDoor(currentCell, new Vector2(0, -1.75f), EdgeDirection.Down, floorplan, cellList, cell);
        TryPlaceDoor(currentCell, new Vector2(-4.25f, 0), EdgeDirection.Left, floorplan, cellList, cell);
        TryPlaceDoor(currentCell, new Vector2(4.25f, 0), EdgeDirection.Right, floorplan, cellList, cell);
    }

    public void SetupOneByTwo(Cell cell, int[] floorplan, List<Cell> cellList)
    {
        var cellA = cell.cellList[0];
        var cellB = cell.cellList[1];

        TryPlaceDoor(cellA, new Vector2(0f, 4f), EdgeDirection.Up, floorplan, cellList, cell);
        TryPlaceDoor(cellA, new Vector2(-4.25f, 2.6125f), EdgeDirection.Left, floorplan, cellList, cell);
        TryPlaceDoor(cellA, new Vector2(4.25f, 2.6125f), EdgeDirection.Right, floorplan, cellList, cell);

        TryPlaceDoor(cellB, new Vector2(0f, -4f), EdgeDirection.Down, floorplan, cellList, cell);
        TryPlaceDoor(cellB, new Vector2(-4.25f, -2.6125f), EdgeDirection.Left, floorplan, cellList, cell);
        TryPlaceDoor(cellB, new Vector2(4.25f, -2.6125f), EdgeDirection.Right, floorplan, cellList, cell);
    }

    public void SetupTwoByOne(Cell cell, int[] floorplan, List<Cell> cellList)
    {
        var cellA = cell.cellList[0];
        var cellB = cell.cellList[1];

        TryPlaceDoor(cellA, new Vector2(-5f, 1.5f), EdgeDirection.Up, floorplan, cellList, cell);
        TryPlaceDoor(cellA, new Vector2(-9.75f, 0f), EdgeDirection.Left, floorplan, cellList, cell);
        TryPlaceDoor(cellA, new Vector2(-5f, -1.5f), EdgeDirection.Down, floorplan, cellList, cell);

        TryPlaceDoor(cellB, new Vector2(5f, 1.5f), EdgeDirection.Up, floorplan, cellList, cell);
        TryPlaceDoor(cellB, new Vector2(5f, -1.5f), EdgeDirection.Down, floorplan, cellList, cell);
        TryPlaceDoor(cellB, new Vector2(9.75f, 0f), EdgeDirection.Right, floorplan, cellList, cell);
    }

    public void SetupTwoByTwo(Cell cell, int[] floorplan, List<Cell> cellList)
    {
        var cellA = cell.cellList[0];
        var cellB = cell.cellList[1];
        var cellC = cell.cellList[2];
        var cellD = cell.cellList[3];

        Debug.Log(cell.cellList[0]);

        TryPlaceDoor(cellA, new Vector2(-5.3125f, 4.5f), EdgeDirection.Up, floorplan, cellList, cell);
        TryPlaceDoor(cellB, new Vector2(5.3125f, 4.5f), EdgeDirection.Up, floorplan, cellList, cell);

        TryPlaceDoor(cellA, new Vector2(-9.75f, 2.6125f), EdgeDirection.Left, floorplan, cellList, cell);
        TryPlaceDoor(cellC, new Vector2(-9.75f, -2.6125f), EdgeDirection.Left, floorplan, cellList, cell);

        TryPlaceDoor(cellC, new Vector2(-5.3125f, -4.5f), EdgeDirection.Down, floorplan, cellList, cell);
        TryPlaceDoor(cellD, new Vector2(5.3125f, -4.5f), EdgeDirection.Down, floorplan, cellList, cell);

        TryPlaceDoor(cellB, new Vector2(9.75f, 2.6125f), EdgeDirection.Right, floorplan, cellList, cell);
        TryPlaceDoor(cellD, new Vector2(9.75f, -2.6125f), EdgeDirection.Right, floorplan, cellList, cell);
    }

    public void SetupLShapeRoom(Cell cell, int[] floorplan, List<Cell> cellList)
    {
        var cellA = cell.cellList[0];
        var cellB = cell.cellList[1];
        var cellC = cell.cellList[2];

        if (cellA + 1 == cellB && cellA + 10 == cellC)
        {
            TryPlaceDoor(cellA, new Vector2(-5.3125f, 4.5f), EdgeDirection.Up, floorplan, cellList, cell);
            TryPlaceDoor(cellA, new Vector2(-9.75f, 2.6125f), EdgeDirection.Left, floorplan, cellList, cell);

            TryPlaceDoor(cellB, new Vector2(5.3125f, 4.5f), EdgeDirection.Up, floorplan, cellList, cell);
            TryPlaceDoor(cellB, new Vector2(9.75f, 2.6375f), EdgeDirection.Right, floorplan, cellList, cell);
            TryPlaceDoor(cellB, new Vector2(5.3125f, 1f), EdgeDirection.Down, floorplan, cellList, cell);

            TryPlaceDoor(cellC, new Vector2(-5.3125f, -4.5f), EdgeDirection.Down, floorplan, cellList, cell);
            TryPlaceDoor(cellC, new Vector2(-1f, -2.6375f), EdgeDirection.Right, floorplan, cellList, cell);
            TryPlaceDoor(cellC, new Vector2(-9.75f, -2.6125f), EdgeDirection.Left, floorplan, cellList, cell);

            RoomShapeIndex = 1;
        }
        else if (cellA + 1 == cellB && cellB + 10 == cellC)
        {
            TryPlaceDoor(cellA, new Vector2(-5.3125f, 4.5f), EdgeDirection.Up, floorplan, cellList, cell);
            TryPlaceDoor(cellA, new Vector2(-9.75f, 2.6125f), EdgeDirection.Left, floorplan, cellList, cell);
            TryPlaceDoor(cellA, new Vector2(-5.3125f, 1f), EdgeDirection.Down, floorplan, cellList, cell);

            TryPlaceDoor(cellB, new Vector2(5.3125f, 4.5f), EdgeDirection.Up, floorplan, cellList, cell);
            TryPlaceDoor(cellB, new Vector2(9.75f, 2.6375f), EdgeDirection.Right, floorplan, cellList, cell);

            TryPlaceDoor(cellC, new Vector2(5.3125f, -4.5f), EdgeDirection.Down, floorplan, cellList, cell);
            TryPlaceDoor(cellC, new Vector2(9.75f, -2.6375f), EdgeDirection.Right, floorplan, cellList, cell);
            TryPlaceDoor(cellC, new Vector2(1, -2.6125f), EdgeDirection.Left, floorplan, cellList, cell);

            RoomShapeIndex = 2;
        }
        else if (cellA + 10 == cellB)
        {
            TryPlaceDoor(cellA, new Vector2(-5.3125f, 4.5f), EdgeDirection.Up, floorplan, cellList, cell);
            TryPlaceDoor(cellA, new Vector2(-9.75f, 2.6125f), EdgeDirection.Left, floorplan, cellList, cell);
            TryPlaceDoor(cellA, new Vector2(-1f, 2.6125f), EdgeDirection.Right, floorplan, cellList, cell);

            TryPlaceDoor(cellB, new Vector2(-5.3125f, -4.5f), EdgeDirection.Down, floorplan, cellList, cell);
            TryPlaceDoor(cellB, new Vector2(-9.75f, -2.6125f), EdgeDirection.Left, floorplan, cellList, cell);

            TryPlaceDoor(cellC, new Vector2(5.3125f, -1), EdgeDirection.Up, floorplan, cellList, cell);
            TryPlaceDoor(cellC, new Vector2(5.3125f, -4.5f), EdgeDirection.Down, floorplan, cellList, cell);
            TryPlaceDoor(cellC, new Vector2(9.75f, -2.6375f), EdgeDirection.Right, floorplan, cellList, cell);

            RoomShapeIndex = 3;
        }
        else if (cellA + 10 == cellC)
        {
            TryPlaceDoor(cellA, new Vector2(5.3125f, 4.5f), EdgeDirection.Up, floorplan, cellList, cell);
            TryPlaceDoor(cellA, new Vector2(1, 2.6125f), EdgeDirection.Left, floorplan, cellList, cell);
            TryPlaceDoor(cellA, new Vector2(9.75f, 2.6125f), EdgeDirection.Right, floorplan, cellList, cell);

            TryPlaceDoor(cellB, new Vector2(-5.3125f, -1f), EdgeDirection.Up, floorplan, cellList, cell);
            TryPlaceDoor(cellB, new Vector2(-5.3125f, -4.5f), EdgeDirection.Down, floorplan, cellList, cell);
            TryPlaceDoor(cellB, new Vector2(-9.75f, -2.6125f), EdgeDirection.Left, floorplan, cellList, cell);

            TryPlaceDoor(cellC, new Vector2(5.3125f, -4.5f), EdgeDirection.Down, floorplan, cellList, cell);
            TryPlaceDoor(cellC, new Vector2(9.75f, -2.6375f), EdgeDirection.Right, floorplan, cellList, cell);

            RoomShapeIndex = 4;
        }
    }

    private void TryPlaceDoor(int fromIndex, Vector2 positionOffset, EdgeDirection direction, int[] floorplan, List<Cell> cellList, Cell currentCell)
    {
        int neighbourIndex = fromIndex + GetOffset(direction);

        if (neighbourIndex < 0 || neighbourIndex >= floorplan.Length) return;

        if (floorplan[neighbourIndex] != 1) return;

        var foundCell = cellList.FirstOrDefault(x => x.cellList.Contains(neighbourIndex));

        if (foundCell.roomType == RoomType.Secret) return;

        var door = Instantiate(RoomManager.instance.doorPrefab, transform);

        door.transform.position = (Vector2)transform.position + positionOffset;

        door.Direction = direction;
        door.CurrentIndex = fromIndex;
        currentCell.doorList.Add(door);

        SetupDoor(door, direction, currentCell.roomType == RoomType.Regular ? foundCell.roomType : currentCell.roomType, currentCell.index);
    }



    private void SetupDoor(Door door, EdgeDirection direction, RoomType roomType,int bigRoomIndex)
    {
        var doorTypes = GetDoorOptions(roomType);

        switch (direction)
        {
            case EdgeDirection.Up:
                door.SetDoorObject(doorTypes.upDoor, direction, bigRoomIndex);
                break;
            case EdgeDirection.Down:
                door.SetDoorObject(doorTypes.downDoor, direction, bigRoomIndex);
                break;
            case EdgeDirection.Left:
                door.SetDoorObject(doorTypes.leftDoor, direction, bigRoomIndex);
                break;
            case EdgeDirection.Right:
                door.SetDoorObject(doorTypes.rightDoor, direction, bigRoomIndex);
                break;
            default:
                break;
        }
    }

    private DoorScriptable GetDoorOptions(RoomType roomType)
    {
        return RoomManager.instance.doors.FirstOrDefault(x => x.roomType == roomType);
    }

    private int GetOffset(EdgeDirection direction)
    {
        switch (direction)
        {
            case EdgeDirection.Up:
                return -10;
            case EdgeDirection.Down:
                return 10;
            case EdgeDirection.Right:
                return 1;
            case EdgeDirection.Left:
                return -1;
        }
        return 0;
    }
   
}
