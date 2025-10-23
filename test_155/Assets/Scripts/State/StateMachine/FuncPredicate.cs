using System;

/// <summary>
/// 델리게이트 기반 조건
/// </summary>
public class FuncPredicate : IPredicate {
    private readonly Func<bool> _condition;

    public FuncPredicate(Func<bool> condition) {
        _condition = condition;
    }

    public bool Evaluate() => _condition();
}
