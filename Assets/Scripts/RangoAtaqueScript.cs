using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoAtaqueScript : MonoBehaviour
{
    ScriptMovimientoHeroe heroeScript;
    public Animator animatorHero;
    private bool atacando;
    private void Start()
    {
        heroeScript = GetComponentInParent<ScriptMovimientoHeroe>();
    }
    private void OnTriggerStay2D(Collider2D collision)

    {
        if (!atacando)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                animatorHero.SetBool("Atack", true);
                atacando = true;
                print("empiezo a atacar");

            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest"))
            {
                animatorHero.SetBool("Atack", true);
                atacando = true;
                print("empiezo a atacar");


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
                print("dejo de tocar"+collision.gameObject.name);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest"))
            {
                atacando = false;

                animatorHero.SetBool("Atack", false);
                print("dejo de tocar" + collision.gameObject.name);

            }
        }
    }
}
