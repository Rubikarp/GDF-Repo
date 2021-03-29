using UnityEngine;

public class CamController : MonoBehaviour
{
    public Rigidbody rb = null;
    public Transform avatar = null;
    public float camDistance = 5f;
    public float camHeight = 0f;
    [SerializeField] bool handControl;
    [Range(-1, 1)] public float camAngle = 0f;
    [Range(0, 180)] public float maxSideAngle = 30f;


    public Transform aimTarget = null;
    public Vector3 camAimOffSet = Vector3.zero;

    public float posLerpSpeed = 0.02f;
    public float lookLerpSpeed = 0.1f;

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
        float rotRad = (camAngle * maxSideAngle / 180) * Mathf.PI;

        wantedPos = avatar.position;

        Vector2 temp = Rotate(new Vector2(-velDir.x, -velDir.z), rotRad);
        wantedPos += new Vector3(temp.x, 0, temp.y) * camDistance;

        //wantedPos += -velDir * camDistance;
        wantedPos.y += camHeight;

        transform.position = Vector3.Lerp(transform.position, wantedPos, posLerpSpeed);

        Quaternion look = Quaternion.LookRotation(avatar.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, lookLerpSpeed);
    }

    private Vector2 Rotate(Vector2 baseVect,float radian)
    {
        Vector2 result = Vector2.zero;

        result.x = (baseVect.x * Mathf.Cos(radian)) - (baseVect.y * Mathf.Sin(radian));
        result.y = (baseVect.x * Mathf.Sin(radian)) + (baseVect.y * Mathf.Cos(radian));

        return result;
    }
}
