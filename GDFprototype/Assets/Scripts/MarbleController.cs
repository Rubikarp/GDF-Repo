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
    private bool canJump = true;

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
    [SerializeField] bool bounceInput;
    [SerializeField] bool gravityInput;
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
        jumpInput = InputHandler.jump;
        bounceInput = InputHandler.bounce;
        gravityInput = InputHandler.gravity;
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
                if(rb.angularVelocity.magnitude > 15)
                {
                    rb.angularVelocity = rb.angularVelocity.normalized * 15;
                }
            }
            else
            {
                rb.maxAngularVelocity = movingForce *8f;
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
                canJump = false;
                Invoke("JumpCD", 0.1f);
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

            Physics.gravity = Vector3.down * 10;
        }

        if (gravityInput)
        {
            Physics.gravity = Vector3.down * 2 * 9.81f;
        }
        else
        {
            Physics.gravity = Vector3.down * 9.81f;
        }
        //Bounciness
        if (bounceInput)
        {
            phyMat.bounciness = bounceOn;
        }
        else
        {
            phyMat.bounciness = bounceOff;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        touchingGround = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        touchingGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingGround = false;
        //rb.angularVelocity = rb.velocity;

    }

    public void JumpCD()
    {
        canJump = true;
    }
}
