
public class BaseBulletMovementController : BaseMovementController
{
    protected override void HandleMovement()
    {
        _rigidbody2D.velocity = MovementTrigger.MovementDirection * movementSettings.MovementSpeed;
    }
}
