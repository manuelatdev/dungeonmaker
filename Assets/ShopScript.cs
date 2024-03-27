using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] attackSprites;

    [SerializeField]
    private Sprite[] armorSprites;

    [SerializeField]
    private Image attackImg;

    [SerializeField]
    private Image armorImg;

    [SerializeField]
    private TextMeshProUGUI precioAttackText;
    [SerializeField]
    private TextMeshProUGUI precioSpeedText;
    [SerializeField]
    private TextMeshProUGUI precioArmorText;
    [SerializeField]
    private TextMeshProUGUI precioHealText;

    [SerializeField]
    private int[] preciosItems= { 15, 30, 60, 100 };
    private int precioAttack;
    private int precioSpeed;
    private int precioArmor;
    private int precioHeal;
    [SerializeField]
    private HeroScript scriptHero;

    [SerializeField]
    private AudioSource rainSound;

    [SerializeField]
    private GameObject parentItems;
    [SerializeField]
    private GameObject items;

    [SerializeField]
    Animator moneyAnim;

    [SerializeField]
    AudioSource deniedSound;

    private Animator anim;

    [SerializeField]
    ParticleSystem particulasCompra;

    [SerializeField]
    AudioSource buySound;
    private void Start()
    {
        anim = GetComponent<Animator>();
        LoadPrecios();
    }
    private void LoadPrecios()
    {
        precioAttack = preciosItems[ScriptGameManager.shopAttack];
        precioSpeed = preciosItems[ScriptGameManager.shopSpeed];
        precioArmor = preciosItems[ScriptGameManager.shopArmor];
        precioHeal = 10;
        precioAttackText.text = "+" + precioAttack;
        precioSpeedText.text = "+" + precioSpeed;
        precioArmorText.text = "+" + precioArmor;
        precioHealText.text = "+" + precioHeal;
        armorImg.sprite = armorSprites[ScriptGameManager.shopArmor];
        attackImg.sprite = attackSprites[ScriptGameManager.shopAttack];

    }
    public void BuyAttack()
    {
        if (ScriptGameManager.gold>=precioAttack)
        {
            ScriptGameManager.shopAttack++;
            ScriptGameManager.gold -= precioAttack;
            ScriptGameManager.attack++;
            LoadPrecios();
            scriptHero.LoadStatsFromManager();
            particulasCompra.Play();
            buySound.Play();
            anim.SetTrigger("Love");
        }
        else
        {
            anim.SetTrigger("Denied");
            deniedSound.Play();
            moneyAnim.SetTrigger("Denied");
            
        }
    }
    public void BuySpeed()
    {
        if (ScriptGameManager.gold >= precioSpeed)
        {
            ScriptGameManager.shopSpeed++;
            ScriptGameManager.gold -= precioSpeed;
            ScriptGameManager.speed++; // Asume que tienes una variable speed en ScriptGameManager
            LoadPrecios();
            scriptHero.LoadStatsFromManager();
            particulasCompra.Play();
            buySound.Play();
            anim.SetTrigger("Love");
        }
        else
        {
            anim.SetTrigger("Denied");
            deniedSound.Play();
            moneyAnim.SetTrigger("Denied");
        }
    }

    public void BuyArmor()
    {
        if (ScriptGameManager.gold >= precioArmor)
        {
            ScriptGameManager.shopArmor++;
            ScriptGameManager.gold -= precioArmor;
            ScriptGameManager.def++; // Asume que tienes una variable armor en ScriptGameManager
            LoadPrecios();
            scriptHero.LoadStatsFromManager();
            particulasCompra.Play();
            buySound.Play();
            anim.SetTrigger("Love");
        }
        else
        {
            anim.SetTrigger("Denied");
            deniedSound.Play();
            moneyAnim.SetTrigger("Denied");
        }
    }

    public void BuyHeal()
    {
        if (ScriptGameManager.gold >= precioHeal)
        {

            ScriptGameManager.gold -= precioHeal;
            ScriptGameManager.health = Mathf.Min(ScriptGameManager.health + 10, ScriptGameManager.maxHealth);
            LoadPrecios();
            scriptHero.LoadStatsFromManager();
            particulasCompra.Play();
            buySound.Play();
            anim.SetTrigger("Love");
        }
        else
        {
            anim.SetTrigger("Denied");
            deniedSound.Play();
            moneyAnim.SetTrigger("Denied");
        }
    }

    public void EnterShop()
    {
        items.transform.SetParent(parentItems.transform,false);
        if (rainSound != null)
        {
            rainSound.Stop();
        }
    }
    


}
