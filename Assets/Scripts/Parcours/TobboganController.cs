using UnityEngine;

namespace DodgyOrb
{
    public class TobboganController : MonoBehaviour
    {
        [SerializeField] private float rotationValue;
        [SerializeField] private Transform tobbogan;


        // Update is called once per frame
        void Update()
        {
            tobbogan.localRotation = Quaternion.Euler(0, 0, rotationValue);
        }
    }
}
