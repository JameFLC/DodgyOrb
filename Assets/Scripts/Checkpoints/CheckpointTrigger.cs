using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    [RequireComponent(typeof(Collider))]
    public class CheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private string tagToCheck = "Orb";
        [SerializeField] private bool useCheckpoints = true;
        private CheckpointManager _manager;
        // Start is called before the first frame update
        void Start()
        {
            _manager = CheckpointManager.instance;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == tagToCheck)
            {
                if (useCheckpoints)
                    _manager.TeleportToLastCheckpoint();
                else
                    _manager.TeleportToCheckpoint(0);
            }
        }
    }
}
