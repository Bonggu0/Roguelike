public class MoveToIdlePredicate : IPredicate
{
    PlayerController _playerController;

    public MoveToIdlePredicate(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public bool Evaluate()
    {
        return _playerController.IsIdle;
    }
}

public class IdleToMovePredicate : IPredicate
{
    PlayerController _playerController;

    public IdleToMovePredicate(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public bool Evaluate()
    {
        return _playerController.IsMove;
    }
}