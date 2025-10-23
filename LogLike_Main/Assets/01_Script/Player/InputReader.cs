using UnityEngine;
using UnityEngine.InputSystem;
using static InputSystem_Actions;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "PlayerInputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    private InputSystem_Actions _actions;

    public Vector2 Dir;

    public void Initialize()
    {
        _actions = new InputSystem_Actions();
        _actions.Player.SetCallbacks(this);

        _actions.Enable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
    }
    public void OnJump(InputAction.CallbackContext context)
    {
    }
    public void OnLook(InputAction.CallbackContext context)
    {
    }
    public void OnNext(InputAction.CallbackContext context)
    {
    }
    public void OnPrevious(InputAction.CallbackContext context)
    {
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Dir = context.ReadValue<Vector2>();
    }
    
}
