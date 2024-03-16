using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    private Image cursorImage;

    private void Start()
    {
        Cursor.visible = false;
        cursorImage = GetComponent<Image>();
    }

    private void Update()
    {
       cursorImage.rectTransform.position = Input.mousePosition;
    }
}
