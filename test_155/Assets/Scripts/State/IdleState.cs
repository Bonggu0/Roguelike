using UnityEngine;

public class IdleState : IState
{
    private PlayerController _controller;

    public IdleState (PlayerController playerController)
    {
        _controller = playerController;
    }


    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        Debug.Log("Idle Enter");
        _controller.IsMove = false;

    }

    public void OnExit()
    {
        _controller.IsIdle = false;
    }

    public void Update()
    {
        Debug.Log("Idle Update");
    }
}
