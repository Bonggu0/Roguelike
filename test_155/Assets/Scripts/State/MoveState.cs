using UnityEngine;

public class MoveState : IState
{
    private PlayerController _controller;

    public MoveState(PlayerController playerController)
    {
        _controller = playerController;
    }


    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        Debug.Log("move Enter");
        _controller.IsIdle = false;
        Debug.Log(_controller.IsIdle);

    }

    public void OnExit()
    {
        _controller.IsMove = false;
    }

    public void Update()
    {
        _controller.Obj.transform.Translate(_controller.InputReader.Direction * Time.deltaTime * 5);
        Debug.Log("move Update");
    }
}
