using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BaseEntity : MonoBehaviour
{
    public event Action<GameObject> OnDie;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected int gold;
    [SerializeField]
    protected ParticleSystem particleEmitter;

    protected AudioSource dieSound;

    protected Collider2D meCollider;

    protected int initialHealth;
    public virtual void TakeAttack(int damage)
    {
        health -= damage;

        particleEmitter.Play();
        if (health <= 0)
        {
            Die();
        }
    }

    public int getGold()
    {
        return gold;
    }

    protected virtual void Die()
    {
        dieSound.Play();
        OnDie?.Invoke(gameObject);

        GetComponent<SpriteRenderer>().enabled = false;

        meCollider.enabled = false;

        Destroy(gameObject, 3f);
    }

    void Update()
    {

    }

    public int getHeath()
    {
        return this.health;
    }



    // Start is called before the first frame update
    public virtual void Start()
    {
        dieSound = GetComponent<AudioSource>();
        particleEmitter = GetComponent<ParticleSystem>();
        meCollider = GetComponent<Collider2D>();
    }

}
