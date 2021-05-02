using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[
    RequireComponent(typeof(UIDocument)),
]
public class PlayerHealthDisplayer : MonoBehaviour
{
  [SerializeField]
  private FloatVariable playerCurrentHealth;
  [SerializeField]
  private FloatVariable playerMaxHealth;
  private UIDocument document;
  private Label currentHealthText;

  private void Awake()
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
    this.document = GetComponent<UIDocument>();
  }

  private void Setup()
  {
    VisualElement wrapper = this.document.rootVisualElement.Q<VisualElement>("health-wrapper");
    this.currentHealthText = wrapper.Q<Label>("current-health");

    this.SetHealth();
  }

  private void SetHealth()
  {
    this.currentHealthText.text = this.playerCurrentHealth.ToString();
  }

  /// <summary>
  /// If there are any differences between what's displayed and players current health, the UI will update
  /// </summary>
  private void CheckForHealthChanges()
  {
    if (this.currentHealthText.text != this.playerCurrentHealth.ToString())
    {
      if (this.playerCurrentHealth.Value > 0)
      {
        this.currentHealthText.text = this.playerCurrentHealth.ToString();
      }
      else
      {
        this.currentHealthText.text = "0";
      }
    }
  }

  /// <summary>
  /// TODO: Move this to another component, this component should only display values
  /// </summary>
  public void ResetHealth()
  {
    this.playerCurrentHealth.Value = this.playerMaxHealth.Value;
  }
}
