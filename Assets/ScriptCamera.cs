using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using static UnityEngine.GraphicsBuffer;

public class ScriptCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject hero;
    [SerializeField]
    private float transitionTime;
    private Camera thisCamera;

    private void Start()
    {
        thisCamera= GetComponent<Camera>();
    }

    public void DeadCamera()
    {
        StartCoroutine(MoveCamera());
    }
    public void ResetCamera()
    {
        StopAllCoroutines();
        transform.position = new Vector3(0, 0, -10);
        thisCamera.orthographicSize = 5;
    }
    IEnumerator MoveCamera()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(hero.transform.position.x, hero.transform.position.y, -10);
        float startSize = Camera.main.orthographicSize;
        float endSize = 3.5f;

        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            float progress = t / transitionTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            Camera.main.orthographicSize = Mathf.Lerp(startSize, endSize, progress);
            yield return null;
        }

        // Asegúrate de que la cámara llegue exactamente a la posición y tamaño deseados
        transform.position = endPosition;
        Camera.main.orthographicSize = endSize;
    }
}
