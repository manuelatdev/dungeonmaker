using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int range;

    [SerializeField]
    private float attackSpeed;

    public int getDamage()
    {
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
