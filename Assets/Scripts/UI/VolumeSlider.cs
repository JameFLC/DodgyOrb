using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DodgyOrb
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private string parameterName = "MasterVolume";
        // Start is called before the first frame update

        public void SetMixerVolume(float value)
        {
            mixer.SetFloat(parameterName, Mathf.Log10(value) * 20);
        }
        
    }
}
