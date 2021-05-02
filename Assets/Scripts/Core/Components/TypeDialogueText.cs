using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[
  RequireComponent(typeof(UIDocument)),
  RequireComponent(typeof(AudioSource))
]
public class TypeDialogueText : MonoBehaviour
{
  [SerializeField]
  [Tooltip("The duration of the display/hide effect")]
  private float displayDurationTime;

  [SerializeField]
  [Tooltip("The time it takes to write a new letter")]
  private float typeSpeed;
  [SerializeField]
  [Tooltip("Triggers when dialogue has opened")]
  private UnityEvent dialogueOpened;
  [SerializeField]
  [Tooltip("Triggers when dialogue has closed")]
  private UnityEvent dialogueClosed;
  [SerializeField]
  [Tooltip("The text to display in the dialogue")]
  private StringRuntimeSet dialogueTexts;
  private VisualElement dialogue;
  private VisualElement dialogueBox;
  private VisualElement dialogueMoreIcon;
  private VisualElement dialogueCloseIcon;
  private Label visibleText;
  private Label hiddenText;
  private AudioSource typingSound;
  private UIDocument document;
  private Coroutine typingCoroutine;
  private List<string> queuedDialogue = new List<string>();
  private string currentMessage;
  private float dialogueDisplayEffectTimer;
  private float dialogueStartPosition;
  private bool isOpen;

  private void Awake()
  {
    this.SetComponents();
    this.Setup();
  }

  private void Update()
  {
    this.CheckForDialogueChange();
  }

  private void SetComponents()
  {
    this.typingSound = GetComponent<AudioSource>();
    this.document = GetComponent<UIDocument>();
  }

  /// <summary>
  /// Gets the dialogue box and the text of the dialogue
  /// The hidden text forms the box, so it gets the right size from the start
  /// The visible text is the text that will be visible for the player, and will have a typing effect
  /// </summary>
  private void Setup()
  {
    this.dialogue = this.document.rootVisualElement.Q<VisualElement>("dialogue");
    this.dialogueBox = this.dialogue.Q<VisualElement>("dialogue-box");
    this.dialogueCloseIcon = this.dialogue.Q<VisualElement>("dialogue-close");
    this.dialogueMoreIcon = this.dialogue.Q<VisualElement>("dialogue-more");
    this.visibleText = this.dialogue.Q<Label>("visible-text");
    this.hiddenText = this.dialogue.Q<Label>("hidden-text");
    this.dialogueDisplayEffectTimer = 0;
    this.isOpen = false;
    this.currentMessage = string.Empty;
    this.dialogueTexts.Clear();

    if (this.typingSound != null)
    {
      this.typingSound.loop = true;
    }
  }

  /// <summary>
  /// Checks if there are any texts to display
  /// </summary>
  private void CheckForDialogueChange()
  {
    if (this.HasTextToDisplay() && !this.IsDialogueOpen() && this.currentMessage == string.Empty)
    {
      this.currentMessage = this.dialogueTexts.items[0];
      this.TryShowCloseIcon();
      StartCoroutine(this.SetupDialogue());
    }
  }

  /// <summary>
  /// Dialogue cleanup
  /// </summary>
  private void ResetDialogue()
  {
    this.dialogueBox.style.top = new Length(this.dialogueStartPosition);
    this.isOpen = false;
    this.dialogueTexts.Clear();
    this.currentMessage = string.Empty;
    this.visibleText.text = string.Empty;
    this.hiddenText.text = string.Empty;
    this.ShowMoreIcon();
  }

  /// <summary>
  /// Checks if there's any text to display in the dialogue-box
  /// Nullcheck everything!!
  /// </summary>
  /// <returns></returns>
  private bool HasTextToDisplay()
  {
    return this.dialogueTexts != null
      && this.dialogueTexts.items != null
      && this.dialogueTexts.items.Count > 0
      && this.dialogueTexts.items[0] != string.Empty;
  }

  /// <summary>
  /// Checks if the dialogue is in position and if it's open
  /// </summary>
  /// <returns></returns>
  private bool IsDialogueOpen()
  {
    return this.isOpen && this.dialogueBox.style.top == 0f;
  }

  private bool IsLastText()
  {
    return this.HasTextToDisplay() && this.dialogueTexts.items.Count == 1;
  }

  /// <summary>
  /// Sets the volume and plays the sound effect in a loop
  /// </summary>
  private void PlaySoundEffect()
  {
    if (this.typingSound != null)
    {
      this.typingSound.volume = 0f;
      this.typingSound.Play();
    }
  }

  private void StopSoundEffect()
  {
    if (this.typingSound != null)
    {
      StartCoroutine(this.FadeSound());
    }
  }

  private void TryShowCloseIcon()
  {
    if (this.IsLastText())
    {
      this.dialogueMoreIcon.style.opacity = 0;
      this.dialogueCloseIcon.style.opacity = 100;
    }
  }

  private void ShowMoreIcon()
  {
    this.dialogueMoreIcon.style.opacity = 100;
    this.dialogueCloseIcon.style.opacity = 0;
  }

  /// <summary>
  /// Moves the dialogue from bottom outside the screen to its display position
  /// </summary>
  /// <returns></returns>
  private IEnumerator ShowDialogue()
  {
    while (!this.isOpen)
    {
      if (this.dialogueDisplayEffectTimer < this.displayDurationTime)
      {
        this.dialogueDisplayEffectTimer += Time.deltaTime;

        if (this.dialogueDisplayEffectTimer > this.displayDurationTime)
        {
          this.dialogueDisplayEffectTimer = this.displayDurationTime;
        }

        float topPosition = Mathf.Floor(Mathf.Lerp(this.dialogueStartPosition, 0f, this.dialogueDisplayEffectTimer / this.displayDurationTime));
        this.dialogueBox.style.top = new Length(topPosition);

        if (topPosition <= 0)
        {
          this.isOpen = true;
          this.typingCoroutine = StartCoroutine(this.StartTyping());
        }

        yield return null;
      }
    }
  }

  /// <summary>
  /// Moves the dialogue down, outside the screen, smoothly
  /// </summary>
  /// <returns></returns>
  private IEnumerator HideDialogue()
  {
    while (this.isOpen)
    {
      if (this.dialogueDisplayEffectTimer > 0f)
      {
        this.dialogueDisplayEffectTimer -= Time.deltaTime;

        if (this.dialogueDisplayEffectTimer < 0f)
        {
          this.dialogueDisplayEffectTimer = 0f;
        }

        // Resets the dialogue position linearly 
        float topPosition = this.dialogueStartPosition - Mathf.Floor(Mathf.Lerp(0f, this.dialogueStartPosition, this.dialogueDisplayEffectTimer / this.displayDurationTime));

        if (topPosition >= this.dialogueStartPosition)
        {
          this.ResetDialogue();

          if (this.dialogueClosed != null)
          {
            this.dialogueClosed.Invoke();
          }
        }
        else
        {
          this.dialogueBox.style.top = new Length(topPosition);
        }

        yield return null;
      }
    }
  }

  /// <summary>
  /// Waiting for the style of the dialogue to be set.
  /// Then saves the starting top position in a variable
  /// </summary>
  /// <returns></returns>
  private IEnumerator SetupDialogue()
  {
    int numberOfFrames = 2;

    if (this.dialogueOpened != null)
    {
      this.dialogueOpened.Invoke();
    }

    while (numberOfFrames > 0)
    {
      numberOfFrames--;

      yield return new WaitForFixedUpdate();
    }

    this.dialogueStartPosition = this.dialogueBox.resolvedStyle.top;
    this.hiddenText.text = this.dialogueTexts.items.Max(texts => texts);
    StartCoroutine(this.ShowDialogue());
  }

  /// <summary>
  /// Fixes the typing effect in the dialogue
  /// </summary>
  /// <returns></returns>
  private IEnumerator StartTyping()
  {
    char[] charArray = this.currentMessage.ToCharArray();
    this.visibleText.text = $"<color=#00000000>{this.currentMessage}</color>";
    this.PlaySoundEffect();

    for (int i = 0; i < charArray.Length; i++)
    {
      yield return new WaitForSeconds(this.typeSpeed);
      this.visibleText.text = $"{this.currentMessage.Substring(0, i)}<color=#00000000>{this.currentMessage.Substring(i)}</color>";
    }

    this.visibleText.text = this.currentMessage;
    this.typingCoroutine = null;
    this.StopSoundEffect();
  }

  /// <summary>
  /// Smooths out the volume before shutting off the loop, to prevent clicking noice, when the sound suddenly interupts.
  /// It takes the number of seconds left of the sound clip, split it in four, and turning the volume down before the sound stops.
  /// </summary>
  /// <returns></returns>
  private IEnumerator FadeSound()
  {
    float timeLeft = this.typingSound.clip.length - this.typingSound.time;
    float timesToLoop = 4;

    while (timesToLoop > 0)
    {
      this.typingSound.volume -= 0.6f / timesToLoop;
      yield return new WaitForSeconds(timeLeft / timesToLoop);
      timesToLoop--;
    }

    this.typingSound.Stop();
  }

  /// <summary>
  /// Get the next text in dialogue
  /// If there are no more text to display, it closes the dialogue
  /// </summary>
  public void Next()
  {
    // If the dialogue is trying to type, set the displayed text instant
    if (this.typingCoroutine != null)
    {
      StopCoroutine(this.typingCoroutine);
      this.typingCoroutine = null;
      this.StopSoundEffect();

      this.visibleText.text = this.currentMessage;
    }
    else
    {
      this.dialogueTexts.Remove(this.currentMessage);

      // If there are more text to display, start typing it
      if (this.dialogueTexts.items.Count > 0)
      {
        this.currentMessage = this.dialogueTexts.items[0];
        this.TryShowCloseIcon();
        this.typingCoroutine = StartCoroutine(this.StartTyping());
      }
      // If there are no more text to display, close de dialogue
      else if (this.IsDialogueOpen())
      {
        StartCoroutine(this.HideDialogue());
      }
    }
  }
}
