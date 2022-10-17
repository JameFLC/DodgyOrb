using UnityEngine;

namespace DodgyOrb
{
    public class OrbSleepPreventer : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody != null)
            {
                _rigidbody.sleepThreshold = -1;
            }
        }
    }
}
