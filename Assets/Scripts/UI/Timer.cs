using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DodgyOrb
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float time = 0;
        [SerializeField] private CheckpointToggle CheckpointToggle;

        private TextMeshProUGUI displayTMP;
        private bool isPause;
        [HideInInspector] public int number_click = 0;


        // Start is called before the first frame update
        void Start()
        {
            displayTMP = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (CheckpointToggle.thereIsTimer)
            {

                displayTMP.enabled = true;
            }
            else
            {
                pauseTimer();
                time = 0;
                displayTMP.enabled = false;
            }


            if (!isPause)
            {
                time += Time.deltaTime;
            }
                

            displayTMP.text = string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60);
        }

        public void RestartTimer()
        {
            time = 0;
            isPause = false;
            number_click = 0;
        }

        public void pauseTimer() => isPause = true;

    }
}
