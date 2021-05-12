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

    private Vector3 cam2sphere;

    public LayerMask layer;
    [SerializeField] bool touchingGround;

    [Header("Parameter")]
    [SerializeField] float movingForce = 20f;
    [SerializeField] float jumpForce = 20f;
    [Space(10)]
    [SerializeField, Range(0, 1)] float bounceOn = 1f;
    [SerializeField, Range(0, 1)] float bounceOff = 0f;
    [Space(5)]
    [SerializeField, Range(0, 1)] float frictionOn = 0.6f;
    [SerializeField, Range(0, 1)] float frictionOff = 0f;

    Vector3 planNormal;
    Vector3 forwardVector;
    Vector3 rightVector;

    [Header("Input Check")]
    [SerializeField] float vertInput;
    [SerializeField] float horiInput;

    [SerializeField] bool jumpInput;
    [SerializeField] bool slidInput;
    [SerializeField] bool gripInput;

    private void Update()
    {
        GetInput();
        UpdateAndDebug();

    }

    #region UpdateMethode
    void GetInput()
    {
        vertInput = InputHandler.axisVertical;
        horiInput = InputHandler.axisHorizontal;
        jumpInput = InputHandler.bounce;
        slidInput = InputHandler.slid;
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
    #endregion

    void FixedUpdate()
    {
        if (touchingGround)
        {
            #region Mouvement

            if (gripInput)
            {
                rb.maxAngularVelocity = movingForce;
                if(rb.angularVelocity.magnitude > 10)
                {
                    rb.angularVelocity = rb.angularVelocity.normalized * 10;
                }
            }
            else
            {
                rb.maxAngularVelocity = movingForce *5;
            }

            if (gripInput)
            {
                rb.AddTorque(rightVector * vertInput * movingForce * Time.deltaTime, ForceMode.VelocityChange);
                rb.AddTorque(forwardVector * -horiInput * movingForce * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddTorque(rightVector * vertInput * movingForce* Time.deltaTime, ForceMode.Force);
                rb.AddTorque(forwardVector * -horiInput * movingForce * Time.deltaTime, ForceMode.Force);
            }

            #endregion

            //Jump
            if (jumpInput)
            {
                rb.AddForce(planNormal * jumpForce, ForceMode.Impulse);
            }
        }

        //Adhésion
        if (slidInput)
        {
            phyMat.staticFriction = frictionOff;
            phyMat.dynamicFriction = frictionOff;
        }
        else
        {
            phyMat.staticFriction = frictionOn;
            phyMat.dynamicFriction = frictionOn;
        }
        //Bounciness
        if (jumpInput)
        {
            phyMat.bounciness = bounceOn;
        }
        else
        {
            phyMat.bounciness = bounceOff;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        touchingGround = true;

    }
    private void OnCollisionExit(Collision collision)
    {
        touchingGround = false;

    }
}
