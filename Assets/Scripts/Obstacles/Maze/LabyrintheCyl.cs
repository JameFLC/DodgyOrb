using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class LabyrintheCyl : MonoBehaviour
    {
        [SerializeField] private float rotationValue;
        [SerializeField] private Transform wheel;
        [SerializeField] private float speed = 0.01f;

        private Quaternion previousRotation;

        // Start is called before the first frame update
        void Start()
        {
            rotationValue = 180;
        }

        // Update is called once per frame
        void Update()
        {
            wheel.localRotation = Quaternion.Lerp(previousRotation, Quaternion.Euler(rotationValue, wheel.localRotation.y, wheel.localRotation.z), Time.deltaTime * speed);
            previousRotation = wheel.localRotation;
        }

        public void GetData(Vector2 data)
        {
            float angle = Vector2.Angle(new Vector2(0, 1), data) * (data.x > 0 ? 1 : -1);
            rotationValue = -angle;
        }
    }
}
