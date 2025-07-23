using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public GameObject settingsPanel;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}
