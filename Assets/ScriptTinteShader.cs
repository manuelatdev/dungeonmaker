using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTinteShader : MonoBehaviour
{
    public SpriteRenderer[] spritesTintables;
    private Color originalColor;
    private float targetAlpha = .98f;
    private float duration = .06f;
    


    private void Start()
    {
        if (spritesTintables.Length > 0)
        {
            originalColor = spritesTintables[0].material.GetColor("_Tint");
        }
    }

    public void TintColor()
    {
        foreach (var sprite in spritesTintables)
        {
            StartCoroutine(ChangeAlpha(sprite));
        }
    }


    private IEnumerator ChangeAlpha(SpriteRenderer sprite)
    {
        Material material = sprite.material;
        float startTime = Time.time;

        // Transición de 0 a 0.95
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, targetAlpha, t));
            material.SetColor("_Tint", newColor);
            yield return null;
        }
        material.SetColor("_Tint", new Color(originalColor.r, originalColor.g, originalColor.b,  targetAlpha));

        yield return new WaitForSeconds(.1f);

        startTime = Time.time;

        // Transición de 0.95 a 0
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(targetAlpha, 0, t));
            material.SetColor("_Tint", newColor);
            yield return null;
        }
        material.SetColor("_Tint", originalColor);
    }
}
