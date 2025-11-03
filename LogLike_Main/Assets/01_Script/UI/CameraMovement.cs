using UnityEngine;
using static UnityEngine.GridBrushBase;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject cameraTarget;

    void Start()
    {

    }

    void Update()
    {

    }

    public void UpdateCamera(Room room)
    {

        if (room != null)
        {
            SpriteRenderer roomRenderer = room.gameObject.GetComponent<SpriteRenderer>();


            Bounds roomBounds = roomRenderer.bounds;


            float halfHeight = camera.orthographic ? camera.orthographicSize : 0f;
            float halfWidth = halfHeight * camera.aspect;

            float minX = roomBounds.min.x + halfWidth;
            float maxX = roomBounds.max.x - halfWidth;
            float minY = roomBounds.min.y + halfHeight;
            float maxY = roomBounds.max.y - halfHeight;

            Vector3 desired = player.transform.position;

            float clampedX = (minX > maxX) ? roomBounds.center.x : Mathf.Clamp(desired.x, minX, maxX);
            float clampedY = (minY > maxY) ? roomBounds.center.y : Mathf.Clamp(desired.y, minY, maxY);

            Vector3 newTargetPos = new Vector3(clampedX, clampedY, cameraTarget.transform.position.z);
            cameraTarget.transform.position = newTargetPos;

            float cameraZ = camera.transform.position.z;
            camera.transform.position = new Vector3(clampedX, clampedY, cameraZ);



            
        }
    }
}
