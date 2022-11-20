using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class ResetButton : MonoBehaviour
    {
        [SerializeField] CheckpointTrigger checkpointTrigger;

        public void TriggerCheckpoint() => _isClicked = false;

        private bool _isClicked = false;

        private void Update()
        {
            if (!_isClicked)
            {
                checkpointTrigger.TriggerCheckpoint();
                _isClicked = true;
            }
        }
    }
}
