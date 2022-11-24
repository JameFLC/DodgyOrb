using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DodgyOrb
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceTrigger : MonoBehaviour
    {
        // Parameters
        [SerializeField] private string tagToCheck = "Orb";
        // Variables
        private AudioSource _source;
        // Start is called before the first frame update
        void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == tagToCheck)
            {
                _source.PlayOneShot(_source.clip);
                Debug.Log(_source.clip.name + " clip was played You finished");
            }
        }
    }
}
