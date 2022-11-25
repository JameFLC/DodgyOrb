using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb.ThreeDButtons
{
    public class DragPlane : MonoBehaviour, IDraggable
    {
        [SerializeField] bool offsetFromFirstInput = true;
        private Collider _collider;

        private Vector2 currentPos = Vector2.zero;
        private Vector2 defaultPos = Vector2.zero;

        private Vector2 dragMovement = Vector2.zero;
        private bool isReseted = true;
        


        void Start()
        {
            _collider = GetComponent<Collider>();
            DeactivateDragPlane();
        }

        public void ActivateDragPlane()
        {
            _collider.enabled = true;
        }
        public void DeactivateDragPlane()
        {
            _collider.enabled = false;
        }
        public void GetDragged(Vector2 position)
        {
            if (isReseted)
            {
                isReseted = false;
                if (offsetFromFirstInput)
                    defaultPos = position;
                else
                    defaultPos = new Vector2(transform.position.x, transform.position.z);
            }
            currentPos = position;
            dragMovement = currentPos - defaultPos;
        }

        public Vector2 GetDragMovement() => dragMovement;

        public void GetReseted()
        {
            isReseted = true;
            //Debug.Log("Reseted");
        }

    }
}
