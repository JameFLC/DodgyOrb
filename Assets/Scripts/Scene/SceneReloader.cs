using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DodgyOrb
{
    public class SceneReloader : MonoBehaviour
    {


        // Update is called once per frame
        void Update()
        {
            if (!Debug.isDebugBuild)
                return;
            if (Input.GetKey(KeyCode.Tab))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
