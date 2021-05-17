using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static float axisHorizontal = 0f;
    public static float axisVertical = 0f;

    public static float axisCamera = 0f;

    public static bool jump = false;
    public static bool bounce = false;
    public static bool gravity = false;

    public static bool grip = false;
    public static bool slid = false;

    [Header("Input Check")]
    [SerializeField] float vertInput;
    [SerializeField] float horiInput;
    [Space(10)]
    [SerializeField] float horiCamInput;
    [Space(10)]
    [SerializeField] bool jumpInput;
    [SerializeField] bool bounceInput;
    [Space(10)]
    [SerializeField] bool slidInput;
    [SerializeField] bool gripInput;

    void Update()
    {
        InputCollect();
        ShowInput();
    }
    private void FixedUpdate()
    {
        InputCollect();
        ShowInput();
    }
    private void LateUpdate()
    {
        InputCollect();
        ShowInput();
    }

    private void InputCollect()
    {
        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical = Input.GetAxis("Vertical");

        axisCamera = Input.GetAxis("CameraAxis");

        jump = Input.GetButton("Jump");
        bounce = Input.GetButton("Bounce");
        gravity = Input.GetButton("Gravity");

        grip = Input.GetButton("Grip");
        slid = Input.GetButton("Slid");
        
    }

    void ShowInput()
    {
        vertInput = InputHandler.axisVertical;
        horiInput = InputHandler.axisHorizontal;
        jumpInput = InputHandler.jump;
        bounceInput = InputHandler.bounce;
        slidInput = InputHandler.slid;
        gripInput = InputHandler.grip;

        horiCamInput = InputHandler.axisCamera;
    }
}
