using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static float axisHorizontal = 0f;
    public static float axisVertical = 0f;

    public static float axisCamera = 0f;

    public static bool jump = false;
    public static bool bounce = false;

    public static bool grip = false;
    public static bool slid = false;

    void Update()
    {
        InputCollect();
    }
    private void FixedUpdate()
    {
        InputCollect();
    }
    private void LateUpdate()
    {
        InputCollect();
    }

    private void InputCollect()
    {
        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical = Input.GetAxis("Vertical");

        axisCamera = Input.GetAxis("CameraAxis");

        jump = Input.GetButtonDown("Jump");
        bounce = Input.GetButton("Jump");

        grip = Input.GetButton("Grip");
        slid = Input.GetButton("Slid");
        
    }
}
