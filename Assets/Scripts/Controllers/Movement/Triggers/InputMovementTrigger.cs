using UnityEngine;

public class InputMovementTrigger : MovementTrigger
{
    private readonly string HORIZONTAL_AXIS_NAME = "Horizontal";
    private readonly string VERTICAL_AXIS_NAME = "Vertical";

    protected override void SetAxis()
    {
        XDirection = Input.GetAxis(HORIZONTAL_AXIS_NAME);
        YDirection = Input.GetAxis(VERTICAL_AXIS_NAME);
    }

    private void OnDisable()
    {
        Input.ResetInputAxes();
    }
}
