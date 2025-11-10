using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cell", menuName = "Scriptable Objects/Cell")]
public class CellScriptatble : ScriptableObject
{
    public List<Enemy> EnemyList;
}
