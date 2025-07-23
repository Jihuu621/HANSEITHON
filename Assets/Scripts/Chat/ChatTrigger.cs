using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public GameObject dialogueManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.SetActive(true);

            DialogueManager dm = dialogueManager.GetComponent<DialogueManager>();
            if (dm != null)
            {
                dm.StartDialogue();
            }

            gameObject.SetActive(false); 
        }
    }
}
