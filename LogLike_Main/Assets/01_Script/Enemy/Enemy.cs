using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptable Data;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
   
    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void Update()
    {
            
    }

    //���� Ž���� �ִϸ��̼� ��¼�� ��¼��
}
