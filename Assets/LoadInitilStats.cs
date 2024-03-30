using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInitilStats : MonoBehaviour
{
    int health = 10;
    int maxHealth = 10;
    int level = 1;
    int exp = 0;
    int gold = 0;
    int attack = 1;
    int def = 0;
    int speed = 1;
    int shopAttack = 0;
    int shopSpeed = 0;
    int shopArmor = 0;

    private void Awake()
    {
        ScriptGameManager.health = health;
        ScriptGameManager.maxHealth = maxHealth;
        ScriptGameManager.level = level;
        ScriptGameManager.exp = exp;
        ScriptGameManager.gold = gold;
        ScriptGameManager.attack = attack;
        ScriptGameManager.def = def;
        ScriptGameManager.speed = speed;
        ScriptGameManager.shopAttack = shopAttack;
        ScriptGameManager.shopSpeed = shopSpeed;
        ScriptGameManager.shopArmor = shopArmor;
    }
   
}
