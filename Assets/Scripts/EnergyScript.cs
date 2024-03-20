using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyScript : MonoBehaviour
{
    [SerializeField]
    private int totalEnergyRef;

    public static int currentEnergy;
    public static int totalEnergy;
    public static TextMeshProUGUI textMesh;
    public static AudioSource deniedSound;
    public static Animator anim;
    private void Start()
    {
        totalEnergy = totalEnergyRef;
        deniedSound = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        currentEnergy = totalEnergy;
    }
    public static void ResetEnergy()
    {
        currentEnergy = totalEnergy;
        textMesh.text = currentEnergy.ToString() + "/" + totalEnergy;

    }
    public static bool UseEnergy(int valor)
    {
        if (currentEnergy + valor<0)
        {
            deniedSound.Play();
            anim.SetTrigger("Denied");
            return false;
        }
        else
        {

            currentEnergy += valor;
            textMesh.text = currentEnergy.ToString() + "/"+ totalEnergy;

            return true;

        }

    }
}
