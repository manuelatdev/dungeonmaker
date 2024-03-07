using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemy : BaseEntity
{

    private TextMeshProUGUI healText;

    [SerializeField]
    private Image healImage;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected int range;

    [SerializeField]
    protected float attackSpeed;

    [SerializeField]
    protected int cost;

    [SerializeField]
    protected int experience;

    public override void Start()
    {
        base.Start();

        healText = GetComponentInChildren<TextMeshProUGUI>();
        initialHealth = health;
        healText.text = health + " / " + initialHealth;
    }

    public int getExperience()
    {
        return experience;
    }

    public override void TakeAttack(int damage)
    {
        base.TakeAttack(damage);


        healImage.fillAmount -= (float)damage / initialHealth;
        healText.text = health + " / " + initialHealth;
    }

    protected override void Die()
    {
        base.Die();

        transform.GetChild(0).gameObject.SetActive(false);
    }

}
