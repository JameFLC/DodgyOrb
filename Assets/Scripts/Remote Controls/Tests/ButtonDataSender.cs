using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb.RemoteControls
{
    public class ButtonDataSender : MonoBehaviour
    {
        private RemoteController _remoteController;
        private Vector2 _dataToSend = new Vector2(0.5f, 0.5f);
        // Start is called before the first frame update
        void Start()
        {
            _remoteController = GetComponent<RemoteController>();
        }

        public void SendData()
        {
            _remoteController.SendData(new Vector2(0, 0));
        }
    }
}
