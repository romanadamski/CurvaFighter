using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private MovementSettingsSO movementSettings;
    [SerializeField]
    private InputReader inputReader;

    private Animator _animator;
    private GroundCheck _groundCheck;
    private float _jumpVelocity;
    private Vector2 _movementAxis;
    private bool _isPerformingMovement;
    private bool _isJumping;
    private float _jumpStartHeight;

    private const string MOVE_FLAG = "IsMoving";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _groundCheck = GetComponent<GroundCheck>();
        
        SubscribeToInputActions();
    }

    private void SubscribeToInputActions()
    {
        inputReader.MovementEvent += Input_HandleMovement;
        inputReader.MovementPerformedEvent += Input_HandleMovementPerformed;
        inputReader.MovementCanceledEvent += Input_HandleMovementCanceled;
        inputReader.JumpPerformedEvent += Input_HandleJumpPerformed;
        inputReader.JumpCanceledEvent += Input_HandleJumpCanceled;
        inputReader.FirePerformedEvent += Input_HandleFirePerformed;
        inputReader.FireCanceledEvent += Input_HandleFireCanceled;
        inputReader.InventoryEvent += Input_HandleInventory;
        inputReader.PauseEvent += Input_HandlePause;
    }

    private void Update()
    {
        HandleJumpVelocity();
    }

    private void HandleJumpVelocity()
    {
        _jumpVelocity += movementSettings.Gravity * movementSettings.GravityScale * Time.deltaTime;
        if (_groundCheck.IsGrounded && _jumpVelocity < 0)
        {
            _jumpVelocity = 0;
            transform.position = new Vector3(transform.position.x, _groundCheck.SurfacePosition.y);
        }
        if (transform.position.y > _jumpStartHeight + movementSettings.MaxJumpHeight)
        {
            _isJumping = false;
        }
        if (_isJumping)
        {
            _jumpVelocity = movementSettings.JumpForce;
        }
    }

    private void FixedUpdate()
    {
        UpdateJumpingPosition();
        UpdateMovementPosition();
    }

    private void UpdateMovementPosition()
    {
        if (!_isPerformingMovement) return;

        transform.position += new Vector3(_movementAxis.x * movementSettings.MovementSpeed, 0) * Time.deltaTime;
    }

    private void UpdateJumpingPosition()
    {
        transform.position += new Vector3(0, _jumpVelocity) * Time.deltaTime;
    }

    private void Input_HandleJumpPerformed()
    {
        if (!_groundCheck.IsGrounded) return;

        _isJumping = true;
        _jumpStartHeight = transform.position.y;
    }

    private void Input_HandleJumpCanceled()
    {
        _isJumping = false;
    }

    private void Input_HandleMovement(Vector2 value)
    {
        _movementAxis = value;
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
