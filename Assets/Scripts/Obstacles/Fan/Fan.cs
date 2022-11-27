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
        [SerializeField] private Vector2 pitchRange = new Vector2(1, 2);
        [SerializeField] private AnimationCurve pitchResponse = new();
        [Space]
        [SerializeField] private Vector2 volumeRange = new Vector2(0, 1);
        [SerializeField] private AnimationCurve volumeResponse = new();
        [SerializeField] private float fanSpeed = 1;
        [SerializeField] private float fanAcceleration = 5;
        // Variables
        private float _push = 0;
        private List<Collider> _colliders = new();
        private AudioSource _source;
        private float _fanLerp = 0  ;
        private void Start()
        {
            _source = GetComponent<AudioSource>();
            _source.volume = 1;
        }
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
            Transform child = transform.GetChild(0).GetChild(0);
            if (child)
            {
                _fanLerp = Mathf.Lerp(_fanLerp, _push, fanAcceleration* Time.deltaTime);
                child.localRotation *= Quaternion.Inverse(Quaternion.Euler(0, Time.deltaTime * 360 * _fanLerp * fanSpeed, 0));

                _source.pitch = Mathf.Lerp(pitchRange[0], pitchRange[1], pitchResponse.Evaluate(_fanLerp));
                _source.volume = Mathf.Lerp(volumeRange[0], volumeRange[1], volumeResponse.Evaluate(_fanLerp));

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
