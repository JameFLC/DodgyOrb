using UnityEngine;

namespace DodgyOrb
{
    public class GrueController : MonoBehaviour
    {
        [SerializeField] private float rotationValue = 0;
        [SerializeField] private Transform top;

        private Rigidbody rigidbodyOrb;
        private Transform orb = null;

        private float previousRotationValue;

        // Update is called once per frame
        void Update()
        {
            //If the orb has already been catch by the collider
            if (orb)
            {
                //If the difference of the rotationValue variable between two frames is too big then the orb is dropped 
                if (rotationValue > previousRotationValue + 10 || rotationValue < previousRotationValue - 10)
                {
                    DropOrb(orb);
                }
            }
            top.localRotation = Quaternion.Euler(0, rotationValue, 0);
            previousRotationValue = rotationValue;
        }

        //Check the activity top collider
        private void OnTriggerEnter(Collider orbCollider)
        {
            orb = orbCollider.transform;
            rigidbodyOrb = orb.GetComponent<Rigidbody>();
            CatchOrb(orb);
        }

        private void CatchOrb(Transform orb)
        {
            orb.parent = top;
            rigidbodyOrb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            rigidbodyOrb.freezeRotation = true;
            rigidbodyOrb.interpolation = RigidbodyInterpolation.None;
        }

        private void DropOrb(Transform orb)
        {
            orb.parent = null;
            rigidbodyOrb.constraints = RigidbodyConstraints.None;
            rigidbodyOrb.freezeRotation = false;
            rigidbodyOrb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }
}
