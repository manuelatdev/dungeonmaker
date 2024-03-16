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
   
    private void Start()
    {
        gameMode = ModoJuego.Edit;
    }
}

