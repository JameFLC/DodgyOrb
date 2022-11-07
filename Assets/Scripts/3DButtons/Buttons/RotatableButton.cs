using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DodgyOrb.RemoteControls;

namespace DodgyOrb.ThreeDButtons
{
    public class RotatableButton : MonoBehaviour, IClickable, IHoverable
    {
        [SerializeField] DragPlane dragPlane;
        [SerializeField] Material hoveredMaterial;


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
                Vector2 dragMovement = dragPlane.GetDragMovement();
         
                Vector2 normalizedMovement = dragMovement / (dragMovement.magnitude+ 0.000001f);

                float angle = Vector2.Angle(new Vector2(0, 1), normalizedMovement) * (normalizedMovement.x > 0 ? 1 : -1);
                
                transform.localRotation = Quaternion.Euler(0,angle,0);

                _controller.SendData(normalizedMovement);
            }
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
