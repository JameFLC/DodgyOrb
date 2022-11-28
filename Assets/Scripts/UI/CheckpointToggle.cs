
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DodgyOrb
{
    [RequireComponent(typeof(Image))]
    public class CheckpointToggle : MonoBehaviour
    {
        // Parameters
        [SerializeField] private Sprite activatedSprite;
        [SerializeField] private Sprite deactivatedSprite;
        [SerializeField] private CheckpointTrigger checkpointTrigger;

        // Variables

        private Image _checkpointImage;
        public bool thereIsTimer;
        
        // Start is called before the first frame update
        void Start()
        {
            _checkpointImage = GetComponent<Image>();
            UpdateCheckpoints(checkpointTrigger.GetUseCheckpoints());
        }

        public void ToggleCheckpoints()
        {
            UpdateCheckpoints(!checkpointTrigger.GetUseCheckpoints());
        }

        private void UpdateCheckpoints(bool useCheckpoints)
        {
            UpdateToggleSprite(useCheckpoints);
            CheckpointManager.instance.ResetCheckpoints();
            checkpointTrigger.SetUseCheckpoint(useCheckpoints);
        }

        private void UpdateToggleSprite(bool isCheckpointActivated)
        {
            if (isCheckpointActivated)
            {
                _checkpointImage.sprite = activatedSprite;
                thereIsTimer = false;
            }
            else
            {
                _checkpointImage.sprite = deactivatedSprite;
                thereIsTimer = true;
            }
        }

    }
}
