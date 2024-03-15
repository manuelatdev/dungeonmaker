using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModoJuego
{
    Play,
    Edit,
    Menu
}
public class ScriptGameManager : MonoBehaviour
{

    public static ModoJuego gameMode;
    [SerializeField]
    private Texture2D cursorTexture;
    private void Start()
    {
        gameMode = ModoJuego.Edit;

        Vector2 hotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);

        // Establece el modo del cursor en Auto
        CursorMode cursorMode = CursorMode.Auto;

        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

    }
}

