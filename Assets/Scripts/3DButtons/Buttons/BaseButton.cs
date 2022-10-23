using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DodgyOrb.RemoteControls;
namespace DodgyOrb
{
    public class BaseButton : MonoBehaviour, IClickable, IHoverable
    {
        [SerializeField] private Material hoveredMaterial;
        [SerializeField] private bool sendDataOnRelease = true;
        [SerializeField] private Vector2 clickedData = new Vector2(1, 0);
        [SerializeField] private Vector2 releasedData = new Vector2(0, 0);

        private RemoteController _controller;

        private Material _defaultMaterial;
        private bool isHold = false;
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
        }
        public void GetClicked()
        {
            _controller.SendData(clickedData);

            transform.position += new Vector3(0, -0.1f, 0);
        }
        public void GetReleased()
        {



            if (sendDataOnRelease)
                _controller.SendData(releasedData);


            transform.position += new Vector3(0, 0.1f, 0);

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
            if (!isHold)
                SetMaterial(_isHovered);
        }

        private void SetMaterial(bool setDefault)
        {
            GetComponent<MeshRenderer>().material = setDefault ? hoveredMaterial : _defaultMaterial;
        }

    }
}
