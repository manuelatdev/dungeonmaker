using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashScript : MonoBehaviour
{
    public static bool mouseOnTrash;
    private static Image trashImg;

    private void Start()
    {
        trashImg = GetComponent<Image>();
    }
    public void MouseOnTrash(bool value)
    {
        mouseOnTrash = value;
    }
    public static void ShowTrash(bool value)
    {
        trashImg.enabled = value;
    }
}
