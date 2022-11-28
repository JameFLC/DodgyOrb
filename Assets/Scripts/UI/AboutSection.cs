using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class AboutSection : MonoBehaviour
    {
        [SerializeField] private CanvasGroup aboutGroup;
        [SerializeField] private CanvasGroup toolbarGroup;
        [SerializeField] private MeshRenderer blurRenderer;
        // Start is called before the first frame update
        void Start()
        {
            SetAboutSectionVisibility(false);
        }

        public void SetAboutSectionVisibility(bool visible)
        {
            HandleCanvas(aboutGroup, visible, new Vector2(0f,0.75f));
            HandleCanvas(toolbarGroup, !visible, new Vector2(0f,1f));

            HandleBlur(visible);
        }
        private void HandleBlur(bool visible)
        {
            blurRenderer.material.SetFloat("_Alpha", visible ? 1f : 0f);
        }

        private void HandleCanvas(CanvasGroup canvas,bool visible, Vector2 alphaStates)
        {
            canvas.alpha = visible ? alphaStates[1] : alphaStates[0];
            canvas.blocksRaycasts = visible;
            canvas.interactable = visible;
        }
    }
}
