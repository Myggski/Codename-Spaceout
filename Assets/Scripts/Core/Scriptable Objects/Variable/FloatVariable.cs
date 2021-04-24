using UnityEngine;

[CreateAssetMenu(menuName = "Variables/FloatVariable")]
public class FloatVariable : ScriptableObject
{

  [Multiline]
  public string DeveloperDescription = "";

  public float Value;

  public void SetValue(float value)
  {
    Value = value;
  }

  public void SetValue(FloatVariable value)
  {
    Value = value.Value;
  }

  public void ApplyChange(float amount)
  {
    Value += amount;
  }

  public void ApplyChange(FloatVariable amount)
  {
    Value += amount.Value;
  }
}