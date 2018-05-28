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
            if(_isGameplay)
                Cursor.SetCursor(GameplayCursorTexture, Vector2.zero, CursorMode.Auto);
            else
                Cursor.SetCursor(MenuCursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}

