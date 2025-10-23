using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputReader InputReader;

    public bool IsIdle = false;
    public bool IsMove = false;

    public GameObject Obj;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        IsIdle = !Input.anyKey;
        IsMove = Input.anyKey;
    }
}
