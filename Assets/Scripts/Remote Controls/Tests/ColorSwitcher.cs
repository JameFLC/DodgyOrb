using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb.RemoteControls
{
    public class ColorSwitcher : MonoBehaviour
    {
        [SerializeField] List<Material> materials;

        private Vector2 _previousData = Vector2.zero;
        private int currentMaterial = 0;
        // Start is called before the first frame update

        public void ReceiveData(Vector2 data)
        {
            if (materials.Count != 0)
            {
                SwitchColors();
                _previousData = data;
            }
        }
        private void SwitchColors()
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = materials[currentMaterial];
                currentMaterial = (currentMaterial + 1) % materials.Count;
            }
        }
    }
}
