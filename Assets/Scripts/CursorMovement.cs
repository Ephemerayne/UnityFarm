using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{

    public Texture2D cursorTexture;
    private Vector2 mousePosition;
    /*Vector2 mousePosition = Input.mousePosition;

     Vector2 pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);*/

    private void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        transform.position = mousePosition;

        if (Input.GetButtonDown("Fire1"))
        {
            // Debug.Log(mousePosition.x);
            //  Debug.Log(mousePosition.y);
            Debug.Log(Camera.main.ScreenToWorldPoint(mousePosition));
        }     
    }

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
