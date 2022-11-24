using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class TrapDoor : MonoBehaviour
    {
        [SerializeField] private Vector2 angleRanges = new Vector3(0,70);
        [SerializeField] private float speed = 10;

        // Variables 
        private float _angleOffset = 0;
        private float _currentAngle = 0;
        private float _targetAngle = 0;
        private void Start()
        {
            _angleOffset = transform.localEulerAngles.x;
            _currentAngle = _angleOffset + angleRanges[0];
            _targetAngle = _currentAngle;
        }
        
        // Update is called once per frame
        void Update()
        {
            _currentAngle = Mathf.LerpAngle(_currentAngle, _targetAngle, Time.deltaTime * speed);
            RotatePivot();
        }

        private void RotatePivot()
        {
            transform.localEulerAngles = new Vector3(_currentAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        public void GetData(Vector2 data)
        {
            _targetAngle = (data[0] < 0.5 ? angleRanges[0] : angleRanges[1]) + _angleOffset;
            Debug.Log("Data Recieved");
        }
    }
}
