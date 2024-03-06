using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
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

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        if (TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }
    }


}
