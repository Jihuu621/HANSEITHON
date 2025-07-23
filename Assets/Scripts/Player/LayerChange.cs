using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LayerChange : MonoBehaviour
{
    public int maxLayer = 4;
    private int layer = 1;
    private int prevLayer = 1;

    public GameObject[] layerObjs;
    public GameObject particlePrefab;
    public int particleCount = 10;

    [Header("비네트 이미지")]
    public Image vignetteImage;  // V_Canvas 안의 Vignette Image 컴포넌트 연결
    public float vignetteFadeDuration = 0.5f;

    private Coroutine vignetteCoroutine;

    private void Start()
    {
        if (vignetteImage != null)
        {
            Color c = vignetteImage.color;
            c.a = 0f;
            vignetteImage.color = c;
        }
        else
        {
            Debug.LogWarning("Vignette Image가 연결되지 않았습니다.");
        }

        LayerSwitch(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            prevLayer = layer;
            layer = (layer > 1) ? layer - 1 : maxLayer;
            LayerSwitch(false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            prevLayer = layer;
            layer = (layer < maxLayer) ? layer + 1 : 1;
            LayerSwitch(false);
        }
    }

    private void LayerSwitch(bool isfirst)
    {
        if (!isfirst)
        {
            // 이전 코루틴 취소
            if (vignetteCoroutine != null)
            {
                StopCoroutine(vignetteCoroutine);
                vignetteCoroutine = null;
            }

            // 비네트 이미지 색상 & 알파 초기화
            if (vignetteImage != null)
            {
                Color c = GetColorForLayer(layer);
                c.a = 0.7f;
                vignetteImage.color = c;
                vignetteCoroutine = StartCoroutine(FadeOutVignette());
            }
        }
        for (int i = 0; i < layerObjs.Length; i++)
        {
            GameObject obj = layerObjs[i];

            if (i + 1 == layer)
            {
                obj.SetActive(true);
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform child in obj.transform)
                {
                    bool wasInPrevLayer = (i + 1 == prevLayer);
                    if (wasInPrevLayer && particlePrefab != null)
                    {
                        GameObject p = Instantiate(particlePrefab, child.position, Quaternion.identity);
                        p.transform.parent = null;

                        ParticleSystem ps = p.GetComponent<ParticleSystem>();
                        if (ps != null)
                        {
                            var main = ps.main;
                            main.startSize = 0.4f;
                            main.startColor = GetColorForLayer(prevLayer);

                            var shape = ps.shape;
                            BoxCollider2D box = child.GetComponent<BoxCollider2D>();
                            if (box != null)
                            {
                                shape.shapeType = ParticleSystemShapeType.Box;
                                shape.scale = box.size;
                            }
                            else
                            {
                                Debug.LogWarning("BoxCollider2D가 없습니다: " + child.name);
                            }

                            StartCoroutine(PlayParticleAndStopEmitting(ps, 0.3f, particleCount));
                        }
                        else
                        {
                            Debug.LogWarning("Particle prefab에 ParticleSystem이 없습니다.");
                            Destroy(p);
                        }
                    }

                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator FadeOutVignette()
    {
        float elapsed = 0f;
        Color c = vignetteImage.color;

        while (elapsed < vignetteFadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.1f, 0f, elapsed / vignetteFadeDuration);
            c.a = alpha;
            vignetteImage.color = c;
            yield return null;
        }

        c.a = 0f;
        vignetteImage.color = c;
        vignetteCoroutine = null;
    }

    private IEnumerator PlayParticleAndStopEmitting(ParticleSystem ps, float duration, int emitCount)
    {
        if (ps == null) yield break;

        ps.Play();
        ps.Emit(emitCount);

        yield return new WaitForSeconds(duration);

        ps.Stop(withChildren: false, ParticleSystemStopBehavior.StopEmitting);

        float maxLifetime = ps.main.startLifetime.constantMax;
        yield return new WaitForSeconds(maxLifetime);

        if (ps.gameObject != null)
        {
            Destroy(ps.gameObject);
        }
    }

    private Color GetColorForLayer(int layer)
    {
        switch (layer)
        {
            case 1: return Color.red;
            case 2: return Color.blue;
            case 3: return Color.green;
            case 4: return new Color(1f, 0.4f, 0.7f); // 핑크
            default: return Color.white;
        }
    }                       
}
                         