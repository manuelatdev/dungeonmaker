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
        anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0); //obtiene el estado actual de la animaci�n
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f)); //inicia la animaci�n en un momento aleatorio
        SetAudioRef();
    }
    private void SetAudioRef()
    {
        switch (enemyScript.enemyClass)
        {
            case BasicEnemy.enemyType.slime:
                attackSound = AudioManagerScript.slimeAttack;
                break;
            case BasicEnemy.enemyType.skull:
                attackSound = AudioManagerScript.skullAttack;

                break;
            case BasicEnemy.enemyType.archer:
                attackSound = AudioManagerScript.archerAttack;

                break;
            case BasicEnemy.enemyType.wizard:
                attackSound = AudioManagerScript.wizardAttack;

                break;

        }
    }
    public void AttackToHero()
    {
        attackSound.Play();
        scriptHero.TakeAttack(enemyScript.getDamage());

    }
}