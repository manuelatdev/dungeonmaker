using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : BaseEntity
{

    [SerializeField]
    private GameObject outline;
    private float timeMouseOver=0;
    private bool descriptionOn;
   

    public override void TakeAttack(int damage)
    {
        base.TakeAttack(damage);

    }
    public override void ResetEntity()
    {
        base.ResetEntity();
        timeMouseOver = 0;
        descriptionOn = false;
        outline.SetActive(false);
    }
        private void OnMouseEnter()
    {
        if (!SelectorScript.movingObject&&ScriptGameManager.gameMode != ModoJuego.Menu)
        {
            outline.SetActive(true);
        }

    }
    private void OnMouseOver()
    {
        if (!SelectorScript.movingObject && ScriptGameManager.gameMode != ModoJuego.Menu)
        {
            timeMouseOver += Time.deltaTime;
        }
        if (timeMouseOver > 0.5f&&!descriptionOn)
        {
            DesplegablesScript.ShowChestDescription("+"+gold);
            descriptionOn = true;
        }
    }
    private void OnMouseExit()
    {
        descriptionOn = false;
        timeMouseOver = 0;
        outline.SetActive(false);
        DesplegablesScript.HideAllDescriptions();
    }
}
