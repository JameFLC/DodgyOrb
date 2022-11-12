using UnityEngine;

namespace DodgyOrb
{
    public class TobboganController : MonoBehaviour
    {
        [SerializeField] private float rotationValue;
        [SerializeField] private float maxAngle = 15f;
        [SerializeField] private Transform tobbogan;
        [SerializeField] private float speed = 5.00f;

        private Quaternion previousRotationValue;

        private void Start()
        {
            previousRotationValue = Quaternion.Euler(0, 0, rotationValue * maxAngle);
        }

        // Update is called once per frame
        void Update()
        {
            tobbogan.localRotation = Quaternion.Lerp(previousRotationValue, Quaternion.Euler(0, 0, rotationValue * maxAngle), Time.deltaTime * speed);
            previousRotationValue = tobbogan.localRotation;
        }

        public void GetData(Vector2 data)
        {
            rotationValue = data.x;
        }
    }
}
