using UnityEngine;

namespace DodgyOrb
{
    public class GrueController : MonoBehaviour
    {
        [SerializeField] private float rotationValueY = 0;
        [SerializeField] private Transform top;
        [SerializeField] private float speed = 0.01f;

        private Rigidbody rigidbodyOrb;
        private Transform orb = null;

        private float previousYValue;
        private Quaternion previousRotationValue;

        private AudioSource craneSoundClap;

        private void Start()
        {
            craneSoundClap = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            //If the orb has already been catch by the collider
            if (orb)
            {
                //If the difference of the rotationValue variable between two frames is too big then the orb is dropped 
                if (rotationValueY > previousYValue + 10 || rotationValueY < previousYValue - 10)
                {
                    DropOrb(orb);
                }
            }
            top.localRotation = Quaternion.Lerp(previousRotationValue, Quaternion.Euler(0, rotationValueY, 0), Time.deltaTime * speed);
            previousYValue = rotationValueY;
            previousRotationValue = top.localRotation;
        }

        //Check the activity top collider
        private void OnTriggerEnter(Collider orbCollider)
        {
            if (orbCollider.tag == "Orb")
            {
                craneSoundClap.Play();
                orb = orbCollider.transform;
                rigidbodyOrb = orb.GetComponent<Rigidbody>();
                CatchOrb(orb);
            }
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

        public void GetData(Vector2 data)
        {
            float angle = Vector2.Angle(new Vector2(0, 1), data) * (data.x > 0 ? 1 : -1);

            rotationValueY = angle;
        }
    }
}
