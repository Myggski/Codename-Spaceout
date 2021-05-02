using UnityEngine;
using UnityEngine.UIElements;
using System;

[
    RequireComponent(typeof(UIDocument)),
]
public class GameOverMenu : MonoBehaviour
{
  [SerializeField]
  private FloatVariable numberOfKills;
  [SerializeField]
  private FloatVariable endTime;
  [SerializeField]
  private GameEvent retryEvent;
  private UIDocument document;
  private Label timeResultText;
  private Label killsResultText;
  private Label missionResultText;
  private Button retryButton;
  private Button quitButton;

  private void Awake()
  {
    this.SetComponents();
  }

  private void OnEnable()
  {
    this.Setup();
  }

  /// <summary>
  /// Sets the document
  /// </summary>
  private void SetComponents()
  {
    this.document = GetComponent<UIDocument>();
  }

  /// <summary>
  /// Set the values of the texts and actions on the buttons
  /// </summary>
  private void Setup()
  {
    this.timeResultText = this.document.rootVisualElement.Q<Label>("time-text");
    this.killsResultText = this.document.rootVisualElement.Q<Label>("kills-text");
    this.missionResultText = this.document.rootVisualElement.Q<Label>("mission-text");
    this.retryButton = this.document.rootVisualElement.Q<Button>("retry-button");
    this.quitButton = this.document.rootVisualElement.Q<Button>("quit-button");

    this.timeResultText.text = this.ConvertToDisplayTime(this.endTime.Value);
    this.killsResultText.text = this.SetScore(this.numberOfKills.Value);
    // TODO: Fix hardcoded value (120)
    this.missionResultText.text = this.endTime.Value >= 120 ? this.SuccessText() : this.FailText();

    this.retryButton.RegisterCallback<MouseUpEvent>(ev => this.Retry());
    this.quitButton.RegisterCallback<MouseUpEvent>(ev => this.Quit());
  }

  /// <summary>
  /// Triggers the retry event
  /// </summary>
  private void Retry()
  {
    if (this.retryEvent != null)
    {
      this.retryEvent.Call();
    }
  }

  /// <summary>
  /// Closing the application
  /// </summary>
  private void Quit()
  {
    Application.Quit();
  }

  /// <summary>
  /// Converts float to time-string
  /// TODO: Add this to a helper-class, because it's also used in TimeDisplayer
  /// </summary>
  /// <param name="timeInSeconds"></param>
  /// <returns></returns>
  private string ConvertToDisplayTime(float timeInSeconds)
  {
    TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);

    return string.Format("{0:D2}:{1:D2}:{2:D3}",
              time.Minutes,
              time.Seconds,
              time.Milliseconds);
  }

  /// <summary>
  /// Sets the success text
  /// </summary>
  /// <returns></returns>
  private string SuccessText()
  {
    return "<color=#91db69ff>Successed</color>";
  }

  /// <summary>
  /// Sets the fail text
  /// </summary>
  /// <returns></returns>
  private string FailText()
  {
    return "<color=#f04f78ff>Failed</color>";
  }

  /// <summary>
  /// Convert float to score-string
  /// TODO: Add this to a helper-class, because it's also in DisplayKills
  /// </summary>
  private string SetScore(float numberOfKills)
  {
    string scoreText = "000";
    int numberOfChars = numberOfKills.ToString().Length;
    return $"{scoreText.Substring(0, scoreText.Length - numberOfChars)}{numberOfKills}";
  }
}
