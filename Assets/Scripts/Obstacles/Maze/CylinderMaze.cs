using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class CylinderMaze : MonoBehaviour
    {
        // Parameters

        [SerializeField] private Transform wheel;
        [SerializeField] private float maxSpeed = 0.5f;
        [SerializeField] private float acceleration = 2;

        // Variables
        private float _currentNormalizedSpeed = 0;
        private float _inputNormalizedSpeed = 0;
        private float _currentAngle;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
        // Update is called once per frame
        void Update()
        {
            _currentNormalizedSpeed = Mathf.Lerp(_currentNormalizedSpeed, _inputNormalizedSpeed, Time.deltaTime * acceleration);

            audioSource.pitch = Mathf.Lerp(0.8f , 1f, Mathf.Abs(_currentNormalizedSpeed));
            audioSource.volume = Mathf.Lerp(0, 0.5f, Mathf.Abs(_currentNormalizedSpeed));
            _currentAngle = _currentNormalizedSpeed * maxSpeed * 360 * Time.deltaTime * -1;
            wheel.Rotate(new Vector3(1,0,0), _currentAngle);
        }

        public void GetData(Vector2 data)
        {
            _inputNormalizedSpeed = data[0];
        }
    }
}
