using UnityEngine;

public class MarbleController : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Transform cam;

    [SerializeField]
    private Transform sphere;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PhysicMaterial phyMat;
    #endregion

    [SerializeField]
    private Vector3 cam2sphere;

    [SerializeField] float vertInput;
    [SerializeField] float horiInput;

    [SerializeField] bool jumpInput;
    [SerializeField] bool slidInput;
    [SerializeField] bool gripInput;

    public LayerMask layer;

    [SerializeField] float movingForce = 20f;

    Vector3 planNormal;
    Vector3 forwardVector;
    Vector3 rightVector;

    private void Update()
    {
        InputGetting();
        UpdateAndDebug();

    }

    void FixedUpdate()
    {
        if(Mathf.Abs(vertInput) > 0.1f)
        {
            if (gripInput)
            {
                rb.AddForce(forwardVector * vertInput * movingForce * Time.deltaTime, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(forwardVector * vertInput * movingForce * Time.deltaTime, ForceMode.VelocityChange);
            }
        }

        if (Mathf.Abs(horiInput) > 0.1f)
        {
            if (gripInput)
            {
                rb.AddForce(rightVector * horiInput * movingForce * Time.deltaTime, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(rightVector * horiInput * movingForce * Time.deltaTime, ForceMode.VelocityChange);
            }
        }

        if (slidInput)
        {
            phyMat.staticFriction = 0;
            phyMat.dynamicFriction = 0;
        }
        else
        {
            phyMat.staticFriction = 0.6f;
            phyMat.dynamicFriction = 0.6f;
        }

        if (jumpInput)
        {
            phyMat.bounciness = 0.6f;
        }
        else
        {
            phyMat.bounciness = 0;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (jumpInput)
        {
            rb.AddForce(planNormal * 10, ForceMode.VelocityChange);
        }
    }

    void InputGetting()
    {
        vertInput = InputHandler.axisVertical;
        horiInput = InputHandler.axisHorizontal;
        jumpInput = InputHandler.bounce;
        gripInput = InputHandler.grip;
    }
    void UpdateAndDebug()
    {
        cam2sphere = (sphere.position - cam.position).normalized;
        Debug.DrawRay(cam.position, cam2sphere, Color.yellow);

        planNormal = Vector3.Cross(Vector3.forward, Vector3.right).normalized;
        Debug.DrawRay(sphere.position, planNormal, Color.blue);

        forwardVector = Vector3.ProjectOnPlane(cam2sphere, planNormal).normalized;
        Debug.DrawRay(sphere.position, forwardVector, Color.red);

        rightVector = Vector3.Cross(planNormal, forwardVector).normalized;
        Debug.DrawRay(sphere.position, rightVector, Color.green);
    }

}
