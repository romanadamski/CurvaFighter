using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    protected MovementSettingsSO movementSettings;

    private Vector2 _jumpAxis;
    private Rigidbody2D _rigidbody2D;
    private PlayerInputActions _playerInputActions;
    private Vector2 _movementAxis;
    private bool _isPerformingMovement;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        SubscribeToInputActions();
    }

    private void SubscribeToInputActions()
    {
        _playerInputActions.Player.Jump.performed += Jump_performed;
        _playerInputActions.Player.Jump.canceled += Jump_canceled;
        _playerInputActions.Player.Movement.performed += Movement_performed;
        _playerInputActions.Player.Movement.canceled += Movement_canceled;
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        _isPerformingMovement = true;
        var movement = context.ReadValue<Vector2>();
        _movementAxis = new Vector2(movement.x, _jumpAxis.y);
    }

    private void Movement_canceled(InputAction.CallbackContext context)
    {
        _isPerformingMovement = false;
        StopMovement();
    }

    private void FixedUpdate()
    {
        if (!_isPerformingMovement) return;

        _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, _movementAxis * movementSettings.MovementSpeed, Time.deltaTime * movementSettings.MovementPrecision);
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        _jumpAxis = new Vector2(0, movementSettings.JumpForce);
        _rigidbody2D.AddForce(_jumpAxis, ForceMode2D.Impulse);
    }

    private void Jump_canceled(InputAction.CallbackContext context)
    {
        _jumpAxis = Vector2.zero;
    }

    private void HandleMovement()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (_rigidbody2D.velocity != Vector2.zero)
        {
            
        }
    }

    private void OnDisable()
    {
        StopMovement();
    }

    private void StopMovement()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
    }
}
