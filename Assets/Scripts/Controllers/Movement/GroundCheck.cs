using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Collider2D[] _results = new Collider2D[1];
    
    public bool IsGrounded { get; private set; }
    public Vector2 SurfacePosition { get; private set; }

    private void Update()
    {
        Vector2 point = transform.position;
        if (Physics2D.OverlapPointNonAlloc(point, _results) > 0)
        {
            IsGrounded = true;
            SurfacePosition = Physics2D.ClosestPoint(transform.position, _results[0]);
        }
        else
        {
            IsGrounded = false;
        }
    }

}
