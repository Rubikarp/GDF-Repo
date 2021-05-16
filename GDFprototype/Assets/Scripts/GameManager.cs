using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Component")]
    public Transform marble;
    public Rigidbody rb;
    [Space(5)]
    public Timer timer;
    [Space(15)]
    public Vector3 startPos;

    void Start()
    {
        marble.position = startPos;

        timer.RebootChrono();
        timer.StartChrono();
    }

    public void RestartRun()
    {
        marble.position = startPos;

        timer.RebootChrono();
        timer.StartChrono();
    }

    public void StopGame()
    {
        timer.StopChrono();
    }
}
