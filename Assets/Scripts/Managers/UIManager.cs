using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIManager
{
    public static IEnumerator LerpCanvasRendererAlpha(CanvasRenderer renderer, bool isVisible, float duration) 
    {
        if (renderer != null) 
        {
            float t = 0f;
            float targetedAlpha = isVisible ? 1f : 0f;
            float currentAlpha  = renderer.GetAlpha();

            while (t < duration) 
            {
                renderer.SetAlpha(Mathf.Lerp(currentAlpha, targetedAlpha, t / duration));
                t += Time.deltaTime;
                yield return null;
            }

            if (t >= duration) { renderer.SetAlpha(targetedAlpha); }
        }
    }
}
