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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCamera(Room room)
    {

        // Renderer 확인
        if (room != null)
        {
            SpriteRenderer roomRenderer = room.gameObject.GetComponent<SpriteRenderer>();


            Bounds roomBounds = roomRenderer.bounds;


            // 카메라 뷰의 절반 크기 (월드 단위)
            float halfHeight = camera.orthographic ? camera.orthographicSize : 0f;
            float halfWidth = halfHeight * camera.aspect;

            // 카메라 중심이 들어갈 수 있는 최소/최대값 (룸 바운즈를 기준으로 보정)
            float minX = roomBounds.min.x + halfWidth;
            float maxX = roomBounds.max.x - halfWidth;
            float minY = roomBounds.min.y + halfHeight;
            float maxY = roomBounds.max.y - halfHeight;

            // 원하는(따르고자 하는) 위치 — 보통 cameraTarget이 플레이어를 따라가도록 설정되어 있음
            Vector3 desired = player.transform.position;

            // 룸이 카메라보다 작아 min > max가 되는 경우 처리:
            // 이때는 해당 축은 룸의 중심으로 고정 (또는 다른 정책으로 변경 가능)
            float clampedX = (minX > maxX) ? roomBounds.center.x : Mathf.Clamp(desired.x, minX, maxX);
            float clampedY = (minY > maxY) ? roomBounds.center.y : Mathf.Clamp(desired.y, minY, maxY);

            Vector3 newTargetPos = new Vector3(clampedX, clampedY, cameraTarget.transform.position.z);
            cameraTarget.transform.position = newTargetPos;

            // 카메라 z는 유지 (예: -10)
            float cameraZ = camera.transform.position.z;
            camera.transform.position = new Vector3(clampedX, clampedY, cameraZ);



            /*Renderer roomRendere = room.gameObject.GetComponent<Renderer>();

            Bounds roomBounds = roomRendere.bounds;

            float limitX = Mathf.Clamp(room.transform.position.x, roomBounds.min.x, roomBounds.max.x);
            float limitY = Mathf.Clamp(room.transform.position.y, roomBounds.min.y, roomBounds.max.y);

            cameraTarget.transform.position = new Vector3(limitX, limitY,-10);
            Debug.Log($"{limitX},{limitY}");
            camera.transform.position = cameraTarget.transform.position;*/
        }
    }
}
