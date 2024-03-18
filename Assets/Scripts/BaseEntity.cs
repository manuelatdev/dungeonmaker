using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BaseEntity : MonoBehaviour
{

    [SerializeField]
    protected int health;

    [SerializeField]
    protected int gold;

    protected ParticleSystem particleEmitter;

    [SerializeField]
    protected GameObject[] disabledOnDead;

    protected AudioSource dieSound;

    protected Collider2D meCollider;

    protected int initialHealth;

    protected HeroScript scriptHero;
    

    

    

    public int getGold()
    {
        return gold;
    }
    public int getHeath()
    {
        return this.health;
    }
    public virtual void TakeAttack(int damage)
    {
        health -= damage;

        particleEmitter.Play();
        if (health <= 0)
        {
            Die();
        }
    }

    

    protected virtual void Die()
    {
        dieSound.Play();
        scriptHero.OnEnemyDied(null,this);

        foreach (GameObject obj in disabledOnDead)
        {
            obj.SetActive(false);
        }

        meCollider.enabled = false;

    }

   

    

    


    
    // Start is called before the first frame update
    public virtual void Start()
    {
        dieSound = GetComponent<AudioSource>();
        particleEmitter = GetComponent<ParticleSystem>();
        meCollider = GetComponent<Collider2D>();
        scriptHero = FindAnyObjectByType<HeroScript>();
    }

    

}
