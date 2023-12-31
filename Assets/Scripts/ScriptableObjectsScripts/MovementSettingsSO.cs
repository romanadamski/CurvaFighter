using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player Movement Settings", fileName = "PlayerMovementSettings")]
public class MovementSettingsSO : ScriptableObject
{
    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed => movementSpeed;

    [SerializeField]
    private float rotationSpeed;
    public float RotationSpeed => rotationSpeed;

    [SerializeField]
    private float movementPrecision;
    public float MovementPrecision => movementPrecision;

    [SerializeField]
    private float jumpForce;
    public float JumpForce => jumpForce;

    [SerializeField]
    private float landingForce;
    public float LandingForce => landingForce;
}
