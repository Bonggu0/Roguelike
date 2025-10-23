/// <summary>
/// 상태 전환을 정의하는 인터페이스
/// </summary>
public interface ITransition {
    IState Target { get; }
    IPredicate Condition { get; }
}
