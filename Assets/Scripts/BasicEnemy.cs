using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BasicEnemy : BaseEntity
{

    private TextMeshProUGUI healText;

    [SerializeField]
    private Image greenHealthBarImage;

    [SerializeField]
    private Image redHealthBarImage;

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


        greenHealthBarImage.fillAmount -= (float)damage / initialHealth;
        healText.text = health + " / " + initialHealth;
        StopCoroutine(AnimateHealthBarDecrease());
        StartCoroutine(AnimateHealthBarDecrease());
    }
    IEnumerator AnimateHealthBarDecrease()
    {
        float elapsedTime = 0;
        float startValue = redHealthBarImage.fillAmount;
        float endValue = greenHealthBarImage.fillAmount;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            redHealthBarImage.fillAmount = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }

        // Asegurarse de que fillAmount es exactamente 0.5 al final de la animación
        redHealthBarImage.fillAmount = endValue;
    }


    

}
