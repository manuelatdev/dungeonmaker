using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartRunScript : MonoBehaviour
{
   public void RestartRun()
    {
        SceneManager.LoadScene(1);
    }
}
