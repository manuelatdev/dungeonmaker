using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [SerializeField]
    private static GameObject cursor;

    [SerializeField]
    private  GameObject cursorRef;

    private static GameObject stone;

    [SerializeField]
    private GameObject stoneRef;

    public static GameObject denied;

    [SerializeField]
    private GameObject deniedRef;

    private RectTransform rect;

    [SerializeField]
    private Camera canvasCamera;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        Cursor.visible = false;
        stone = stoneRef;
        denied = deniedRef;
        cursor = cursorRef;
    }

    private void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), Input.mousePosition, canvasCamera, out pos); transform.position = rect.TransformPoint(pos);
    }
    public static void SwitchStone(bool estado)
    {
        stone.SetActive(estado);
    }
    public static void SwitchDenied(bool estado)
    {

        denied.SetActive(estado);
        cursor.SetActive(!estado);

    }



}
