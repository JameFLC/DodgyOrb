using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{

    public class RaycastInputHandler : MonoBehaviour
    {
        [SerializeField] private float maxRayDistance = 100f;
        [SerializeField] LayerMask buttonMask;
        [SerializeField] LayerMask dragMask;
        private Camera _cam;

        private GameObject _selectedButton;

        private bool _wasSelectedLastFrame = false;
        private bool _wasLastReleased = false;


        // Start is called before the first frame update
        void Start()
        {
            _cam = Camera.main;
            Debug.Log(buttonMask);
            Debug.Log(dragMask);
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit buttonHit;
            RaycastHit dragHit;

            if (Physics.Raycast(ray, out buttonHit, maxRayDistance, buttonMask))
            {
                _selectedButton = buttonHit.transform.gameObject;

                HandleHover();

                HandleClick();
                
                _wasSelectedLastFrame = true;
            }
            else
            {
                if (_wasSelectedLastFrame)
                {
                    HandleUnHover();
                    _wasSelectedLastFrame = false;
                }
            }

            if (_selectedButton != null)
            {
                HandleRelease();

            }
            if (Physics.Raycast(ray, out dragHit, maxRayDistance, dragMask))
            {
                
                HandleDrag(dragHit);
            }
        }



        private void HandleHover()
        {
            if (!_wasSelectedLastFrame)
            {
                IHoverable iHover = _selectedButton.GetComponent<IHoverable>();
                if (iHover == null)
                    return;

                iHover.GetHovered();

            }
        }
        private void HandleUnHover()
        {
            IHoverable ihover = _selectedButton.GetComponent<IHoverable>();
            if (ihover != null)
            {
                ihover.GetUnhovered();
            }
        }
        private void HandleClick()
        {
            IClickable iClick = _selectedButton.GetComponent<IClickable>();
            if (iClick == null)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                iClick.GetClicked();
                _wasLastReleased = false;
            }
        }
        private void HandleRelease()
        {
            if (_wasLastReleased)
                return;
            IClickable iClick = _selectedButton.GetComponent<IClickable>();
            if (iClick == null)
                return;
            if (Input.GetMouseButtonUp(0))
            {
                iClick.GetReleased();
                _wasLastReleased = true;
            }
        }

        private void HandleDrag(RaycastHit dragHit)
        {
            GameObject selectedPlane = dragHit.transform.gameObject;

            IDraggable iDrag = selectedPlane.GetComponent<IDraggable>();
            if (iDrag == null)
                return;


            Vector2 hitPos = new Vector2(dragHit.point.x, dragHit.point.z);



            Debug.DrawRay(dragHit.transform.position, dragHit.point, Color.red);
            if (Input.GetMouseButton(0))
            {
                iDrag.GetDragged(hitPos);
            }
            if (Input.GetMouseButtonUp(0))
            {

            }

        }
    }
}
