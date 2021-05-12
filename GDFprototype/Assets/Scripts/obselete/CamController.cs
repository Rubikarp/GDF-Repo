using UnityEngine;

public class CamController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb = null;
    public Transform avatar = null;

    [Header("Input")]
    [Range(-1, 1)] public float camAngle = 0f;

    [Header("Parameter")]
    [SerializeField] bool handControl;
    public Transform aimTarget = null;

    public CameraSettings settings = new CameraSettings(true);

    [Header("Valeurs Tampon")]
    private Vector3 velDir = Vector3.one;
    private Vector3 wantedPos;

    private void Awake()
    {
        velDir = (avatar.position - transform.position).normalized;
    }

    private void Update()
    {
        if(rb.velocity.magnitude >= 0.5f)
        {
            velDir = rb.velocity.normalized;
        }

        if (!handControl)
        {
            camAngle = InputHandler.axisCamera;
        }
    }

    void FixedUpdate()
    {
        float rotRad = (camAngle * settings.maxSideAngle / 180) * Mathf.PI;

        wantedPos = avatar.position;

        Vector2 temp = Rotate(new Vector2(-velDir.x, -velDir.z), rotRad);
        wantedPos += new Vector3(temp.x, 0, temp.y).normalized * settings.camDistance;

        //wantedPos += -velDir * camDistance;
        wantedPos.y += settings.camHeight;

        transform.position = Vector3.Lerp(transform.position, wantedPos, settings.posLerpSpeed);

        Quaternion look = Quaternion.LookRotation(avatar.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, settings.lookLerpSpeed);
    }

    private Vector2 Rotate(Vector2 baseVect,float radian)
    {
        Vector2 result = Vector2.zero;

        result.x = (baseVect.x * Mathf.Cos(radian)) - (baseVect.y * Mathf.Sin(radian));
        result.y = (baseVect.x * Mathf.Sin(radian)) + (baseVect.y * Mathf.Cos(radian));

        return result;
    }
}

[System.Serializable]
public struct CameraSettings
{
    public float camDistance;
    public float camHeight;

    [Range(0, 180)] 
    public float maxSideAngle;

    public Vector3 camAimOffSet;

    public float posLerpSpeed;
    public float lookLerpSpeed;
    public CameraSettings(bool basic)
    {
        camDistance = 5f;
        camHeight = 0f;
        maxSideAngle = 30f;
        camAimOffSet = Vector3.zero;
        posLerpSpeed = 0.02f;
        lookLerpSpeed = 0.1f;
    }
}
