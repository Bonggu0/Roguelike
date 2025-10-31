using System;
using System.Collections;
using UnityEngine;

public class BattleManager  
{
    public bool IsBattle = false;

    public Cell currenCellData;


    //public static event Action OnEndBattle;

    void Start()
    {
        
    }
    
    public void StartBattle(Cell cellData)
    {
        //setting battle
        IsBattle = true;
        currenCellData = cellData;
    }

    public void UpdateBattle()
    {
        if (IsBattle)
        {
            if (currenCellData != null) return;
            if (currenCellData.enemyList.Count == 0)
            {
                EndBattle();
            }
            
        }
    }

    public void EndBattle()
    {
        IsBattle = false;
    }

    private bool IsBatleEnd()
    {
        if(IsBattle) return true;

        return false;
    }
}
