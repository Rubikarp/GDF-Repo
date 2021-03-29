using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SphereController : MonoBehaviour
{
    #region Components

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private Transform sphere;
    [SerializeField]
    private Rigidbody rb;
    #endregion

    [SerializeField]
    private Vector3 cam2sphere;

    [SerializeField]
    public float vertInput;
    [SerializeField]
    public float horiInput;
    [SerializeField]
    public bool jumpInput;

    public TextMeshProUGUI speedText;


    Vector3 planNormal;
    Vector3 forwardVector;
    Vector3 rightVector;

    void Start()
    {

    }

    private void Update()
    {
        vertInput = Input.GetAxis("Vertical");
        horiInput = -Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");

        cam2sphere = (sphere.position - cam.position).normalized;
        Debug.DrawRay(cam.position, cam2sphere, Color.yellow);

        planNormal = Vector3.Cross(Vector3.forward, Vector3.right).normalized;
        Debug.DrawRay(sphere.position, planNormal, Color.blue);

        forwardVector = Vector3.ProjectOnPlane(cam2sphere, planNormal).normalized;
        Debug.DrawRay(sphere.position, forwardVector, Color.red);

        rightVector = Vector3.Cross(planNormal, forwardVector).normalized;
        Debug.DrawRay(sphere.position, rightVector, Color.green);
    }

    void FixedUpdate()
    {
        Roolling(forwardVector, horiInput*10);

        Roolling(rightVector, vertInput*10);

        if (jumpInput)
        {
            rb.AddForce(planNormal *10, ForceMode.VelocityChange);
        }


        speedText.text = Mathf.RoundToInt(rb.velocity.magnitude * 10) + " m/sec";
    }


    void Roolling(Vector3 axis, float input)
    {
        rb.AddTorque(axis * input, ForceMode.Force);
    }

}
