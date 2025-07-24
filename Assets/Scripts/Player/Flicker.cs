using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeDuration = 1f;
    public bool startOnAwake = false;
    public bool loopFlicker = false;
    public float delayBetweenFlickers = 1f;

    private Coroutine flickerCoroutine;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (startOnAwake)
            StartFlicker();
    }

    public void StartFlicker()
    {
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);

        flickerCoroutine = StartCoroutine(FlickerLoop());
    }

    public void StopFlicker()
    {
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);

        flickerCoroutine = null;
    }

    public void FlickerOnce()
    {
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);

        flickerCoroutine = StartCoroutine(FadeOutIn());
    }

    public void FadeIn()
    {
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);

        flickerCoroutine = StartCoroutine(FadeAlpha(0f, 1f));
    }

    public void FadeOut()
    {
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);

        flickerCoroutine = StartCoroutine(FadeAlpha(1f, 0f));
    }

    private IEnumerator FlickerLoop()
    {
        while (true)
        {
            yield return FadeAlpha(1f, 0f);
            yield return new WaitForSeconds(delayBetweenFlickers + Random.Range(5, 10));
            yield return FadeAlpha(0f, 1f);
            yield return new WaitForSeconds(delayBetweenFlickers);

            if (!loopFlicker)
                break;
        }
    }

    private IEnumerator FadeOutIn()
    {
        yield return FadeAlpha(1f, 0f);
        yield return FadeAlpha(0f, 1f);
    }

    private IEnumerator FadeAlpha(float from, float to)
    {
        float elapsed = 0f;
        Color c = spriteRenderer.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            spriteRenderer.color = new Color(c.r, c.g, c.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(c.r, c.g, c.b, to);
    }
}
