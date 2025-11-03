using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BattleManager
{
    public bool IsBattle = false;

    public Cell currenCellData;
    public Room room;

    public static event Action<Cell> OpenDoorAction;

    public bool CheckEnemy(Cell cellData)
    {
        currenCellData = cellData;
        if (cellData.enemyList.Count > 0)
        {
            return IsBattle = true;
        }
        else return IsBattle = false;
    }

    public void StartBattle(Cell cellData)
    {
        //setting battle
    }

    public void UpdateBattle()
    {
        if (IsBattle)
        {
            Debug.Log(currenCellData.enemyList.Count);
            if (currenCellData == null) return;

            if (currenCellData.enemyList.Count == 0)
            {
                Debug.Log(123);
                EndBattle();
            }
        }
    }
    public void EndBattle()
    {
        IsBattle = false;
        OpenDoorAction?.Invoke(currenCellData);
    }

    private bool IsBatleEnd()
    {
        if (IsBattle) return true;

        return false;
    }
}
