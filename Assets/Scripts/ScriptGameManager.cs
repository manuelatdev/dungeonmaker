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

    public static int health=10;
    public static int maxHealth=10;
    public static int level=1;
    public static int exp=0;
    public static int gold=222;
    public static int attack=4;
    public static int def=0;
    public static int speed=1;
    public static int shopAttack=0;
    public static int shopSpeed = 0;
    public static int shopArmor = 0;


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

