using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[
    RequireComponent(typeof(UIDocument)),
]
public class PlayerHealthDisplayer : MonoBehaviour
{
  [SerializeField]
  private FloatVariable playerHealth;
  private VisualElement healthbar;
  private List<VisualElement> hearts = new List<VisualElement>();
  private float currentDisplayedNumberOfHealth;

  private void Start()
  {
    this.SetComponents();
    this.Setup();
  }

  private void Update()
  {
    this.CheckForHealthChanges();
  }

  private void SetComponents()
  {
    this.healthbar = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("health-bar");
  }

  /// <summary>
  /// Add hearts to the UI
  /// </summary>
  private void Setup()
  {
    this.currentDisplayedNumberOfHealth = this.playerHealth.Value;

    this.AddNumberOfHearts(Mathf.RoundToInt(this.currentDisplayedNumberOfHealth));
  }

  /// <summary>
  /// If there are any differences between what's displayed and players current health, the UI will update
  /// </summary>
  private void CheckForHealthChanges()
  {
    if (!this.currentDisplayedNumberOfHealth.Equals(this.playerHealth.Value))
    {
      int diffrence = Mathf.RoundToInt(this.playerHealth.Value - this.currentDisplayedNumberOfHealth);
      this.currentDisplayedNumberOfHealth += diffrence;

      if (diffrence > 0)
      {
        this.AddNumberOfHearts(diffrence);
      }
      else if (diffrence < 0)
      {
        this.RemoveNumberOfHearts(Mathf.Abs(diffrence));
      }
    }
  }

  /// <summary>
  /// Adds number of hearts
  /// </summary>
  /// <param name="numberOfHearts"></param>
  private void AddNumberOfHearts(int numberOfHearts)
  {
    for (var i = 0; i < numberOfHearts; i++)
    {
      this.AddHeart();
    }
  }

  /// <summary>
  /// Removes number of hearts
  /// </summary>
  /// <param name="numberOfHearts"></param>
  private void RemoveNumberOfHearts(int numberOfHearts)
  {
    for (var i = 0; i < numberOfHearts; i++)
    {
      this.RemoveHeart();
    }
  }

  /// <summary>
  /// Adds a heart icon in the UI
  /// </summary>
  private void AddHeart()
  {
    VisualElement heartClone = new VisualElement();
    heartClone.AddToClassList("heart-icon");

    this.healthbar.Add(heartClone);
    this.hearts.Add(heartClone);
  }

  /// <summary>
  /// Removes a heart icon in the UI
  /// </summary>
  private void RemoveHeart()
  {
    if (this.hearts.Count > 0)
    {
      VisualElement heart = this.hearts[this.hearts.Count - 1];

      this.healthbar.Remove(heart);
      this.hearts.Remove(heart);
    }
  }
}
