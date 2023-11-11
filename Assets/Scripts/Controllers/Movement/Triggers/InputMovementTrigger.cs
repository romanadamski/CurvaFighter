using UnityEngine;

public class InputMovementTrigger : MovementTrigger
{
    private readonly string HORIZONTAL_AXIS_NAME = "Horizontal";

    protected override void SetAxis()
    {
        XDirection = Input.GetAxis(HORIZONTAL_AXIS_NAME);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            YDirection = 1;
        }
    }

    private void OnDisable()
    {
        Input.ResetInputAxes();
    }
}
