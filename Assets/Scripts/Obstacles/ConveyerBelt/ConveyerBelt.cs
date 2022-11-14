using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace DodgyOrb
{
    [RequireComponent(typeof(Rigidbody))]
    public class ConveyerBelt : MonoBehaviour
    {
        // Parameters
        [SerializeField] public float speed = 1;

        // Variables
        private Rigidbody _beltRB;

        private void Start()
        {
            _beltRB = GetComponent<Rigidbody>();
            if (_beltRB != null)
                _beltRB.isKinematic = true;
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = transform.forward * speed * Time.fixedDeltaTime;
            if (_beltRB != null)
            {
                _beltRB.position += moveDirection;
                _beltRB.MovePosition(_beltRB.position - moveDirection);
            }
        }
    }
}
