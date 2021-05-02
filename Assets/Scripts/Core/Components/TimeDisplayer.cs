using UnityEngine;
using TMPro;
using System;

[
  RequireComponent(typeof(TextMeshProUGUI))
]
public class TimeDisplayer : MonoBehaviour
{
  [SerializeField]
  private FloatVariable currentTime;
  private TextMeshProUGUI displayText;

  private bool timerActive;

  private void Awake()
  {
    this.SetComponent();
  }

  private void Update()
  {
    this.CheckTextChange();
    this.UpdateTime();
  }

  private void SetComponent()
  {
    this.displayText = GetComponent<TextMeshProUGUI>();
  }

  /// <summary>
  /// Converts float to time-string
  /// TODO: Add this to a helper-class, because it's also used in GameOverMenu
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

  private void UpdateTime()
  {
    if (this.timerActive)
    {
      this.currentTime.Value += Time.deltaTime;
    }
  }

  private void CheckTextChange()
  {
    string time = this.ConvertToDisplayTime(this.currentTime.Value);

    if (time != this.displayText.text)
    {
      this.displayText.text = time;
    }
  }

  public void StartTimer()
  {
    this.currentTime.Value = 0f;
    this.timerActive = true;
  }

  public void StopTimer()
  {
    this.timerActive = false;
  }
}
