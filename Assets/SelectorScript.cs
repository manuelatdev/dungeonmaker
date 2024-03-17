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
    private Animator anim;
    [SerializeField]
    private Animator panelAnim;

    [SerializeField]
    private AudioSource cardSound;
    private static bool instanciado;
    private Vector3 initialMousePosition;


    private void Start()
    {
        panelAnim = transform.parent.GetComponent<Animator>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (seleccionado && objetoInstanciado != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            objetoInstanciado.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);

        }
        if (seleccionado && !instanciado)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float dragDistance = Vector3.Distance(initialMousePosition, currentMousePosition);
            if (dragDistance >= 20f)
            {
                if (!objetoInstanciado && ScriptGameManager.gameMode == ModoJuego.Edit)
                {
                    instanciado = true;
                    Vector3 mousePosition = Input.mousePosition;
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                    // Instancia el prefab en la posición del ratón
                    objetoInstanciado = Instantiate(prefab, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
                    objetoInstanciado.GetComponent<BaseEntity>().SetSelected(true);
                    objetoInstanciado.GetComponent<BasicEnemy>().SpriteLayerUp();
                    print("layerlevantada");
                    anim.SetBool("MouseIn", false);
                    panelAnim.SetBool("PanelOut", true);

                }
            }

        }


    }


    public void ClickIn()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Edit)
        {
            seleccionado = true;
            initialMousePosition = Input.mousePosition;
        }
    }
    public void ClickOut()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Edit)
        {
            if (objetoInstanciado != null)
            {
                if (!objetoInstanciado.GetComponent<BasicEnemy>().IsCreable())
                {
                    Destroy(objetoInstanciado);
                }


                objetoInstanciado.GetComponent<BaseEntity>().SetSelected(false);
                objetoInstanciado.GetComponent<BasicEnemy>().SpriteLayerDown();
                print("layerBajada");

                objetoInstanciado = null;
            }
            seleccionado = false;
            instanciado = false;

        }

    }
    public void MouseExit()
    {
        // outLine.gameObject.SetActive(false);
        if (!instanciado)
        {
            anim.SetBool("MouseIn", false);
            panelAnim.SetBool("PanelOut", true);
            if (ScriptGameManager.gameMode == ModoJuego.Edit)
            {
                CursorScript.SwitchStone(false);
            }

        }



    }
    public void MouseEnter()
    {

        //outLine.gameObject.SetActive(true);
        if (!instanciado)
        {
            cardSound.pitch = Random.Range(1f, 1.2f);
            cardSound.Play();
            anim.SetBool("MouseIn", true);
            panelAnim.SetBool("PanelOut", false);
            transform.SetAsLastSibling();
            if (ScriptGameManager.gameMode == ModoJuego.Edit)
            {
                CursorScript.SwitchStone(true);

            }
        }

    }

}
