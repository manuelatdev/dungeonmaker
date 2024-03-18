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

    private void Start()
    {
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
