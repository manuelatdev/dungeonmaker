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
    private GameObject menuESC;

    [SerializeField]
    private PlayButtonScript playScript;
   
    private void Start()
    {
        gameMode = ModoJuego.Edit;
    }
    public static void ExitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuESC.activeSelf && gameMode != ModoJuego.Menu)
            {
                menuESC.SetActive(true);
                Time.timeScale = 0;
                playScript.SetCurrentMode();
                gameMode = ModoJuego.Menu;
                AudioManagerScript.click.Play();
            }
            else if (menuESC.activeSelf)
            {
                menuESC.SetActive(false);
                Time.timeScale = 1;
                playScript.GoCurrentMode();
                AudioManagerScript.click.Play();
            }
        }
    }
}

