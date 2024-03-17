using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorScript : MonoBehaviour
{
    private Animator anim;
    private AudioSource attackSound;
    private BasicEnemy enemyScript;
    private HeroScript scriptHero;

    void Start()
    {
        scriptHero = FindObjectOfType<HeroScript>();
        enemyScript = GetComponentInParent<BasicEnemy>();
        attackSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0); //obtiene el estado actual de la animación
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f)); //inicia la animación en un momento aleatorio
    }
    public void AttackToHero()
    {
        attackSound.Play();
        scriptHero.TakeAttack(enemyScript.getDamage());

    }
}
