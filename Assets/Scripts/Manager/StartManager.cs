using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    Transform top, bottom, lightS;
    Image lightRenderer;

    private void Start()
    {
        top = transform.Find("Top");
        bottom = transform.Find("Bottom");
        lightS = transform.Find("Light");

        lightRenderer = lightS.GetComponent<Image>();

        // 각각 움직이기 시작
        StartCoroutine(MoveY(top, 550f, 0.25f));
        StartCoroutine(MoveY(bottom, -550f, 0.25f));
        StartCoroutine(FadeOutAlpha(lightRenderer, .4f)); // 1초 동안 알파 0으로
    }

    private IEnumerator MoveY(Transform target, float deltaY, float duration)
    {
        Vector3 startPos = target.localPosition;
        Vector3 endPos = startPos + new Vector3(0, deltaY, 0);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localPosition = endPos;
    }

    private IEnumerator FadeOutAlpha(Image renderer, float duration)
    {
        Color startColor = renderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            renderer.color = Color.Lerp(startColor, endColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        renderer.color = endColor;
    }
}
