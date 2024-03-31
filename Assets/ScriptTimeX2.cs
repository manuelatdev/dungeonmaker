using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptTimeX2 : MonoBehaviour
{
    public static TextMeshProUGUI texto;
    public static bool activado;
    void Start()
    {
        texto = GetComponentInChildren<TextMeshProUGUI>();
    }

    public static void ActivarDesactivarSpeed()
    {
        if (activado)
        {
            ReiniciarTiempo();

            AudioManagerScript.click.Play();
;        }
        else 
        {
            Time.timeScale = 2;
            texto.text = "SPEED MODE: ON";
            activado=true;
            AudioManagerScript.click.Play();


        }
    }
    public static void ReiniciarTiempo()
    {
        Time.timeScale = 1;
        texto.text = "SPEED MODE: OFF";
        activado = false;
    }
    public static void VolverTiempoAnterior()
    {
        if (activado)
        {
            Time.timeScale = 2;
            texto.text = "SPEED MODE: ON";
        }
        else
        {
            Time.timeScale = 1;
            texto.text = "SPEED MODE: OFF";
        }
    }
}
