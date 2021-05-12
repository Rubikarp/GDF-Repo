using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarbleHUD : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject marble;

    public TextMeshProUGUI speedShower;

    void Update()
    {
        speedShower.text = string.Format("{0:000}",rb.velocity.magnitude) + "Unit /Sec";
    }
}
