using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    [RequireComponent(typeof(AudioSource))]
    public class PlasticFritctionSoundManager : MonoBehaviour
    {
        // Parameters
        [SerializeField] private float maxVelocity = 5;
        [SerializeField] private float changeSpeed = 20;
        [SerializeField] private bool switchFromAngleToPosition = false;
        [SerializeField] private AnimationCurve velocityVolumeResponse = new();
        [SerializeField] private AnimationCurve velocityPitchResponse = new();
        // Variables
        private AudioSource _audioSource;
        private Quaternion _lastFrameRotation;
        private Vector3 _lastFramePosition;
        private float _currentNormalizedVelocity = 0;
        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _lastFrameRotation = transform.rotation;
            _lastFramePosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            ComputeCurrentNormalizedVelocity();

            _audioSource.volume = velocityVolumeResponse.Evaluate(_currentNormalizedVelocity);
            _audioSource.pitch = velocityPitchResponse.Evaluate(_currentNormalizedVelocity);


            _lastFrameRotation = transform.rotation;
            _lastFramePosition = transform.position;
        }

        private void ComputeCurrentNormalizedVelocity()
        {
            float normalizedVelocity = computeVelocity()/ maxVelocity;
            _currentNormalizedVelocity = Mathf.Clamp01(Mathf.Lerp(_currentNormalizedVelocity, normalizedVelocity , Time.deltaTime * changeSpeed));
        }

        private float computeVelocity()
        {
            float difference;
            if (switchFromAngleToPosition)
                difference = Vector3.Magnitude(transform.position - _lastFramePosition);
            else
                difference = Quaternion.Angle(transform.rotation, _lastFrameRotation);

            return difference / (1 - Time.deltaTime);
        }
    }
}
