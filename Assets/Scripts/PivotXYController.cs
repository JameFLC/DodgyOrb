using UnityEngine;

public class PivotXYController : MonoBehaviour
{
    //Privates variables reachable in the inspector
    [SerializeField] private Vector2 rotationAxis;    //Vector modified by the controller
    [SerializeField] private float maxAngle = 15f;
    [SerializeField] private Transform pivot;
    [SerializeField] private float speed = 1f;

    //Privates variables not reachable in the inspector
    private GameObject targetRotation;  //Used in Lerp function as target rotation
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        //Initialization
        rotationAxis = new Vector2(0.0f, 0.0f);
        targetRotation = new GameObject("targetRotationGO");
        targetRotation.transform.parent = transform;
        _rigidbody = pivot.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation.transform.localRotation = Quaternion.Euler(rotationAxis.y * maxAngle, 0, -rotationAxis.x * maxAngle);

        //Interpolates between pivot.rotation and targetRotation.transform.rotation by timeCount * speed and normalizes the result afterwards
        _rigidbody.MoveRotation(Quaternion.Lerp(pivot.localRotation, targetRotation.transform.localRotation, Time.deltaTime * speed));
    }
    public void GetData(Vector2 data)
    {
        rotationAxis = data;
        Debug.Log(data);
    }
}
