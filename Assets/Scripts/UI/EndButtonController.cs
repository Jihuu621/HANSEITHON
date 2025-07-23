using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndButtonController : MonoBehaviour
{
    public Button endButton;
    public TextMeshProUGUI endButtonText;
    public int requiredClicks = 3;

    private int clickCount = 0;

    void Start()
    {
        endButton.onClick.AddListener(OnEndButtonClicked);
    }

    void OnEndButtonClicked()
    {
        if (SceneManager.GetActiveScene().name != "Level_5")
            return;

        clickCount++;

        if (clickCount == 1)
        {
            endButtonText.text = "¡æ";
        }

        if (clickCount >= requiredClicks)
        {
            SceneManager.LoadScene("888");
        }
    }
}
