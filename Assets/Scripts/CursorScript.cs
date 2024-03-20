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

    private void Start()
    {
        Cursor.visible = false;
        stone = stoneRef;
        denied = deniedRef;
        cursor = cursorRef;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
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
