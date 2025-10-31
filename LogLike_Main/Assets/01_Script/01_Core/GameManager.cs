using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private  CameraMovement camareMove;
    [SerializeField]
    private RoomManager roomManager;

    BattleManager battleManager;

    private int currentCelliIndex = 45;

    public Room curRoom;

    //레벨 정보 나중에 넣기

    void Awake()
    {
        battleManager = new BattleManager();
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            battleManager.EndBattle();
        }

        if (battleManager.IsBattle)
        {
            battleManager.UpdateBattle();
            Debug.Log("battle ing");
        }
        else
        {
            Debug.Log("battle end");
            Cell curCell = mapGenerator.GetSpawnedCells.FirstOrDefault(c => c.index == currentCelliIndex);

            foreach (var door in curCell.doorList)
            {
                door.OpenDoor();
            }
        }

        camareMove.UpdateCamera(curRoom);

    }

    private void OnEnable()
    {
        Door.OnPlayerMovedThroughDoor += MoveRoom;
    }

    private void OnDisable()
    {
        Door.OnPlayerMovedThroughDoor -= MoveRoom;
    }

    private void MoveRoom(int currenIndex)
    {
        currentCelliIndex = currenIndex;

        Cell curCell = mapGenerator.GetSpawnedCells.FirstOrDefault(c => c.index == currentCelliIndex);

        foreach (var door in curCell.doorList)
        {
            door.CloseDoor();
        }

        if(curCell.HaveMonster == true)
        {
            battleManager.StartBattle(curCell);
        }

        curRoom = roomManager.CreatedRooms.FirstOrDefault(c => c.Index == currentCelliIndex);
    }

    private void EnterDungeon()
    {
        MapGenerate();
    }

    private void MapGenerate()
    {
        mapGenerator.SetupDungeon();
    }
}
