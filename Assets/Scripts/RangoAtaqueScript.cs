using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoAtaqueScript : MonoBehaviour
{
    public Animator animatorHero;
    private ScriptMovimientoHeroe heroScript;
    public bool atacando;

    private void Start()
    {
        heroScript = GetComponentInParent<ScriptMovimientoHeroe>();
    }

    private void OnTriggerStay2D(Collider2D collision)

    {
        if (!atacando && ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (collision.gameObject == heroScript.targetActual)
            {
                animatorHero.SetBool("Attack", true);
                atacando = true;

            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (atacando)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                atacando = false;
                animatorHero.SetBool("Attack", false);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest"))
            {
                atacando = false;

                animatorHero.SetBool("Attack", false);

            }
        }
    }
}
