using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class CursorController : MonoBehaviour
    {
        public Texture2D MenuCursorTexture;
        public Texture2D GameplayCursorTexture;

        public void SetCursor(bool _isGameplay)
        {
            if (_isGameplay)
            {
                Vector3 cursorHotspot = new Vector2(GameplayCursorTexture.width / 2, GameplayCursorTexture.height / 2);
                Cursor.SetCursor(GameplayCursorTexture, cursorHotspot, CursorMode.Auto);
            }
            else
                Cursor.SetCursor(MenuCursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}

