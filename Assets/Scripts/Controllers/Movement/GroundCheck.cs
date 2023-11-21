using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Collider2D[] _results = new Collider2D[1];
    
    public bool IsGrounded { get; private set; }

    private void Update()
    {
        Vector2 point = transform.position - Vector3.up * 0.2f;
        if (Physics2D.OverlapPointNonAlloc(point, _results) > 0)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

}
