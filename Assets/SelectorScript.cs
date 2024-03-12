using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
        if (ScriptGameManager.gameMode == ModoJuego.Edit)
        {
            if (!objetoInstanciado.GetComponent<BaseEntity>().IsCreable())
            {
                Destroy(objetoInstanciado);
            }

            objetoInstanciado.GetComponent<BaseEntity>().SetSelected(false);
            objetoInstanciado.GetComponent<BaseEntity>().SpriteLayerDown();
            objetoInstanciado = null;
            seleccionado = false;
        }

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
            objetoInstanciado.GetComponent<BaseEntity>().SetSelected(true);
            objetoInstanciado.GetComponent<BaseEntity>().SpriteLayerUp();
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
