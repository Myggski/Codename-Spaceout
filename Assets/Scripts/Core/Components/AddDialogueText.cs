using System.Collections.Generic;
using UnityEngine;

public class AddDialogueText : MonoBehaviour
{
  [SerializeField]
  private List<string> texts = new List<string>();
  [SerializeField]
  private StringRuntimeSet dialogueTexts;

  private void OnTriggerEnter2D(Collider2D other)
  {
    foreach (string text in this.texts)
    {
      this.dialogueTexts.Add(text);
    }

    this.gameObject.SetActive(false);
  }
}
