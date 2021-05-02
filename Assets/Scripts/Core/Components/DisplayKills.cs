using UnityEngine;
using UnityEngine.UIElements;

public class DisplayKills : MonoBehaviour
{
  [SerializeField]
  [Tooltip("Number of kills that the player has done.")]
  private FloatVariable numberOfKills;
  private UIDocument document;
  private VisualElement killIcon;
  private VisualElement killIconScored;
  private Label numberOfKillsText;

  private void Awake()
  {
    this.SetComponents();
    this.Setup();
  }

  private void Update()
  {
    this.CheckScore();
  }

  private void SetComponents()
  {
    this.document = GetComponent<UIDocument>();
  }

  private void Setup()
  {
    VisualElement wrapper = this.document.rootVisualElement.Q<VisualElement>("kill-wrapper");
    this.numberOfKillsText = wrapper.Q<Label>("kill-text");

    this.SetScore();
  }

  private void CheckScore()
  {
    if (this.numberOfKillsText.text != this.numberOfKills.ToString())
    {
      this.SetScore();
    }
  }

  /// <summary>
  /// Convert float to score-string
  /// TODO: Add this to a helper-class, because it's also in GameOverMenu
  /// </summary>
  private void SetScore()
  {
    string scoreText = "000";
    int numberOfChars = this.numberOfKills.ToString().Length;
    this.numberOfKillsText.text = $"{scoreText.Substring(0, scoreText.Length - numberOfChars)}{this.numberOfKills}";
  }

  /// <summary>
  /// Resets the score
  /// TODO: Move this to another component, this component should only display things
  /// </summary>
  public void ResetScore()
  {
    this.numberOfKills.Value = 0;
  }
}
