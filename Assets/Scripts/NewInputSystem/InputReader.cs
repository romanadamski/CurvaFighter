using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/Input Reader")]
public class InputReader : ScriptableObject, GameplayInputActions.IGameplayActions, GameplayInputActions.IUIActions
{
    private GameplayInputActions _inputActions;

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new GameplayInputActions();

            _inputActions.Gameplay.SetCallbacks(this);
            _inputActions.UI.SetCallbacks(this);

            SetGameplay();
        }
    }

    public void SetGameplay()
    {
        _inputActions.Gameplay.Enable();
        _inputActions.UI.Disable();
    }

    public void SetUI()
    {
        _inputActions.UI.Enable();
        _inputActions.Gameplay.Disable();
    }

    public event Action<Vector2> MovementEvent;
    public event Action MovementPerformedEvent;
    public event Action MovementCanceledEvent;

    public event Action JumpPerformedEvent;
    public event Action JumpCanceledEvent;

    public event Action FirePerformedEvent;
    public event Action FireCanceledEvent;
    public event Action InventoryEvent;
    public event Action PauseEvent;

    public event Action ResumeEvent;
    public event Action SubmitEvent;


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            FirePerformedEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            FireCanceledEvent?.Invoke();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InventoryEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpPerformedEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            JumpCanceledEvent?.Invoke();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
        if (context.phase == InputActionPhase.Performed)
        {
            MovementPerformedEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            MovementCanceledEvent?.Invoke();
        }
    }


    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SubmitEvent?.Invoke();
        }
    }
}
