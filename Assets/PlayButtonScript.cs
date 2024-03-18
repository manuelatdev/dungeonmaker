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

    private void Start()
    {
        playButton = GetComponent<Image>();
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
