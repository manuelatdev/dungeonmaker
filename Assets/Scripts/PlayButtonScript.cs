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
    private void Start()
    {
        cameraScript = Camera.main.GetComponent<ScriptCamera>();
        playButton = GetComponent<Image>();
        sombra = GetComponent<Shadow>();
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
    public void GoStop()
    {
        ScriptGameManager.gameMode = ModoJuego.Edit;
        ResetAllEntitys();
        heroMoveScript.GoStopMode();
        playButton.sprite = playSprite;
       

    }
    public void GoStopFromDead()
    {
        GoStop();
        screenEffectScript.anim.SetTrigger("ResetScreen");
        cameraScript.ResetCamera();
        AudioManagerScript.music.Play();
        Time.timeScale = 1;

    }

    private void DestroyAllEnemys()
    {
        BasicEnemy[] enemys = GameObject.FindObjectsOfType<BasicEnemy>();

        // Itera sobre cada entidad y llama a ResetEnemy
        foreach (BaseEntity enemy in enemys)
        {
            Destroy(enemy.gameObject);
        }
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
