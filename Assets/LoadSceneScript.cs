using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    private AudioSource whooshSound;
    private Animator anim;
    [SerializeField]
    private GameObject destruibleObject;
    [SerializeField]
    private GameObject items;
    public bool titleScreen;
    private void Start()
    {
        anim = GetComponent<Animator>();
        whooshSound = GetComponent<AudioSource>();
    }
    public void DestroyThisObject()
    {
        AudioManagerScript.music.Play();
        Destroy(destruibleObject);
    }
    public void PlayWhoosh()
    {
        whooshSound.Play();
    }
    public void CargarSiguienteEscena()
    {
        StartCoroutine(LoadSceneAsync());
        if (items != null)
        {
            Destroy(items);

        }
    }
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOperation;

        if (!titleScreen)
        {
            asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            asyncOperation = SceneManager.LoadSceneAsync(5);
        }

        // Espera a que la escena se haya cargado completamente
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        AudioManagerScript.music.Stop();

        anim.SetTrigger("EndTransition");
    }
}
