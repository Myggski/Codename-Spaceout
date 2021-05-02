using UnityEngine;

[CreateAssetMenu(menuName = "Variables/FloatVariable")]
public class FloatVariable : ScriptableObject
{

  [Multiline]
  public string DeveloperDescription = "";

  public float Value;

  public void SetValue(float value)
  {
    this.Value = value;
  }

  public void SetValue(FloatVariable value)
  {
    this.Value = value.Value;
  }

  public void ApplyChange(float amount)
  {
    this.Value += amount;
  }

  public void ApplyChange(FloatVariable amount)
  {
    this.Value += amount.Value;
  }

  public override string ToString()
  {
    return this.Value.ToString();
  }
}