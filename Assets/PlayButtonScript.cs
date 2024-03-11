using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField]
    private ScriptMovimientoHeroe heroMoveScript;
    [SerializeField]

    private TextMeshProUGUI goText;

    public void preshButton()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Play)
        {
            GoStop();
        }
        else if (ScriptGameManager.gameMode == ModoJuego.Edit)
        {
            GoPlay();
        }
    }
    public void GoPlay()
    {
        ScriptGameManager.gameMode = ModoJuego.Play;
        heroMoveScript.GoPlayMode();
        goText.text = "Stop";
    }
    public void GoStop()
    {
        ScriptGameManager.gameMode = ModoJuego.Edit;

        heroMoveScript.GoStopMode();
        goText.text = "GO !!";

    }
}
