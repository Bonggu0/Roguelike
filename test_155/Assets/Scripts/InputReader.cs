using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public class InputReader : MonoBehaviour , IPlayerActions
{
    private PlayerInputActions _inputActions;

    public Vector2 Direction = Vector2.zero;

    private void Awake()
    {
       _inputActions = new PlayerInputActions();
    }

   
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(123);
            Direction = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log(456);
            Direction = Vector2.zero;
        }
    }
}
