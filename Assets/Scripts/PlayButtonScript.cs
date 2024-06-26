using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField]
    private ScriptMovimientoHeroe heroMoveScript;
    
    private Image playButton;
    [SerializeField]
    private Sprite playSprite;
    [SerializeField]
    private Sprite stopSprite;

    private Shadow sombra;

    [SerializeField]
    private ScreenEffectAnimator screenEffectScript;

    private ScriptCamera cameraScript;

    private ModoJuego currentGameMode;
    private void Start()
    {
        cameraScript = Camera.main.GetComponent<ScriptCamera>();
        playButton = GetComponent<Image>();
        sombra = GetComponent<Shadow>();
        currentGameMode = ModoJuego.Edit;
    }

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
    public void ResetLevel()
    {
        
            ScriptGameManager.gameMode = ModoJuego.Edit;
            heroMoveScript.GoStopMode();
            DestroyAllEnemys();
            playButton.sprite = playSprite;
            EnergyScript.ResetEnergy();
        ScriptTimeX2.VolverTiempoAnterior();

    }
    public void ShadowState(bool estado)
    {
        sombra.enabled = estado;
    }
    public void GoPlay()
    {
        ScriptGameManager.gameMode = ModoJuego.Play;
        heroMoveScript.GoPlayMode();
        playButton.sprite = stopSprite;

    }
    public void SetCurrentMode()
    {
        currentGameMode = ScriptGameManager.gameMode;
    }
    public void GoStop()
    {
        
        ScriptGameManager.gameMode = ModoJuego.Edit;
        ResetAllEntitys();
        heroMoveScript.GoStopMode();
        RangoAtaqueScript.victory = false;
        playButton.sprite = playSprite;
        ScriptTimeX2.VolverTiempoAnterior();


    }
    public void GoStopFromDead()
    {
        GoStop();
        screenEffectScript.anim.SetTrigger("ResetScreen");
        cameraScript.ResetCamera();
        AudioManagerScript.music.Play();
        ScriptTimeX2.VolverTiempoAnterior();

    }
    public void GoMenuMode()
    {
        currentGameMode = ScriptGameManager.gameMode;
        ScriptGameManager.gameMode = ModoJuego.Menu;
        Time.timeScale = 0;
    }
    public void GoCurrentMode()
    {

        ScriptGameManager.gameMode = currentGameMode;
        ScriptTimeX2.VolverTiempoAnterior();

    }

    private void DestroyAllEnemys()
    {
        BaseEntity[] enemys = GameObject.FindObjectsOfType<BaseEntity>();

        // Itera sobre cada entidad y llama a ResetEnemy
        foreach (BaseEntity enemy in enemys)
        {
            if (!enemy.gameObject.CompareTag("Cofre"))
            {
                Destroy(enemy.gameObject);

            }
            else
            {
                enemy.ResetEntity();
            }
        }
        BasicEnemy.wizardsInGame = 0;
    }
    private void ResetAllEntitys()
    {
        BaseEntity[] entities = GameObject.FindObjectsOfType<BaseEntity>();

        // Itera sobre cada entidad y llama a ResetEnemy
        foreach (BaseEntity entity in entities)
        {
            entity.ResetEntity();
        }
    }
}
