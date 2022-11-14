using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class Fan : MonoBehaviour
    {
        // Parametters
        [SerializeField] private float fanPower = 1;
        [SerializeField] private ForceMode forceMode = ForceMode.Force;
        [SerializeField] private bool switchToYAxis = false;

        // Variables
        private float _push = 0;
        private List<Collider> _colliders = new();

        private void FixedUpdate()
        {
            if (_push <= 0 || _colliders == null)
                return;
            foreach (Collider collider in _colliders)
            {
                Rigidbody colliderRB = collider.GetComponent<Rigidbody>();
                if (colliderRB != null)
                {
                    colliderRB.AddForce(transform.up * (fanPower * _push), forceMode);
                }
            }
        }

        private void Update()
        {
            Transform child = transform.GetChild(0);
            if (child)
            {
                child.localRotation *= Quaternion.Inverse(Quaternion.Euler(0, Time.deltaTime * 360 * _push, 0));
            }
        }

        public void GetInput(Vector2 input)
        {
            _push = Mathf.Clamp01(switchToYAxis ? input.y : input.x);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_colliders.Contains(other))
            {
                _colliders.Add(other);
            }
            LogColliders();
        }

        private void OnTriggerExit(Collider other)
        {
            if (_colliders.Contains(other))
            {
                _colliders.Remove(other);
            }
            LogColliders();
        }
        private void LogColliders()
        {
            Debug.Log(_colliders);
        }
    }
}
