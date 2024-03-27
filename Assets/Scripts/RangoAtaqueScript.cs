using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoAtaqueScript : MonoBehaviour
{
    public Animator animatorHero;
    private ScriptMovimientoHeroe heroScript;
    public bool atacando;
    public static bool victory;
    [SerializeField]
    private VictoryScript scriptVictory;

    private void Start()
    {
        heroScript = GetComponentInParent<ScriptMovimientoHeroe>();
        victory = false;
    }

    private void OnTriggerStay2D(Collider2D collision)

    {
        if (!atacando && ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (collision.gameObject == heroScript.targetActual)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")|| collision.gameObject.layer == LayerMask.NameToLayer("Chest"))
                {
                    animatorHero.SetBool("Attack", true);
                    atacando = true;
                }
                

            }
            
        }
        if (!victory&&ScriptGameManager.gameMode == ModoJuego.Play&&collision.gameObject == heroScript.targetActual&& collision.gameObject.layer == LayerMask.NameToLayer("Exit"))
        {
            ScriptGameManager.gameMode = ModoJuego.Menu;
            scriptVictory.gameObject.SetActive(true);
            scriptVictory.GoVictory();
            scriptVictory.GetStats();
            heroScript.StopHero();
            victory = true;
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
