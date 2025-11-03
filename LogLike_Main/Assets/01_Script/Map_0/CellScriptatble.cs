using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Scriptable Objects/Cell")]
public class CellScriptatble : ScriptableObject
{
    public List<Enemy> EnemyList;
}
