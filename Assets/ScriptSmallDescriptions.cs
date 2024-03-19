using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScriptSmallDescriptions : MonoBehaviour
{
    
    [SerializeField]
    private string descriptionText;
    private float mouseOverTime;
    private bool descriptionOn;
    private bool isMouseOver;
    private void Update()
    {
        if (isMouseOver)
        {
            if (!descriptionOn && !SelectorScript.movingObject)
            {
                mouseOverTime += Time.deltaTime;
                if (mouseOverTime > 0.6f)
                {
                    DesplegablesScript.ShowDescription(descriptionText);
                    descriptionOn = true;
                }
            } 
        }
    }
    public void PointerEnter()
    {
        isMouseOver = true;
    }

    public void PointerExit()
    {
        isMouseOver = false;
        mouseOverTime = 0;
        descriptionOn = false;
        DesplegablesScript.HideAllDescriptions();
        
    }

 
    
    
}
