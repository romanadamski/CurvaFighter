using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(SpriteRenderer))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private MovementSettingsSO movementSettings;
    [SerializeField]
    private InputReader inputReader;

    private CharacterController _characterController;
    private Animator _animator;
    private Vector2 _jumpAxis;
    private Vector2 _movementAxis;
    private bool _isPerformingMovement;

    private const string MOVE_FLAG = "IsMoving";

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        
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

    private void FixedUpdate()
    {
        if (!_isPerformingMovement) return;

        _characterController.Move(_movementAxis * Time.deltaTime * movementSettings.MovementSpeed);
    }

    private void Update()
    {
        HandleGravity();
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            _movementAxis.y = -.5f;
        }
        else
        {
            _movementAxis.y += -9.8f;
        }
    }

    private void HandleMovement(Vector2 value)
    {
        _movementAxis = value;
    }

    private void HandleMovementPerformed(Vector2 value)
    {
        _isPerformingMovement = true;
        _animator.SetBool(MOVE_FLAG, true);
        Rotate(value);
    }

    private void HandleMovementCanceled()
    {
        _isPerformingMovement = false;
        _animator.SetBool(MOVE_FLAG, false);
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
    }

    private void HandleJumpCanceled()
    {
        _jumpAxis = Vector2.zero;
    }

    private void Rotate(Vector2 value)
    {
        if (value.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (value.x < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
