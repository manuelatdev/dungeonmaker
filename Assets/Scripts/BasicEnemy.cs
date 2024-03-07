using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int range;

    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private int cost;

    [SerializeField]
    private int gold;

    [SerializeField]
    private int experience;

    [SerializeField]
    private ParticleSystem bloodEffect;

    [SerializeField]
    private Image healImage;

    [SerializeField]
    private bool cofre;

    private AudioSource dieSound;

    private TextMeshProUGUI healText;

    private int initialHeal;




    // Start is called before the first frame update
    void Start()
    {
        dieSound = GetComponent<AudioSource>();
        bloodEffect = GetComponent<ParticleSystem>();
        if (!cofre)
        {
            healText = GetComponentInChildren<TextMeshProUGUI>();
            initialHeal = health;
            healText.text = health + " / " + initialHeal; 
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getHeath()
    {
        return this.health;
    }

    public void InflictDamage(int damage)
    {
        health -= damage;
        if (!cofre)
        {
            healImage.fillAmount -= (float)damage / initialHeal;
            print((float)damage / initialHeal);
            healText.text = health + " / " + initialHeal; 
        }
        bloodEffect.Play();
        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        dieSound.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        if (!cofre)
        {
            transform.GetChild(0).gameObject.SetActive(false);

        }        if (TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }
    }


}
