using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class LabyrintheCyl : MonoBehaviour
    {
        [SerializeField] private float rotationValue;
        [SerializeField] private Transform wheel;

        // Start is called before the first frame update
        void Start()
        {
            rotationValue = 180;
        }

        // Update is called once per frame
        void Update()
        {
            wheel.localRotation = Quaternion.Euler(rotationValue, wheel.localRotation.y, wheel.localRotation.z);
        }
    }
}
