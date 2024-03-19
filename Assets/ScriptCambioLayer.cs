using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptCambioLayer : MonoBehaviour
{
    private GameObject hero;
    private bool subido=true;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        hero = GameObject.FindWithTag("Hero");
    }
    private void Update()
    {
        if (hero.transform.position.y>transform.position.y&&!subido)
        {
            sprite.sortingOrder = 20;
            subido = true;
        }
        else if (hero.transform.position.y < transform.position.y && subido)
        {
            sprite.sortingOrder = 10;
            subido = false;
        }
    }
}
