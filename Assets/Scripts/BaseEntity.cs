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

    protected ParticleSystem particleEmitter;

    [SerializeField]
    protected GameObject[] disabledOnDead;

    protected AudioSource dieSound;

    protected Collider2D meCollider;

    protected int initialHealth;

    private bool creatingSelected;

    private Color originalColor;

    private SpriteRenderer renderer;

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

    public bool IsCreable()
    {
        if (renderer.color != Color.red)
        {
            return true;
        }

        return false;
    }

    protected virtual void Die()
    {
        dieSound.Play();
        OnDie?.Invoke(gameObject);

        foreach (GameObject obj in disabledOnDead)
        {
            obj.SetActive(false);
        }

        meCollider.enabled = false;

        Destroy(gameObject, 3f);
    }

    void Update()
    {

    }

    public void SetSelected(bool selected)
    {
        Debug.Log("Selectedchanged to" + selected.ToString());
        creatingSelected = selected;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (creatingSelected && renderer.color != Color.red)
        {
            
            originalColor = renderer.color;
            renderer.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(creatingSelected)
        {
            renderer.color = originalColor;
        }
    }


    
    // Start is called before the first frame update
    public virtual void Start()
    {
        dieSound = GetComponent<AudioSource>();
        particleEmitter = GetComponent<ParticleSystem>();
        meCollider = GetComponent<Collider2D>();
    }

    private void Awake()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();

    }

}
