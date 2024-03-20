using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesplegablesScript : MonoBehaviour
{
    private static GameObject description;
    private static TextMeshProUGUI descriptionText;

    private static GameObject enemyDescription;
    private static TextMeshProUGUI attak;
    private static TextMeshProUGUI speed;
    private static TextMeshProUGUI exp;
    private static TextMeshProUGUI gold;
    private static TextMeshProUGUI nameText;

    private static GameObject enemyPlusDescription;
    private static TextMeshProUGUI attakPlus;
    private static TextMeshProUGUI speedPlus;
    private static TextMeshProUGUI expPlus;
    private static TextMeshProUGUI goldPlus;
    private static TextMeshProUGUI nameTextPlus;

    private void Start()
    {
        description = transform.GetChild(0).gameObject;
        enemyDescription = transform.GetChild(1).gameObject;
        enemyPlusDescription = transform.GetChild(2).gameObject;

        descriptionText = description.transform.GetComponentInChildren<TextMeshProUGUI>();

        attak = enemyDescription.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        speed = enemyDescription.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        exp = enemyDescription.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        gold = enemyDescription.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        nameText = enemyDescription.transform.GetChild(4).GetComponent<TextMeshProUGUI>();


        attakPlus = enemyPlusDescription.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        speedPlus = enemyPlusDescription.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        expPlus = enemyPlusDescription.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        goldPlus = enemyPlusDescription.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        nameTextPlus = enemyPlusDescription.transform.GetChild(4).GetComponent<TextMeshProUGUI>(); ;
    }

    public static void ShowDescription(string descriptionRef)
    {
        description.SetActive(true);

        descriptionText.text = descriptionRef;
    }
    public static void ShowEnemyDescription(string atkRef, string dpsRef, string expRef, string goldRef, string nameRef)
    {
        enemyDescription.SetActive(true);

        attak.text = atkRef;
        speed.text = dpsRef;
        exp.text = expRef;
        gold.text = goldRef;
        nameText.text = nameRef;
    }
    public static void ShowEnemyPlusDescription(string atkRef, string dpsRef, string expRef, string goldRef, string nameRef)
    {
        enemyPlusDescription.SetActive(true);

        attakPlus.text = atkRef;
        speedPlus.text = dpsRef;
        expPlus.text = expRef;
        goldPlus.text = goldRef;
        nameTextPlus.text = nameRef;
    }
    public static void HideAllDescriptions()
    {
        description.SetActive(false);
        enemyDescription.SetActive(false);
        enemyPlusDescription.SetActive(false);
    }

}
