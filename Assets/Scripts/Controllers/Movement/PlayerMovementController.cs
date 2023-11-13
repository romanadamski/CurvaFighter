using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private MovementSettingsSO movementSettings;
    [SerializeField]
    private InputReader inputReader;

    private Vector2 _jumpAxis;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementAxis;
    private bool _isPerformingMovement;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        SubscribeToInputActions();
    }

    private void SubscribeToInputActions()
    {
        inputReader.MovementEvent += HandleMovement;
        inputReader.MovementPerformedEvent += HandleMovementPerformed;
        inputReader.MovementCanceledEvent += HandleMovementCanceled;
        inputReader.MovementEvent += HandleMovement;
        inputReader.JumpPerformedEvent += HandleJumpPerformed;
        inputReader.JumpCanceledEvent += HandleJumpCanceled;
        inputReader.FirePerformedEvent += HandleFirePerformed;
        inputReader.FireCanceledEvent += HandleFireCanceled;
        inputReader.InventoryEvent += HandleInventory;
        inputReader.PauseEvent += HandlePause;
    }

    private void HandleMovementPerformed()
    {
        _isPerformingMovement = true;
    }

    private void HandleMovementCanceled()
    {
        _isPerformingMovement = false;
        StopMovement();
    }

    private void HandlePause()
    {
        Debug.Log("Pauza");
    }

    private void HandleInventory()
    {
        Debug.Log("Otwieram ekpifunek");
    }

    private void HandleFirePerformed()
    {
        Debug.Log("Zaczynam szczelać!");
    }

    private void HandleFireCanceled()
    {
        Debug.Log("Kończę szczelać!");
    }

    private void HandleJumpPerformed()
    {
        _jumpAxis = new Vector2(0, movementSettings.JumpForce);
        _rigidbody2D.AddForce(_jumpAxis/* * Mathf.Clamp(Mathf.Abs(_rigidbody2D.velocity.x), 1, Mathf.Abs(_rigidbody2D.velocity.x))*/, ForceMode2D.Impulse);
    }

    private void HandleJumpCanceled()
    {
        _jumpAxis = Vector2.zero;
    }

    private void HandleMovement(Vector2 value)
    {
        _movementAxis = value;
    }

    private void FixedUpdate()
    {
        if (!_isPerformingMovement) return;

        _rigidbody2D.AddForce( _movementAxis * movementSettings.MovementSpeed);
    }

    private void Update()
    {
        if (Mathf.Abs(_rigidbody2D.velocity.x) > movementSettings.MaxMovementSpeed)
        {
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, movementSettings.MaxMovementSpeed);
        }
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
