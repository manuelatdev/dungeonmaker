using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkins : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer head;
    [SerializeField]
    private SpriteRenderer arm;
    [SerializeField]
    private SpriteRenderer sword;
    [SerializeField]
    private SpriteRenderer body;

    [SerializeField]
    private Sprite[] heads;
    [SerializeField]
    private Sprite[] arms;
    [SerializeField]
    private Sprite[] swords;
    [SerializeField]
    private Sprite[] bodys;

    private void Start()
    {
        
       
        head.sprite = heads[ScriptGameManager.shopArmor];
        arm.sprite = arms[ScriptGameManager.shopArmor];
        sword.sprite = swords[ScriptGameManager.shopAttack];
        body.sprite = bodys[ScriptGameManager.shopArmor];

    }
}
