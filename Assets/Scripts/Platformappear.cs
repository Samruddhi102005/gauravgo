using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformappear : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float delay = 5f; // Time in seconds before the platform appears or disappears
    [SerializeField] private float fadeDuration = 2f; // Time in seconds for the platform to fade in/out

    private Renderer platformRenderer;
    private Color platformColor;

    private void Start()
    {
        platformRenderer = platform.GetComponent<Renderer>();
        platformColor = platformRenderer.material.color;

        // Start the coroutine to make the platform appear and disappear repeatedly
        StartCoroutine(AppearDisappearCycle());
    }

    private IEnumerator AppearDisappearCycle()
    {
        while (true)
        {
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(delay);
            yield return StartCoroutine(FadeOut());
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetPlatformAlpha(alpha);
            yield return null;
        }
        SetPlatformAlpha(1f);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            SetPlatformAlpha(alpha);
            yield return null;
        }
        SetPlatformAlpha(0f);
    }

    private void SetPlatformAlpha(float alpha)
    {
        Color color = platformColor;
        color.a = alpha;
        platformRenderer.material.color = color;
    }
}
