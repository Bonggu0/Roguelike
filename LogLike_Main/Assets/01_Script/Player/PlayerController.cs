using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputReader InputReader;
    public GameObject Player;
    public Rigidbody2D Rigidbody;
    public float Speed;
   
    void Awake()
    {
        InputReader.Initialize();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        //Player.transform.Translate(InputReader.Dir *Time.deltaTime* Speed);
        //Rigidbody.AddForce(InputReader.Dir * Time.deltaTime * Speed, ForceMode2D.Force);
        Rigidbody.linearVelocity = InputReader.Dir * Speed;
    }
}
