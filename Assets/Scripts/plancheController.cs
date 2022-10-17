using UnityEngine;

public class PlancheController : MonoBehaviour
{
    //Privates variables reachable in the inspector
    [SerializeField] private Vector2 rotationXZ;    //Vector modified by the controller
    [SerializeField] private Transform plancheMesh;
    [SerializeField] private float speed = 0.01f;

    //Privates variables not reachable in the inspector
    private float timeCount = 0.0f;
    private GameObject targetRotation;  //Used in Lerp function as target rotation

    // Start is called before the first frame update
    void Start()
    {
        //Initialization
        rotationXZ = new Vector2(0.0f, 0.0f);
        targetRotation = new GameObject("targetRotationGO");
        targetRotation.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation.transform.rotation = Quaternion.Euler(rotationXZ.x, targetRotation.transform.rotation.y, rotationXZ.y);

        //Interpolates between plancheMesh.rotation and targetRotation.transform.rotation by timeCount * speed and normalizes the result afterwards
        plancheMesh.rotation = Quaternion.Lerp(plancheMesh.rotation, targetRotation.transform.rotation, Time.deltaTime * speed);

        timeCount = timeCount + Time.deltaTime;
    }
}
