using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    private static readonly Dictionary<EdgeDirection, Vector3> DirectionOffsets = new()
    {
        { EdgeDirection.Up, Vector2.up },
        { EdgeDirection.Down, Vector2.down },
        { EdgeDirection.Left, Vector2.left },
        { EdgeDirection.Right, Vector2.right }
    };

    public float moveRoomOffset = 4f;
    public SpriteRenderer SpriteRenderer;
    private EdgeCollider2D col;

    public bool IsOpen = true;

    public EdgeDirection Direction;
    public int CurrentIndex;
    public int CurBigRoomIndex;
    public int NextCellIndex;

    public Door NextDoor;

    public static event Action<int> OnPlayerMovedThroughDoor;

    private void Start()
    {
        col = GetComponent<EdgeCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        collision.transform.position = NextDoor.transform.position + DirectionOffsets[Direction] * 0.5f;

        OnPlayerMovedThroughDoor?.Invoke(NextDoor.CurBigRoomIndex);
    }

    public void SetDoorObject(Sprite door,EdgeDirection dir,int bigRoomIndex)
    {
        SpriteRenderer.sprite = door;
        SetCollider();
        CurBigRoomIndex = bigRoomIndex;
    }
    private void SetCollider()
    {
        var sprite = GetComponent<SpriteRenderer>().sprite;
        var polygonCollider2D = GetComponent<EdgeCollider2D>();
        var pointsList = new List<Vector2>();
        sprite.GetPhysicsShape(0, pointsList);
        polygonCollider2D.points = pointsList.ToArray();
    }
    //start battle
    public void CloseDoor()
    {
        SpriteRenderer.color = Color.black;
        col.isTrigger = true;


    }

    //end battel,etc...
    public void OpenDoor()
    {
        SpriteRenderer.color = Color.white;
        col.isTrigger = false;
    }
}
