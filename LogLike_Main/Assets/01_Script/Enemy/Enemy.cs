using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptable Data;

    public event Action<Enemy> EnemyDead;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyDead?.Invoke(this.gameObject.GetComponent<Enemy>());
            Debug.Log("bonk");
            Destroy(gameObject);
        }
    }
   
}
