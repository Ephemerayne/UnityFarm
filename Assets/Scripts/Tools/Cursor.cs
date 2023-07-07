using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Texture2D cursorTexture;

    private void Start()
    {
        UnityEngine.Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
