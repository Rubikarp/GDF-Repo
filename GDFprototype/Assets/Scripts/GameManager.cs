using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [Header("Component")]
    public Transform marble;
    public Rigidbody rb;
    [Space(5)]
    public CinemachineFreeLook cineMach;
    public Camera cam;
    [Space(5)]
    public Timer timer;
    [Space(15)]
    public Vector3 startPos;

    void Start()
    {
        RestartRun();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            RestartRun();
        }
    }
    public void RestartRun()
    {
        marble.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.angularDrag = 0f;
        float tempRot = 0;
        float temp = Vector3.Dot(Vector3.right, cam.transform.forward);
        
        if (temp < 0)
        {
            tempRot += 180;
            temp *= -1;
        }

        tempRot = (1 - temp) * 90;

        cineMach.m_XAxis.Value = tempRot;

        timer.RebootChrono();
        timer.StartChrono();
    }

    public void StopGame()
    {
        timer.StopChrono();
    }
}
