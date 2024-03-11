using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorScript : MonoBehaviour
{
    private bool seleccionado;
    [SerializeField]
    private Image outLine;
    [SerializeField]
    private GameObject prefab;
    private GameObject objetoInstanciado;

    private void Update()
    {
        if (seleccionado && objetoInstanciado != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            objetoInstanciado.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        }
    }

    public void ClickIn()
    {
        seleccionado = true;
    }
    public void ClickOut()
    {
        seleccionado = false;
        objetoInstanciado = null;
    }
    public void MouseExit()
    {
        outLine.gameObject.SetActive(false);
        if (seleccionado && !objetoInstanciado && ScriptGameManager.gameMode == ModoJuego.Edit)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Instancia el prefab en la posición del ratón
            objetoInstanciado = Instantiate(prefab, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
          
        }

    }
    public void MouseEnter()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Edit)
        {
            outLine.gameObject.SetActive(true);

        }
    }
   
}
