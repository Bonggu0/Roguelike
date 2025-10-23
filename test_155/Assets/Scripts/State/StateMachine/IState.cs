/// <summary>
/// 모든 상태가 구현해야 하는 기본 인터페이스
/// </summary>
public interface IState {
    void OnEnter();
    void Update();
    void FixedUpdate();
    void OnExit();
}
