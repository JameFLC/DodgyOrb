using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DodgyOrb.RemoteControls;

namespace DodgyOrb.ThreeDButtons
{
    public class DraggableButton : MonoBehaviour, IClickable, IHoverable
    {
        [SerializeField] float dragRadius = 0.5f;
        [SerializeField] DragPlane dragPlane;
        [SerializeField] Material hoveredMaterial;
        [Space]
        [Space]
        [SerializeField] bool axisConstrained = false;
        [SerializeField] bool axis = false;
        [SerializeField] bool resetOnReleased = true;

        private RemoteController _controller;

        private Material _defaultMaterial;

        private bool _isHold = false;
        private bool _isHovered = false;
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<RemoteController>();
            _defaultMaterial = GetComponent<MeshRenderer>().material;

            

            if (_controller == null)
            {
                Debug.LogWarning("Missing controller in game object");
            }

            if (dragPlane == null)
            {
                Debug.LogWarning("Missing dragPlane in Prefab");
            }
        }
        private void Update()
        {
            if (_isHold)
            {
                Vector2 dragMovement = ClampVector(dragPlane.GetDragMovement(), new Vector2(-dragRadius, -dragRadius), new Vector2(dragRadius, dragRadius));


                if (axisConstrained)
                {
                    dragMovement = new Vector2(axis ? dragMovement.x : 0, axis ? 0 : dragMovement.y);
                }
                
                Vector2 normalizedMovement = dragMovement / dragRadius;


                transform.localPosition = new Vector3(dragMovement.x, transform.localPosition.y, dragMovement.y);

                _controller.SendData(normalizedMovement);
            }
        }
        private Vector2 ClampVector(Vector2 vector, Vector2 min, Vector2 max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);
            return vector;
        }
        public void GetClicked()
        {
            dragPlane.ActivateDragPlane();
            


            _isHold = true;
        }

        public void GetReleased()
        {
            dragPlane.DeactivateDragPlane();
            dragPlane.GetReseted();

            _isHold = false;
            transform.localPosition = new Vector3(0, transform.localPosition.y, 0);

            if (resetOnReleased)
                _controller.SendData(new Vector2(0,0));

            SetMaterial(_isHovered);
        }

        public void GetHovered()
        {
            _isHovered = true;
            SetMaterial(_isHovered);
        }

        public void GetUnhovered()
        {
            _isHovered = false;
            if (!_isHold)
                SetMaterial(_isHovered);
        }

        private void SetMaterial(bool setDefault)
        {
            GetComponent<MeshRenderer>().material = setDefault ? hoveredMaterial : _defaultMaterial;
        }

    }
}
