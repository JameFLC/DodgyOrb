using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DodgyOrb
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasAlphaFader : MonoBehaviour
    {
        // Parameters
        [SerializeField] private float fadeInSpeed = 2;
        [SerializeField] private float fadeOutSpeed = 10;
        
        // Variables
        private float _currentAlpha = 0f;
        private float _targetAlpha = 0f;
        private const float _updateThreshold = 0.1f;
        private CanvasGroup _canvasGroup;
        private bool _isAlphaSnapped = false;
        
        // Start called before first frame
        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = _targetAlpha;
        }
        // Update is called once per frame
        void Update()
        {
            if (Mathf.Abs(_currentAlpha - _targetAlpha) > _updateThreshold)
            {
                float fadeSpeed = _currentAlpha > _targetAlpha ? fadeOutSpeed : fadeInSpeed;

                _currentAlpha = Mathf.Lerp(_canvasGroup.alpha,_targetAlpha, Time.deltaTime * fadeSpeed);

                _canvasGroup.alpha = _currentAlpha;

                _isAlphaSnapped = false;
            }
            else if (!_isAlphaSnapped)
            {
                _canvasGroup.alpha = _targetAlpha;
                _isAlphaSnapped = true;
            }
        }
        // Public fading functions
        public void FadeIn() => FadeTo(1);
        public void FadeOut() => FadeTo(0); 
        public void FadeTo(float alpha) => _targetAlpha = alpha;
    }
}
