using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    public Texture2D cursorTexture;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
