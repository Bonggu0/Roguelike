using System;
using System.Collections.Generic;

/// <summary>
/// 단순화된 스테이트 머신.
/// 상태 전환 관리와 Update 루프 실행을 담당한다.
/// </summary>
public class StateMachine {
    private IState _currentState;
    private readonly Dictionary<string, IState> _states = new();
    private readonly List<ITransition> _globalTransitions = new();

    public void Update() {
        var transition = CheckTransitions();
        if (transition != null) {
            SwitchState(transition.Target);
        }

        _currentState?.Update();
    }

    public void FixedUpdate() {
        _currentState?.FixedUpdate();
    }

    /// <summary>
    /// 스테이트 등록
    /// </summary>
    public void AddState(string key, IState state) {
        if (!_states.ContainsKey(key)) {
            _states.Add(key, state);
        }
    }

    /// <summary>
    /// 전역적으로 항상 검사되는 전이 추가
    /// </summary>
    public void AddGlobalTransition(ITransition transition) {
        _globalTransitions.Add(transition);
    }

    /// <summary>
    /// 첫 상태 설정
    /// </summary>
    public void SetInitialState(string key) {
        if (_states.TryGetValue(key, out var state)) {
            _currentState = state;
            _currentState?.OnEnter();
        }
    }

    /// <summary>
    /// 상태 변경
    /// </summary>
    private void SwitchState(IState newState) {
        if (newState == _currentState) return;

        _currentState?.OnExit();
        _currentState = newState;
        _currentState?.OnEnter();
    }

    /// <summary>
    /// 전이 검사 (현재 상태 + 전역 전이)
    /// </summary>
    private ITransition CheckTransitions() {
        foreach (var transition in _globalTransitions) {
            if (transition.Condition.Evaluate()) {
                return transition;
            }
        }

        if (_currentState is IHasTransitions hasTransitions) {
            foreach (var t in hasTransitions.Transitions) {
                if (t.Condition.Evaluate()) {
                    return t;
                }
            }
        }

        return null;
    }
}

/// <summary>
/// 전이를 가지는 상태에만 구현하도록 한 확장 인터페이스
/// </summary>
public interface IHasTransitions {
    IEnumerable<ITransition> Transitions { get; }
}
