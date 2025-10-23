using UnityEngine;

public class MovementStateMachine : MonoBehaviour
{
    [SerializeField]
    PlayerController _controller;

    StateMachine _stateMachine;

    private IdleState _idleState;
    private MoveState _moveState;

    private Transition _moveToIdleTransition;
    private Transition _idleToMoveTransition;

    void Start()
    {
        _stateMachine = new StateMachine();

        _idleState = new IdleState(_controller);
        _moveState = new MoveState(_controller);


        //======================================


        _stateMachine.AddState("Move", _moveState);
        _stateMachine.AddState("Idle", _idleState);

        _moveToIdleTransition = new Transition(_idleState, new MoveToIdlePredicate(_controller));
        _idleToMoveTransition = new Transition(_moveState, new IdleToMovePredicate(_controller));


        _stateMachine.AddGlobalTransition(_moveToIdleTransition);
        _stateMachine.AddGlobalTransition(_idleToMoveTransition);

        _stateMachine.SetInitialState("Move");

    }

    void Update()
    {
        Debug.Log("stateMachine update");
        _stateMachine.Update();
    }
}
