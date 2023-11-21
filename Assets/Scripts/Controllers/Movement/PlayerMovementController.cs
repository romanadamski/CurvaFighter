using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private MovementSettingsSO movementSettings;
    [SerializeField]
    private InputReader inputReader;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private GroundCheck _groundCheck;

    private Vector2 _movementVelocity;
    private bool _isPerformingMovement;
    private float _defaultGravityScale;

    private const string MOVE_FLAG = "IsMoving";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<GroundCheck>();

        _defaultGravityScale = _rigidbody2D.gravityScale;

        SubscribeToInputActions();
    }

    private void SubscribeToInputActions()
    {
        inputReader.MovementEvent += Input_HandleMovement;
        inputReader.MovementPerformedEvent += Input_HandleMovementPerformed;
        inputReader.MovementCanceledEvent += Input_HandleMovementCanceled;
        inputReader.JumpPerformedEvent += Input_HandleJumpPerformed;
        inputReader.FirePerformedEvent += Input_HandleFirePerformed;
        inputReader.FireCanceledEvent += Input_HandleFireCanceled;
        inputReader.InventoryEvent += Input_HandleInventory;
        inputReader.PauseEvent += Input_HandlePause;
    }

    private void Update()
    {
        if (_groundCheck.IsGrounded)
        {
            _rigidbody2D.gravityScale = _defaultGravityScale;
        }

        if (!_groundCheck.IsGrounded && _rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.gravityScale = movementSettings.LandingForce;
        }
    }

    private void FixedUpdate()
    {
        UpdateMovementPosition();
    }

    private void UpdateMovementPosition()
    {
        if (!_isPerformingMovement) return;

        _rigidbody2D.velocity = Vector3.Lerp(_rigidbody2D.velocity, new Vector3(_movementVelocity.x * movementSettings.MovementSpeed, 0), Time.deltaTime * movementSettings.MovementPrecision);
    }

    private void Input_HandleJumpPerformed()
    {
        if (!_groundCheck.IsGrounded) return;

        _rigidbody2D.gravityScale = _defaultGravityScale;
        _rigidbody2D.velocity = new Vector2(0, movementSettings.JumpForce + movementSettings.JumpForce * Mathf.Abs(_rigidbody2D.velocity.x));
    }

    private void Input_HandleMovement(Vector2 value)
    {
        _movementVelocity = value;
    }

    private void Input_HandleMovementPerformed(Vector2 value)
    {
        _isPerformingMovement = true;
        _animator.SetBool(MOVE_FLAG, true);
        Rotate(value);
    }

    private void Input_HandleMovementCanceled()
    {
        _isPerformingMovement = false;
        _animator.SetBool(MOVE_FLAG, false);
        StopMovement();
    }

    private void StopMovement()
    {
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.angularVelocity = 0;
    }

    private void Input_HandlePause()
    {
        Debug.Log("Pauza");
    }

    private void Input_HandleInventory()
    {
        Debug.Log("Otwieram ekpifunek");
    }

    private void Input_HandleFirePerformed()
    {
        Debug.Log("Zaczynam szczelać!");
    }

    private void Input_HandleFireCanceled()
    {
        Debug.Log("Kończę szczelać!");
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
