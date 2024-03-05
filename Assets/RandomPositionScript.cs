using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionScript : MonoBehaviour
{
    public Collider2D otroCollider; // Referencia al Box Collider 2D del otro GameObject
    public float tiempoEntreMovimientos = 3f; // Tiempo entre movimientos (en segundos)

    private bool moverALaDerecha = true; // Variable para alternar entre izquierda y derecha

    private void Start()
    {
        // Inicia la corutina para mover el objeto
        StartCoroutine(MoverObjetoAlternativo());
    }

    private IEnumerator MoverObjetoAlternativo()
    {
        while (true)
        {
            // Calcula una posición aleatoria en la mitad izquierda o derecha
            Vector2 posicionAleatoria = ObtenerPosicionAleatoriaEnMitad();

            // Mueve este objeto a la posición aleatoria (z se mantiene igual)
            transform.position = new Vector3(posicionAleatoria.x, posicionAleatoria.y, transform.position.z);

            // Alterna la dirección para el próximo movimiento
            moverALaDerecha = !moverALaDerecha;

            // Espera durante el tiempo especificado antes de moverlo nuevamente
            yield return new WaitForSeconds(tiempoEntreMovimientos);
        }
    }

    private Vector2 ObtenerPosicionAleatoriaEnMitad()
    {
        // Obtiene los límites del Box Collider 2D del otro GameObject
        Bounds bounds = otroCollider.bounds;

        // Calcula una posición aleatoria en la mitad izquierda o derecha
        float x;
        if (moverALaDerecha)
        {
            // Mitad derecha
            x = Random.Range(bounds.center.x, bounds.max.x);
        }
        else
        {
            // Mitad izquierda
            x = Random.Range(bounds.min.x, bounds.center.x);
        }

        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }

}
