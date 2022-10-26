using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class GrueController : MonoBehaviour
    {
        [SerializeField] private float rotationValue = 0;

        private Rigidbody rigidbodyOrb;
        private Transform top;

        private float previousRotationValue;

        // Start is called before the first frame update
        void Start()
        {
            top = transform.GetChild(0).GetChild(1);
        }

        // Update is called once per frame
        void Update()
        {
            if (rotationValue > previousRotationValue + 10 || rotationValue < previousRotationValue - 10)
            {
                Debug.Log("La boule est lache");
            }


            top.localRotation = Quaternion.Euler(0, rotationValue, 0);
            previousRotationValue = rotationValue;
        }

        private void OnTriggerEnter(Collider orb)
        {
            Debug.Log("TRIGGER");
            rigidbodyOrb = orb.transform.GetComponent<Rigidbody>();
            //rigidbodyOrb.isKinematic = true;

            orb.transform.parent = top;
            //rigidbodyOrb.detectCollisions = false;
            rigidbodyOrb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            rigidbodyOrb.freezeRotation = true;
            rigidbodyOrb.interpolation = RigidbodyInterpolation.None;
        }
    }
}
