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

    [SerializeField]
    private int energyValue;
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
    public static bool movingObject;
    private Vector3 initialMousePosition;
    private BasicEnemy enemyScript;

    


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
        if (seleccionado && !movingObject)
        {
            
            Vector3 currentMousePosition = Input.mousePosition;
            float dragDistance = Vector3.Distance(initialMousePosition, currentMousePosition);
            if (dragDistance >= 20f)
            {
                if (!objetoInstanciado && ScriptGameManager.gameMode == ModoJuego.Edit &&EnergyScript.UseEnergy(-energyValue))
                {
                    AudioManagerScript.cardTaken.Play();
                    movingObject = true;
                    Vector3 mousePosition = Input.mousePosition;
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                    // Instancia el prefab en la posici�n del rat�n
                    objetoInstanciado = Instantiate(prefab, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
                    enemyScript = objetoInstanciado.GetComponent<BasicEnemy>();

                    enemyScript.SetSelected(true);
                    enemyScript.SpriteLayerUp();
                    enemyScript.ActivateOutline(true);
                    anim.SetBool("MouseIn", false);
                    panelAnim.SetBool("PanelOut", true);
                    TrashScript.ShowTrash(true);


                }
                else
                {
                    seleccionado = false;
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
                if (!enemyScript.IsCreable())
                {
                    EnergyScript.UseEnergy(energyValue);
                    Destroy(objetoInstanciado);
                    if (TrashScript.mouseOnTrash)
                    {
                        enemyScript.dieSound.pitch = Random.Range(1f, 1.2f);

                        enemyScript.dieSound.Play();
                    }
                }
                else
                {
                    AudioManagerScript.cardPlaced.Play();

                    enemyScript.ActualizarCurrentPosition();
                    enemyScript.ActivateOutline(false);

                    enemyScript.SetSelected(false);
                    enemyScript.SpriteLayerDown();
                }

                
                enemyScript = null;
                objetoInstanciado = null;
            }
            TrashScript.ShowTrash(false);
            seleccionado = false;
            movingObject = false;

        }

    }
    public void MouseExit()
    {
        // outLine.gameObject.SetActive(false);
        if (!movingObject)
        {
            anim.SetBool("MouseIn", false);
            panelAnim.SetBool("PanelOut", true);
            if (ScriptGameManager.gameMode == ModoJuego.Edit)
            {
                CursorScript.SwitchStone(false);
            }

        }



    }
    public void MouseOver()
    {
        if (!movingObject)
        {
            anim.SetBool("MouseIn", true);
            panelAnim.SetBool("PanelOut", false);
            CursorScript.SwitchStone(true);
        }
    }
    public void MouseEnter()
    {

        //outLine.gameObject.SetActive(true);
        if (!movingObject)
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
