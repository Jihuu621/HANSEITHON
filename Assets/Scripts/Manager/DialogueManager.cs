using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Emotion
{
    P2_Straight,
    P2_Straight_Pyodok,
    P2_Smile,
    P2_Smile_Pyodok,
    P2_Smile2,
    P2_Smile2_Pyodok,
}

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string line;
    public Emotion emotion;
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

    private Dictionary<Emotion, Sprite> emotionSprites;

    void Awake()
    {
        LoadEmotionSprites();
    }

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
        UpdateEmotionImage(line.speaker, line.emotion);
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

            yield return new WaitForSeconds(typingSpeed);
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

    void UpdateEmotionImage(string speaker, Emotion emotion)
    {
        if (!emotionSprites.ContainsKey(emotion))
            return;

        if (speaker == "플레이어")
        {
            return;
        }
        else if (speaker == "캐릭터")
        {
            rightImage.sprite = emotionSprites[emotion];
        }
    }

    void LoadEmotionSprites()
    {
        emotionSprites = new Dictionary<Emotion, Sprite>();

        foreach (Emotion emo in System.Enum.GetValues(typeof(Emotion)))
        {
            string path = "Chat/" + emo.ToString();
            Sprite sprite = Resources.Load<Sprite>(path);

            if (sprite != null)
            {
                emotionSprites[emo] = sprite;
            }
            else
            {
                Debug.LogWarning($"스프라이트를 찾을 수 없음: {path}");
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        if (dialogueLines.Count > 0)
            ShowLine(dialogueLines[index]);
    }
}