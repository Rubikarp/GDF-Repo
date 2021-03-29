using UnityEngine;

public class template : MonoBehaviour
{
    #region Variables
    [Header("Variable")]

    [Space(10)]

    [SerializeField, Tooltip("c'est ma booléen")]
    private bool myBool = true;

    [SerializeField, Min(0)]
    private int myInt = 0;

    [SerializeField, Range(0,100)] 
    private float myFloat = 0.0f;

    [SerializeField, TextArea(1,3)]
    private string myTrue = "hello world";

    [SerializeField]
    private Vector3 myVector3;

    [SerializeField, ColorUsage(true, false)]
    private Color myColor;

    #endregion

    #region Methodes

    [ContextMenu("MyMethode")]
    public void MyMethode()
    {
        myInt = 1;
    }

    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }


    #endregion

}

[System.Serializable]
struct templateStruct
{

    public templateStruct(string name)  //Constructor
    {

    }
}

[CreateAssetMenu(fileName = "NewSCO", menuName = "ScriptableObjects/ScriptableObjectTemplate")]
public class ScriptableObjectTemplate : ScriptableObject
{

}
