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
    [SerializeField]
    protected ParticleSystem particleEmitter;

    [SerializeField]
    protected GameObject[] disabledOnDead;
    [HideInInspector]
    public AudioSource dieSound;

    protected Collider2D meCollider;

    protected int totalHealth;

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
    public virtual void ResetEntity()
    {

        foreach (GameObject obj in disabledOnDead)
        {
            obj.SetActive(true);
        }
        health = totalHealth;

        meCollider.enabled = true;

    }









    // Start is called before the first frame update
    public virtual void Start()
    {
        dieSound = GetComponent<AudioSource>();
        if (particleEmitter == null)
        {
            particleEmitter = GetComponent<ParticleSystem>();

        }
        meCollider = GetComponent<Collider2D>();
        scriptHero = FindAnyObjectByType<HeroScript>();
    }

    

}
