using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string line;
}

public class DialogueManager : MonoBehaviour
{
    public Image leftImage;
    public Image rightImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public List<DialogueLine> dialogueLines;

    private int index = 0;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public AudioSource audioSource;
    public AudioClip typeSound;


    void Start()
    {
        if (dialogueLines.Count > 0)
            ShowLine(dialogueLines[index]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogueLines[index].line;
                isTyping = false;
            }
            else
            {
                index++;
                if (index < dialogueLines.Count)
                {
                    ShowLine(dialogueLines[index]);
                }
                else
                {
                    Time.timeScale = 1f;
                    gameObject.SetActive(false);
                }
            }
        }
    }


    void ShowLine(DialogueLine line)
    {
        nameText.text = line.speaker;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(line.line));
        AdjustSpeakerHighlight(line.speaker);
    }

    IEnumerator TypeText(string fullText)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in fullText)
        {
            dialogueText.text += c;

            if (c != ' ' && typeSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typeSound);
            }

            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    void AdjustSpeakerHighlight(string currentSpeaker)
    {
        string leftName = "플레이어";
        string rightName = "캐릭터";

        Color active = Color.white;
        Color dimmed = new Color(1f, 1f, 1f, 0.5f);

        if (currentSpeaker == leftName)
        {
            leftImage.color = active;
            rightImage.color = dimmed;
        }
        else if (currentSpeaker == rightName)
        {
            leftImage.color = dimmed;
            rightImage.color = active;
        }
        else
        {
            leftImage.color = dimmed;
            rightImage.color = dimmed;
        }
    }


    public void StartDialogue()
    {
        index = 0;
        Time.timeScale = 0f;
        if (dialogueLines.Count > 0)
            ShowLine(dialogueLines[index]);
    }

}