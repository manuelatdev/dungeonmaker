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

    private Image descriptionImg;

    private float mouseOverTime = 0;

    private bool descriptionOn=false;

    private ScriptTinteShader tinteScript;

    protected bool creatingSelected;

    

    [SerializeField]
    private SpriteRenderer[] renderers;

    [SerializeField]
    private GameObject outline;

    [SerializeField]
    private Color originalColor;
    public override void Start()
    {
        base.Start();
        tinteScript = GetComponentInChildren<ScriptTinteShader>();
        healText = GetComponentInChildren<TextMeshProUGUI>();
        initialHealth = health;
        healText.text = health + " / " + initialHealth;
    }

    public void SetSelected(bool selected)
    {
        creatingSelected = selected;
    }
    public void SpriteLayerUp()
    {
        foreach (SpriteRenderer render in renderers)
        {
            render.sortingOrder += 20;
        }
    }
    public void SpriteLayerDown()
    {
        foreach (SpriteRenderer render in renderers)
        {
            render.sortingOrder -= 20;
        }
    }
    public int getExperience()
    {
        return experience;
    }
    public int getDamage()
    {
        return damage;
    }
    private void OnMouseOver()
    {
        if (!SelectorScript.instanciado)
        {
            ActivateOutline(true);
            CursorScript.SwitchStone(true); 
        }
        if (!descriptionOn&&!creatingSelected )
        {
            mouseOverTime += Time.deltaTime;
            if (mouseOverTime > 0.5f)
            {
                descriptionOn = true;
            } 
        }
    }
    public void ActivateOutline(bool active)
    {
        outline.SetActive(active);

    }
    private void OnMouseExit()
    {
        if (!SelectorScript.instanciado)
        {
            ActivateOutline(false);
            CursorScript.SwitchStone(false);
        }
        if (descriptionOn)
        {
            mouseOverTime = 0;
            descriptionOn = false; 
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (creatingSelected && !CursorScript.denied.activeSelf && !collision.gameObject.CompareTag("Hero"))
        {
            if (CursorScript.denied != null)
            {
                CursorScript.SwitchDenied(true);
            }
            else
            {
                StartCoroutine(WaitForSwitch(true));
            }
        }
    }
    IEnumerator WaitForSwitch(bool boleano)
    {
        yield return new WaitForEndOfFrame();
        CursorScript.SwitchDenied(boleano);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (creatingSelected && !collision.gameObject.CompareTag("Hero"))
        {
            if (CursorScript.denied != null)
            {
                CursorScript.SwitchDenied(false);
            }
            else
            {
                StartCoroutine(WaitForSwitch(false));
            }

        }
    }

    public bool IsCreable()
    {
        if (!CursorScript.denied.activeSelf)
        {
            return true;
        }

        return false;
    }


    public override void TakeAttack(int damage)
    {
        base.TakeAttack(damage);

        if (health > 0)
        {
            tinteScript.TintColor();
            greenHealthBarImage.fillAmount -= (float)damage / initialHealth;
            healText.text = health + " / " + initialHealth;
            StopCoroutine(AnimateHealthBarDecrease());
            StartCoroutine(AnimateHealthBarDecrease()); 
        }
    }
    protected override void Die()
    {
        dieSound.Play();
        scriptHero.OnEnemyDied(this, this);

        foreach (GameObject obj in disabledOnDead)
        {
            obj.SetActive(false);
        }

        meCollider.enabled = false;
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
