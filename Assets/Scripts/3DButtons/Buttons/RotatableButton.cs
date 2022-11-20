using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DodgyOrb.RemoteControls;

namespace DodgyOrb.ThreeDButtons
{
    public class RotatableButton : MonoBehaviour, IClickable, IHoverable
    {
        [SerializeField] private DragPlane dragPlane;
        [SerializeField] private Material hoveredMaterial;
        [Space]
        [Space]
        [SerializeField] private bool useRelativeAngle = true;


        private RemoteController _controller;

        private Material _defaultMaterial;

        private bool _isHold = false;
        private bool _isClicked = false;
        private bool _isHovered = false;
        private float _defaultAngle = 0;
        private float _lastAngle = 0;
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
            Vector2 dragMovement = dragPlane.GetDragMovement();
            if (_isHold)
            {
                Vector2 normalizedMovement = GetNormalizedMovement();
                if (_isClicked)
                {
                    if (useRelativeAngle)
                        _defaultAngle = ConvertVector2ToAngle(normalizedMovement) - _lastAngle;
                    else
                        _defaultAngle = 0;
                    _isClicked = false;
                }

                _lastAngle = ConvertVector2ToAngle(normalizedMovement) - _defaultAngle;
                transform.localRotation = Quaternion.Euler(0, _lastAngle, 0);

                Vector2 newNormalizedMovement = ConvertAngleToVector2(_lastAngle);

                _controller.SendData(newNormalizedMovement);
            }
        }

        private Vector2 GetNormalizedMovement()
        {
            Vector2 dragMovement = dragPlane.GetDragMovement();
            // Prevent division by 0
            float magnitude = dragMovement.magnitude == 0 ? 0.0000001f : dragMovement.magnitude;
            Vector2 normalizedMovement = dragMovement / (magnitude);
            return normalizedMovement;
        }

        private float ConvertVector2ToAngle(Vector2 vector)
        {
            return Vector2.Angle(new Vector2(0, 1), vector) * (vector.x > 0 ? 1 : -1);
        }

        private Vector2 ConvertAngleToVector2(float angle)
        {
            return new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
        }

        public void GetClicked()
        {
            dragPlane.ActivateDragPlane();
            _isHold = true;
            _isClicked = true;
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
