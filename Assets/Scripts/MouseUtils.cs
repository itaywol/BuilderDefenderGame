using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUtils
{
    // Start is called before the first frame update
    public static Vector3 GetMouseWorldPosition() {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
