using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    [RequireComponent(typeof(Collider))]
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private string tagToCheck = "Orb";
        [SerializeField] private bool isReachableOnce = true;

        private CheckpointManager _manager;
        private bool _alreadyReached = false;
        // Start is called before the first frame update
        void Start()
        {
            _manager = CheckpointManager.instance;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (isReachableOnce)
                if (_alreadyReached)
                    return;

            if (other.gameObject.tag == tagToCheck)
            {
                _manager.AddCheckpoint(transform.position);
                _alreadyReached = true;
            }
        }
    }
}
