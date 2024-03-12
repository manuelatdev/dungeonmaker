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
    private bool isTouching;

    private void Update()
    {
        if (seleccionado && objetoInstanciado != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            objetoInstanciado.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);

            //RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            //if (hit.collider != null)
            //{
            //    // Debug.Log(hit.collider.gameObject.name);
            //    if (objetoInstanciado.GetComponent<Collider2D>().IsTouching(hit.collider))
            //    {
                   
            //        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            //    }
            //} else
            //{
            //    Debug.Log("Tocoto");
            //}






        }


    }

    public void Touching()
    {
        isTouching = true;
    }
    public void ClickIn()
    {
        seleccionado = true;
    }
    public void ClickOut()
    {
        if(ScriptGameManager.gameMode == ModoJuego.Edit)
        {
            if (!objetoInstanciado.GetComponent<BaseEntity>().IsCreable())
            {
                Destroy(objetoInstanciado);
            }

            objetoInstanciado.GetComponent<BaseEntity>().SetSelected(false);
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
            Debug.Log("Instantiate");
            objetoInstanciado.GetComponent<BaseEntity>().SetSelected(true);
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
