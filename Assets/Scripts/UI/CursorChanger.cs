using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class CursorChanger : MonoBehaviour
    {
        // Parameters
        [SerializeField] Texture2D defaultCursor;
        [SerializeField] Texture2D hoverCursor;

        // Variables
        [HideInInspector] public static CursorChanger instance;

        private void Awake()
        {
            // Singelton Pattern
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }
        // Start is called before the first frame update
        void Start() => SetCursor(defaultCursor);

        public void SetCursorToDefault() => SetCursor(defaultCursor);

        public void SetCursorToHover() => SetCursor(hoverCursor);

        private void SetCursor(Texture2D newCursor)
        {
            Cursor.visible = false;
            Vector2 cursorOffset = new Vector2(newCursor.width / 2, newCursor.height / 2);

            Cursor.SetCursor(newCursor, cursorOffset, CursorMode.Auto);
        }
    }
}
