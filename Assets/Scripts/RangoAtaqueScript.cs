using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoAtaqueScript : MonoBehaviour
{
    ScriptMovimientoHeroe heroeScript;
    public Animator animatorHero;
    public bool atacando;
    private void Start()
    {
        heroeScript = GetComponentInParent<ScriptMovimientoHeroe>();
    }
    private void OnTriggerStay2D(Collider2D collision)

    {
        if (!atacando && ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                animatorHero.SetBool("Atack", true);
                atacando = true;

            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest"))
            {
                animatorHero.SetBool("Atack", true);
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
                animatorHero.SetBool("Atack", false);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest"))
            {
                atacando = false;

                animatorHero.SetBool("Atack", false);

            }
        }
    }
}
