using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnClick(Vector2 coordinate);

public class OnClickChecker
{
    public void checkClick(OnClick onClick) {
        if (Input.GetButtonDown("Fire1")) {
            onClick(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
