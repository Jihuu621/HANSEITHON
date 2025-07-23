using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateAndChangeScene : MonoBehaviour
{
    public float rotationDuration = 1f;
    public int rotationCount = 5;     

    void Start()
    {
        StartCoroutine(RotateAndLoadScene());
    }

    IEnumerator RotateAndLoadScene()
    {
        yield return new WaitForSeconds(1f);

        float totalRotation = 360f * rotationCount;
        float currentRotation = 0f;
        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            float delta = (Time.deltaTime / rotationDuration) * totalRotation;
            transform.Rotate(0, 0, delta); 
            currentRotation += delta;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.Rotate(0, 0, totalRotation - currentRotation);
        SceneManager.LoadScene("Level_1");
    }
}
