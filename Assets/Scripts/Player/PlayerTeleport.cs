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

        // ��ƼŬ ��ȯ �� ����
        if (respawnParticlePrefab != null)
        {
            GameObject particle = Instantiate(respawnParticlePrefab, targetPosition, Quaternion.identity);

            var ps = particle.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                main.startColor = Color.black;
                main.startSize = 0.4f;

                // ���� ��� (Ȥ�ö� Play On Awake�� ���� ���� ��� ���)
                ps.Play();

                // 0.3�ʸ� ���� �� ���߰� �ڿ������� �������
                StartCoroutine(StopEmitting(ps, 0.3f));
            }
            else
            {
                Debug.LogWarning("respawnParticlePrefab�� ParticleSystem�� ����");
            }
        }

        // �÷��̾� ��ġ �̵�
        transform.position = targetPosition;

        // ��Ÿ��
        yield return new WaitForSeconds(0.5f);
        isTeleporting = false;
    }

    private IEnumerator StopEmitting(ParticleSystem ps, float duration)
    {
        var emission = ps.emission;
        yield return new WaitForSeconds(duration);
        emission.rateOverTime = 0; // ���⸸ ���߰� ���� ���ڵ��� �ڿ������� �Ҹ�
    }
}
