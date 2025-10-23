/// <summary>
/// 기본 전이 구현체
/// </summary>
public class Transition : ITransition {
    public IState Target { get; }
    public IPredicate Condition { get; }

    public Transition(IState target, IPredicate condition) {
        Target = target;
        Condition = condition;
    }
}
