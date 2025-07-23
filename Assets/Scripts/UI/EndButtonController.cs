using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndButtonController : MonoBehaviour
{
    public Button endButton;
    public TextMeshProUGUI endButtonText;

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
    }
}
