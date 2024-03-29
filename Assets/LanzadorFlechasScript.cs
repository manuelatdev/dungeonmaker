using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzadorFlechasScript : MonoBehaviour
{
    [SerializeField]
    private GameObject lanzador;
    [SerializeField]
    private GameObject[] flechas;
    private int numeroFlecha;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        numeroFlecha = 0;
    }
    private void Update()
    {
        if (ScriptGameManager.gameMode != ModoJuego.Play)
        {
            anim.SetBool("Attack", false);
        }
       else
        {
            anim.SetBool("Attack", true);
        }
    }

    public void LanzarFlechas()
    {
        flechas[numeroFlecha].SetActive(true);
        flechas[numeroFlecha].transform.position = lanzador.transform.position;
        numeroFlecha++;
        if (numeroFlecha==flechas.Length)
        {
            numeroFlecha = 0;
        }
        AudioManagerScript.archerCharge.Play();
        
    }


}

