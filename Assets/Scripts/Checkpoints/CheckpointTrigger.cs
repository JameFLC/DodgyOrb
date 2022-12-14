using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    [RequireComponent(typeof(Collider))]
    public class CheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private string tagToCheck = "Orb";
        [SerializeField] private bool _useCheckpoints = true;
        private CheckpointManager _manager;
        private AudioSource _audioSource;
        // Start is called before the first frame update
        void Start()
        {
            _manager = CheckpointManager.instance;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == tagToCheck)
            {
                TriggerCheckpoint();
            }
        }

        public void TriggerCheckpoint()
        {
            if (_useCheckpoints)
                _manager.TeleportToLastCheckpoint();
            else
                _manager.TeleportToCheckpoint(0);
        }

        public bool GetUseCheckpoints() => _useCheckpoints;

        public void SetUseCheckpoint(bool useCheckpoints)
        {
            _useCheckpoints = useCheckpoints;
        }
    }
}
