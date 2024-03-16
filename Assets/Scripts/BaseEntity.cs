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

    protected bool creatingSelected;

    [SerializeField]
    private Color originalColor;

    [SerializeField]
    private SpriteRenderer[] renderers;

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
        if (renderers[0].color != Color.red)
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

    public void SpriteLayerUp()
    {
        foreach (SpriteRenderer render in renderers)
        {
            render.sortingOrder += 10;
        }
    }
    public void SpriteLayerDown()
    {
        foreach (SpriteRenderer render in renderers)
        {
            render.sortingOrder -= 10;
        }
    }

    public void SetSelected(bool selected)
    {
        creatingSelected = selected;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (creatingSelected && renderers[0].color != Color.red&&collision.gameObject.layer != LayerMask.NameToLayer("Hero"))
        {
            
            foreach (SpriteRenderer render in renderers)
            {
                render.color = Color.red;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(creatingSelected && collision.gameObject.layer != LayerMask.NameToLayer("Hero"))
        {
            foreach (SpriteRenderer render in renderers)
            {
                render.color = originalColor;

            }

        }
    }


    
    // Start is called before the first frame update
    public virtual void Start()
    {
        dieSound = GetComponent<AudioSource>();
        particleEmitter = GetComponent<ParticleSystem>();
        meCollider = GetComponent<Collider2D>();

    }

    

}
