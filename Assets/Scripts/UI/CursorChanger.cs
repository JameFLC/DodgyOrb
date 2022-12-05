using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class CursorChanger : MonoBehaviour
    {
        // Parameters
        [SerializeField] private Texture2D defaultCursor;
        [SerializeField] private Texture2D hoverCursor;
        [SerializeField] private bool disableCursorForTouchBuild = false;

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
        void Start()
        {
            Cursor.visible = !disableCursorForTouchBuild;
            SetCursor(defaultCursor);
        }

        public void SetCursorToDefault() => SetCursor(defaultCursor);

        public void SetCursorToHover() => SetCursor(hoverCursor);

        private void SetCursor(Texture2D newCursor)
        {
            Vector2 cursorOffset = new Vector2(newCursor.width / 2, newCursor.height / 2);
            
            Cursor.SetCursor(newCursor, cursorOffset, CursorMode.ForceSoftware);
        }
    }
}
