using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;
        transform.position = Vector3.Lerp(transform.position, mousePosition,100f);
    }
}
