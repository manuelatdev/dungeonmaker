using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScriptSmallDescriptions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        mouseOverTime = 0;
        descriptionOn = false;
        DesplegablesScript.HideAllDescriptions();
        
    }

 
    
    
}
