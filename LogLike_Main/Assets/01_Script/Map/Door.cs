using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float moveRoomOffset = 4f;
    public SpriteRenderer SpriteRenderer;

    public bool IsOpen = true;

    public EdgeDirection Direction;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Debug.Log(Direction.ToString());

        switch (Direction)
        {
            case EdgeDirection.Up:
                collision.transform.Translate(0, moveRoomOffset, 0);
                break;
            case EdgeDirection.Down:
                collision.transform.Translate(0, -moveRoomOffset, 0);
                break;
            case EdgeDirection.Left:
                collision.transform.Translate(-moveRoomOffset, 0, 0);
                break;
            case EdgeDirection.Right:
                collision.transform.Translate(moveRoomOffset, 0, 0);
                break;

            default:
                break;
        }
    }

    public void SetDoorObject(Sprite door,EdgeDirection dir)
    {
        SpriteRenderer.sprite = door;
        SetCollider();
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

    }

    //end battel,etc...
    public void OpenDoor()
    {

    }
}
