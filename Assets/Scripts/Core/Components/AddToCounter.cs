using UnityEngine;

/// <summary>
/// This components will be used for example when a player killes an enemy, it adds to the score
/// </summary>
public class AddToCounter : MonoBehaviour
{
  [SerializeField]
  [Tooltip("The number that holds track of the counting.")]
  private FloatVariable counter;

  public void Add(float number)
  {
    this.counter.ApplyChange(number);
  }
}
