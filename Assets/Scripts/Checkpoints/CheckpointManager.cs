using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class CheckpointManager : MonoBehaviour
    {
        // Parameters
        [SerializeField] private string tagToCheck = "Orb";


        // Variables
        [HideInInspector] public static CheckpointManager instance;
        private GameObject _objectToTeleport;
        private List<Vector3> _checkpointPositions = new();
        private void Awake()
        {
            // Singelton Pattern
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }
        private void Start()
        {
            _objectToTeleport = GameObject.FindGameObjectWithTag(tagToCheck);
            if (_objectToTeleport == null)
            {
                Debug.LogError("No object to teleport");
            }
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.C))
            {
                TeleportToLastCheckpoint();
            }
        }

        public void TeleportToLastCheckpoint()
        {
            if (_checkpointPositions != null && _checkpointPositions.Count == 0)
            {
                Debug.LogWarning("Cant teleport to any checkpoints since no one was reached");
                return;
            }
            TeleportToCheckpoint(_checkpointPositions.Count - 1);
        }
        // Teleport the desired object to a desired checkpoint position present in the scene
        public void TeleportToCheckpoint(int checkpointIndex)
        {
            if (_checkpointPositions != null && _checkpointPositions.Count == 0)
            {
                Debug.LogWarning("Cant teleport to any checkpoints since no one was reached");
                return;
            }
            if (checkpointIndex >= _checkpointPositions.Count)
            {
                Debug.LogWarning("Cant teleport to desired checkpoints no checkpoint has this index");
                return;
            }
            _objectToTeleport.transform.position = _checkpointPositions[checkpointIndex];
            // Reset velocity to prevent the teleported object to fly everywhere
            ResetObjectVeloity(_objectToTeleport.gameObject);
        }
        private void ResetObjectVeloity(GameObject gameObject)
        {
            Rigidbody objectRigiddody = gameObject.GetComponent<Rigidbody>();
            if (objectRigiddody != null)
            {
                objectRigiddody.velocity = new Vector3(0, 0, 0);
            }
        }
        public void AddCheckpoint(Vector3 position)
        {
            _checkpointPositions.Add(position);

        }
    }
}