using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb.RemoteControls
{
    public class SliderDataCombiner : MonoBehaviour
    {

        private RemoteController _remoteController;
        private Vector2 _dataToSend = new Vector2(0.5f,0.5f);
        // Start is called before the first frame update
        void Start()
        {
            _remoteController = GetComponent<RemoteController>();
        }

        // Update is called once per frame

        public void UpdateX(float dataX)
        {
            _dataToSend.x = dataX;
            Remote();
        }
        public void UpdateY(float dataY)
        {
            _dataToSend.y = dataY;
            Remote();
        }
        private void Remote()
        {
            if (_remoteController != null)
            {
                _remoteController.SendData(_dataToSend);
                Debug.Log("Sending Data (" + _dataToSend + ") to Controller");
            }
            else
            {
                Debug.LogWarning("No RemoteController component in game object");
            }
            
        }
    }
}
