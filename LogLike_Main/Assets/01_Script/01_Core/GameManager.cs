using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CameraMovement camareMove;
    [SerializeField]
    private RoomManager roomManager;

    private BattleManager battleManager;

    private int currentCellIndex = 45;

    private Cell curCell;
    private Room curRoom;


    //레벨 정보 나중에 넣기

    void Awake()
    {
        battleManager = new BattleManager();
        roomManager.Initialization();
        mapGenerator.Initialization();
    }


    void Update()
    {
        camareMove.UpdateCamera(curRoom);

        if (Input.GetKeyDown(KeyCode.E))
        {
            battleManager.EndBattle();
        }

        if (battleManager.IsBattle) battleManager.UpdateBattle();
        


    }

    private void OnEnable()
    {
        Door.OnPlayerMovedThroughDoor += MoveRoom;
    }

    private void OnDisable()
    {
        Door.OnPlayerMovedThroughDoor -= MoveRoom;
    }

    private void Initiate()
    {
        MapGenerate();
    }
    public void MoveRoom(int index)
    {
        currentCellIndex = index;

        Cell cell = mapGenerator.GetSpawnedCells.FirstOrDefault(c => c.index == currentCellIndex);

        //curCell = cell;

        curRoom = roomManager.CreatedRooms.FirstOrDefault(c => c.Index == currentCellIndex);

        roomManager.ActivateRoom(cell, battleManager.CheckEnemy(cell));
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
