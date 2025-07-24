using UnityEngine;
using System.Collections;

public class PlayerTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public Transform telTar2;
    public Transform telTar3;
    public GameObject respawnParticlePrefab;

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTeleporting) return;

        if (collision.CompareTag("Obstacle") && teleportTarget != null)
        {
            StartCoroutine(TeleportWithEffect(teleportTarget.position));
        }
        else if (collision.CompareTag("Obstacle1") && telTar2 != null)
        {
            StartCoroutine(TeleportWithEffect(telTar2.position));
        }
        else if (collision.CompareTag("Obstacle2") && telTar3 != null)
        {
            StartCoroutine(TeleportWithEffect(telTar3.position));
        }
    }

    private IEnumerator TeleportWithEffect(Vector3 targetPosition)
    {
        isTeleporting = true;

        // 파티클 소환 및 설정
        if (respawnParticlePrefab != null)
        {
            GameObject particle = Instantiate(respawnParticlePrefab, targetPosition, Quaternion.identity);

            var ps = particle.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                main.startColor = Color.black;
                main.startSize = 0.4f;

                // 강제 재생 (혹시라도 Play On Awake가 꺼져 있을 경우 대비)
                ps.Play();

                // 0.3초만 방출 후 멈추고 자연스럽게 사라지게
                StartCoroutine(StopEmitting(ps, 0.3f));
            }
            else
            {
                Debug.LogWarning("respawnParticlePrefab에 ParticleSystem이 없음");
            }
        }

        // 플레이어 위치 이동
        transform.position = targetPosition;

        // 쿨타임
        yield return new WaitForSeconds(0.5f);
        isTeleporting = false;
    }

    private IEnumerator StopEmitting(ParticleSystem ps, float duration)
    {
        var emission = ps.emission;
        yield return new WaitForSeconds(duration);
        emission.rateOverTime = 0; // 방출만 멈추고 기존 입자들은 자연스럽게 소멸
    }
}
