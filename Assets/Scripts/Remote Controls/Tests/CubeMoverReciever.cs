using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb.RemoteControls
{
    public class CubeMoverReciever : MonoBehaviour
    {
        [SerializeField] Vector4 cubeRange = Vector4.zero;
        [SerializeField] bool lockXAxis = false;
        [SerializeField] bool lockYAxis = false;

        public void MoveCube(Vector2 data)
        {
            //Debug.Log("Data Recived : " + data);
            // Convert normalized data to the cube Range coordinates
            float newX = lockXAxis ? 0 :cubeRange.x + data.x * (cubeRange.y - cubeRange.x);
            float newY = lockYAxis ? 0 : cubeRange.z + data.y * (cubeRange.w - cubeRange.z);

  
            Vector3 newPosition = new Vector3(newX, newY, 0f);


            transform.localPosition = newPosition;
        }
    }
}
